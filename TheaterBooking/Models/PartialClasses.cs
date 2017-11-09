using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TheaterBooking.Models
{
    [MetadataType(typeof(BookingMetadata))]
    public partial class Booking
    {
    }


    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
    }


    [MetadataType(typeof(GenreMetadata))]
    public partial class Genre
    {
    }


    [MetadataType(typeof(MovieMetadata))]
    public partial class Movie
    {
    }


    [MetadataType(typeof(PriceMetadata))]
    public partial class Price
    {
    }


    [MetadataType(typeof(RatingMetadata))]
    public partial class Rating
    {
    }


    [MetadataType(typeof(ScreenMetadata))]
    public partial class Screen
    {
    }


    [MetadataType(typeof(ShowtimeMetadata))]
    public partial class Showtime
    {
    }


    [MetadataType(typeof(TheaterMetadata))]
    public partial class Theater
    {
    }


    [MetadataType(typeof(TicketMetadata))]
    public partial class Ticket
    {
    }


}