using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public class FusionInheritanceModel
    {
        public Persona FusionResult { get; set; }
        public int AllowableNumberOfSkills { get; set; }
        public List<Skill> InheritableSkills { get; set; }
        public CompendiumEntry CompendiumEntry { get; set; }
    }
}
