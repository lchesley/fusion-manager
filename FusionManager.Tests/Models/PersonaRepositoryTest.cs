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

        [TestMethod]
        public void ConvertFromBase64ToName()
        {
            //Arrange
            string expected = "Jack Frost";
            string base64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(expected));

            //Act
            var result = repository.ConvertToStringFromBase64(base64);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(string));
        }
    }
}
