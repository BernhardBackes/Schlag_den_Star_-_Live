using System;
using System.Linq;
using System.Xml.Linq;

namespace Cliparts.Serialization {
    public static class XmlSerializationHelper<T> where T : class {
        public static T getDeserialization(XElement element) {
            return XmlSerializationHelper.getDeserialization(element, typeof(T)) as T;
        }
    }

    public static class XmlSerializationHelper {
        private const string typeAttributeLiteral = "type";
        private const string isNullLiteral = "isNull";

        public static object getDeserialization(XElement element, Type requestedType) {
            if (element.statesIsNull())
                return null;

            var type = determineTypeOfObjectToCreate(element, requestedType);
            if (type == null) {
                throw new Exception("Could not find the given Type. Usually that's because a class was renamed");
            }
            if (StringBasedSerializer.CanHandle(type))
                return StringBasedSerializer.deserialize(element.Value, type);

            else if (CustomizedXMLSerializer.CanHandle(type))
                return CustomizedXMLSerializer.deserialize(element, type);

            else if (ArraySerializer.CanHandle(type)) {
                return ArraySerializer.deserialize(element, type);
            }
            else if (DictionarySerializer.CanHandle(type))
                return DictionarySerializer.deserialize(element, type);

            else if (GeneralCollectionSerializer.CanHandle(type))
                return GeneralCollectionSerializer.deserialize(element, type);

            else if (CompositeObjectSerializer.CanHandle(type))
                return CompositeObjectSerializer.deserialize(element, type);

            else
                throw new NotImplementedException("No Serializer found for this object");
        }
        private static bool statesIsNull(this XElement element) {
            return element.Attributes(isNullLiteral).Count() != 0;
        }
        private static Type determineTypeOfObjectToCreate(XElement element, Type requestedBasicType) {
            var typeString = element.GetAttributeValue(typeAttributeLiteral);
            if(typeString != null)
                return Type.GetType(typeString);
            else
                return requestedBasicType;
        }

        public static XElement getXmlSerialization(object objectToSerialize, string Name = "Serialization") {
            return getXmlSerialization(objectToSerialize, Name, objectToSerialize.GetType());
        }
        public static XElement getXmlSerialization(object objectToSerialize, string name, Type typeRequestedByParent) {
            if (objectToSerialize == null)
                return new XElement(name, new XAttribute(isNullLiteral, true));

            XElement element;
            var typeOfCurrentObject = objectToSerialize.GetType();

            if (StringBasedSerializer.CanHandle(typeOfCurrentObject))
                element = StringBasedSerializer.serialize(objectToSerialize);

            else if (CustomizedXMLSerializer.CanHandle(typeOfCurrentObject))
                element = CustomizedXMLSerializer.serialize(objectToSerialize);

            else if (ArraySerializer.CanHandle(typeOfCurrentObject))
                element = ArraySerializer.serialize(objectToSerialize);

            else if (DictionarySerializer.CanHandle(typeOfCurrentObject))
                element = DictionarySerializer.serialize(objectToSerialize);

            else if (GeneralCollectionSerializer.CanHandle(typeOfCurrentObject))
                element = GeneralCollectionSerializer.serialize(objectToSerialize);

            else
                element = CompositeObjectSerializer.serialize(objectToSerialize);

            element.Name = name;
            AppendObjectTypeToAttributesIfNotTriviallyReconstructable(element, typeOfCurrentObject, typeRequestedByParent);
            return element;
        }
        private static void AppendObjectTypeToAttributesIfNotTriviallyReconstructable(XElement ThisElement, Type typeOfCurrentObject, Type typeRequestedByParent) {
            if (typeRequestedByParent != typeOfCurrentObject)
                ThisElement.SetAttributeValue(typeAttributeLiteral, typeOfCurrentObject.AssemblyQualifiedName);
        }
    }
}