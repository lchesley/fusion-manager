using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }        
        public string Type { get; set; }
        public string Cost { get; set; }        
        public bool CanPassDown { get; set; }        
        public SkillInheritanceType SkillType { get; set; }

        public override bool Equals(object obj)
        {
            Skill skill = obj as Skill;
            if (skill == null)
            {
                return false;
            }
            else
            {
                return Name.Equals(skill.Name);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
