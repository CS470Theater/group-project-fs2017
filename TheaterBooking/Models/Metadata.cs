using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheaterBooking.Models
{
    public class BookingMetadata
    {
        public int Booking_ID { get; set; }
        public int Customer_ID { get; set; }
        public System.DateTime Booking_Date { get; set; }
    }
    public partial class Booking
    {

    }


    public class CustomerMetadata
    {
        public int Customer_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public partial class Customer
    {

    }


    public class GenreMetadata
    {
        public int Genre_ID { get; set; }
        public string Genre_Name { get; set; }
    }
    public partial class Genre
    {

    }


    public class MovieMetadata
    {
        public int Movie_ID { get; set; }
        public int Rating_ID { get; set; }
        public string Movie_Name { get; set; }
        public string Movie_Desc { get; set; }
        public byte[] Movie_Poster { get; set; }
        public System.DateTime Date_Start { get; set; }
        public System.DateTime Date_End { get; set; }
        public byte Duration_Hour { get; set; }
        public byte Duration_Minute { get; set; }
    }
    public partial class Movie
    {
        
    }


    public class PriceMetadata
    {
        public int Price_ID { get; set; }
        public string Price_Desc { get; set; }
        public decimal Price_Amount { get; set; }
    }
    public partial class Price
    {

    }


    public class RatingMetadata
    {
        public int Rating_ID { get; set; }
        public string Rating_Symbol { get; set; }
    }
    public partial class Rating
    {

    }


    public class ScreenMetadata
    {
        public int Screen_ID { get; set; }
        public int Theater_ID { get; set; }
        public short Capacity { get; set; }
    }
    public partial class Screen
    {

    }


    public class ShowtimeMetadata
    {
        public int Showtime_ID { get; set; }
        public int Screen_ID { get; set; }
        public int Movie_ID { get; set; }
        public System.DateTime Show_Date { get; set; }
        public System.TimeSpan Show_Time { get; set; }
    }
    public partial class Showtime
    {

    }


    public class TheaterMetadata
    {
        public int Theater_ID { get; set; }
        public string Theater_Name { get; set; }
        public string Theater_Owner { get; set; }
        public string Theater_Address { get; set; }
        public string Theater_Phone { get; set; }
    }
    public partial class Theater
    {

    }


    public class TicketMetadata
    {
        public int Ticket_ID { get; set; }
        public int Booking_ID { get; set; }
        public int Showtime_ID { get; set; }
        public int Price_ID { get; set; }
    }
    public partial class Ticket
    {

    }

}