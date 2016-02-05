using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FusionManager.Models;
using System.IO;
using System.Web;


namespace FusionManager.Tests.Models
{
    [TestClass]
    public class SkillModelTests
    {
        SkillModel model;

        [TestInitialize]
        [DeploymentItem(@"App_Data\", @"App_Data\")]
        public void Initialize()
        {
            StreamReader reader = new StreamReader("App_Data\\SkillList.csv");
            model = new SkillModel(reader);
        }

        [TestMethod]
        public void GetSkillList()
        {
            //Arrange                

            //Act
            var result = model.GetSkillList();

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreNotNull(result);
        }

        [TestMethod]
        public void GetSkillList_CountGreaterThanZero()
        {
            //Arrange                
            int count = 0;

            //Act
            var result = model.GetSkillList();

            //Assert
            Assert.AreNotEqual(count, result.Count);
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Skill));
            CollectionAssert.AllItemsAreUnique(result);
        }
    }
}
