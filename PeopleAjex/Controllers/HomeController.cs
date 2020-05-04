using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleAjex.Data;

namespace PeopleAjex.Controllers
{
    public class HomeController : Controller
    {
        private PeopleDB _database = new PeopleDB("Data Source=.\\sqlexpress69;Initial Catalog=People;Integrated Security=True");
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPeople()
        {
            return Json(_database.GetPeople());
        }
        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            return Json(new { Success = _database.AddPerson(person) == 1 });
        }
        public IActionResult GetPerson(int id)
        {
            return Json(_database.GetPerson(id));
        }
        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
            return Json(new { Success = _database.EditPerson(person) == 1 });
        }
        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            return Json(new { Success = _database.DeletePerson(id) == 1 });
        }
    }
}
