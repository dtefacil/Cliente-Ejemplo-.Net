using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EjemploDTEFacil
{
    class Documento
    {
        public String Id { get; set; }
        public String Url { get; set; }
        public int Folio { get; set; }
        public int Tipo { get; set; }
        public String RutEmisor { get; set; }
        public String RutRecep { get; set; }
        public int MntTotal { get; set; }
        public DateTime FchEmis { get; set; }
    }
}
