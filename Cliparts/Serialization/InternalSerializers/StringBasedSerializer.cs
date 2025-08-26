using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cliparts.Serialization {
    internal static class StringBasedSerializer {
        public static object deserialize(string value, Type type) {
            if (type == typeof(string)) {
                value = Regex.Replace(value, "(?<!\r)\n", "\r\n");//Replace \n with \rn (Winforms Textboxes don't display linebreaks otherwise)
                return value;
            }
            else {
                var converter = TypeDescriptor.GetConverter(type);
                var result = converter.ConvertFromString(value);
                return result;
            }
        }
        public static XElement serialize(object obj) {
            var converter = TypeDescriptor.GetConverter(obj.GetType());
            var result = converter.ConvertToString(obj);
            return new XElement("A", result);
        }
        public static bool CanHandle(Type type) {
            return type.CanBeAssignedFromAndToString();
        }

        private static bool CanBeAssignedFromAndToString(this Type type) {
            var converter = TypeDescriptor.GetConverter(type);
            return converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(typeof(string));
        }

    }
}
