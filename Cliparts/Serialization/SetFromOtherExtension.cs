using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Cliparts.Serialization.SetFromOther {
    public static class SetFromOtherExtension {
        /// <summary>
        /// Sets all publically readable and writable properties to the values of another object
        /// </summary>
        public static void SetFromOther<T>(this T obj, T other) {
            var TType = typeof(T);
            var TProps = getReadAndWritableProperties(TType);
            var thisProps = getReadAndWritableProperties(obj.GetType()).
                Where(x => TProps.Any(tprop => tprop.Name == x.Name));
            

            if (obj.GetType() != other.GetType()) {
                var otherObjectProps = getReadAndWritableProperties(other.GetType()).
                    Where(x => TProps.Any(tprop => tprop.Name == x.Name));

                foreach (var prop in thisProps) {
                    var matchingProp = otherObjectProps.FirstOrDefault(x => x.Name == prop.Name);
                    if(matchingProp != null){
                        prop.SetValue(obj, matchingProp.GetValue(other));
                    }
                }
            }
            else {
                foreach (var prop in thisProps) {
                    prop.SetValue(obj, prop.GetValue(other));
                }
            }
        }

        private static IEnumerable<PropertyInfo> getReadAndWritableProperties(Type type) {
            var properties = type.GetProperties().Where(x => x.GetGetMethod() != null 
                            && x.GetSetMethod() != null);
            return properties;
        }
    }
}
