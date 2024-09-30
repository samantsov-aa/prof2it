using DomainLookup.Models.Entities;
using DomainLookup.Models.Interfaces;
using Newtonsoft.Json.Linq;

namespace DomainLookup.Models.Services
{
    public class WhoisService : IWhois
    {
        private string rapidApiKey;
        private string rapidApiHost;
        public WhoisService(IConfiguration configuration)
        {
            rapidApiKey = configuration.GetSection("Whois:RapidApiKey").Value;
            rapidApiHost = configuration.GetSection("Whois:RapidApiHost").Value;
        }

        public WhoisDomainInfo CheckDomain(string domainName)
        {
            var res = new WhoisDomainInfo()
            {
                Domain = domainName,
                Tld = string.Join(".", domainName.Split(".").Skip(1))
            };
         
            var whois = QueryWhois(domainName);
            res.IsAvailable = !whois.Value<bool>("registered");

            return res;
        }

        private JObject QueryWhois(string domainName)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{rapidApiHost}/?domain={domainName}&format=json&_forceRefresh=0"),
                Headers =
                {
                    { "x-rapidapi-key", rapidApiKey },
                    { "x-rapidapi-host", rapidApiHost },
                },
            };
            using var response = client.SendAsync(request).Result;
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                return JObject.Parse(body);
            }
        }
    }
}
