using System.Collections.Generic;

namespace FusionManager.Models
{
    public interface ISkillModel
    {
        List<Skill> GetSkillList();
        Skill GetSkillBySkillName(string skillName);        
        List<LearnedSkill> GetLearnedSkillsFromSkillList(string skillList);
    }
}