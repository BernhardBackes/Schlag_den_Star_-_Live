using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cliparts.Serialization {
    public static class ArraySerializer {
        private const string dimensionsLiteal = "Dimensions";
        public static object deserialize(XElement value, Type type) {
            var collectionItemType = getCollectionItemType(type);
            var dimensionsArray = getDimensionsArray(value);
            var deserializedArray = Array.CreateInstance(collectionItemType, dimensionsArray);

            var currentIndexArray = new int[dimensionsArray.Length];
            currentIndexArray[dimensionsArray.Length - 1] = -1;
            var items = value.Elements();
            for (int i = 0; i < items.Count(); i++) {
                var item = XmlSerializationHelper.getDeserialization(items.ElementAt(i), collectionItemType); 
                setNextIndex(currentIndexArray, dimensionsArray);
                deserializedArray.SetValue(item, currentIndexArray);
            }
            return deserializedArray;
        }

        private static void setNextIndex(int[] currentIndexArray, int[] dimensionsArray) {
            var indexOfFirstIncrementable = getIndexOfFirstIncrementable(currentIndexArray, dimensionsArray);
            for (int i = currentIndexArray.Length - 1; i > indexOfFirstIncrementable; i--)
                currentIndexArray[i] = 0;
            currentIndexArray[indexOfFirstIncrementable]++;
        }

        private static int getIndexOfFirstIncrementable(int[] currentIndexArray, int[] dimensionsArray) {
            for (int i = currentIndexArray.Length - 1; i >= 0 ; i--)
                if (currentIndexArray[i] + 1 < dimensionsArray[i])
                    return i;
            throw new ArgumentException("currentIndexArrayIsAlreadyTooLarge");
        }


        private static int[] getDimensionsArray(XElement element) {
            var csv = element.GetAttributeValue(dimensionsLiteal);
            var list = csv.Split(';').Select(x => Int32.Parse(x));
            return list.ToArray();
        }

        public static XElement serialize(object obj) {
            var element = new XElement("DummyName");
            var array = obj as Array;

            var dimensionsList = new List<int>();
            for (int i = 0; i < array.Rank; i++)
                dimensionsList.Add(array.GetLength(i));
            var attribute = new XAttribute(dimensionsLiteal, String.Join(";", dimensionsList));
            element.Add(attribute);

            var collectionItemType = getCollectionItemType(obj.GetType());
            foreach (var item in obj as ICollection)
                element.Add(XmlSerializationHelper.getXmlSerialization(item, "Item", collectionItemType));

            return element;
        }

        public static bool CanHandle(Type type) {
            return type.IsArray;
        }

        private static Type getCollectionItemType(Type type) {
            return type.GetElementType();
        }
    }
}
