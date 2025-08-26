using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cliparts.Serialization {
    internal static class DictionarySerializer {
        private class KeyValueTypeTuple {
            public KeyValueTypeTuple(Type KeyType, Type ValueType) {
                this.KeyType = KeyType;
                this.ValueType = ValueType;
            }
            public Type KeyType { get; set; }
            public Type ValueType { get; set; }
        }

        public static object deserialize(XElement element, Type type) {
            var instance = Activator.CreateInstance(type);
            var kvptypes = getKeyValueTypeTuple(type);

            foreach (var child in element.Elements()) {
                instance.GetType().GetMethod("Add").Invoke(instance, new object[]{
                        XmlSerializationHelper.getDeserialization(child.Element("Key"), kvptypes.KeyType),
                        XmlSerializationHelper.getDeserialization(child.Element("Value"), kvptypes.ValueType)
                    });
            }
            return instance;
        }
        public static XElement serialize(object obj) {
            var element = new XElement("A");
            var KeyValueTypeTuple = getKeyValueTypeTuple(obj.GetType());

            var iDictionary = obj as IDictionary;
            var enumerator = iDictionary.GetEnumerator();

            for (int i = 0; i < iDictionary.Count; i++) {
                enumerator.MoveNext();
                var key = enumerator.Key;
                var value = enumerator.Value;

                element.Add(new XElement("Item",
                    XmlSerializationHelper.getXmlSerialization(key, "Key", KeyValueTypeTuple.KeyType),
                    XmlSerializationHelper.getXmlSerialization(value, "Value", KeyValueTypeTuple.ValueType)
                ));
            }
            return element;
        }
        public static bool CanHandle(Type type) {
            return type.GetInterface("IDictionary") != null;
        }
        private static KeyValueTypeTuple getKeyValueTypeTuple(Type type) {
            var array = type.GetGenericArguments();
            return new KeyValueTypeTuple(array[0], array[1]);
        }

    }
}
