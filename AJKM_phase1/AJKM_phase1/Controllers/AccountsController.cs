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
        // JALEN
        public ActionResult LoginSignUp()
        {
            return View();
        }
        // JALEN
        public ActionResult Login()
        {
            return View();
        }
        // JALEN
        public ActionResult SignUp()
        {
            return View();
        }
        // MAT
        public ActionResult ConsumerDashboard()
        {
            return View();
        }
        // MAT
        public ActionResult AdminDashboard()
        {
            return View();
        }
        // KULLY
        public ActionResult Data()
        {
            return View();
        }
        // KULLY
        public ActionResult Insights()
        {
            return View();
        }
        // KULLY
        public ActionResult DeviceManager()
        {
            return View();
        }

        public ActionResult RegisterDevices()
        {
            return View();
        }

        public ActionResult AccountView()
        {
            return View();
        }

        public ActionResult ViewAllConsumerAccounts()
        {
            return View();
        }


    }
}