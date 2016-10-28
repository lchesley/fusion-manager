using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FusionManager.Models
{
    public interface IPersonaRepository
    {
        IEnumerable<Persona> GetPersonaList();
        Persona GetPersonaByPersonaName(string name);
        IEnumerable<Persona> GetCompendiumList();
        CompendiumEntry GetCompendiumEntry(string name);
        List<SelectListItem> GetPersonaNames(bool includeCompendium);
        string ConvertToStringFromBase64(string name);
    }

    public class PersonaRepository : IPersonaRepository
    {
        IFusionModel fusionModel;
        IPersonaModel personaModel;
        ISkillModel skillModel;
        IInheritanceModel inheritanceModel;
        IFusionArcanaModel fusionArcanaModel;
        ICompendiumModel compendiumModel;

        public PersonaRepository()
        {
            fusionArcanaModel = new FusionArcanaModel();
            StreamReader fusionReader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/FusionGuide.csv"));
            StreamReader skillsReader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/SkillList.csv"));
            StreamReader compendiumReader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/Compendium.csv"));
            skillModel = new SkillModel(skillsReader);
            inheritanceModel = new InheritanceModel();
            compendiumModel = new CompendiumModel(compendiumReader);
            personaModel = new PersonaModel(fusionReader, skillModel, inheritanceModel, compendiumModel);
            fusionModel = new FusionModel(fusionArcanaModel, personaModel);
        }

        public IEnumerable<Persona> GetPersonaList()
        {
            return personaModel.GetPersonaList();
        }

        public Persona GetPersonaByPersonaName(string name)
        {
            return personaModel.GetPersonaByPersonaName(name);
        }

        public IEnumerable<Persona> GetCompendiumList()
        {
            return personaModel.GetPersonaList().Where(o => o.HasCompendiumEntry);
        }
        
        public CompendiumEntry GetCompendiumEntry(string name)
        {
            return compendiumModel.GetCompendiumEntryByPersonaName(name);
        }

        public List<SelectListItem> GetPersonaNames(bool includeCompendium)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Persona> names = new List<Persona>();

            if(includeCompendium)
            {
                names = personaModel.GetPersonaList().OrderBy(o => o.Name).ToList<Persona>();
            }
            else
            {
                names = personaModel.GetPersonaList().Where(o => !o.HasCompendiumEntry).OrderBy(o => o.Name).ToList<Persona>();
            }
            
            if (names != null && names.Count > 0)
            {
                foreach (var name in names)
                {
                    list.Add(new SelectListItem { Value = name.Name, Text = name.Name });
                }
            }

            return list;
        }

        public string ConvertToStringFromBase64(string name)
        {
            string result = String.Empty;

            result = Encoding.UTF8.GetString(Convert.FromBase64String(name));

            return result;
        }
    }
}
