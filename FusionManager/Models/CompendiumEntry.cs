using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FusionManager.Models
{
    public class CompendiumEntry
    {        
        [Display(Name = "Name")]
        public string PersonaName { get; set; }
        [Display(Name = "Level")]
        public int ActualLevel { get; set; }
        [Display(Name = "Skills")]
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