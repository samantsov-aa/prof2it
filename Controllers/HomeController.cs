using DomainLookup.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DomainLookup.Models.Entities;
using DomainLookup.Models.Interfaces;

namespace DomainLookup.Controllers
{
    public class HomeController : Controller
    {
        private IWhois whois;

        public HomeController(IWhois whois)
        {
            this.whois = whois;
        }

        public IActionResult Index(string domainName)
        {
            WhoisDomainInfo info = null;
            if (!string.IsNullOrEmpty(domainName))
            {
                info = whois.CheckDomain(domainName);
            }
            return View(info);
        }
    }
}
