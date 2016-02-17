using System; using System.Collections.Generic; using System.Linq; using System.Web; using System.Web.Http.Cors; using System.Web.Mvc;  namespace AJKM_phase1.Controllers {     [EnableCors(origins: "*", headers: "*", methods: "*")]     public class HomeController : Controller     {         // ANDREW          public ActionResult Index()         {             return View();         }          public ActionResult AboutSupport()
        {
            return View();
        }          public ActionResult About()         {             return View();         }         public ActionResult Support()         {             return View();         }         public ActionResult FAQ()         {             return View();         }         public ActionResult Contact()         {             return View();         }          public ActionResult DashTest()
        {
            return View();
        }          public ActionResult API()
        {
            return View();
        }     } }