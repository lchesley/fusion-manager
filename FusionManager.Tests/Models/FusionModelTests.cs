using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FusionManager.Models;
using System.IO;
using System.Collections.Generic;

namespace FusionManager.Tests.Models
{
    [TestClass]
    public class FusionModelTests
    {
        IFusionModel model;
        IPersonaModel personaModel;
        ISkillModel skillModel;
        IInheritanceModel inheritanceModel;
        ICompendiumModel compendiumModel;
        IFusionArcanaModel fusionArcanaModel;

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {
            fusionArcanaModel = new FusionArcanaModel();
            StreamReader fusionReader = new StreamReader("App_Data\\FusionGuide.csv");
            StreamReader skillsReader = new StreamReader("App_Data\\SkillList.csv");
            StreamReader compendiumReader = new StreamReader("App_Data\\Compendium.csv");
            skillModel = new SkillModel(skillsReader);
            inheritanceModel = new InheritanceModel();
            compendiumModel = new CompendiumModel(compendiumReader);
            personaModel = new PersonaModel(fusionReader, skillModel, inheritanceModel, compendiumModel);
            model = new FusionModel(fusionArcanaModel, personaModel);
        }

        [TestMethod]
        public void FusePersonaDouble()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("Ukobach");
            Persona second = personaModel.GetPersonaByPersonaName("Alice");

            //Act
            var result = model.FusePersona(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FusePersonaDouble_PersonaCannotBeFusedToItself()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("Ukobach");
            Persona second = personaModel.GetPersonaByPersonaName("Ukobach");

            //Act
            var result = model.FusePersona(first, second);

            //Assert
            
        }

        [TestMethod]
        public void FusePersonaDouble_CheckSpecialDoubleFusionReturned()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("Belial");
            Persona second = personaModel.GetPersonaByPersonaName("Nebiros");
            Persona expected = personaModel.GetPersonaByPersonaName("Alice");

            //Act
            var result = model.FusePersona(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }

        [TestMethod]
        public void FusePersonaDouble_CheckSameArcanaFusion()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("Matador");
            Persona second = personaModel.GetPersonaByPersonaName("Turdak");

            Persona expected = personaModel.GetPersonaByPersonaName("Mokoi");

            //Act
            var result = model.FusePersona(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }

        [TestMethod]
        public void FusePersonaDouble_CheckDifferentArcanaFusion()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByPersonaName("Neko Shogun");
            Persona second = personaModel.GetPersonaByPersonaName("Hua Po");

            Persona expected = personaModel.GetPersonaByPersonaName("Turdak");

            //Act
            var result = model.FusePersona(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }

        [TestMethod]
        public void FusePersonaDouble_CheckDifferentArcanaFusion_LowLevel()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByPersonaName("Anzu");
            Persona second = personaModel.GetPersonaByPersonaName("Pixie");

            Persona expected = personaModel.GetPersonaByPersonaName("Sandman");

            //Act
            var result = model.FusePersona(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }

        [TestMethod]
        public void FusePersonaTriple()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("Ukobach");
            Persona second = personaModel.GetPersonaByPersonaName("Alice");
            Persona third = personaModel.GetPersonaByPersonaName("Pixie");

            //Act
            var result = model.FusePersona(first, second, third);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }        

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FusePersonaTriple_PersonaCannotBeFusedToItself()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("Ukobach");
            Persona second = personaModel.GetPersonaByPersonaName("Ukobach");
            Persona third = personaModel.GetPersonaByPersonaName("Pixie");

            //Act
            var result = model.FusePersona(first, second, third);

            //Assert

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FusePersonaTriple_PersonaCannotBeFusedToItself_AfterInitialStep()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("Neko Shogun");
            Persona second = personaModel.GetPersonaByPersonaName("Hua Po");
            Persona third = personaModel.GetPersonaByPersonaName("Turdak");

            //Act
            var result = model.FusePersona(first, second, third);

