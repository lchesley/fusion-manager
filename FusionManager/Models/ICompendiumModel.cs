using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FusionManager.Models
{
    public interface ICompendiumModel
    {
        List<CompendiumEntry> GetCompendium();
        CompendiumEntry GetCompendiumEntryByPersonaName(string name);
    }
}