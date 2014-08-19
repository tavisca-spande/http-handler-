using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;

namespace WebApp.Framework
{
    public class AbcServiceHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var type = GetServiceType(context);
            var method = GetMethod(context, type);
            InvokeMethod(type, method, context);
        }

        private void InvokeMethod(Type type, MethodInfo method, HttpContext context)
        {
            using (var reader = new StreamReader(context.Request.InputStream))
            {
                var xml = reader.ReadToEnd();
                var parameter = method.GetParameters().Single();
                var value = ParseXml(xml, parameter.ParameterType);
                var service = Activator.CreateInstance(type);
                method.Invoke(service, new[] { value });
            }
        }

        private object ParseXml(string xml, Type type)
        {
            using( var reader = new StringReader(xml) )
            {
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(reader);
            }
        }

        private MethodInfo GetMethod(HttpContext context, Type serviceType)
        {
            var method = context.Request.QueryString["method"];
            return serviceType
                        .GetMethods()
                        .Where(m => m.Name.Equals(method))
                        .Where(m => m.GetParameters().Length == 1)
                        .SingleOrDefault();
        }

        private Type GetServiceType(HttpContext context)
        {
            var typeName = File.ReadAllText(context.Request.PhysicalPath);
            return Type.GetType(typeName);
        }
    }
}