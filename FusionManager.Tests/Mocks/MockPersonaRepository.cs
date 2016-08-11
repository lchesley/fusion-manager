using FusionManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Tests.Mocks
{
    public class MockPersonaRepository : IPersonaRepository
    {
        IFusionModel fusionModel;
        IPersonaModel personaModel;
        ISkillModel skillModel;
        IInheritanceModel inheritanceModel;
        IFusionArcanaModel fusionArcanaModel;

        public MockPersonaRepository()
        {
            fusionArcanaModel = new FusionArcanaModel();
            StreamReader fusionReader = new StreamReader("App_Data\\FusionGuide.csv");
            StreamReader skillsReader = new StreamReader("App_Data\\SkillList.csv");
            skillModel = new SkillModel(skillsReader);
            inheritanceModel = new InheritanceModel();
            personaModel = new PersonaModel(fusionReader, skillModel, inheritanceModel);
            fusionModel = new FusionModel(fusionArcanaModel, personaModel);
        }

        public IEnumerable<Persona> GetPersonaList()
        {
            return personaModel.GetPersonaList();
        }
    }
}
