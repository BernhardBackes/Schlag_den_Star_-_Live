using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cliparts.Serialization {
    public class NotSerializedAttribute : Attribute { }
    internal static class XMLSerializationExtensions {
        public static string GetAttributeValue(this XElement element, XName name) {
            var attribute = element.Attribute(name);
            return attribute != null ? attribute.Value : null;
        }
    }
}
