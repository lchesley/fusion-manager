using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FusionManager.Models;
using System.Collections.Generic;

namespace FusionManager.Tests.Models
{
    [TestClass]
    public class CompendiumModelTests
    {
        ICompendiumModel compendiumModel;

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {            
            StreamReader compendiumReader = new StreamReader("App_Data\\Compendium.csv");         
            compendiumModel = new CompendiumModel(compendiumReader);            
        }

        [TestMethod]
        public void UpdateCompendium()
        {
            //Arrange
            CompendiumEntry entry = new CompendiumEntry{ ActualLevel = 1, InheritedSkills="Endure, Poisma", PersonaName = "Slime" };
            StreamWriter writer = new StreamWriter("App_Data\\Compendium.csv", false);
            var expected = new List<CompendiumEntry>();
            expected.Add(entry);

            //Act
            compendiumModel.UpdateCompendium(entry, writer);
            var result = compendiumModel.GetCompendium();

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(CompendiumEntry));
            CollectionAssert.AreEqual(expected, result);
        }        

        [TestMethod]
        public void GetCompendiumEntryByPersonaName()
        {
            //Arrange
            CompendiumEntry entry = new CompendiumEntry { ActualLevel = 1, InheritedSkills = "Endure, Poisma", PersonaName = "Slime" };
            StreamWriter writer = new StreamWriter("App_Data\\Compendium.csv", false);
            compendiumModel.UpdateCompendium(entry, writer);
            var expected = entry;

            //Act            
            var result = compendiumModel.GetCompendiumEntryByPersonaName("Slime");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(CompendiumEntry));                        
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetCompendiumEntryByPersonaName_EntryDoesNotExist()
        {
            //Arrange
            CompendiumEntry entry = new CompendiumEntry { ActualLevel = 1, InheritedSkills = "Endure, Poisma", PersonaName = "Slime" };
            StreamWriter writer = new StreamWriter("App_Data\\Compendium.csv", false);
            compendiumModel.UpdateCompendium(entry, writer);
            var expected = entry;

            //Act            
            var result = compendiumModel.GetCompendiumEntryByPersonaName("Angel");

            //Assert           
        }
    }
}
