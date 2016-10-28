using FusionManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FusionManager.Tests.Mocks
{
    public class MockPersonaRepository : IPersonaRepository
    {
        IFusionModel fusionModel;
        IPersonaModel personaModel;
        ISkillModel skillModel;
        IInheritanceModel inheritanceModel;
        IFusionArcanaModel fusionArcanaModel;
        ICompendiumModel compendiumModel;

        public MockPersonaRepository()
        {
            fusionArcanaModel = new FusionArcanaModel();
            StreamReader fusionReader = new StreamReader("App_Data\\FusionGuide.csv");
            StreamReader skillsReader = new StreamReader("App_Data\\SkillList.csv");
            StreamReader compendiumReader = new StreamReader("App_Data\\Compendium.csv");
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
            throw new NotImplementedException();
        }
                
        public CompendiumEntry GetCompendiumEntry(string name)
        {
            throw new NotImplementedException();
        }        

        public List<SelectListItem> GetPersonaNames(bool includeCompendium)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Persona> GetCompendiumList()
        {
            throw new NotImplementedException();
        }

        public Persona GetPersonaByPersonaName(string name)
        {
            throw new NotImplementedException();
        }

        public string ConvertToStringFromBase64(string name)
        {
            string result = String.Empty;

            result = Encoding.UTF8.GetString(Convert.FromBase64String(name));

            return result;
        }
    }
}
