using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EjemploDTEFacil
{
    [XmlRootAttribute("ubicacion")]
    public class Ubicacion
    {
        [XmlElement("direccion")]
        public String Direccion { get; set; }
        [XmlElement("comuna")]
        public String Comuna { get; set; }
        [XmlElement("ciudad")]
        public String Ciudad { get; set; }
    }

    [XmlRootAttribute("receptor")]
    public class Receptor
    {
        [XmlElement("rut")]
        public String Rut { get; set; }

        [XmlElement("razonSocial")]
        public String RazonSocial { get; set; }

        [XmlElement("giro")]
        public String Giro { get; set; }

        [XmlElement("ubicacion")]
        public Ubicacion Ubicacion { get; set; }
    }

    [XmlRootAttribute("detalle")]
    public class Detalle
    {
        [XmlElement("nombre")]
        public String Nombre { get; set; }

        [XmlElement("descripcion")]
        public String Descripcion { get; set; }

        [XmlElement("unidad")]
        public String Unidad { get; set; }

        [XmlElement("cantidad")]
        public float? Cantidad { get; set; }

        public bool ShouldSerializeCantidad()
        {
            return Cantidad.HasValue;
        }

        [XmlElement("precioUnitario", IsNullable = false)]
        public float PrecioUnitario { get; set; }
    }

    [XmlRootAttribute("facturaElectronica", Namespace = "http://dtefacil.cl/1.2")]
    public class FacturaElectronica
    {
        [XmlElement("actividadEconomica")]
        public int? ActividadEconomica { get; set; }

        [XmlElement("receptor")]
        public Receptor Receptor { get; set; }
        
        [XmlArrayAttribute("detalles")]
        [XmlArrayItemAttribute("detalle")]
        public List<Detalle> Detalles { get; set; }
    }
}
