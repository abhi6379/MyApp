using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using WebApiApp.Models;

namespace WebApiApp
{
    public static class WebApiConfig
    {
        public static object EnableCors { get; private set; }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new BasicAuthentication());
            
            //Output Mediaformatter Json 
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //Output Mediaformatter XML 
            ///var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            ///xml.UseXmlSerializer = true;
            ///xml.Indent = true;

            // Remove the JSON formatter
            ///config.Formatters.Remove(config.Formatters.JsonFormatter);

            // or

            // Remove the XML formatter
            ///config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.EnableCors();
        }
    }
}
