using System.Collections.Generic;

namespace FusionManager.Models
{
    public interface IPersonaModel
    {
        List<Persona> GetPersonaList();
        Persona GetPersonaByPersonaName(string name);
    }
}