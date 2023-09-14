namespace Andretta.Library.Business.Models
{
    public record Country
    {
        //public Country(
        //    string name)
        //{
        //    CountryId = Guid.NewGuid();
        //    Name = name;
        //}
        public Guid CountryId { get; init; }
        public string Name { get; init; }

        public ICollection<Address> Addresses { get; init; } = new List<Address>();  // [One to Many Relation]
        public ICollection<Book> Books { get; init; } = new List<Book>(); // [One to Many Relation]

    }
}
