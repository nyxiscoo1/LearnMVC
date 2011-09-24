using System.Web.Mvc;
using LearnMVC.Controllers;
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
                Menu.Action<HomeController>(p => p.Index()),
                Menu.Action<HomeController>(p => p.About(), "About"),
                Menu.Action<FilesController>(p => p.Index(null, null), "Files"),
                Menu.Link("http://microsoft.com", "Big Blue"),
                Menu.Link("http://google.com", "Google"),
                Menu.Secure<AccountController>(p => p.ChangePassword())).SetListClass("menu");
        }
    }
}