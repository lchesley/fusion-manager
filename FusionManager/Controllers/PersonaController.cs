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

        public ActionResult Details(string name)
        {
            var persona = repository.GetPersonaByPersonaName(repository.ConvertToStringFromBase64(name));
            return View(persona);
        }

        public ActionResult CompendiumIndex()
        {
            var compendiumList = repository.GetCompendiumList();
            return View(compendiumList);
        }

        public ActionResult CompendiumDetails(string name)
        {
            Persona persona = repository.GetPersonaByPersonaName(repository.ConvertToStringFromBase64(name));

            if(persona.HasCompendiumEntry)
            {
                var model = repository.GetCompendiumEntry(persona.Name);                
                return View(model);
            }
            else
            {
                return CompendiumEdit(name);
            }            
        }

        public ActionResult CompendiumEdit(string name)
        {
            //CompendiumEntryModel model = new CompendiumEntryModel();
            //CompendiumEntry entry = new CompendiumEntry();

            //if (ID > 0)
            //{
            //    Persona persona = repository.GetPersonaByPersonaName(name);                

            //    if (persona.HasCompendiumEntry)
            //    {
            //        entry = repository.GetCompendiumEntry(persona.Name);
            //        model.InheritedSkills = persona.InheritedSkills;
            //    }
            //    else
            //    {
            //        entry.PersonaName = persona.Name;
            //        entry.ActualLevel = persona.ActualLevel;                                                            
            //    }
            //}
            //else
            //{
            //    model.PersonaNames = repository.GetPersonaNames(false);
            //}

            //model.Entry = entry;

            //return View(model);
            return View();
        }
    }
}