            //Assert

        }

        [TestMethod]
        public void FusePersonaTriple_CheckSpecialTripleFusionReturned()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("White Rider");
            Persona second = personaModel.GetPersonaByPersonaName("Black Rider");
            Persona third = personaModel.GetPersonaByPersonaName("Red Rider");
            Persona expected = personaModel.GetPersonaByPersonaName("Pale Rider");

            //Act
            var result = model.FusePersona(first, second, third);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }

        [TestMethod]
        public void FusePersonaTriple_CheckSameArcanaFusion()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByPersonaName("Nata Taishi");
            Persona second = personaModel.GetPersonaByPersonaName("Chimera");
            Persona third = personaModel.GetPersonaByPersonaName("Eligor");

            Persona expected = personaModel.GetPersonaByPersonaName("Ares");

            //Act
            var result = model.FusePersona(first, second, third);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }

        [TestMethod]
        public void FusePersonaTriple_CheckDifferentArcanaFusion()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByPersonaName("Nue");
            Persona second = personaModel.GetPersonaByPersonaName("Eligor");
            Persona third = personaModel.GetPersonaByPersonaName("Turdak");

            Persona expected = personaModel.GetPersonaByPersonaName("Mithra");

            //Act
            var result = model.FusePersona(first, second, third);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(Persona));
        }

        [TestMethod]
        public void FusionSearch_ByArcana()
        {
            //Arrange
            Arcana desiredArcana = Arcana.Fool;

            //Act
            var result = model.FusionSearch(desiredArcana);

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Tuple<Arcana,Arcana>));
        }

        [TestMethod]
        public void GetInheritableSkillsDouble()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByPersonaName("Ukobach");
            Persona second = personaModel.GetPersonaByPersonaName("Angel");
            Persona resulting = model.FusePersona(first, second);            

            //Act
            var result = model.GetInheritableSkills(resulting, first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Item1, typeof(int));
            Assert.AreNotEqual(0, result.Item1);
            CollectionAssert.AllItemsAreInstancesOfType(result.Item2, typeof(Skill));
            CollectionAssert.AllItemsAreNotNull(result.Item2);
            CollectionAssert.AllItemsAreUnique(result.Item2);
        }

        [TestMethod]
        public void GetInheritableSkillsDouble_CheckResult()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("Anzu");
            Persona second = personaModel.GetPersonaByPersonaName("Pixie");
            Persona resulting = personaModel.GetPersonaByPersonaName("Sandman");
            List<Skill> expected = new List<Skill>();
            expected.Add(skillModel.GetSkillBySkillName("Dia"));
            expected.Add(skillModel.GetSkillBySkillName("Zio"));
            expected.Add(skillModel.GetSkillBySkillName("Mutudi"));
            expected.Add(skillModel.GetSkillBySkillName("Garu"));

            //Act
            var result = model.GetInheritableSkills(resulting, first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Item1, typeof(int));
            Assert.AreNotEqual(0, result.Item1);
            CollectionAssert.AllItemsAreInstancesOfType(result.Item2, typeof(Skill));
            CollectionAssert.AllItemsAreNotNull(result.Item2);
            CollectionAssert.AllItemsAreUnique(result.Item2);
            CollectionAssert.AreEquivalent(expected, result.Item2);
        }

        [TestMethod]
        public void GetInheritableSkillsDouble_ExcludedSkill()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("Belial");
            first.ActualLevel = 54;
            Persona second = personaModel.GetPersonaByPersonaName("Nebiros");
            Persona resulting = personaModel.GetPersonaByPersonaName("Alice");
            Skill excluded = skillModel.GetSkillBySkillName("Revenge Blow");            

            //Act
            var result = model.GetInheritableSkills(resulting, first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Item1, typeof(int));
            Assert.AreNotEqual(0, result.Item1);
            CollectionAssert.AllItemsAreInstancesOfType(result.Item2, typeof(Skill));
            CollectionAssert.AllItemsAreNotNull(result.Item2);
            CollectionAssert.AllItemsAreUnique(result.Item2);
            CollectionAssert.DoesNotContain(result.Item2, excluded);
        }

        [TestMethod]
        public void GetInheritableSkillsDouble_ExcludeDoubles()
        {
            //Arrange
            Persona first = personaModel.GetPersonaByPersonaName("Anzu");            
            Persona second = personaModel.GetPersonaByPersonaName("Angel");
            Persona resulting = model.FusePersona(first, second);

            //Act
            var result = model.GetInheritableSkills(resulting, first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Item1, typeof(int));
            Assert.AreNotEqual(0, result.Item1);
            CollectionAssert.AllItemsAreInstancesOfType(result.Item2, typeof(Skill));
            CollectionAssert.AllItemsAreNotNull(result.Item2);
            CollectionAssert.AllItemsAreUnique(result.Item2);            
        }

        [TestMethod]
        public void GetInheritableSkillsTriple()
        {
            //Arrange            
            Persona first = personaModel.GetPersonaByPersonaName("Ukobach");
            Persona second = personaModel.GetPersonaByPersonaName("Angel");
            Persona third = personaModel.GetPersonaByPersonaName("Agathion");
            Persona resulting = model.FusePersona(first, second);            

            //Act
            var result = model.GetInheritableSkills(resulting, first, second, third);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Item1);
            Assert.IsInstanceOfType(result.Item1, typeof(int));
            CollectionAssert.AllItemsAreInstancesOfType(result.Item2, typeof(Skill));
            CollectionAssert.AllItemsAreNotNull(result.Item2);
            CollectionAssert.AllItemsAreUnique(result.Item2);
        }

        [TestMethod]
        public void FusionSearch_ByArcana_CheckActualResult()
        {
            //Arrange
            Arcana desiredArcana = Arcana.Death;
            int expectedResults = 20;

            //Act
            var result = model.FusionSearch(desiredArcana);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResults, result.Count);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Tuple<Arcana,Arcana>));
        }
    }
}
