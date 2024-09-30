namespace DomainLookup.Models.Entities
{
    public class WhoisDomainInfo
    {
        public string Domain { get; set; } = String.Empty;
        public string Tld { get; set; } = String.Empty;
        public bool IsAvailable { get; set; } = false;
    }
}
