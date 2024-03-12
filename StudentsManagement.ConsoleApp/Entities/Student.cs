using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.ConsoleApp.Entities
{
    public class Student
    {
        public int RollNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Course { get; set; }
        public string College { get; set; }
    }
    public class StudentNotFoundException : ApplicationException
    {
        public StudentNotFoundException(string msg) : base(msg) { }
    }
    public class EmptyDatabaseException : ApplicationException
    {
        public EmptyDatabaseException(string msg) : base(msg) { }
    }
}
