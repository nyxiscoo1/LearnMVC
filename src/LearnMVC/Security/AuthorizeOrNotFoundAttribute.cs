﻿using System;
using System.Web;
using System.Web.Mvc;

namespace LearnMVC.Security
{
    /// <summary>
    /// For more info see http://farm-fresh-code.blogspot.com/2011/03/revisiting-custom-authorization-in.html
    /// </summary>
    public class AuthorizeOrNotFoundAttribute : AuthorizeAttribute
    {
        public void CacheValidationHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                //... your custom code ...
                SetCachePolicy(filterContext);
            }
            else if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {             
                // auth failed, redirect to login page
                filterContext.Result = new RedirectResult("~/Errors/NotFound");
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Errors/NotFound?aspxerrorpath=" + HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri));
                //... handle a different case than not authenticated
            }
        }

        protected void SetCachePolicy(AuthorizationContext filterContext)
        {
            // ** IMPORTANT **
            // Since we're performing authorization at the action level, the authorization code runs
            // after the output caching module. In the worst case this could allow an authorized user
            // to cause the page to be cached, then an unauthorized user would later be served the
            // cached page. We work around this by telling proxies not to cache the sensitive page,
            // then we hook our custom authorization code into the caching mechanism so that we have
            // the final say on whether a page should be served from the cache.
            HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            cachePolicy.AddValidationCallback(CacheValidationHandler, null /* data */);
        }
    }
}