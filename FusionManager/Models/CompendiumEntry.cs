using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FusionManager.Models
{
    public class CompendiumEntry
    {
        public string PersonaName { get; set; }
        public int ActualLevel { get; set; }
        public string InheritedSkills { get; set; }

        public override bool Equals(object obj)
        {
            CompendiumEntry item = obj as CompendiumEntry;
            if (item == null)
            {
                return false;
            }
            else
            {
                return PersonaName.Equals(item.PersonaName) && ActualLevel.Equals(item.ActualLevel) && InheritedSkills.Equals(item.InheritedSkills);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}