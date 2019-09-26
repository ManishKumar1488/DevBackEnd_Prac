using DEV_BackendPratical_Services.Repository;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace DEV_BackendPratical
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IServiceRepository, ServiceRepository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}