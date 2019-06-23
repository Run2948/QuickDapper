using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.IRepository;
using Quick.IServices;

namespace QuickWeb.Controllers
{
    public class HomeController : Controller
    {
        public IStudentService DogRepository { get; set; }

        // GET: Home
        public ActionResult Index()
        {
            var name = DogRepository.Get<string>(1, "Name");
            return Json(new { id = 1, name }, JsonRequestBehavior.AllowGet);
            //            return Content("ok");
        }
    }
}