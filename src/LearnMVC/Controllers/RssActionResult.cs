using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace LearnMVC.Controllers
{
    /// <summary>
    /// Returns an RSS feed to the resonse stream.
    /// http://weblogs.asp.net/seanmcalinden/archive/2009/10/25/create-rss-and-atom-feeds-using-custom-asp-net-mvc-action-results-and-the-microsoft-syndication-classes.aspx
    /// </summary>
    public class RssActionResult : ActionResult
    {
        SyndicationFeed feed;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RssActionResult() { }
        /// <summary>
        /// Constructor to set up the action result feed.
        /// </summary>
        /// <param name="feed">Accepts a <see cref="SyndicationFeed"/>.</param>
        public RssActionResult(SyndicationFeed feed)
        {
            this.feed = feed;
        }
        /// <summary>
        /// Executes the call to the ActionResult method and returns the created feed to the output response.
        /// </summary>
        /// <param name="context">Accepts the current <see cref="ControllerContext"/>.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";
            Rss20FeedFormatter formatter = new Rss20FeedFormatter(this.feed);
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                formatter.WriteTo(writer);
            }
        }
    }
}