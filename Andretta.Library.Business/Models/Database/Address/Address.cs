namespace Andretta.Library.Business.Models
{
    public record Address
    {
        //public Address(
        //    Guid countryId,
        //    string street)
        //{
        //    IdAddress = Guid.NewGuid();
        //    CountryId = countryId;
        //    Street = street;
        //}

        public Guid AddressId { get; init; }
        public Guid CountryId { get; init; }
        public string Street { get; init; }

        public Country Country { get; init; } = null!;
        public ICollection<Publisher> Publishers = new List<Publisher>();
    }
}
