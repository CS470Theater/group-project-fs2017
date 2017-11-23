using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using JetBrains.Annotations;

namespace TheaterBooking.Web.Database
{
    public class TheaterDbEntities : DbContext
    {
        public TheaterDbEntities() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating([NotNull] DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Movie>()
                .HasMany(movie => movie.Genres)
                .WithMany(genre => genre.Movies)
                .Map(movieGenre =>
                {
                    movieGenre.MapLeftKey("Movie_Id");
                    movieGenre.MapRightKey("Genre_Id");
                    movieGenre.ToTable("Movie_Genre");
                });
        }

        public virtual DbSet<Booking> Bookings { get; set; }
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
