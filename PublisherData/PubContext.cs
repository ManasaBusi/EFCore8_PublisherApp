﻿using Microsoft.EntityFrameworkCore;
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
                @"Server=(localdb)\mssqllocaldb;Database=PubDatabase;Integrated Security=True");

            //All Queries will NOT be tracked by default

            //optionsBuilder.UseSqlServer(
            //    @"Server=(localdb)\mssqllocaldb;Database=PubDatabae;Integrated Security=True").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(new Author { Id = 1, FirstName = "Rhoda", LastName = "Lerman" });

            var authorList = new Author[] {
                new Author { Id = 2, FirstName = "Ruth", LastName = "Ozeki" },
                new Author { Id = 3, FirstName = "Sofia", LastName = "Segovia" },
                new Author { Id = 4, FirstName = "Ursula", LastName = "LeGuin" },
                new Author { Id = 5, FirstName = "Hugh", LastName = "Howey" },
                new Author { Id = 6, FirstName = "Isabelle", LastName = "Allende" },
            };

            modelBuilder.Entity<Author>().HasData(authorList);
        }
    }
}
