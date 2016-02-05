using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public class SkillModel
    {
        List<Skill> skillList;

        public SkillModel(StreamReader reader)
        {
            skillList = BuildSkillList(reader);
        }

        public List<Skill> GetSkillList()
        {
            return skillList;
        }

        protected List<Skill> BuildSkillList(StreamReader reader)
        {
            List<Skill> list = new List<Skill>();

            using (TextReader textReader = reader)
            {
                var csv = new CsvReader(textReader);
                while (csv.Read())
                {
                    Skill skill = new Skill();
                    skill.CanPassDown = (csv.GetField<string>("Heritable") == "X") ? true : false;
                    skill.Cost = csv.GetField<string>("Cost");
                    skill.Description = csv.GetField<string>("Description");
                    skill.Name = csv.GetField<string>("Skill");
                    skill.SkillType = (SkillInheritanceType)Enum.Parse(typeof(SkillInheritanceType), csv.GetField<string>("InheritanceType")); 
                    skill.Type = csv.GetField<string>("Type");                    
                    list.Add(skill);
                }
            }

            return list;
        }
    }
}
