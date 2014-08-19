using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebApp;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Employee));
            serializer.Serialize(Console.Out, new Employee { FirstName = "John", LastName = "Doe", Id = "10" });
            Console.ReadKey(true);
        }
    }
}
