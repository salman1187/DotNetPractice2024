using MovieApp.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            ITicketBookingService ticketBookingService = new TicketBookingService();
            ticketBookingService.DisplayReport("Goodfellas");
            //check other methods too
        }
    }
}
