using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult LoginSignUp()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {
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