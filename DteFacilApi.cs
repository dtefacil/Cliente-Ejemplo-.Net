using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using System.Xml.Serialization;

namespace EjemploDTEFacil
{
    class DteFacilApi
    {
        private RestClient client;
        private const String baseUrl = "https://localhost:8443/1.2";
        private const String downloads = "downloads";

        private Contribuyente contribuyente;
        public Contribuyente Contribuyente 
        { 
            get { return contribuyente; } 
        }

        public DteFacilApi()
        {
            ServicePointManager.ServerCertificateValidationCallback = MyCertHandler;
            client = new RestClient(baseUrl);
            Directory.CreateDirectory(downloads);
        }

        static bool MyCertHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        public void EstablecerUsuario(String username, String password)
        {
            client.Authenticator = new HttpBasicAuthenticator(username, password);
            var r = new RestRequest();
            r.Resource = "usuarios/yo";
            RestResponse<Contribuyente> response = client.Execute<Contribuyente>(r);
            
            if(response.ErrorException != null || 
                !response.StatusCode.Equals(HttpStatusCode.OK) ||
                !response.Data.Tipo.Equals("contribuyenteDteFacil"))
            {
                throw new UnauthorizedException();
            }

            contribuyente = response.Data;
        }

        public PaginaDocumentos Documentos(int? pag)
        {
            var r = new RestRequest();
            r.Resource = "documentos";
            if (pag != null)
            {
                r.AddParameter("pag", pag, ParameterType.GetOrPost);
            }
            RestResponse<PaginaDocumentos> resp = client.Execute<PaginaDocumentos>(r);
            if (resp.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                throw new UnauthorizedException();
            }
            return resp.Data;
        }

        public void DescargarDocumento(Documento d)
        {
            DescargarDocumento(d.Id);
        }

        public void DescargarDocumento(String id)
        {
            var r = new RestRequest();
            r.AddHeader("Accept", "application/pdf");
            r.Resource = "documentos/" + id+".pdf";
            var bytes = client.DownloadData(r);
            String filename = downloads+"\\"+id + ".pdf";
            File.WriteAllBytes(filename, bytes);
            System.Diagnostics.Process.Start(filename);
        }

        public void CrearFactura(FacturaElectronica fe)
        {
            var r = new RestRequest(Method.POST);
            r.Resource = "documentos";
            var sw = new StringWriter();
            var s = new XmlSerializer(typeof(FacturaElectronica));
            s.Serialize(sw, fe);

            r.AddParameter("application/xml", sw.ToString(), ParameterType.RequestBody);
            var resp = client.Execute(r);
            if (resp.ErrorException != null || !resp.StatusCode.Equals(HttpStatusCode.Created))
            {
                throw new ApiException(resp);
            }

            DescargarDocumento(GetCreatedId(resp));
        }

        private String GetCreatedId(RestResponse resp)
        {
            foreach(Parameter p in resp.Headers)
            {
                if ("Location".Equals(p.Name))
                {
                    var l = p.Value.ToString();
                    return l.Substring(l.LastIndexOf("/")+1);
                }
            }
            return null;
        }

    }
}
