using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System;
using FusionManager.Models;
using FusionManager.Controllers;

namespace FusionManager
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IPersonaRepository, PersonaRepository>();
            //container.RegisterType<IController, HomeController>("Home");
            return container;
        }
    }
}
