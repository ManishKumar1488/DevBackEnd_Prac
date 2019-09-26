using System.Threading.Tasks;

namespace DEV_BackendPratical_Services.Repository
{
    public interface IServiceRepository
    {
        Task<string> GetIPDomainWorker(string[] serviceName,string IP_Domain);
        string Ping(string IP);
        string RDAP(string IP_Domain);
        string GeoIP(string IP_Domain);
        string ReverseDNS(string IP);
    }
}