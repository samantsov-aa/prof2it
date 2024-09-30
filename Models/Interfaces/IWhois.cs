using DomainLookup.Models.Entities;

namespace DomainLookup.Models.Interfaces
{
    public interface IWhois
    {
        public WhoisDomainInfo CheckDomain(string domainName);
    }
}
