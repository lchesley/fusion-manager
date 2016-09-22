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

    public class InheritanceModel : IInheritanceModel
    {
        bool[,] skillInheritance;

        public InheritanceModel()
        {
            #region Inheritance Arrays

            skillInheritance = new bool[31, 13] {
                {true, true, true, true, true, true, true, true, true, true, true, true, true},
                {true, false, false, false, false, false, false, true, true, true, true, true, true},
                {true, false, false, false, false, false, false, false, true, false, true, false, true},
                {true, true, true, true, true, true, true, true, true, true, true, true, true},
                {true, true, true, true, true, true, true, false, true, false, true, false, true},
                {false, true, true, true, true, true, true, true, true, true, true, true, true},
                {false, true, true, true, true, true, true, false, true, false, true, false, true},
                {true, true, false, true, true, true, true, true, true, true, true, true, true},
                {true, true, false, true, true, true, true, false, true, false, true, false, true},
                {false, true, false, true, true, true, true, true, true, true, true, true, true},
                {false, true, false, true, true, true, true, false, true, false, true, false, true},
                {true, false, true, true, true, true, true, true, true, true, true, true, true},
                {true, false, true, true, true, true, true, false, true, false, true, false, true},
                {false, false, true, true, true, true, true, true, true, true, true, true, true},
                {false, false, true, true, true, true, true, false, true, false, true, false, true},
                {true, true, true, true, false, true, true, true, true, true, true, true, true},
                {true, true, true, true, false, true, true, false, true, false, true, false, true},
                {false, true, true, true, false, true, true, true, true, true, true, true, true},
                {false, true, true, true, false, true, true, false, true, false, true, false, true},
                {true, true, true, false, true, true, true, true, true, true, true, true, true},
                {true, true, true, false, true, true, true, false, true, false, true, false, true},
                {false, true, true, false, true, true, true, true, true, true, true, true, true},
                {false, true, true, false, true, true, true, false, true, false, true, false, true},
                {true, true, true, true, true, true, false, true, true, true, true, true, true},
                {true, true, true, true, true, true, false, false, true, false, true, false, true},
                {false, true, true, true, true, true, false, true, true, true, true, true, true},
                {false, true, true, true, true, true, false, false, true, false, true, false, true},
                {true, true, true, true, true, false, true, true, true, true, true, true, true},
                {true, true, true, true, true, false, true, false, true, false, true, false, true},
                {false, true, true, true, true, false, true, true, true, true, true, true, true},
                {false, true, true, true, true, false, true, false, true, false, true, false, true},
            };

            #endregion
        }

        public List<SkillInheritance> GetSkillInheritanceByPersonaInheritanceType(PersonaInheritanceType type)
        {
            List<SkillInheritance> list = new List<SkillInheritance>();

            for (int i = 0; i < 13; i++)
            {
                SkillInheritance item = new SkillInheritance();
                item.Type = (SkillInheritanceType)i;
                item.CanInherit = skillInheritance[(int)type, i];
                if (item.CanInherit)
                {
                    list.Add(item);
                }
            }

            return list;
        }
    }
}
