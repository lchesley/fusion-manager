using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public class LearnedSkill
    {
        public Skill Skill { get; set; }
        public int LevelLearned { get; set; }

        public override bool Equals(object obj)
        {
            LearnedSkill item = obj as LearnedSkill;
            if (item == null)
            {
                return false;
            }
            else
            {                
                return LevelLearned.Equals(item.LevelLearned) && Skill.Equals(item.Skill);
            }
        }

        public override int GetHashCode()
        {
            return this.Skill.GetHashCode();
        }
    }
}
