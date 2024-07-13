// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PublisherData;
using PublisherDomain;
using System.Net.NetworkInformation;

//using (PubContext context = new PubContext())
//{ 
//    context.Database.EnsureCreated();
//}

using PubContext _context = new();

//GetAuthors();
//AddAuthor();
//GetAuthors();

//AddAuthorWithBook();
//GetAuthorsWithBooks();

//QueryFilters();

//AddSomeMoreAuthors();
//SkipAndTakeAuthors();

//SortAuthors();
QueryAggregate();

void QueryAggregate()
{
    var author = _context.Authors.FirstOrDefault(a => a.LastName =="mmm");
    //var author = _context.Authors.First(a => a.LastName == "mmm");//This will throw an exception

    //var authorWithLastName = _context.Authors.LastOrDefault(a => a.LastName.StartsWith("B"));//This will thrown an exception because LastOrDefault shold have an OrderBy clause
    var authorWithLastName = _context.Authors.OrderBy(a => a.LastName).LastOrDefault(a => a.LastName.StartsWith("B"));

}

void SortAuthors()
{
    //var authorsByLastName = _context.Authors.OrderBy(a => a.LastName).OrderBy(a => a.FirstName).ToList();
    var authorsByLastName = _context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();
    authorsByLastName.ForEach(a=> Console.WriteLine($"{a.FirstName} {a.LastName}"));
}

void SkipAndTakeAuthors()
{
    var groupSize = 2;
    for (int i = 0; i < 5; i++)
    {
        var authors = _context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
        Console.WriteLine($"Group {i}:");
        foreach (var author in authors)
        {
            Console.WriteLine($"{author.FirstName} {author.LastName}");
        }
    }
}

void AddSomeMoreAuthors()
{
    _context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
    _context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
    _context.Authors.Add(new Author { FirstName = "Jim", LastName = "Chris" });
    _context.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });
    _context.SaveChanges();
}
void QueryFilters()
{
    //var authors = _context.Authors.Where(a => a.FirstName == "AAA").ToList();
    var authors = _context.Authors.Where(a => EF.Functions.Like(a.FirstName, "A%")).ToList();
}

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}

void AddAuthor()
{
    var author = new Author { FirstName = "Manasa", LastName = "Deepthi" };
    var author1 = new Author { FirstName = "John", LastName = "Doe" };
    using var context = new PubContext();
    context.Authors.Add(author);
    context.Authors.Add(author1);
    context.SaveChanges();
}

void AddAuthorWithBook()
{
    var author = new Author { FirstName = "AAA", LastName = "BBB" };
    author.Books.Add(new Book { Title = "C# Programming", PublishDate = new DateOnly(2009, 1, 1) });
    author.Books.Add(new Book { Title = "ASP.NET Core", PublishDate = new DateOnly(2010, 8, 1) });

    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void GetAuthorsWithBooks()
{
    using var context = new PubContext();
    var authors = context.Authors.Include(a => a.Books).ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
        foreach (var book in author.Books)
        {
            Console.WriteLine(book.Title);
        }
    }
}

