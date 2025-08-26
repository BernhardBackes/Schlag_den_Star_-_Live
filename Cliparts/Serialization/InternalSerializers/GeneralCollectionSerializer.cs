using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cliparts.Serialization {
    internal static class GeneralCollectionSerializer {
        public static object deserialize(XElement value, Type type) {
            var instance = Activator.CreateInstance(type);

            var collectionItemType = getCollectionItemType(type);
            foreach (var child in value.Elements())
                type.GetMethod("Add").Invoke(instance, new object[] { XmlSerializationHelper.getDeserialization(child, collectionItemType) });
            return instance;
        }
        public static XElement serialize(object obj) {
            var element = new XElement("DummyName");
            var collectionItemType = getCollectionItemType(obj.GetType());
            foreach (var item in obj as ICollection)
                element.Add(XmlSerializationHelper.getXmlSerialization(item, "Item", collectionItemType));

            return element;
        }

        public static bool CanHandle(Type type) {
            return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>));
        }        

        private static Type getCollectionItemType(Type type) {
            return type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>)).GetGenericArguments().First();
        }

    }
}
