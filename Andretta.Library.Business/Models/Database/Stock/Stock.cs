namespace Andretta.Library.Business.Models
{
    public record Stock
    {
        //public Stock(
        //    Guid    countryId,
        //    Guid    bookId,
        //    int     quantity,
        //    decimal price)
        //{
        //    StockId   = Guid.NewGuid();
        //    CountryId = countryId;
        //    BookId    = bookId;
        //    Quantity  = quantity;
        //    Price     = price;
        //}

        public Guid StockId { get; init; }
        public Guid CountryId { get; init; }
        public Guid BookId { get; init; }
        public int Quantity { get; init; }
        public decimal Price { get; init; }

        public Book Book { get; init; } = null!; // [One to Many Relation]
    }
}
