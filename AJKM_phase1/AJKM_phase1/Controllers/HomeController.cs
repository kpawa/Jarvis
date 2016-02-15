using AJKM_phase1.Models;
using AJKM_phase1.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using AJKM_phase1.Repository;
using AJKM_phase1.ViewModels;

namespace AJKM_phase1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Support()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public async Task<ActionResult> GetJarvisAPI()
        {
            JarvisEntities db = new JarvisEntities();
            JarvisAPIRepo jarvisAPIRepo = new JarvisAPIRepo();
            var users = await jarvisAPIRepo.GetAll();
            var json = JsonConvert.SerializeObject(users);
            return new ContentResult
            {
                Content = json.ToString(),
                ContentType = "application/json"
            };
        }
    }
}