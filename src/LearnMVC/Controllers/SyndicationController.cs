using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;

namespace LearnMVC.Controllers
{
    public partial class SyndicationController : Controller
    {
        //
        // GET: /Syndication/

        public virtual ActionResult Rss()
        {
            var sf = new SyndicationFeed("My files", "Some description for my syndication", new Uri("http://SomeAlternativeLink.com"), "SomeId", DateTime.Now);

            return new RssActionResult(sf);
        }

    }
}
