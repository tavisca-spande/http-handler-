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
    public class EmployeeDb
    {
        public Employee GetEmployee(string id)
        {
            return new Employee
            {
                Id = id,
                LastName = "Doe",
                FirstName = "John"
            };
        }
    }

}