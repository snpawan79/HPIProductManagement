using System.Web.Http;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Unity.Mvc3;
using HPI.BusinessServices;
using HPI.DataAccessLayer.UnitOfWork;
using HPI.Resolver;

namespace HPI.WebAPI
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

            //Component initialization via MEF
            ComponentLoader.LoadContainer(container, ".\\bin", "HPI.WebAPI.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "HPI.BusinessServices.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "HPI.DataAccessLayer.dll");


        }
    }
}