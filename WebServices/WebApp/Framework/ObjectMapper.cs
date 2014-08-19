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
    
    public class ObjectMapper
    {
        public ObjectMapper(object o)
        {
            this.Target = o;
        }

        public string GetValue(string field)
        {
            var type = this.Target.GetType();
            var matchingProperty = type.GetProperties()
                                       .Where(p => field.Equals(p.Name, StringComparison.OrdinalIgnoreCase) == true)
                                       .SingleOrDefault();
            if (matchingProperty == null)
                return string.Empty;
            var value = matchingProperty.GetValue(this.Target);
            return value == null ? string.Empty : value.ToString();
        }

        public object Target { get; set; }
    }
}