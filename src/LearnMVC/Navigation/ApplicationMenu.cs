using System.Web.Mvc;
using MvcContrib.UI.MenuBuilder;

namespace LearnMVC.Navigation
{
    public class ApplicationMenu
    {
        public static MenuItem MainMenu(UrlHelper url)
        {
            Menu.DefaultDisabledClass = "disabled";
            Menu.DefaultSelectedClass = "selected";
            return Menu.Begin(
                Menu.Link(url.Action(MVC.Files.Index()), "Home"),
                Menu.Link(url.Action(MVC.Files.Search()), "Search"),
                Menu.Link("http://microsoft.com", "Big Blue"),
                Menu.Link("http://google.com", "Google")).SetListClass("menu");
        }
    }
}