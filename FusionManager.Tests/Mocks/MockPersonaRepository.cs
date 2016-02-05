using FusionManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Tests.Mocks
{
    public class MockPersonaRepository : IPersonaRepository
    {
        public IEnumerable<Persona> GetPersonaList()
        {
            throw new NotImplementedException();
        }
    }
}
