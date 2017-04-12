using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace AntDemoWeb.Common
{
    public class AutofacHelper
    {
        public static void Inject()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assembly).Where(t => IsOk(t)).InstancePerLifetimeScope();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }

        private static bool IsOk(Type t)
        {
            return !t.IsAbstract &&
                (t.Name.EndsWith("Service") || t.Name.EndsWith("Repository") || typeof(Controller).IsAssignableFrom(t));

            //|| 
        }

    }
}