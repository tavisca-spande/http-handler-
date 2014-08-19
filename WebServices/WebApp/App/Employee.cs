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
    [Serializable]
    public class Employee
    {
        public string Name 
        {
            get 
            {
                return this.FirstName ?? string.Empty + " " +
                    this.LastName ?? string.Empty;
            }
            set { }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Id { get; set; }
    }

    
}