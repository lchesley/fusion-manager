using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FusionManager.Models
{
    public interface ICompendiumModel
    {
        List<CompendiumEntry> GetCompendium();
        CompendiumEntry GetCompendiumEntryByPersonaName(string name);
        void UpdateCompendium(CompendiumEntry personaToUpdate, StreamWriter writer);
    }

    public class CompendiumModel : ICompendiumModel
    {
        List<CompendiumEntry> compendium;

        public CompendiumModel(StreamReader reader)
        {
            compendium = BuildCompendium(reader);
        }

        public List<CompendiumEntry> GetCompendium()
        {
            return compendium;
        }

        public CompendiumEntry GetCompendiumEntryByPersonaName(string name)
        {
            try
            {
                return compendium.Where(o => o.PersonaName == name).Single();
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(String.Format("No compendium entry for {0} exists.", name), ex);
            }
        }

        public void UpdateCompendium(CompendiumEntry personaToUpdate, StreamWriter writer)
        {
            var persona = compendium.FirstOrDefault(x => x.PersonaName == personaToUpdate.PersonaName);

            if(persona != null)
            {
                persona.ActualLevel = personaToUpdate.ActualLevel;
                persona.InheritedSkills = personaToUpdate.InheritedSkills;
            }
            else
            {
                compendium.Add(personaToUpdate);
            }

            using (TextWriter textWriter = writer)
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(compendium);
            }            
        }

        protected List<CompendiumEntry> BuildCompendium(StreamReader reader)
        {
            List<CompendiumEntry> list = new List<CompendiumEntry>();

            using (TextReader textReader = reader)
            {
                var csv = new CsvReader(textReader);
                while (csv.Read())
                {
                    CompendiumEntry entry = new CompendiumEntry();
                    entry.ActualLevel = Convert.ToInt32(csv.GetField<string>("Level"));
                    entry.PersonaName = csv.GetField<string>("Name");
                    entry.InheritedSkills = csv.GetField<string>("Skills");                    
                    list.Add(entry);
                }
            }

            return list;
        }
    }
}