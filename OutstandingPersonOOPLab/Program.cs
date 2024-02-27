using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutstandingPersonOOPLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Professor prof1 = new Professor();
            prof1.Name = "Arjun";
            prof1.BooksPublished = 5;
            Professor prof2 = new Professor();
            prof2.Name = "Jimmy";
            prof2.BooksPublished = 3;
            Student stu1 = new Student();
            stu1.Name = "Jordan";
            stu1.Percentage = 70;
            Student stu2 = new Student();
            stu2.Name = "Vijay";
            stu2.Percentage = 90;
            Student stu3 = new Student();
            stu3.Name = "Lana";
            stu3.Percentage = 92;

            IPerson p1 = prof1;
            IPerson p2 = prof2;
            IPerson p3 = stu1;
            IPerson p4 = stu2;
            IPerson p5 = stu3;

            List<IPerson> list = new List<IPerson>();
            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
            list.Add(p4);
            list.Add(p5);

            foreach (IPerson person in list)
            {
                if (person is Professor p)
                    p.Print();
                else if (person is Student stu)
                    stu.Display();
                if (person.IsOutstanding())
                    Console.WriteLine("outstanding");
                else Console.WriteLine("not really");
            }
        }
    }
    interface IPerson
    {
        string Name { get; set; }
        bool IsOutstanding();
    }
    class Professor : IPerson
    {
        public string Name { get; set; }
        public int BooksPublished { get; set; }
        public void Print()
        {
            Console.WriteLine($"name: {Name} and books published: {BooksPublished}");
        }
        public bool IsOutstanding()
        {
            if(BooksPublished > 4) 
                return true;
            return false;
        }
    }
    class Student : IPerson
    {
        public string Name { get; set; }
        public double Percentage { get; set; }
        public void Display()
        {
            Console.WriteLine($"name: {Name} and percentage: {Percentage}");
        }
        public bool IsOutstanding()
        {
            if(Percentage > 85)
                return true;
            return false;
        }
    }
}
