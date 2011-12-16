using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EjemploDTEFacil
{
    public class ActividadEconomica
    {
        public int Value { get; set; }
    }

    public class Contribuyente
    {
        public String Nombre { get; set; }
        public String Tipo { get; set; }
        public List<ActividadEconomica> ActividadesEconomicas { get; set; }
    }
}
