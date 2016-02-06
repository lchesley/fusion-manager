using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FusionManager.Models
{
    public class PersonaModel : IPersonaModel
    {
        List<Persona> personaList;
        ISkillModel skillModel;
        IInheritanceModel inheritanceModel;

        public PersonaModel(StreamReader reader, ISkillModel skillModel, IInheritanceModel inheritanceModel)
        {
            this.skillModel = skillModel;
            this.inheritanceModel = inheritanceModel;
            personaList = BuildPersonaList(reader);
        }

        public List<Persona> GetPersonaList()
        {
            return personaList;
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

        protected List<Persona> BuildPersonaList(StreamReader reader)
        {
            List<Persona> list = new List<Persona>();

            using (TextReader textReader = reader)
            {
                var csv = new CsvReader(textReader);
                while (csv.Read())
                {
                    Persona persona = new Persona();
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
                    list.Add(persona);
                }
            }

            return list;
        }
    }
}
