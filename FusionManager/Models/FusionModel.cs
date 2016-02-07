using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public class FusionModel : IFusionModel
    {
        IFusionArcanaModel fusionArcanaModel;
        IPersonaModel personaModel;

        public FusionModel(IFusionArcanaModel fusionArcanaModel, IPersonaModel personaModel)
        {
            this.fusionArcanaModel = fusionArcanaModel;
            this.personaModel = personaModel;
        }

        public Persona FusePersona(Persona first, Persona second)
        {
            if (first.Equals(second))
            {
                throw new ArgumentException("A persona cannot be fused to itself");
            }

            if (fusionArcanaModel.CombinationResultsInSpecialFusion(new string[] { first.Name, second.Name }))
            {
                return personaModel.GetPersonaByPersonaName(fusionArcanaModel.GetSpecialFusionResult(new string[] { first.Name, second.Name }));
            }

            double averageBaseLevel = ((double)first.InitialLevel + (double)second.InitialLevel) / 2 + 1;

            if (first.Arcana == second.Arcana)
            {
                return personaModel.GetPersonaList(first.Arcana).Where(o => o.InitialLevel < averageBaseLevel && !o.Equals(first) && !o.Equals(second)).OrderByDescending(o => o.InitialLevel).FirstOrDefault();
            }

            Persona result = new Persona();

            
            return result;
        }

        public Persona FusePersona(Persona first, Persona second, Persona third)
        {
            if (first.Equals(second) || second.Equals(third) || first.Equals(third))
            {
                throw new ArgumentException("A persona cannot be fused to itself");
            }

            if (fusionArcanaModel.CombinationResultsInSpecialFusion(new string[] { first.Name, second.Name, third.Name }))
            {
                return personaModel.GetPersonaByPersonaName(fusionArcanaModel.GetSpecialFusionResult(new string[] { first.Name, second.Name, third.Name }));
            }

            double averageBaseLevel = ((double)first.InitialLevel + (double)second.InitialLevel + (double)third.InitialLevel) / 3 + 5;

            if (first.Arcana == second.Arcana && first.Arcana == third.Arcana)
            {
                return personaModel.GetPersonaList(first.Arcana).Where(o => o.InitialLevel > averageBaseLevel && !o.Equals(first) && !o.Equals(second) && !o.Equals(third)).OrderBy(o => o.InitialLevel).FirstOrDefault();
            }

            Persona result = new Persona();


            return result;
        }
    }
}
