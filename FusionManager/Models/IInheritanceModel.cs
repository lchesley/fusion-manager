using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public interface IInheritanceModel
    {
        List<SkillInheritance> GetSkillInheritanceByPersonaInheritanceType(PersonaInheritanceType type);
    }
}
