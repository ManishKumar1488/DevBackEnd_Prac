using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DEV_BackendPratical_Services.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        HttpClient client;
        public ServiceRepository()
        {
            client = new HttpClient();
        }
        public async Task<string> GetIPDomainWorker(string[] serviceName, string IP_Domain)
        {
            StringBuilder strResult = new StringBuilder();
            if (
                (serviceName.Length > 0 && !string.IsNullOrEmpty(IP_Domain)) &&
                (Common.IsValidateIPv4(IP_Domain) || Common.IsValidDomainName(IP_Domain))
                )
            {
                //Loop through List of service
                foreach (var service in serviceName)
                {
                    switch (service.ToString().ToLower())
                    {
                        case "ping":
                            strResult.Append("{\"PingResult\":" + await Task.Run(() => Ping(IP_Domain)) + "}");
                            break;
                        case "rdap":
                            strResult.Append("{\"RDAPResult\":" + await Task.Run(() => RDAP(IP_Domain)) + "}");
                            break;
                        case "reversedns":
                            strResult.Append("{\"DNSResult\":" + await Task.Run(() => ReverseDNS(IP_Domain)) + "}");
                            break;
                        case "geoip":
                            strResult.Append("{\"GeoIpResult\":" + await Task.Run(() => GeoIP(IP_Domain)) + "}");
                            break;
                    }
                }
                strResult.Append("}");
            }
            else
            {
                string DefaultIPDomain = ConfigurationManager.AppSettings["DefaultIPDomain"].ToString();
                strResult.Append("{\"PingResult\":" + await Task.Run(() => Ping(DefaultIPDomain)) + "}" +
                                 "{\"RDAPResult\":" + await Task.Run(() => RDAP(DefaultIPDomain)) + "}" +
                                 "{\"GeoIpResult\":" + await Task.Run(() => GeoIP(DefaultIPDomain)) + "}" +
                                 "{\"DNSResult\":" + await Task.Run(() => ReverseDNS(DefaultIPDomain)) + "}"
                    );
            }
            return strResult.ToString();
        }
        public string Ping(string IP)
        {
            client.BaseAddress = null;
            client.BaseAddress = new Uri(Common.Ping + "/" + IP);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsAsync<dynamic>().Result;
                    return JsonConvert.SerializeObject(dataObjects);
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                client =new HttpClient();
            }            
        }
        public string RDAP(string IP_Domain)
        {
            client.BaseAddress = null;
            client.BaseAddress = new Uri(Common.RDAP + "/" + IP_Domain);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {

                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                if (response.IsSuccessStatusCode)
                {
                    //var dataObjects = response.Content.ReadAsStringAsync<dynamic>().Result;
                    var dataObjects = response.Content.ReadAsAsync<dynamic>().Result;
                    return JsonConvert.SerializeObject(dataObjects);
                }
                else                
                    return string.Empty;                
            }
            finally
            {
                client = new HttpClient();
            }
        }
        public string GeoIP(string IP_Domain)
        {
            client.BaseAddress = null;
            client.BaseAddress = new Uri(Common.GeoIP + "/" + IP_Domain);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {

                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsAsync<dynamic>().Result;
                    return JsonConvert.SerializeObject(dataObjects);
                }
                else
                    return string.Empty;
            }
            finally
            {
                client = new HttpClient();
            }
        }
        public string ReverseDNS(string IP)
        {
            client.BaseAddress = null;
            client.BaseAddress = new Uri(Common.ReverseDNS + IP);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsAsync<dynamic>();
                    return JsonConvert.SerializeObject(dataObjects);
                }
                else
                    return string.Empty;
            }
            finally
            {
                client = new HttpClient();
            }
        }
    }
}