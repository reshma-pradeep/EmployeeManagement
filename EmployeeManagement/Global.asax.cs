using Autofac;
using Autofac.Integration.Mvc;
using BusinessLayer.Services;
using DataAccessLayer.Repository;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmployeeManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            var builder = new ContainerBuilder();
            builder.RegisterFilterProvider();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<LoginService>().As<ILoginService>();
            builder.RegisterType<LoginRepository>().As<ILoginRepository>();

            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            
            var container = builder.Build();
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
