using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cliparts.Serialization {
    static class CompositeObjectSerializer {
        public static object deserialize(XElement element, Type type) {
            var instance = Activator.CreateInstance(type);
            var PropertiesOfType = type.GetProperties();
            foreach (var child in element.Elements()) {
                setCorrespondingPropertyIfExisting(instance, PropertiesOfType, child);
            }
            return instance;
        }

        private static void setCorrespondingPropertyIfExisting(object instance, PropertyInfo[] props, XElement child) {
            var propInfo = props.FirstOrDefault(prop => prop.Name == child.Name);
            if (propInfo != null && propInfo.GetSetMethod() != null)
                propInfo.SetValue(instance, XmlSerializationHelper.getDeserialization(child, propInfo.PropertyType));
        }
        public static XElement serialize(object obj) {
            var element = new XElement("DummyName");
            var shouldPropertyBeSerializedPredicate = new Func<PropertyInfo, bool>(
                (prop) => {
                    var isMarkedAsNonSerializable = prop.GetCustomAttributes(false).Any(attribute => attribute is NotSerializedAttribute);
                    var hasGet = prop.GetGetMethod() != null;
                    var hasSet = prop.GetSetMethod() != null;

                    return !isMarkedAsNonSerializable && hasGet && hasSet;
                }
            );
            var RelevantProperties = obj.GetType().GetProperties().Where(shouldPropertyBeSerializedPredicate);
            
            foreach (var info in RelevantProperties) {
                element.Add(XmlSerializationHelper.getXmlSerialization(info.GetValue(obj), info.Name, info.PropertyType));
            }
            return element;
        }

        public static bool CanHandle(Type type) {
            return true;
        }
    }
}
