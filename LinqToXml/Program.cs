using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;

namespace LinqToXml
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("XMLFile1.xml");
            Console.WriteLine("1.");
            Console.WriteLine(doc);

            Console.WriteLine("Q2.");
            var EmployeeNames = from emp in doc.Descendants("Employee")
                                select emp.Element("Name").Value;
            foreach (var emp in EmployeeNames)
                Console.WriteLine(emp);

            Console.WriteLine("Q3.");
            var EmployeeIdNames = from emp in doc.Descendants("Employee")
                                  select new {Name =  emp.Element("Name").Value, Id = emp.Element("EmpId").Value };
            foreach (var emp in EmployeeIdNames)
                Console.WriteLine($"{emp.Id}, {emp.Name}");

            Console.WriteLine("Q4.");
            var FemaleEmployees = from emp in doc.Descendants("Employee")
                                  where emp.Element("Sex").Value == "Female"
                                  select emp;
            foreach(var emp in FemaleEmployees)
                Console.WriteLine(emp.Element("Name").Value);

            Console.WriteLine("Q5.");
            var HomePhoneNos = from ph in doc.Descendants("Phone")
                               where (string)ph.Attribute("Type") == "Home"
                               select ph.Value;
            foreach (var ph in HomePhoneNos)
                Console.WriteLine(ph);

            Console.WriteLine("Q6.");
            var AltaEmployees = from emp in doc.Descendants("Employee")
                                where emp.Element("Address").Element("City").Value == "Alta"
                                select emp.Element ("Name").Value;
            foreach(var emp in AltaEmployees)
                Console.WriteLine(emp);

            Console.WriteLine("Q7.");
            var SortyByZipCodes = from emp in doc.Descendants("Employee")
                                  orderby emp.Element("Address").Element("Zip").Value
                                  select emp;

            foreach (var emp in SortyByZipCodes)
                Console.WriteLine(emp);

            Console.WriteLine("Q8.");
            var FirstTwoEmps = (from emp in doc.Descendants("Employee")
                                select emp).Take(2);
            foreach(var emp in FirstTwoEmps)
                Console.WriteLine(emp);

            Console.WriteLine("Q9");
            var CAEmps = (from state in doc.Descendants("State")
                          where state.Value == "CA"
                          select state).Count();
            Console.WriteLine(CAEmps);

            Console.WriteLine("Q10.");
            foreach(var emp in FemaleEmployees)
                Console.WriteLine($"{emp.Element("Name").Value} - {emp.Element("Address").Element("City").Value} - {emp.Element("Sex").Value}");
        }
    }
}
