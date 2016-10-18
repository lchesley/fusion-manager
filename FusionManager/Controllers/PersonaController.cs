using FusionManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FusionManager.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IPersonaRepository repository;

        public PersonaController(IPersonaRepository repository)
        {
            this.repository = repository;
        }

        // GET: Persona
        public ActionResult Index()
        {
            var personaList = repository.GetPersonaList();
            return View(personaList);
        }

        public ActionResult Details(int ID)
        {
            var persona = repository.GetPersonaByID(ID);
            return View(persona);
        }

        public ActionResult CompendiumIndex()
        {
            var compendiumList = repository.GetCompendiumList();
            return View(compendiumList);
        }
    }
}