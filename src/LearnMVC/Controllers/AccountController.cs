using System.Threading;
using System.Web.Mvc;
using LearnMVC.Models;
using LearnMVC.Models.Account;
using LearnMVC.Security;
using LearnMVC.Services;
using MvcContrib;

namespace LearnMVC.Controllers
{
    public partial class AccountController : Controller
    {
        private readonly IAuthorizationService authorizationService;

        [OutputCache(CacheProfile = CasheProfiles.Mild, VaryByParam = "userName")]
        public virtual JsonResult IsUserExists(string userName)
        {
            Thread.Sleep(1000);
            return Json(authorizationService.IsUserExists(userName), JsonRequestBehavior.AllowGet);
        }

        public AccountController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        //
        // GET: /Account/SignIn

        public virtual ActionResult SignIn()
        {
            return View();
        }

        //
        // POST: /Account/SignIn

        [HttpPost]
        public virtual ActionResult SignIn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authorizationService.SignIn(model.UserName, model.Password, model.RememberMe))
                {
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction(MVC.Home.Index());
                        //return this.RedirectToAction<HomeController>(x => x.Index());
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/SignOut

        [AuthorizeOrNotFound]
        public virtual ActionResult SignOut()
        {
            authorizationService.SignOut();

            return RedirectToAction(MVC.Home.Index());
        }

        //
        // GET: /Account/Register

        public virtual ActionResult Register()
        {
            ViewBag.MinPasswordLength = authorizationService.MinPasswordLength;
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public virtual ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                var status = authorizationService.CreateUser(model.UserName, model.Password, model.Email);

                if (status.IsOk)
                {
                    return this.RedirectToAction(x => x.SignIn());
                }
                else
                {
                    ModelState.AddModelError("", status.ErrorMessage);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword
        [AuthorizeOrNotFound]
        public virtual ActionResult ChangePassword()
        {
            ViewBag.MinPasswordLength = authorizationService.MinPasswordLength;
            return View();
        }

        //
        // POST: /Account/ChangePassword
        [AuthorizeOrNotFound, HttpPost]
        public virtual ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded = authorizationService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);

                if (changePasswordSucceeded)
                {
                    return this.RedirectToAction(x => x.ChangePasswordSuccess());
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess
        [AuthorizeOrNotFound]
        public virtual ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        [Authorize]
        public virtual ActionResult EditProfile()
        {
            return View(new UserProfile(User.Identity.Name));
        }

        [AuthorizeOrNotFound, HttpPost]
        public virtual ActionResult EditProfile(UserProfile profile)
        {
            return RedirectToAction(MVC.Home.Index());
        }

        [ChildActionOnly]
        public virtual PartialViewResult RenderLogon()
        {
            return PartialView(Views._LogOnPartial);
        }
    }
}
