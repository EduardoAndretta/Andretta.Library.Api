using System.Diagnostics.Metrics;

namespace Andretta.Library.Business.Models
{
    public record Book
    {
        //public Book(
        //    Guid     publisherId,
        //    Guid     categoryId,
        //    Guid     authorId, 
        //    DateOnly publishedDate,
        //    string title)
        //{
        //    BookId        = Guid.NewGuid();
        //    PublisherId   = publisherId;
        //    CategoryId    = categoryId;
        //    AuthorId      = authorId;
        //    PublishedDate = publishedDate;
        //    Title         = title;
        //}


        public Guid BookId { get; init; }
        public Guid PublisherId { get; init; }
        public Guid CategoryId { get; init; }
        public Guid AuthorId { get; init; }

        public DateOnly PublishedDate { get; init; }
        public string Title { get; init; }

        public Category Category { get; init; } = null!; // [One to Many Relation]
        public Author Author { get; init; } = null!; // [One to Many Relation]
        public Publisher Publisher { get; init; } = null!; // [One to Many Relation]

        public ICollection<Stock> Stocks { get; init; } = new List<Stock>();  // [One to Many Relation]
    }
}
