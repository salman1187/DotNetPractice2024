using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MovieApp.DataLayer
{
    public interface ITicketBookingService
    {
        double GetTotalIncomeEarnedByTheatre(string theatreName);
        List<string> GetAllMovieNamesSeenByUserInTheatre(string theatreName, string loginName);
        int GetTotalNumberOfTicketsBookedByCity(string cityName);
        void DisplayReport(string movieName);
    }
    public class TicketBookingService : ITicketBookingService
    {
        public void DisplayReport(string movieName)
        {
            string provider = ConfigurationManager.ConnectionStrings["defaultConnection"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            IDbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            string sqlGetAll = $"SELECT th.TheatreName as thName, m.MovieName as mvName, COUNT(*) AS TicketsSold FROM Movies m JOIN Shows s ON s.MovieId = m.MovieId JOIN Screens sc ON s.ScreenId = sc.ScreenId JOIN Theatres th ON sc.TheatreId = th.TheatreId JOIN Bookings b ON b.ShowId = s.ShowId JOIN Users u ON b.UserId = u.UserId WHERE m.MovieName = '{movieName}' GROUP BY th.TheatreName,m.MovieName;";
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlGetAll;
            cmd.Connection = conn;

            Console.WriteLine($"TheatreName\tMovieName\tTicketsSold");
            using (conn)
            {
                conn.Open();
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["thName"]}\t{reader["mvName"]}\t{reader["TicketsSold"]}");
                }
                reader.Close();
                conn.Close();
            }
        }

        public List<string> GetAllMovieNamesSeenByUserInTheatre(string theatreName, string loginName)
        {
            List<string> movies = new List<string>();

            string provider = ConfigurationManager.ConnectionStrings["defaultConnection"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            IDbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            string sqlGetAll = $"SELECT m.MovieName FROM Movies m JOIN Shows s ON s.MovieId = m.MovieId JOIN Screens sc ON s.ScreenId = sc.ScreenId JOIN Theatres th ON sc.TheatreId = th.TheatreId JOIN Bookings b ON b.ShowId = s.ShowId JOIN Users u ON b.UserId = u.UserId join Tickets tic on tic.BookingId = b.BookingId join Seats st on st.ScreenId = sc.ScreenId WHERE u.LoginName = '{loginName}' AND th.TheatreName = '{theatreName}';";
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlGetAll;
            cmd.Connection = conn;

            using (conn)
            {
                conn.Open();
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add((string)reader["MovieName"]);
                }
                reader.Close();
                conn.Close();
            }
            return movies;
        }

        public double GetTotalIncomeEarnedByTheatre(string theatreName)
        {
            double total = 0;
            string provider = ConfigurationManager.ConnectionStrings["defaultConnection"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            IDbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            IDbCommand cmd = conn.CreateCommand();

            string query1 = $"SELECT SUM(sh.Cost) AS TotalIncome FROM Theatres t INNER JOIN Screens sc ON t.TheatreId = sc.TheatreId INNER JOIN Shows sh ON sc.ScreenId = sh.ScreenId WHERE  t.TheatreName = '{theatreName}' GROUP BY t.TheatreName;";
            cmd.CommandText = query1;
            cmd.Connection = conn;
            using (conn)
            {
                conn.Open();

                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    total = (double)reader["TotalIncome"];
                }
                reader.Close();
                conn.Close();
            }

            return total;
        }

        public int GetTotalNumberOfTicketsBookedByCity(string cityName)
        {
            int total = 0;
            string provider = ConfigurationManager.ConnectionStrings["defaultConnection"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            IDbConnection conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            IDbCommand cmd = conn.CreateCommand();

            string query1 = $"SELECT COUNT(*) as total_tickets FROM tickets t JOIN bookings b ON b.BookingId = t.BookingId JOIN users u ON u.UserId = b.UserId JOIN addresses a ON a.user_id = u.UserId WHERE a.City = '{cityName}';";
            cmd.CommandText = query1;
            cmd.Connection = conn;
            using (conn)
            {
                conn.Open();

                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    total = (int)reader["total_tickets"];
                }
                reader.Close();
                conn.Close();
            }

            return total;
        }
    }
}
