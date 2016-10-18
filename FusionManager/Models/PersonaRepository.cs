using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FusionManager.Models
{
    public interface IPersonaRepository
    {
        IEnumerable<Persona> GetPersonaList();
        Persona GetPersonaByID(int ID);
        IEnumerable<CompendiumEntry> GetCompendiumList();
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

        public Persona GetPersonaByID(int ID)
        {
            return personaModel.GetPersonaByID(ID);
        }

        public IEnumerable<CompendiumEntry> GetCompendiumList()
        {
            return compendiumModel.GetCompendium();
        }
    }
}
