using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPCompanyLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Company c = new Company();

            Employee e1 = new Employee();
            Employee e2 = new Employee();
            e1.Basic = 50000;
            e1.Experience = 1;
            e1.EmpId = 101;
            e2.Basic = 80000;
            e2.Experience = 3;
            e2.EmpId = 102;

            Customer c1 = new Customer();
            Customer c2 = new Customer();
            Customer c3 = new Customer();

            c.Employees.Add(e1);
            c.Employees.Add(e2);
            c.Customers.Add(c1);
            c.Customers.Add(c2);    
            c.Customers.Add(c3);


            if(c.GetEmployee(102) != null)
                Console.WriteLine($"Employee {102} has salary of {c.GetEmployee(102).GetSalary()}");
            else
                Console.WriteLine("Employee doesn't exist");

            Console.WriteLine($"total salaries to pay: {c.GetTotalSalaryPayout()}");
            Console.WriteLine($"total employees: {c.GetTotalEmployees()}");
            Console.WriteLine($"total customers: {c.GetTotalCustomers()}");
        }
    }
    class Company
    {
        public string Name { get; set; }
        public DateTime IncorporatedDt { get; set; }
        public Branch CorporateOffice { get; set; }
        public Branch RegisteredOffice { get; set; }
        public List<Branch> Branches { get; set; } = new List<Branch>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public Employee GetEmployee(int empid)
        {
            foreach (Employee e in Employees)
                if (e.EmpId == empid)
                    return e;

            return null;
        }
        public double GetTotalSalaryPayout()
        {
            double total = 0;
            foreach (Employee e in Employees)
                total += e.GetSalary();
            return total;
        }
        public int GetTotalCustomers()
        {
            return Customers.Count;
        }
        public int GetTotalEmployees()
        {
            return Employees.Count;
        }

    }
    class Branch
    {

    }
    class Employee : Person
    {
        public int EmpId { get; set; }
        public double Basic { get; set; }
        public double Experience { get; set; }
        public double GetSalary()
        {
            return SalaryCalculator.CalculateSalary(Experience, Basic) + Basic;
        }


    }
    class Customer : Person 
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
    }
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
    class SalaryCalculator
    {
        public static double CalculateSalary(double exp, double bas)
        {
            if (exp <= 2)
                return bas * 0.3;
            else if (exp <= 4)
                return bas * 0.4;
            else if (exp <= 6)
                return bas * 0.5;
            else
                return bas * 0.65;
        }
    }
}
