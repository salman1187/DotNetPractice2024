using StudentsManagement.ConsoleApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.ConsoleApp.DataLayer
{
    public interface IStudentsRepository
    {
        void Save(Student student);
        List<Student> GetAllStudents();
        Student SearchStudentByRollNo(int rollNo);
        void DeleteStudentByRollNo(int rollNo);
        void UpdateStudent(int rollNo, Student student);
    }
}
