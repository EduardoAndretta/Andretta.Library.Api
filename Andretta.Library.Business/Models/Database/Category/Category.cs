using System.Net;

namespace Andretta.Library.Business.Models
{
    public record Category
    {
        //public Category(
        //    string name)
        //{
        //    CategoryId = Guid.NewGuid();
        //    Name       = name;
        //}

        public Guid CategoryId { get; init; }
        public string Name { get; init; }

        public ICollection<Book> Books = new List<Book>(); // [One to Many Relation]
    }
}
