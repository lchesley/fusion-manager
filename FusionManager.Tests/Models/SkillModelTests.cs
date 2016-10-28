using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FusionManager.Models;
using System.IO;
using System.Web;
using System.Collections.Generic;

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

        [TestMethod]
        public void GetSkillBySkillName()
        {
            //Arrange                            
            string skillName = "Dia";

            //Act
            var result = model.GetSkillBySkillName(skillName);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Skill));
        }

        [TestMethod]
        public void GetSkillBySkillName_GetActualSkillByName()
        {
            //Arrange                            
            string skillName = "Twin Slash";
            Skill expected = new Skill{
                CanPassDown =true,
                Description = "Deals light Cut damage to one foe (2 hits)",
                Cost = "18 HP",
                Name ="Twin Slash",
                SkillType =SkillInheritanceType.Physical,
                Type ="Cut"};

            //Act
            var result = model.GetSkillBySkillName(skillName);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<Skill>(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetSkillBySkillName_GetActualSkillByInvalidName()
        {
            //Arrange                            
            string skillName = "Invalid";

            //Act
            var result = model.GetSkillBySkillName(skillName);

            //Assert            
        }

        [TestMethod]
        public void GetLearnedSkillsFromSkillList()
        {
            //Arrange                            
            string skillList = "Spotter, Agi, Hama(4), Tarunda(5), Resist Fire(6)";

            //Act
            var result = model.GetLearnedSkillsFromSkillList(skillList);

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(LearnedSkill));
        }

        [TestMethod]
        public void GetLearnedSkillsFromSkillList_GetActualLearnedSkillsFromSkillList()
        {
            //Arrange                            
            string skillList = "Spotter, Agi, Hama(4), Tarunda(5), Resist Fire(6)";

            List<LearnedSkill> expected = new List<LearnedSkill>();
            expected.Add(new LearnedSkill { Skill = model.GetSkillBySkillName("Spotter"), LevelLearned = 1 });
            expected.Add(new LearnedSkill { Skill = model.GetSkillBySkillName("Agi"), LevelLearned = 1 });
            expected.Add(new LearnedSkill { Skill = model.GetSkillBySkillName("Hama"), LevelLearned = 4 });
            expected.Add(new LearnedSkill { Skill = model.GetSkillBySkillName("Tarunda"), LevelLearned = 5 });
            expected.Add(new LearnedSkill { Skill = model.GetSkillBySkillName("Resist Fire"), LevelLearned = 6 });

            //Act
            var result = model.GetLearnedSkillsFromSkillList(skillList);

            //Assert            
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(LearnedSkill));
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AreEqual(expected, result);            
        }

        [TestMethod]
        public void GetInheritableSkillsByInheritanceType()
        {
            //Arrange
            SkillInheritanceType type = SkillInheritanceType.Almighty;

            //Act
            var result = model.GetInheritableSkillsByInheritanceType(type);

            //Assert
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Skill));
        }

        [TestMethod]
        public void GetInheritableSkillsByInheritanceType_GreaterThanZeroResults()
        {
            //Arrange
            SkillInheritanceType type = SkillInheritanceType.Almighty;

            //Act
            var result = model.GetInheritableSkillsByInheritanceType(type);

            //Assert
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Skill));
            CollectionAssert.AllItemsAreUnique(result);
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetInheritableSkillsByInheritanceType_AllSkillsCanPassDown()
        {
            //Arrange
            SkillInheritanceType type = SkillInheritanceType.Almighty;
            bool canPassDown = true;

            //Act
            var result = model.GetInheritableSkillsByInheritanceType(type);

            foreach(Skill skill in result)
            {
                if(!skill.CanPassDown)
                {
                    canPassDown = false;
                }
            }

            //Assert
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Skill));
            CollectionAssert.AllItemsAreUnique(result);
            Assert.AreNotEqual(result.Count, 0);
            Assert.AreEqual(canPassDown, true);            
        }
    }
}
