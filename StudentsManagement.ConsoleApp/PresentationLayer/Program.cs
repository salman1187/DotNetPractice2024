using StudentsManagement.ConsoleApp.DataLayer;
using StudentsManagement.ConsoleApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("-----Student Manager-----");
                Console.WriteLine("1. Save Student");
                Console.WriteLine("2. Get All Student");
                Console.WriteLine("3. Get Student by RollNo");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice [1-6]: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        SaveStudent();
                        break;
                    case 2:
                        try
                        {
                            GeAllStudents();
                        } 
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            SearchStudentByRollNo();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 4:
                        try
                        {
                            UpdateStudent();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 5:
                        try
                        {
                            DeleteStudent();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid Option"); break;
                }
            }
        }
        public static void SaveStudent()
        {
            Student s = new Student();
            Console.Write("First Name: ");
            s.FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            s.LastName = Console.ReadLine();
            Console.Write("DOB: ");
            s.DOB = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Email: ");
            s.Email = Console.ReadLine();
            Console.Write("Mobile: ");
            s.Mobile = Console.ReadLine();
            Console.Write("Course: ");
            s.Course = Console.ReadLine();
            Console.Write("College: ");
            s.College = Console.ReadLine();

            IStudentsRepository repo = new StudentsDatabaseRepository();
            repo.Save(s);
            Console.WriteLine("Student saved succesfully");
        }
        public static void GeAllStudents()
        {
            IStudentsRepository repo = new StudentsDatabaseRepository();
            List<Student> students = repo.GetAllStudents();
            Console.WriteLine("All students are: ");
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.RollNo}. {student.FirstName} {student.LastName}");
            }
        }
        public static void SearchStudentByRollNo()
        {
            IStudentsRepository repo = new StudentsDatabaseRepository();
            Console.WriteLine("Enter RollNo to search: ");
            int r = int.Parse( Console.ReadLine() );    
            Student student = repo.SearchStudentByRollNo(r);
            Console.WriteLine("Student is: ");
            Console.WriteLine($"{student.RollNo}. {student.FirstName} {student.LastName}");
        }
        public static void UpdateStudent()
        {
            Console.WriteLine("Enter RollNo to update: ");
            int r = int.Parse(Console.ReadLine());
            Student s = new Student();
            Console.Write("First Name: ");
            s.FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            s.LastName = Console.ReadLine();
            Console.Write("DOB: ");
            s.DOB = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Email: ");
            s.Email = Console.ReadLine();
            Console.Write("Mobile: ");
            s.Mobile = Console.ReadLine();
            Console.Write("Course: ");
            s.Course = Console.ReadLine();
            Console.Write("College: ");
            s.College = Console.ReadLine();

            IStudentsRepository repo = new StudentsDatabaseRepository();
            repo.UpdateStudent(r, s);
            Console.WriteLine("Student edited succesfully");
        }
        public static void DeleteStudent()
        {
            IStudentsRepository repo = new StudentsDatabaseRepository();
            Console.WriteLine("Enter RollNo to delete: ");
            int r = int.Parse(Console.ReadLine());
            repo.DeleteStudentByRollNo(r);
            Console.WriteLine("Student deleted succesfully");
        }
    }
}
