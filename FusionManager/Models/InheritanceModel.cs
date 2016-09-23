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
        int GetMaximumTransferableSkills(int totalNumberofSkills);
    }

    public class InheritanceModel : IInheritanceModel
    {
        bool[,] skillInheritance;
        Dictionary<int, int> maximumTransferableSkills;

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

            maximumTransferableSkills = new Dictionary<int, int>();
            maximumTransferableSkills.Add(4, 1);
            maximumTransferableSkills.Add(5, 1);
            maximumTransferableSkills.Add(6, 1);
            maximumTransferableSkills.Add(7, 2);
            maximumTransferableSkills.Add(8, 2);
            maximumTransferableSkills.Add(9, 2);
            maximumTransferableSkills.Add(10, 3);
            maximumTransferableSkills.Add(11, 3);
            maximumTransferableSkills.Add(12, 3);
            maximumTransferableSkills.Add(13, 4);            

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

        public int GetMaximumTransferableSkills(int totalNumberofSkills)
        {       
            if(totalNumberofSkills < 4)
            {
                return 1;
            }

            if(totalNumberofSkills > 13)
            {
                return maximumTransferableSkills[13];
            }
            
            return maximumTransferableSkills[totalNumberofSkills];            
        }
    }
}
