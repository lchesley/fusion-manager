﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FusionManager;
using FusionManager.Controllers;
using FusionManager.Tests.Mocks;

namespace FusionManager.Tests.Controllers
{
    [TestClass]
    public class PersonaControllerTest
    {
        MockPersonaRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new MockPersonaRepository();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            PersonaController controller = new PersonaController(repository);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }        
    }
}
