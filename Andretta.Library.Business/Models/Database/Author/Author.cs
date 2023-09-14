namespace Andretta.Library.Business.Models
{
    public record Author
    {
        //public Author(
        //    string firstName,
        //    string lastName)
        //{
        //    AuthorId      = Guid.NewGuid();
        //    FirstName     = firstName;
        //    LastName      = lastName;
        //}

        public Guid AuthorId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }

        public ICollection<Book> Books = new List<Book>();
    }
}
