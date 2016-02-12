using System;
using System.Collections.Generic;

namespace FusionManager.Models
{
    public interface IPersonaModel
    {
        List<Persona> GetPersonaList();
        List<Persona> GetPersonaList(Arcana arcana);
        Persona GetPersonaByPersonaName(string name);
        Tuple<Persona, Persona> GetNextLowestAndNextHighestPersonaByArcana(Arcana arcana, double targetLevel);
    }
}