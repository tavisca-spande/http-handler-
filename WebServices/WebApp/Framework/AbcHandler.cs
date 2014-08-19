using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApp
{
    public class AbcHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true;  }
        }

        public void ProcessRequest(HttpContext context)
        {
            // Get file contents
            var fileContent = File.ReadAllText(context.Request.PhysicalPath);
            // Get the page type.
            var type = GetPageType(fileContent);
            // Create page 
            var page = CreatePage(type, context, fileContent);
            // Execute page.
            page.Process();
        }

        private AbcPage CreatePage(Type type, HttpContext context, string content)
        {
            var page = Activator.CreateInstance(type) as AbcPage;
            page.Context = context;
            page.View = new AbcView(content);
            return page;
        }

        private Type GetPageType(string content)
        {
            // Page directive format : ^!$& page=WebApp.EmployeeDetails
            var directive = content.Split(new string[] {"\r\n" }, StringSplitOptions.RemoveEmptyEntries).First();
            var typeName = directive
                                .Replace("^!$&", string.Empty)
                                .Replace("page=", string.Empty)
                                .TrimStart(' ')
                                .TrimEnd(' ');
            return Type.GetType(typeName);
        }
    }
}