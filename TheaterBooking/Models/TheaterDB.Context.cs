﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheaterBooking.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TheaterDBEntities : DbContext
    {
        public TheaterDBEntities()
            : base("name=TheaterDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Screen> Screens { get; set; }
        public virtual DbSet<Showtime> Showtimes { get; set; }
        public virtual DbSet<Theater> Theaters { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
    }
}