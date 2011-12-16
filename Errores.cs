using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EjemploDTEFacil
{
    public class Error
    {
        [XmlAttribute("descripcion")]
        public String Descripcion { get; set; }
    }

    [XmlRoot("errores",Namespace="http://dtefacil.cl/1.2")]
    public class Errores{
        [XmlElement("error")]
        public List<Error> Errors { get; set; }
    }

}
