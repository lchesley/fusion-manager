using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FusionManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace FusionManager.Tests.Models
{
    [TestClass]
    public class FusionArcanaModelTests
    {
        IFusionArcanaModel model;

        [TestInitialize]        
        public void Initialize()
        {            
            model = new FusionArcanaModel();
        }

        [TestMethod]
        public void GetDoubleFusionArcana()
        {
            //Arrange
            Arcana first = Arcana.Chariot;
            Arcana second = Arcana.Death;

            //Act
            var result = model.GetDoubleFusionResultingArcana(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Arcana));
        }

        [TestMethod]
        public void GetDoubleFusionArcana_CheckDoubleFusionResults()
        {
            //Arrange
            Arcana first = Arcana.Fool;
            Arcana second = Arcana.Judgement;
            Arcana expected = Arcana.Star;

            //Act
            var result = model.GetDoubleFusionResultingArcana(first, second);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(Arcana));
        }

        [TestMethod]
        public void GetTripleFusionArcana()
        {
            //Arrange
            Arcana first = Arcana.Chariot;
            Arcana second = Arcana.Death;

            //Act
            var result = model.GetTripleFusionResultingArcana(first, second);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Arcana));
        }

        [TestMethod]
        public void GetTripleFusionArcana_CheckTripleFusionResults()
        {
            //Arrange
            Arcana first = Arcana.Fool;
            Arcana second = Arcana.Judgement;
            Arcana expected = Arcana.Moon;

            //Act
            var result = model.GetTripleFusionResultingArcana(first, second);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.IsInstanceOfType(result, typeof(Arcana));
        }

        [TestMethod]
        public void OnlyAvailableThroughSpecialFusion_CheckIsTrue()
        {
            //Arrange
            string name = "Alice";
            bool expected = true;

            //Act
            var result = model.OnlyAvailableThroughSpecialFusion(name);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OnlyAvailableThroughSpecialFusion_CheckIsFalse()
        {
            //Arrange
            string name = "Pixie";
            bool expected = false;

            //Act
            var result = model.OnlyAvailableThroughSpecialFusion(name);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CombinationResultsInSpecialFusion_CheckIsTrue()
        {
            //Arrange
            string[] names = {"Shiva", "Parvati"};
            bool expected = true;

            //Act
            var result = model.CombinationResultsInSpecialFusion(names);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CombinationResultsInSpecialFusion_CheckIsFalse()
        {
            //Arrange
            string[] names = { "Ukobach", "Pixie", "Agathion" };
            bool expected = false;

            //Act
            var result = model.CombinationResultsInSpecialFusion(names);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CombinationResultsInSpecialFusion_CheckIsFalseForPartialCombination()
        {
            //Arrange
            string[] names = { "White Rider", "Black Rider" };
            bool expected = false;

            //Act
            var result = model.CombinationResultsInSpecialFusion(names);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetSpecialFusionResult_GetCorrectResult()
        {
            //Arrange
            string[] names = { "White Rider", "Black Rider", "Red Rider" };
            string expected = "Pale Rider";

            //Act
            var result = model.GetSpecialFusionResult(names);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSpecialFusionResult_SpecialFusionDoesNotExist()
        {
            //Arrange
            string[] names = { "White Rider", "Black Rider" };            

            //Act
            var result = model.GetSpecialFusionResult(names);

            //Assert            
        }

        [TestMethod]
        public void GetSpecialFusionCombination_GetCorrectResult()
        {
            //Arrange
            string name = "Pale Rider";
            bool matchFound = false;

            //Act
            List<string[]>result = model.GetSpecialFusionCombination(name);

            foreach (string[] item in result)
            {
                if (item.Contains("White Rider") && item.Contains("Black Rider") && item.Contains("Red Rider"))
                {
                    matchFound = true;
                }
            }

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(matchFound);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSpecialFusionCombination_SpecialFusionCombinationDoesNotExist()
        {
            //Arrange
            string name = "Ukobach";

            //Act
            var result = model.GetSpecialFusionCombination(name);

            //Assert            
        }
    }
}
