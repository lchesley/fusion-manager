using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FusionManager.Models;
using System.IO;
using System.Web;
using System.Collections.Generic;

namespace FusionManager.Tests.Models
{
    [TestClass]
    public class PersonaModelTests
    {
        IPersonaModel model;
        ISkillModel skillModel;
        IInheritanceModel inheritanceModel;

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {
            StreamReader reader = new StreamReader("App_Data\\FusionGuide.csv");
            StreamReader skillsReader = new StreamReader("App_Data\\SkillList.csv");
            skillModel = new SkillModel(skillsReader);
            inheritanceModel = new InheritanceModel();
            model = new PersonaModel(reader, skillModel, inheritanceModel);
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

        [TestMethod]
        public void GetPersonaList_GetListByArcana()
        {
            //Arrange                
            Arcana arcana = Arcana.Fortune;
            List<Persona> expected = new List<Persona>();
            expected.Add(model.GetPersonaByPersonaName("Empusa"));
            expected.Add(model.GetPersonaByPersonaName("Fortuna"));
            expected.Add(model.GetPersonaByPersonaName("Clotho"));
            expected.Add(model.GetPersonaByPersonaName("Lachesis"));
            expected.Add(model.GetPersonaByPersonaName("Ananta"));
            expected.Add(model.GetPersonaByPersonaName("Atropos"));
            expected.Add(model.GetPersonaByPersonaName("Norn"));

            //Act
            var result = model.GetPersonaList(arcana);

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Persona));
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPersonaByPersonaName()
        {
            //Arrange                
            string name = "Ukobach";

            //Act
            var result = model.GetPersonaByPersonaName(name);

            //Assert            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }

        [TestMethod]
        public void GetPersonaByPersonaName_GetActualPersonaByPersonaName()
        {            
            //Arrange                
            string name = "Ukobach";
            Persona expected = new Persona
            {
                Arcana = Arcana.Devil,
                ExtractedSkill = skillModel.GetSkillBySkillName("Panic Circle"),
                HPIncrease = 22,
                InheritableSkillTypes = inheritanceModel.GetSkillInheritanceByPersonaInheritanceType(PersonaInheritanceType.Any),
                InheritanceType = PersonaInheritanceType.Any,
                InitialLevel = 4,
                IsDownloadedContent = false,
                LearnedSkills = skillModel.GetLearnedSkillsFromSkillList("Pulinpa, Agi, Lullaby Song(5), Panic Circle(6), Evil Touch(7)"),
                Name = "Ukobach",
                SPIncrease = 14
            };

            //Act
            var result = model.GetPersonaByPersonaName(name);

            //Assert            
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetPersonaByPersonaName_PersonaDoesNotExist()
        {
            //Arrange                
            string name = "Invalid";

            //Act
            var result = model.GetPersonaByPersonaName(name);

            //Assert                        
        }
    }
}
