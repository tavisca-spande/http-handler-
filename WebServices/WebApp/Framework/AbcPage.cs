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
    public abstract class AbcPage 
    {
        public AbcPage()
        {
        }

        public IAbcView View { get; set; }

        protected abstract void PageLoad();

        public NameValueCollection QueryString
        {
            get
            {
                return this.Context.Request.QueryString;
            }
        }

        public void Process()
        {
            this.PageLoad();
            WriteResponse(this.View.Render());
        }

        private void WriteResponse(string contents)
        {
            this.Context.Response.Write(contents);
        }
        
        public HttpContext Context { get; set; }
    }

    
}