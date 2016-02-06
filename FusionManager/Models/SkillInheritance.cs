using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public class SkillInheritance
    {
        public SkillInheritanceType Type { get; set; }
        public bool CanInherit { get; set; }

        public override bool Equals(object obj)
        {
            SkillInheritance item = obj as SkillInheritance;
            if (item == null)
            {
                return false;
            }
            else
            {
                return Type.Equals(item.Type) && CanInherit.Equals(item.CanInherit);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
