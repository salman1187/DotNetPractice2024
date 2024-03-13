using StudentsManagement.ConsoleApp.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
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
            string provider = ConfigurationManager.ConnectionStrings["defaultConnection"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            IDbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            string sqlInsert = "sp_Insert_Statement"; //for stored procedure!
            //string sqlInsert = $"insert into Students values (@FirstName,@LastName,@DOB,@Email,@Mobile,@Course,@College)";
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure; //for stored procedure!
            cmd.CommandText = sqlInsert;
            cmd.Connection = conn;

            IDbDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@FirstName";
            p1.Value = student.FirstName;
            cmd.Parameters.Add(p1);

            IDbDataParameter p2 = cmd.CreateParameter();
            p2.ParameterName = "@LastName";
            p2.Value = student.LastName;
            cmd.Parameters.Add(p2);

            IDbDataParameter p3 = cmd.CreateParameter();
            p3.ParameterName = "@DOB";
            p3.Value = student.DOB;
            cmd.Parameters.Add(p3);

            IDbDataParameter p4 = cmd.CreateParameter();
            p4.ParameterName = "@Email";
            p4.Value = student.Email;
            cmd.Parameters.Add(p4);

            IDbDataParameter p5 = cmd.CreateParameter();
            p5.ParameterName = "@Mobile";
            p5.Value = student.Mobile;
            cmd.Parameters.Add(p5);

            IDbDataParameter p6 = cmd.CreateParameter();
            p6.ParameterName = "@Course";
            p6.Value = student.Course;
            cmd.Parameters.Add(p6);

            IDbDataParameter p7 = cmd.CreateParameter();
            p7.ParameterName = "@College";
            p7.Value = student.College;
            cmd.Parameters.Add(p7);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            string provider = ConfigurationManager.ConnectionStrings["defaultConnection"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            IDbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            //string sqlGetAll = $"select * from Students";
            string sqlGetAll = "sp_GetAll";
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sqlGetAll;
            cmd.Connection = conn;

            using (conn)
            {
                conn.Open();
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
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
                conn.Close();
            }
            if (students.Count == 0)
                throw new EmptyDatabaseException("Database contains no data");
            return students;    
        }
        Student IStudentsRepository.SearchStudentByRollNo(int rollNo)
        {
            Student student = null;
            string provider = ConfigurationManager.ConnectionStrings["defaultConnection"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            IDbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            //string sqlGet = $"select * from Students where RollNo = @RollNo";
            string sqlGet = "sp_GetByRollNo";
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sqlGet;
            cmd.Connection = conn;
            IDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@RollNo";
            p1.Value = rollNo;
            cmd.Parameters.Add(p1);

            using (conn)
            {
                conn.Open();
                
                IDataReader reader = cmd.ExecuteReader();
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
                conn.Close();
            }
            if (student == null)
                throw new StudentNotFoundException("No such student in database");
            return student;
            
        }
        void IStudentsRepository.DeleteStudentByRollNo(int rollNo)
        {
            int rowsAffected = 0;

            string provider = ConfigurationManager.ConnectionStrings["defaultConnection"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            IDbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            //string sqlDelete = $"delete from Students where RollNo = @RollNo";
            string sqlDelete = "sp_DeleteByRollNo";
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sqlDelete;
            cmd.Connection = conn;
            IDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@RollNo";
            p1.Value = rollNo;
            cmd.Parameters.Add(p1);

            using (conn)
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            if (rowsAffected == 0)
                throw new StudentNotFoundException("No such student in database");
        }
        void IStudentsRepository.UpdateStudent(int rollNo, Student student)
        {
            int rowsAffected = 0;

            string provider = ConfigurationManager.ConnectionStrings["defaultConnection"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            IDbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            //string sqlUpdate = $"UPDATE Students SET FirstName = @FirstName, LastName = @LastName, DOB = @DOB, Email = @Email, Mobile = @Mobile, Course = @Course, College = @College WHERE RollNo = @OriginalRollNo";
            string sqlUpdate = "sp_UpdateByRollNo";
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sqlUpdate;
            cmd.Connection = conn;

            IDbDataParameter p1 = cmd.CreateParameter();
            p1.ParameterName = "@FirstName";
            p1.Value = student.FirstName;
            cmd.Parameters.Add(p1);

            IDbDataParameter p2 = cmd.CreateParameter();
            p2.ParameterName = "@LastName";
            p2.Value = student.LastName;
            cmd.Parameters.Add(p2);

            IDbDataParameter p3 = cmd.CreateParameter();
            p3.ParameterName = "@DOB";
            p3.Value = student.DOB;
            cmd.Parameters.Add(p3);

            IDbDataParameter p4 = cmd.CreateParameter();
            p4.ParameterName = "@Email";
            p4.Value = student.Email;
            cmd.Parameters.Add(p4);

            IDbDataParameter p5 = cmd.CreateParameter();
            p5.ParameterName = "@Mobile";
            p5.Value = student.Mobile;
            cmd.Parameters.Add(p5);

            IDbDataParameter p6 = cmd.CreateParameter();
            p6.ParameterName = "@Course";
            p6.Value = student.Course;
            cmd.Parameters.Add(p6);

            IDbDataParameter p7 = cmd.CreateParameter();
            p7.ParameterName = "@College";
            p7.Value = student.College;
            cmd.Parameters.Add(p7);

            IDbDataParameter p8 = cmd.CreateParameter();
            p8.ParameterName = "@OriginalRollNo";
            p8.Value = rollNo;
            cmd.Parameters.Add(p8);


            using (conn)
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            if (rowsAffected == 0)
                throw new StudentNotFoundException("No such student in database");
        }
    }
}
