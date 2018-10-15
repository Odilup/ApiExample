using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using InnoCVApi.API.Configuration;
using InnoCVApi.API.Endpoint.ActionFilters;
using InnoCVApi.API.Endpoint.Middleware;
using InnoCVApi.Core.Common.ModelMapper;
using InnoCVApi.Core.Logging;
using InnoCVApi.Data.DbContext;
using InnoCVApi.Domain.Entities.Users;
using InnoCVApi.Domain.Repositories;
using InnoCVApi.Domain.Repositories.Interfaces;
using log4net.Config;
using Owin;
using Swashbuckle.Application;

namespace InnoCVApi.API.Endpoint
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "swagger_root",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(message => message.RequestUri.ToString(), "swagger")
            );

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional});

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Load log4net settings
            var log4NetFilePath = HostingEnvironment.MapPath($"~/log4net.config");

            XmlConfigurator.Configure(new FileInfo(log4NetFilePath));

            var logger = new Log4NetLogger();
            builder.RegisterInstance(logger).As<ILogger>().SingleInstance();

            //Load application settings
            var appConfiguration = new AppConfiguration();

            var filePath = HostingEnvironment.MapPath($"~/Configuration/app.config.json");
            appConfiguration.LoadFile(filePath);

            builder.RegisterInstance<IAppConfiguration>(appConfiguration).SingleInstance();

            builder.Register(context => new DatabaseContext(appConfiguration.DatabaseConnectionString))
                .As<BaseDbContext>().InstancePerRequest();

            //register repository
            builder.Register(context => new EfGenericRepository<User, int>(context.Resolve<BaseDbContext>()))
                .As<IRepository<User, int>>()
                .InstancePerRequest();

            builder.RegisterType<EfUnitOfWork>().As<IUnitOfWork>()
                .PropertiesAutowired().InstancePerRequest();

            //register Automapper helper class
            builder.RegisterType<ModelMapper>().As<IModelMapper>().SingleInstance();

            //build dependencies container
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //configure formatters
            var jsonFormatter = new JsonMediaTypeFormatter();
            config.Formatters.Clear();
            config.Formatters.Add(jsonFormatter);

            //configure default exception handler
            var exceptionHandler = new DefaultExceptionHandler(container.Resolve<ILogger>());
            config.Services.Replace(typeof(IExceptionHandler), exceptionHandler);

            //configure Swagger
            config.EnableSwagger(c =>
                {
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\bin\";
                    var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                    c.SingleApiVersion("v1", "InnoCV Api example")
                        .Description("Example API exercise")
                        .Contact(contactBuilder => contactBuilder.Name("Javier Pulido")
                            .Email("fjpulido@gmail.com"));

                    c.IncludeXmlComments(commentsFile);
                })
                .EnableSwaggerUi(c =>
                    c.DocExpansion(DocExpansion.List));

            app.Use(typeof(XNonceHandlerMiddleware));

            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }


}