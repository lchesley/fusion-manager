using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public interface IFusionModel
    {
        Persona FusePersona(Persona first, Persona second);
        Persona FusePersona(Persona first, Persona second, Persona third);
        List<Tuple<Arcana,Arcana>> FusionSearch(Arcana desiredArcana);
        List<string[]> FusionSearch(Persona desiredPersona);
        List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona);
        List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona, Persona secondRequiredPersona);
        List<string[]> FusionSearch(Persona desiredPersona, int upperLevelBoundary);
        List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona, int upperLevelBoundary);
        List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona, Persona secondRequiredPersona, int upperLevelBoundary);
    }
}
