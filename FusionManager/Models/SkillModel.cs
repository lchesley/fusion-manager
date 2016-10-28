using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FusionManager.Models
{
    public interface ISkillModel
    {
        List<Skill> GetSkillList();
        Skill GetSkillBySkillName(string skillName);
        List<LearnedSkill> GetLearnedSkillsFromSkillList(string skillList);
        List<Skill> GetSkillsFromSkillList(string skills);
        List<Skill> GetInheritableSkillsByInheritanceType(SkillInheritanceType type);
    }

    public class SkillModel : ISkillModel
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

        public List<LearnedSkill> GetLearnedSkillsFromSkillList(string skillList)
        {
            List<LearnedSkill> list = new List<LearnedSkill>();

            char[] delimiterChars = { ',' };
            string[] skills = skillList.Split(delimiterChars);

            foreach (string s in skills)
            {
                LearnedSkill item = new LearnedSkill();
                int firstBracket = s.IndexOf("(");
                int secondBracket = s.IndexOf(")");
                if (firstBracket > 0)
                {
                    item.LevelLearned = Convert.ToInt32(s.Substring(firstBracket + 1, (secondBracket - (firstBracket + 1))));
                    item.Skill = GetSkillBySkillName(s.Remove(firstBracket).Trim());
                }
                else
                {
                    item.LevelLearned = 1;
                    item.Skill = GetSkillBySkillName(s.Trim());
                }

                list.Add(item);
            }

            return list;
        }

        public List<Skill> GetSkillsFromSkillList(string skills)
        {
            List<Skill> list = new List<Skill>();

            char[] delimiterChars = { ',' };
            string[] skillList = skills.Split(delimiterChars);

            foreach (string s in skillList)
            {
                Skill item = new Skill();
                item = GetSkillBySkillName(s.Trim());
                list.Add(item);
            }

            return list;
        }

        public Skill GetSkillBySkillName(string skillName)
        {
            try
            {
                return skillList.Where(o => o.Name == skillName).Single();
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(String.Format("No skill named {0} exists.", skillName), ex);
            }
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

        public List<Skill> GetInheritableSkillsByInheritanceType(SkillInheritanceType type)
        {
            List<Skill> skills = new List<Skill>();

            skills = skillList.Where(o => o.SkillType == type && o.CanPassDown).ToList<Skill>();

            return skills;
        }
    }
}
