using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FusionManager.Models;
using System.IO;
using System.Web;

namespace FusionManager.Tests.Models
{
    [TestClass]
    public class PersonaModelTests
    {
        PersonaModel model;

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {
            StreamReader reader = new StreamReader("App_Data\\FusionGuide.csv");
            model = new PersonaModel(reader);
        }

        [TestMethod]
        public void GetPersonaList()
        {
            //Arrange                

            //Act
            var result = model.GetPersonaList();

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreNotNull(result);
        }

        [TestMethod]
        public void GetPersonaList_CountGreaterThanZero()
        {
            //Arrange                
            int count = 0;

            //Act
            var result = model.GetPersonaList();

            //Assert
            Assert.AreNotEqual(count, result.Count);
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Persona));
            CollectionAssert.AllItemsAreUnique(result);
        }
    }
}
