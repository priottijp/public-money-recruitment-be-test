using Microsoft.EntityFrameworkCore;
using VacationRental.DomainEntities;

namespace VacationRental.DataAccess
{
    public class VacationApiContext : DbContext
    {
        public VacationApiContext(DbContextOptions<VacationApiContext> options) : base(options) { }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }

}
