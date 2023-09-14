using System.Net;

namespace Andretta.Library.Business.Models
{
    public record Publisher
    {
        //public Publisher(
        //    Guid idAddress,
        //    string name)
        //{
        //    PublisherId = Guid.NewGuid();
        //    IdAddress   = idAddress;
        //    Name        = name;
        //}

        public Guid PublisherId { get; init; }
        public Guid AddressId { get; init; }
        public string Name { get; init; }

        public Address Address { get; init; } = null!;
        public ICollection<Book> Books { get; init; } = new List<Book>(); // [One to Many Relation]

    }
}
