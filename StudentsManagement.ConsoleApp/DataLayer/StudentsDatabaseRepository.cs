using StudentsManagement.ConsoleApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.ConsoleApp.DataLayer
{
    public class StudentsDatabaseRepository : IStudentsRepository
    {
        public void Save(Student student)
        {
            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=StudentDB;Integrated Security=true"))
            {
                conn.Open();
                string sqlInsert = $"insert into Students values ('{student.FirstName}','{student.LastName}','{student.DOB}','{student.Email}','{student.Mobile}','{student.Course}','{student.College}')";
                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=StudentDB;Integrated Security=true"))
            {
                conn.Open();
                string sqlGetAll = $"select * from Students";
                SqlCommand cmd = new SqlCommand(sqlGetAll, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { //line 34
                    Student student = new Student
                    {
                        RollNo = (int)reader["RollNo"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Email = reader["Email"].ToString(),
                        Mobile = reader["Mobile"].ToString(),
                        Course = reader["Course"].ToString(),
                        College = reader["College"].ToString()

                    };
                    students.Add(student);
                }
                reader.Close();
            }
            if (students.Count == 0)
                throw new EmptyDatabaseException("Database contains no data");
            return students;    
        }
        Student IStudentsRepository.SearchStudentByRollNo(int rollNo)
        {
            Student student = null;
            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=StudentDB;Integrated Security=true"))
            {
                conn.Open();
                string sqlGet = $"select * from Students where RollNo = {rollNo}";
                SqlCommand cmd = new SqlCommand(sqlGet, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    student = new Student
                    {
                        RollNo = (int)reader["RollNo"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Email = reader["Email"].ToString(),
                        Mobile = reader["Mobile"].ToString(),
                        Course = reader["Course"].ToString(),
                        College = reader["College"].ToString()

                    };
                }
                reader.Close();
            }
            if (student == null)
                throw new StudentNotFoundException("No such student in database");
            return student;
            
        }
        void IStudentsRepository.DeleteStudentByRollNo(int rollNo)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=StudentDB;Integrated Security=true"))
            {
                conn.Open();
                string sqlDelete = $"delete from Students where RollNo = {rollNo}";
                SqlCommand cmd = new SqlCommand(sqlDelete, conn);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            if (rowsAffected == 0)
                throw new StudentNotFoundException("No such student in database");
        }
        void IStudentsRepository.UpdateStudent(int rollNo, Student student)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=StudentDB;Integrated Security=true"))
            {
                conn.Open();
                string sqlUpdate = $"UPDATE Students SET FirstName = @FirstName, LastName = @LastName, DOB = @DOB, Email = @Email, Mobile = @Mobile, Course = @Course, College = @College WHERE RollNo = @OriginalRollNo";
                SqlCommand cmd = new SqlCommand(sqlUpdate, conn);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DOB", student.DOB);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Course", student.Course);
                cmd.Parameters.AddWithValue("@College", student.College);
                cmd.Parameters.AddWithValue("@OriginalRollNo", rollNo);

                rowsAffected = cmd.ExecuteNonQuery();
            }
            if (rowsAffected == 0)
                throw new StudentNotFoundException("No such student in database");
        }
    }
}
