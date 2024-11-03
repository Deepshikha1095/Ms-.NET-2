using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using DalLibrary;

namespace MVC_Model.Controllers
{
    public class HomeController : Controller
    {
        static string str = @"Data Source=DESKTOP-RUT99G3\SQLEXPRESS01;Initial Catalog=DacWonders;Integrated Security=true";
        CDal dal = new CDal(str);

        public ActionResult Index()
        {
            return View(dal.GetAllEmployees());
        }
        [HttpGet]
        public ActionResult AddEmp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmp(EmployeeClass e)
        {
            dal.AddEmployee(e);
            return RedirectToAction("Index");
        }
        public ActionResult UpdateEmployee(int id)
        {
            var emp = (from em in dal.GetAllEmployees() where em.Id == id select em).First();
            return View(emp);

        }
        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeClass emp)
        {
            dal.ModifyEmployee(emp);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteEmployee(int id)
        {
            dal.RemoveEmployee(id);
            return RedirectToAction("Index");

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}