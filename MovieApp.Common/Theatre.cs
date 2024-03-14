using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Common
{
    public class Theatre
    {
        public int TheatreId { get; set; }
        public string TheatreName { get; set; }
        public List<Screen> Screens { get; set; } = new List<Screen>();
    }
    public class Screen
    {
        public int ScreenId { get; set; }
        public string ScreenName { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();
        Theatre TheThreatre { get; set; }   
    }
    public class Seat
    {
        public int SeatId { get; set; }
        public string RowId { get; set; }
        Ticket TheTicket { get; set; }  
    }
    public class Ticket
    {
        public long TicketId { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();
        public Booking TheBooking { get; set; } 
    }
    public class Booking
    {
        public long BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public Show TheShow { get; set; }   
        public User TheUser { get; set; }   
    }
    public class Show
    {
        public int ShowId { get; set; } 
        public DateTime ShowTime { get; set; }
        public double Cost { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public Movie TheMovie { get; set; }
    }
    public class Movie
    {
        public long MovieId { get; set; }   
        public string MovieName { get; set; }
    }
    public class User
    {
        public long UserId { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public Address TheAddress { get; set; }
    }
    public class Address
    {
        public string HouseNo { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PinCode { get; set; }
    }
}
