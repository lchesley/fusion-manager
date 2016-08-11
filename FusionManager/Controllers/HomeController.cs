using FusionManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FusionManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonaRepository repository;

        public HomeController(IPersonaRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            var personaList = repository.GetPersonaList();
            return View(personaList);
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