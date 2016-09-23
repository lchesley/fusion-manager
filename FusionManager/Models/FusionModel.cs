using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public interface IFusionModel
    {
        Persona FusePersona(Persona first, Persona second);
        Persona FusePersona(Persona first, Persona second, Persona third);
        Tuple<int,List<Skill>> GetInheritableSkills(Persona result, Persona first, Persona second);
        Tuple<int,List<Skill>> GetInheritableSkills(Persona result, Persona first, Persona second, Persona third);
        List<Tuple<Arcana, Arcana>> FusionSearch(Arcana desiredArcana);
        List<string[]> FusionSearch(Persona desiredPersona);
        List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona);
        List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona, Persona secondRequiredPersona);
        List<string[]> FusionSearch(Persona desiredPersona, int upperLevelBoundary);
        List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona, int upperLevelBoundary);
        List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona, Persona secondRequiredPersona, int upperLevelBoundary);
    }

    public class FusionModel : IFusionModel
    {
        IFusionArcanaModel fusionArcanaModel;
        IPersonaModel personaModel;        

        public FusionModel(IFusionArcanaModel fusionArcanaModel, IPersonaModel personaModel)
        {
            this.fusionArcanaModel = fusionArcanaModel;
            this.personaModel = personaModel;            
        }

        #region Double and Triple Fusion

        public Persona FusePersona(Persona first, Persona second)
        {
            if (first.Equals(second))
            {
                throw new ArgumentException("A persona cannot be fused to itself");
            }

            if (fusionArcanaModel.CombinationResultsInSpecialFusion(new string[] { first.Name, second.Name }))
            {
                return personaModel.GetPersonaByPersonaName(fusionArcanaModel.GetSpecialFusionResult(new string[] { first.Name, second.Name }));
            }

            double averageBaseLevel = ((double)first.ActualLevel + (double)second.ActualLevel) / 2 + 1;

            if (first.Arcana == second.Arcana)
            {
                return personaModel.GetPersonaList(first.Arcana).Where(o => o.ActualLevel < averageBaseLevel && !o.Equals(first) && !o.Equals(second)).OrderByDescending(o => o.ActualLevel).FirstOrDefault();
            }

            Persona result = new Persona();
            
            Arcana resultingArcana;

            if (fusionArcanaModel.GetDoubleFusionResultingArcana(first.Arcana, second.Arcana) == Arcana.None)
            {
                resultingArcana = fusionArcanaModel.GetDoubleFusionResultingArcana(second.Arcana, first.Arcana);
            }
            else
            {
                resultingArcana = fusionArcanaModel.GetDoubleFusionResultingArcana(first.Arcana, second.Arcana);
            }

            //Get the persona on either side of the base level.
            Tuple<Persona, Persona> resultCandidates = personaModel.GetNextLowestAndNextHighestPersonaByArcana(resultingArcana, averageBaseLevel);

            Persona lower = resultCandidates.Item1;
            Persona higher = resultCandidates.Item2;
            
            //Take higher or lower, depending on which one has the smallest difference from the averageBaseLevel.
            if (Math.Abs(averageBaseLevel - lower.ActualLevel) > Math.Abs(higher.ActualLevel - averageBaseLevel) || Math.Abs(averageBaseLevel - lower.ActualLevel) == Math.Abs(higher.ActualLevel - averageBaseLevel))
            {
                result = higher;
            }
            else
            {
                result = lower;
            }

            return result;
        }

        public Persona FusePersona(Persona first, Persona second, Persona third)
        {
            if (first.Equals(second) || second.Equals(third) || first.Equals(third))
            {
                throw new ArgumentException("A persona cannot be fused to itself");
            }

            if (fusionArcanaModel.CombinationResultsInSpecialFusion(new string[] { first.Name, second.Name, third.Name }))
            {
                return personaModel.GetPersonaByPersonaName(fusionArcanaModel.GetSpecialFusionResult(new string[] { first.Name, second.Name, third.Name }));
            }

            double averageBaseLevel = ((double)first.ActualLevel + (double)second.ActualLevel + (double)third.ActualLevel) / 3 + 5;

            if (first.Arcana == second.Arcana && first.Arcana == third.Arcana)
            {
                return personaModel.GetPersonaList(first.Arcana).Where(o => o.ActualLevel > averageBaseLevel && !o.Equals(first) && !o.Equals(second) && !o.Equals(third)).OrderBy(o => o.ActualLevel).FirstOrDefault();
            }

            Persona result = new Persona();

            //Make a list of the persona, order by level.
            List<Persona> components = new List<Persona>();
            components.Add(first);
            components.Add(second);
            components.Add(third);
            components.OrderBy(o => o.ActualLevel);

            //Fuse the lowest two persona.
            Persona temp = FusePersona(components[0], components[1]);

            //If the temporary persona equals the third, not valid.
            if (temp.Equals(components[2]))
            {
                throw new ArgumentException("This fusion is invalid - the first step would result in a persona being fused to itself.");
            }

            Arcana resultingArcana;

            if (fusionArcanaModel.GetTripleFusionResultingArcana(temp.Arcana, components[2].Arcana) == Arcana.None)
            {
                resultingArcana = fusionArcanaModel.GetTripleFusionResultingArcana(components[2].Arcana, temp.Arcana);
            }
            else
            {
                resultingArcana = fusionArcanaModel.GetTripleFusionResultingArcana(temp.Arcana, components[2].Arcana);
            }

            //Get the persona on either side of the base level.
            Tuple<Persona, Persona> resultCandidates = personaModel.GetNextLowestAndNextHighestPersonaByArcana(resultingArcana, averageBaseLevel);

            Persona lower = resultCandidates.Item1;
            Persona higher = resultCandidates.Item2;

            //Take higher or lower, depending on which one has the smallest difference.
            if (Math.Abs(averageBaseLevel - lower.ActualLevel) > Math.Abs(higher.ActualLevel - averageBaseLevel) || Math.Abs(averageBaseLevel - lower.ActualLevel) == Math.Abs(higher.ActualLevel - averageBaseLevel))
            {
                result = higher;
            }
            else
            {
                result = lower;
            }

            return result;
        }

        public Tuple<int,List<Skill>> GetInheritableSkills(Persona result, Persona first, Persona second)
        {
            int maximumTransferableSkills = 0;
            List<Skill> skills = new List<Skill>();

            //Check for compendium skill list, if it doesn't exist, use the learned skills.
            if(first.InheritedSkills != null && first.InheritedSkills.Count > 0)
            {
                skills.AddRange(first.InheritedSkills);
            }
            else
            {
                skills.AddRange(first.LearnedSkills.Where(o => o.LevelLearned <= first.ActualLevel).Select(o => o.Skill).ToList<Skill>());
            }

            if (second.InheritedSkills != null && second.InheritedSkills.Count > 0)
            {
                skills.AddRange(second.InheritedSkills);
            }
            else
            {
                skills.AddRange(second.LearnedSkills.Where(o => o.LevelLearned <= second.ActualLevel).Select(o => o.Skill).ToList<Skill>());
            }

            maximumTransferableSkills = personaModel.GetMaximumTransferableSkills(skills.Count);

            //Now check to make sure all of the skills are inheritable (and unique).
            skills = skills.Where(o => o.CanPassDown && result.InheritableSkillTypes.Where(p => p.CanInherit).Select(p => p.Type).ToList<SkillInheritanceType>().Contains(o.SkillType)).Distinct<Skill>().ToList<Skill>();

            return new Tuple<int, List<Skill>>(maximumTransferableSkills, skills);
        }

        public Tuple<int,List<Skill>> GetInheritableSkills(Persona result, Persona first, Persona second, Persona third)
        {
            int maximumTransferableSkills = 0;
            List<Skill> skills = new List<Skill>();

            //Check for compendium skill list, if it doesn't exist, use the learned skills.
            if (first.InheritedSkills != null && first.InheritedSkills.Count > 0)
            {
                skills.AddRange(first.InheritedSkills);
            }
            else
            {
                skills.AddRange(first.LearnedSkills.Where(o => o.LevelLearned <= first.ActualLevel).Select(o => o.Skill).ToList<Skill>());
            }

            if (second.InheritedSkills != null && second.InheritedSkills.Count > 0)
            {
                skills.AddRange(second.InheritedSkills);
            }
            else
            {
                skills.AddRange(second.LearnedSkills.Where(o => o.LevelLearned <= second.ActualLevel).Select(o => o.Skill).ToList<Skill>());
            }

            if (third.InheritedSkills != null && third.InheritedSkills.Count > 0)
            {
                skills.AddRange(third.InheritedSkills);
            }
            else
            {
                skills.AddRange(third.LearnedSkills.Where(o => o.LevelLearned <= third.ActualLevel).Select(o => o.Skill).ToList<Skill>());
            }

            maximumTransferableSkills = personaModel.GetMaximumTransferableSkills(skills.Count);

            //Now check to make sure all of the skills are inheritable (and unique).
            skills = skills.Where(o => o.CanPassDown && result.InheritableSkillTypes.Where(p => p.CanInherit).Select(p => p.Type).ToList<SkillInheritanceType>().Contains(o.SkillType)).Distinct<Skill>().ToList<Skill>();

            return new Tuple<int, List<Skill>>(maximumTransferableSkills, skills);
        }

        #endregion

        #region Fusion Search

        public List<Tuple<Arcana, Arcana>> FusionSearch(Arcana desiredArcana)
        {
            List<Tuple<Arcana, Arcana>> combinations = new List<Tuple<Arcana, Arcana>>();

            combinations.AddRange(fusionArcanaModel.GetDoubleFusionParametersByArcana(desiredArcana));

            combinations.AddRange(fusionArcanaModel.GetTripleFusionParametersByArcana(desiredArcana));

            return combinations;
        }

        public List<string[]> FusionSearch(Persona desiredPersona)
        {
            throw new NotImplementedException();
        }
       
        public List<string[]> FusionSearch(Persona desiredPersona, int upperLevelBoundary)
        {
            throw new NotImplementedException();
        }

        public List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona)
        {
            throw new NotImplementedException();
        }

        public List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona, int upperLevelBoundary)
        {
            throw new NotImplementedException();
        }

        public List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona, Persona secondRequiredPersona)
        {
            throw new NotImplementedException();
        }

        public List<string[]> FusionSearch(Persona desiredPersona, Persona firstRequredPersona, Persona secondRequiredPersona, int upperLevelBoundary)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
