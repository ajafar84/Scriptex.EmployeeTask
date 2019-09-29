using Newtonsoft.Json;
using Scriptex.EmployeeTask.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Xml;
using System.Web.Http.Cors;

namespace Scriptex.EmployeeTask.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();

            // Remove XML Formatter (Prevent Web Api from returning XML data)
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Configure JSON Serializer Settings
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();
            
            //Global Exception Handler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}
