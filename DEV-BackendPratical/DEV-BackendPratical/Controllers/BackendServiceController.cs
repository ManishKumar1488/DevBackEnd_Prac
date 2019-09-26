using DEV_BackendPratical_Services;
using DEV_BackendPratical_Services.Repository;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Web.Http;

namespace DEV_BackendPratical.Controllers
{
    //[Authorize]
    public class BackendServiceController : ApiController
    {
        private IServiceRepository _serviceRepository;
        public BackendServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        // GET api/BackendService/IPDomainWorker
        [HttpGet]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("api/IPDomainWorker", Name = "IPDomainWorker")]
        public IHttpActionResult GetIPDomainWorker(string IP, string service)
        {
            string[] serviceName = service.Split(',');
            var Serviceresult = _serviceRepository.GetIPDomainWorker(serviceName, IP);
            var jsonResult = JsonConvert.SerializeObject(Serviceresult, Formatting.Indented);
            if ((jsonResult) != null)
                return Ok(jsonResult);
            else
                return NotFound();
        }


        // GET api/BackendService/Ping
        [HttpGet]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("api/Ping", Name = "Ping")]
        public IHttpActionResult GetPing(string IP)
        {
            if (!string.IsNullOrEmpty(IP) && Common.IsValidateIPv4(IP))
            {
                var Serviceresult = _serviceRepository.Ping(IP);
                var jsonResult = JsonConvert.DeserializeObject<dynamic>(Serviceresult);
                if ((jsonResult) != null)
                    return Ok(jsonResult);
                else
                    return NotFound();
            }
            else
                return BadRequest("InValid IP Address.");
        }


        // GET api/BackendService/RDAP
        [HttpGet]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("api/RDAP", Name = "RDAP")]
        public IHttpActionResult RDAP(string Domain_IP)
        {
            if (!string.IsNullOrEmpty(Domain_IP) && (Common.IsValidDomainName(Domain_IP) || Common.IsValidateIPv4(Domain_IP)))
            {
                var Serviceresult = _serviceRepository.RDAP(Domain_IP);
                var jsonResult = JsonConvert.DeserializeObject<dynamic>(Serviceresult);

                if ((jsonResult) != null)
                    return Ok(jsonResult);
                else
                    return NotFound();
            }
            else
                return BadRequest("InValid IP or Domain Address.");
        }


        // GET api/BackendService/GeoIP
        [HttpGet]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("api/GeoIP", Name = "GeoIP")]
        public IHttpActionResult GeoIP(string Domain_IP)
        {
            if (!string.IsNullOrEmpty(Domain_IP) && (Common.IsValidDomainName(Domain_IP) || Common.IsValidateIPv4(Domain_IP)))
            {
                var Serviceresult = _serviceRepository.GeoIP(Domain_IP);
                var jsonResult = JsonConvert.DeserializeObject<dynamic>(Serviceresult);

                if ((jsonResult) != null)
                    return Ok(jsonResult);
                else
                    return NotFound();
            }
            else
                return BadRequest("InValid IP or Domain Address.");
        }


        // GET api/BackendService/ReverseDNS
        [HttpGet]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("api/ReverseDNS", Name = "ReverseDNS")]
        public IHttpActionResult ReverseDNS(string IP)
        {
            if (!string.IsNullOrEmpty(IP) && Common.IsValidateIPv4(IP))
            {
                var Serviceresult = _serviceRepository.ReverseDNS(IP);
                var jsonResult = JsonConvert.DeserializeObject<dynamic>(Serviceresult);
                if ((jsonResult) != null)
                    return Ok(jsonResult);
                else
                    return NotFound();
            }
            else
                return BadRequest("InValid IP Address.");
        }
    }
}
