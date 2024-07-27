using Microsoft.EntityFrameworkCore;
using PublisherDomain;
using Microsoft.Extensions.Logging;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        private StreamWriter _writer = new StreamWriter("EFLog.txt", append: true);
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=PubDatabase;Integrated Security=True")
                .LogTo(Console.WriteLine)
                //.LogTo(_writer.WriteLine, new[] { DbLoggerCategory.Database.Command.Name}, LogLevel.Information)
                .EnableSensitiveDataLogging();

            //All Queries will NOT be tracked by default

            //optionsBuilder.UseSqlServer(
            //    @"Server=(localdb)\mssqllocaldb;Database=PubDatabae;Integrated Security=True").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Author>().HasData(new Author { Id = 1, FirstName = "Rhoda", LastName = "Lerman" });

        //    var authorList = new Author[] {
        //        new Author { Id = 2, FirstName = "Ruth", LastName = "Ozeki" },
        //        new Author { Id = 3, FirstName = "Sofia", LastName = "Segovia" },
        //        new Author { Id = 4, FirstName = "Ursula", LastName = "LeGuin" },
        //        new Author { Id = 5, FirstName = "Hugh", LastName = "Howey" },
        //        new Author { Id = 6, FirstName = "Isabelle", LastName = "Allende" },
        //    };

        //    modelBuilder.Entity<Author>().HasData(authorList);

        //    var someBooks = new Book[] {
        //     new Book { BookId = 1, AuthorId =1, Title = "System Design", PublishDate = new DateOnly(1989,3,1) },
        //     new Book { BookId = 2, AuthorId =2, Title = "Architecture", PublishDate = new DateOnly(1990,4,8) },
        //     new Book { BookId = 3, AuthorId =3, Title = "Design Patterns", PublishDate = new DateOnly(1999,3,5) },
        //    };

        //    modelBuilder.Entity<Book>().HasData(someBooks);
        //}

        public override void Dispose()
        {
            _writer.Dispose();
            base.Dispose();
        }
    }
}
