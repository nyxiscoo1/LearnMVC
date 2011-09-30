using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace LearnMVC.Controllers
{
    /// <summary>
    /// Returns an Atom feed to the resonse stream.
    /// http://weblogs.asp.net/seanmcalinden/archive/2009/10/25/create-rss-and-atom-feeds-using-custom-asp-net-mvc-action-results-and-the-microsoft-syndication-classes.aspx
    /// </summary>
    public class AtomActionResult : ActionResult
    {
        SyndicationFeed feed;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AtomActionResult() { }
        /// <summary>
        /// Constructor to set up the action result feed.
        /// </summary>
        /// <param name="feed">Accepts a <see cref="SyndicationFeed"/>.</param>
        public AtomActionResult(SyndicationFeed feed)
        {
            this.feed = feed;
        }
        /// <summary>
        /// Executes the call to the ActionResult method and returns the created feed to the output response.
        /// </summary>
        /// <param name="context">Accepts the current <see cref="ControllerContext"/>.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/atom+xml";
            Atom10FeedFormatter formatter = new Atom10FeedFormatter(this.feed);
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                formatter.WriteTo(writer);
            }
        }
    }
}