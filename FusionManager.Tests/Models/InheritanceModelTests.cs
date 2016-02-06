using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FusionManager.Models;
using System.Collections.Generic;

namespace FusionManager.Tests.Models
{
    [TestClass]
    public class InheritanceModelTests
    {
        IInheritanceModel model;

        [TestInitialize]
        public void Initialize()
        {
            model = new InheritanceModel();
        }

        [TestMethod]
        public void GetSkillInheritanceByPersonaInheritanceType()
        {
            //Arrange  
            PersonaInheritanceType type = PersonaInheritanceType.Dark_A;

            //Act
            var result = model.GetSkillInheritanceByPersonaInheritanceType(type);

            //Assert
            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(SkillInheritance));
        }

        [TestMethod]
        public void GetSkillInheritanceByPersonaInheritanceType_CountGreaterThanZero()
        {
            //Arrange  
            int actualCount = 0;
            PersonaInheritanceType type = PersonaInheritanceType.Dark_A;

            //Act
            var result = model.GetSkillInheritanceByPersonaInheritanceType(type);

            //Assert
            Assert.AreNotEqual(result.Count, actualCount);
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AllItemsAreUnique(result);
        }

        [TestMethod]
        public void GetSkillInheritanceByPersonaInheritanceType_CheckReturnedValuesAreCorrect()
        {
            //Arrange              
            PersonaInheritanceType type = PersonaInheritanceType.Dark_A;

            List<SkillInheritance> expected = new List<SkillInheritance>();
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Physical });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Fire });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Ice });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Electricity });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Wind });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Dark });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Almighty });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Status });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Recovery });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Support });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Passive });
            expected.Add(new SkillInheritance { CanInherit = true, Type = SkillInheritanceType.Navigator });

            List<SkillInheritance> resultSkillInheritance = new List<SkillInheritance>();

            //Act
            resultSkillInheritance = model.GetSkillInheritanceByPersonaInheritanceType(type);

            //Assert
            CollectionAssert.AreEqual(expected, resultSkillInheritance);
        }
    }
}
