using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=PubDatabae;Integrated Security=True");

            //All Queries will NOT be tracked by default

            //optionsBuilder.UseSqlServer(
            //    @"Server=(localdb)\mssqllocaldb;Database=PubDatabae;Integrated Security=True").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
