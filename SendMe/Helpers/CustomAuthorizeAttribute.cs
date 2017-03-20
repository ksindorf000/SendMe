using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SendMe.Helpers
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        //----------------------------------------------------------------------------
        //    HandleUnauthorizedRequest()
        //      Overrides built-in redirect so that users are redirected
        //      to the home page rather than the log-in screen if they are
        //      Authenticated but not Authorized.
        //      http://stackoverflow.com/questions/238437/why-does-authorizeattribute-redirect-to-the-login-page-for-authentication-and-aut 
        //----------------------------------------------------------------------------
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}