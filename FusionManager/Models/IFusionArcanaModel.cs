using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public interface IFusionArcanaModel
    {
        Arcana GetDoubleFusionResultingArcana(Arcana first, Arcana second);
        List<Tuple<Arcana, Arcana>> GetDoubleFusionParametersByArcana(Arcana desiredResult);
        Arcana GetTripleFusionResultingArcana(Arcana first, Arcana second);
        List<Tuple<Arcana, Arcana>> GetTripleFusionParametersByArcana(Arcana desiredResult);
        bool OnlyAvailableThroughSpecialFusion(string name);
        bool CombinationResultsInSpecialFusion(string[] combination);
        string GetSpecialFusionResult(string[] combination);
        List<string[]> GetSpecialFusionCombination(string name);
    }
}
