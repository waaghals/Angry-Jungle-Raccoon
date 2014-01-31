using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BarometerDomain.OAuth;
using BarometerDomain.Model;
using BarometerDomain.Models;
using BarometerStudent.Services;

namespace BarometerStudent.Controllers
{
    public class LoginController : Controller
    {
        //
        // POST: /login/index

        public ActionResult Index()
        {
            ViewBag.ReturnUrl = null;
            return View();
        }

        //
        // POST: /Account/Logout

        public ActionResult Logout()
        {
            Session["User"] = null;

            return RedirectToAction("Index", "Login");
        }

        //
        // POST: /login/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Login/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var avansUser = (AvansOAuthClient)OAuthWebSecurity.GetOAuthClientData("avans").AuthenticationClient;
            AuthenticationResult result = avansUser.VerifyAuthentication(HttpContext);

            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            //naar yannicks code
            //AuthorizationSplitter(result.ProviderUserId, result.UserName);

            return AuthorizationSplitter(result.ProviderUserId, result.UserName);
        }

        //
        // POST: /Login/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList()
        {
            ViewBag.ReturnUrl = null;
            return PartialView("AuthenticationPosibilitys", OAuthWebSecurity.RegisteredClientData);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

        public ActionResult AuthorizationSplitter(string studentNumber, string login)
        {
            UserService uService = new UserService();
            User user = uService.Login(Convert.ToInt32(studentNumber), login);
            Session["User"] = user; //sla de user op in de sessie
            System.Diagnostics.Debug.WriteLine(((User)Session["User"]).Name);
            
            if (user.RoleType.Equals("Docent") || user.RoleType.Equals("Administrator"))
            {
                return RedirectToAction("Index", "Docent");
            }
            else
            {
                return RedirectToAction("Index", "StudentHome"); //redirect de user naar de juiste startpagina gebaseerd op login data
            }
        }
    }
}

