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
    public class EmployeeDetails : AbcPage
    {
        protected override void PageLoad()
        {
            var employeeId = this.QueryString["id"] ?? "0";
            var data = new EmployeeDb().GetEmployee(employeeId);
            this.View.SetData(data);
        }
    }

}