using AJKM_phase1.Models;
using AJKM_phase1.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace AJKM_phase1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountsController : Controller
    {
        // leave blank
        public ActionResult Index()
        {
            return View();
        }
        /* ================================== */
        /* ====== WEB SECURITY IDENTITY ===== */
        /* ================================== */
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            IdentityUser identityUser = manager.Find(login.UserName,
                                                             login.Password);

            if (ModelState.IsValid)
            {
                if (identityUser != null)
                {
                    IAuthenticationManager authenticationManager
                                           = HttpContext.GetOwinContext().Authentication;
                    authenticationManager
                   .SignOut(DefaultAuthenticationTypes.ExternalCookie);

                    var identity = new ClaimsIdentity(new[] {
                                            new Claim(ClaimTypes.Name, login.UserName),
                                        },
                                        DefaultAuthenticationTypes.ApplicationCookie,
                                        ClaimTypes.Name, ClaimTypes.Role);
                    authenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, identity);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisteredUser newUser)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var identityUser = new IdentityUser()
            {
                UserName = newUser.UserName,
                Email = newUser.Email
            };
            IdentityResult result = manager.Create(identityUser, newUser.Password);

            if (result.Succeeded)
            {
                var authenticationManager
                                  = HttpContext.Request.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(identityUser,
                                           DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { },
                                             userIdentity);
                ViewBag.AccountCreated = "Account has been created.";
            }
            return View();
        }
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        /* ============================ */
        /* ===== ADMIN PRIVILEGES ===== */
        /* ============================ */
        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(AspNetRole role)
        {
            SecurityEntities context = new SecurityEntities();
            context.AspNetRoles.Add(role);
            context.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult AddUserToRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUserToRole(string userName, string roleName)
        {
            SecurityEntities context = new SecurityEntities();
            AspNetUser user = context.AspNetUsers
                             .Where(u => u.UserName == userName).FirstOrDefault();
            AspNetRole role = context.AspNetRoles
                             .Where(r => r.Name == roleName).FirstOrDefault();

            user.AspNetRoles.Add(role);
            context.SaveChanges();
            return View();
        }


        public ActionResult ConsumerDashboard()
        {
            return View();
        }
        public ActionResult AccountView()
        {
            return View();
        }
        public ActionResult Data()
        {
            return View();
        }
        public ActionResult Insights()
        {
            return View();
        }
        public ActionResult DeviceManager()
        {
            return View();
        }
        public ActionResult RegisterDevices()
        {
            return View();
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult ViewAllConsumerAccounts()
        {
            return View();
        }
    }
}