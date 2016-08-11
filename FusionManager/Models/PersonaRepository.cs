using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FusionManager.Models
{
    public class PersonaRepository : IPersonaRepository
    {
        IFusionModel fusionModel;
        IPersonaModel personaModel;
        ISkillModel skillModel;
        IInheritanceModel inheritanceModel;
        IFusionArcanaModel fusionArcanaModel;

        public PersonaRepository()
        {
            fusionArcanaModel = new FusionArcanaModel();
            StreamReader fusionReader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/FusionGuide.csv"));
            StreamReader skillsReader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/SkillList.csv"));
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
