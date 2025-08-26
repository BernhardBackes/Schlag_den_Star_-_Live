using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cliparts.Serialization {
    internal static class CustomizedXMLSerializer {
        public static bool CanHandle(Type type) {
            return type.GetInterfaces().Contains(typeof(IProvidesCustomXMLSerialization));
        }
        public static object deserialize(XElement element, Type type) {
            var instance = Activator.CreateInstance(type) as IProvidesCustomXMLSerialization;
            instance.Deserialize(element.Elements().First());
            return instance;
        }

        public static XElement serialize(object objectToSerialize) {
            var casted = objectToSerialize as IProvidesCustomXMLSerialization;
            return new XElement("DummyName", casted.getSerialization());
        }
    }
}
