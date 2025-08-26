using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cliparts.Serialization {

    public interface IProvidesCustomXMLSerialization {
        XElement getSerialization();
        void Deserialize(XElement Serialization);
    }
}
