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
    }
}
