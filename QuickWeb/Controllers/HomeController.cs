using Quick.Common.Mapper;
using Quick.Common.Mvc.Controllers;
using Quick.IServices;
using QuickWeb.ViewModels;
using System.Web.Mvc;

namespace QuickWeb.Controllers
{
    public class HomeController : BaseController
    {
        public IStudentService StudentService { get; set; }

        // GET: Home
        public ActionResult Index()
        {
            //var name = StudentService.Get<string>(1, "Name");
            //return Ok(new { id = 1, name });
            var student = StudentService.GetById(1).Mapper<StudentViewModel>();
            return Ok(student);
        }
    }
}