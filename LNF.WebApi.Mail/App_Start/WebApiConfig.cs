using LNF.Impl.DependencyInjection;
using LNF.Web;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;

namespace LNF.WebApi.Mail
{
    public static class WebApiConfig
    {
        public static WebApp WebApp { get; private set; }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            WebApp = new WebApp();

            var wcc = new WebContainerConfiguration(WebApp.Context);
            wcc.RegisterAllTypes();

            // setup web dependency injection
            Assembly[] assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
            WebApp.BootstrapMvc(assemblies);

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(WebApp.GetContainer());

            WebApi.WebApiConfig.Register(config);
        }
    }
}
