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
    public interface IAbcView
    {
        void SetData(object data);

        string Render();
    }

    public class AbcView : IAbcView
    {
        public AbcView(string template)
        {
            this.Mapper = null;
            this.Template = template;
        }

        public void SetData(object data)
        {
            this.Mapper = new ObjectMapper(data);
        }

        public string Render()
        {
            var lines = this.Template.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                            .Skip(1)
                            .ToList();
            lines = lines.ConvertAll(x => ReplacePlaceHolders(x));
            var buffer = new StringBuilder();
            buffer
                .Append("<html>")
                .Append("<head>")
                .Append("</head>")
                .Append("<body>")
                .Append( GetBody(lines) )
                .Append("</body>")
                .Append("</html>");
            return buffer.ToString();
        }

        private string ReplacePlaceHolders(string text)
        {
            var matches = Regex.Matches(text, @"\{[a-zA-Z]+\}", RegexOptions.Compiled);
            if (matches.Count == 0) return text;
            foreach( Match match in matches )
            {
                var placeHolder = match.Value;
                var value = this.Mapper.GetValue(placeHolder.TrimStart('{').TrimEnd('}'));
                text = text.Replace(placeHolder, value);
            }
            return text;
        }

        private string GetBody(IEnumerable<string> lines)
        {
            var buffer = new StringBuilder();
            foreach (var line in lines)
            {
                if (line.StartsWith("#") == true)
                    buffer.Append(GetHContent(line));
                else if (string.IsNullOrWhiteSpace(line) == true )
                    buffer.Append(GetLineBreak());
            }
            return buffer.ToString();
        }

        private string GetLineBreak()
        {
            return "<br />";
        }

        private string GetHContent(string line)
        {
            var content = line.Replace("#", string.Empty);
            var hashCount = line.Length - content.Length;
            return string.Format("<h{0}>{1}</h{0}>",
                hashCount,
                content.TrimStart(' '));
        }

        public ObjectMapper Mapper { get; private set; }
        public string Template { get; set; }
    }

}