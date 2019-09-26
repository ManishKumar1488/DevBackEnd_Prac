using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


namespace DEV_BackendPratical_Services
{
    public static class Common
    {
        private static readonly string _GeoIP = string.Empty;
        private static readonly string _RDAP = string.Empty;
        private static readonly string _ReverseDNS = string.Empty;
        private static readonly string _Ping = string.Empty;
        static Common()
        {
            _GeoIP = ConfigurationManager.AppSettings["GeoIP"].ToString();
            _RDAP = ConfigurationManager.AppSettings["RDAP"].ToString();
            _ReverseDNS = ConfigurationManager.AppSettings["ReverseDNS"].ToString() + "&Ip=";
            _Ping = ConfigurationManager.AppSettings["Ping"].ToString();
        }

        public static string GeoIP
        {
            get { return _GeoIP; }
        }
        public static string RDAP
        {
            get { return _RDAP; }
        }
        public static string ReverseDNS
        {
            get { return _ReverseDNS; }
        }
        public static string Ping
        {
            get { return _Ping; }
        }

        public static bool IsValidDomainName(string name)
        {
            return Uri.CheckHostName(name) != UriHostNameType.Unknown;
        }
        public static bool IsValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
                return false;

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
                return false;

            byte tempForParsing;
            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
    }
}