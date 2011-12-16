using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Xml.Serialization;
using System.IO;

namespace EjemploDTEFacil
{
    class ApiException : Exception
    {
        public ApiException(RestResponse resp) : base(ParseErrors(resp)) { }

        private static String ParseErrors(RestResponse resp)
        {
            var s = new XmlSerializer(typeof(Errores));
            var sr = new StringReader(resp.Content);
            Errores errores = (Errores) s.Deserialize(sr);
            var sb = new StringBuilder();
            foreach(Error e in errores.Errors)
            {
                sb.AppendLine(e.Descripcion);
            }
            return sb.ToString();
        }
    }
}
