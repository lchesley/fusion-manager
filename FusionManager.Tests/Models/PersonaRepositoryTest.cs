using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FusionManager.Models;
using FusionManager.Tests.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace FusionManager.Tests.Models
{
    [TestClass]
    public class PersonaRepositoryTest
    {
        IPersonaRepository repository;

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {
            repository = new MockPersonaRepository();
        }

        [TestMethod]
        public void GetRepositoryPersonaList()
        {
            //Arrange                            
            
            //Act
            var result = repository.GetPersonaList();

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreNotNull(result.ToList<Persona>());
            CollectionAssert.AllItemsAreInstancesOfType(result.ToList<Persona>(), typeof(Persona));
            CollectionAssert.AllItemsAreUnique(result.ToList<Persona>());
        }
    }
}
