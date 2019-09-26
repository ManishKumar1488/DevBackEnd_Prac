using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DEV_BackendPratical_Services;
using DEV_BackendPratical_Services.Repository;

namespace Dev_BackendPractical_Test
{
    [TestClass]
    public class ServiceRepositoryTest
    {

        [TestMethod]
        public void GetIPDomainWorker()
        {            
            string[] serviceName = new string[2]{"GeoIP", "RDAP" };
            string IP_Domain = "";
            ServiceRepository serviceRepository = new ServiceRepository();

            StringBuilder strResult = new StringBuilder();
            if (
                (serviceName.Length > 0 && !string.IsNullOrEmpty(IP_Domain))  &&
                (Common.IsValidateIPv4(IP_Domain) || Common.IsValidDomainName(IP_Domain))
                )
            {
                //Loop through List of service
                foreach (var service in serviceName)
                {
                    switch (service.ToString().ToLower())
                    {
                        case "ping":
                            strResult.Append("{\"PingResult\":" + serviceRepository.Ping(IP_Domain) + "}");
                            break;
                        case "rdap":
                            strResult.Append("{\"RDAPResult\":" + serviceRepository.RDAP(IP_Domain) + "}");
                            break;
                        case "reversedns":
                            strResult.Append("{\"DNSResult\":" + serviceRepository.ReverseDNS(IP_Domain) + "}");
                            break;
                        case "geoip":
                            strResult.Append("{\"GeoIpResult\":" + serviceRepository.GeoIP(IP_Domain) + "}");
                            break;
                    }
                }
                strResult.Append("}");
            }
            else
            {
                string DefaultIPDomain = "127.0.0.1";
                strResult.Append("{\"PingResult\":" + serviceRepository.Ping(DefaultIPDomain) + "}" +
                                 "{\"RDAPResult\":" + serviceRepository.RDAP(DefaultIPDomain) + "}" +
                                 "{\"GeoIpResult\":"+ serviceRepository.GeoIP(DefaultIPDomain) + "}" +
                                 "{\"DNSResult\":"  + serviceRepository.ReverseDNS(DefaultIPDomain) + "}"
                    );
            }

            strResult.ToString();


            //Check for the Output and Compare
            Assert.IsNotNull(strResult);
            //
        }
    }
}
