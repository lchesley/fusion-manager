using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FusionManager.Models
{
    public interface IPersonaModel
    {
        List<Persona> GetPersonaList();
        List<Persona> GetPersonaList(Arcana arcana);
        Persona GetPersonaByPersonaName(string name);
        Tuple<Persona, Persona> GetNextLowestAndNextHighestPersonaByArcana(Arcana arcana, double targetLevel);
        int GetMaximumTransferableSkills(int totalNumberOfSkills);
        Persona GetPersonaByID(int iD);
    }

    public class PersonaModel : IPersonaModel
    {
        List<Persona> personaList;
        ISkillModel skillModel;
        IInheritanceModel inheritanceModel;
        ICompendiumModel compendiumModel;

        public PersonaModel(StreamReader reader, ISkillModel skillModel, IInheritanceModel inheritanceModel, ICompendiumModel compendiumModel)
        {
            this.skillModel = skillModel;
            this.inheritanceModel = inheritanceModel;
            this.compendiumModel = compendiumModel;
            personaList = BuildPersonaList(reader);
        }

        public List<Persona> GetPersonaList()
        {
            return personaList.OrderBy(o => o.ActualLevel).ToList<Persona>();
        }

        public List<Persona> GetPersonaList(Arcana arcana)
        {
            return GetPersonaList().Where(o => o.Arcana == arcana).OrderBy(o => o.ActualLevel).ToList<Persona>();
        }

        public Persona GetPersonaByPersonaName(string name)
        {
            try
            { 
                return personaList.Where(o => o.Name == name).Single();
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(String.Format("No persona named {0} exists", name), ex);
            }
        } 
        
        public Persona GetPersonaByID(int ID)
        {
            try
            {
                return personaList.Where(o => o.ID == ID).Single();
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(String.Format("Persona {0} does not exist", ID), ex);
            }
        }

        public Tuple<Persona, Persona> GetNextLowestAndNextHighestPersonaByArcana(Arcana arcana, double targetLevel)
        {
            Persona lower = new Persona();
            Persona higher = new Persona();

            lower = GetPersonaList(arcana).Where(o => o.ActualLevel <= targetLevel).OrderByDescending(o => o.ActualLevel).FirstOrDefault();
            if (lower == null)
            {
                lower = GetPersonaList(arcana).OrderBy(o => o.ActualLevel).FirstOrDefault();
            }

            higher = GetPersonaList(arcana).Where(o => o.ActualLevel >= targetLevel).OrderBy(o => o.ActualLevel).FirstOrDefault();
            if (higher == null)
            {
                higher = GetPersonaList(arcana).OrderByDescending(o => o.ActualLevel).FirstOrDefault();
            }

            Tuple<Persona, Persona> result = new Tuple<Persona, Persona>(lower, higher);

            return result;
        }

        protected List<Persona> BuildPersonaList(StreamReader reader)
        {
            List<Persona> list = new List<Persona>();
            CompendiumEntry entry = new CompendiumEntry();
            int count = 1;

            using (TextReader textReader = reader)
            {
                var csv = new CsvReader(textReader);
                
                while (csv.Read())
                {
                    Persona persona = new Persona();
                    persona.ID = count;
                    persona.HPIncrease = Convert.ToInt32(csv.GetField<string>("HP"));
                    persona.SPIncrease = Convert.ToInt32(csv.GetField<string>("SP"));
                    persona.Arcana = (Arcana)Enum.Parse(typeof(Arcana), csv.GetField<string>("Arcana"));
                    persona.ExtractedSkill = skillModel.GetSkillBySkillName(csv.GetField<string>("Card"));
                    persona.InitialLevel = Convert.ToInt32(csv.GetField<string>("Lv"));
                    persona.IsDownloadedContent = (csv.GetField<string>("DLC") == "X") ? true : false;
                    persona.Name = csv.GetField<string>("Persona");
                    persona.LearnedSkills = skillModel.GetLearnedSkillsFromSkillList(csv.GetField<string>("Skills"));
                    persona.InheritanceType = (csv.GetField<string>("Type") == "") ? PersonaInheritanceType.Any : (PersonaInheritanceType)Enum.Parse(typeof(PersonaInheritanceType), csv.GetField<string>("Type"));
                    persona.InheritableSkillTypes = inheritanceModel.GetSkillInheritanceByPersonaInheritanceType(persona.InheritanceType);
                    try
                    {
                        entry = compendiumModel.GetCompendiumEntryByPersonaName(persona.Name);
                    }
                    catch(KeyNotFoundException)
                    {
                        entry = null;
                    }
                    persona.ActualLevel = entry != null ? entry.ActualLevel : persona.InitialLevel;
                    persona.InheritedSkills = entry != null ? skillModel.GetSkillsFromSkillList(entry.InheritedSkills) : new List<Skill>();                    
                    list.Add(persona);
                    count++;
                }
            }

            return list;
        }

        public int GetMaximumTransferableSkills(int totalNumberOfSkills)
        {
            return inheritanceModel.GetMaximumTransferableSkills(totalNumberOfSkills);
        }
    }
}
