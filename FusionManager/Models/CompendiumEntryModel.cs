using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FusionManager.Models
{
    public class CompendiumEntryModel
    {
        public CompendiumEntry Entry { get; set; }
        public List<Skill> InheritedSkills { get; set; }
        public List<SelectListItem> PersonaNames { get; set; }
    }
}