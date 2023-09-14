using Andretta.Library.Business.Models;
using Andretta.Library.Database.Migrations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Andretta.Library.Database.Migrations
{
    public class InformationContext : DbContext
    {
        private readonly Random random = new();

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyInMemoryDatabase");
        }

        protected readonly List<Action<ModelBuilder>> ModelsMapping = new()
        {
            (modelBuilder) => modelBuilder.ApplyConfiguration(new BookEntity()),
            (modelBuilder) => modelBuilder.ApplyConfiguration(new AuthorEntity()),
            (modelBuilder) => modelBuilder.ApplyConfiguration(new CategoryEntity()),
            (modelBuilder) => modelBuilder.ApplyConfiguration(new PublisherEntity()),
            (modelBuilder) => modelBuilder.ApplyConfiguration(new AddressEntity()),
            (modelBuilder) => modelBuilder.ApplyConfiguration(new StockEntity()),
            (modelBuilder) => modelBuilder.ApplyConfiguration(new CountryEntity())
        };

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (Action<ModelBuilder> modelMapping in ModelsMapping)
            {
                modelMapping(modelBuilder);
            }

            // [Country Aleatory Data]
            List<Country> countries = new();
            for (int index = 0; index < 50; index++)
            {
                countries.Add(
                    new Country 
                    { 
                        CountryId = Guid.NewGuid(), 
                        Name = RandomDataGenerator.GenerateCountryName() 
                    });
            }
            modelBuilder.Entity<Country>().HasData(countries);


            // [Address Aleatory Data]
            List<Address> addresses = new();
            for (int index = 0; index < 150; index++)
            {
                addresses.Add(
                    new Address 
                    { 
                        AddressId = Guid.NewGuid(), 
                        CountryId = countries.ElementAt(random.Next(1, countries.Count())).CountryId, 
                        Street = RandomDataGenerator.GenerateRandomAddress() 
                    });
            }
            modelBuilder.Entity<Address>().HasData(addresses);


            // [Publisher Aleatory Data]
            List<Publisher> publishers = new();
            for (int index = 0; index < 200; index++)
            {
                publishers.Add(
                    new Publisher 
                    { 
                        PublisherId = Guid.NewGuid(), 
                        AddressId = addresses.ElementAt(random.Next(1, addresses.Count())).AddressId, 
                        Name = RandomDataGenerator.GenerateRandomCompanyName() 
                    });
            }
            modelBuilder.Entity<Publisher>().HasData(publishers);


            // [Category Aleatory Data]
            List<Category> categories = new();
            for (int index = 0; index < 50; index++)
            {
                categories.Add(
                    new Category 
                    { 
                        CategoryId = Guid.NewGuid(), 
                        Name = RandomDataGenerator.GenerateRandomCategory() 
                    });
            }
            modelBuilder.Entity<Category>().HasData(categories);


            // [Author Aleatory Data]
            List<Author> authors = new();
            for (int index = 0; index < 90; index++)
            {
                authors.Add(
                    new Author 
                    { 
                        AuthorId = Guid.NewGuid(), 
                        FirstName = RandomDataGenerator.GenerateRandomPersonFirstName(), 
                        LastName = RandomDataGenerator.GenerateRandomPersonLastName() 
                    });
            }
            modelBuilder.Entity<Author>().HasData(authors);

            // [Book Aleatory Data]
            List<Book> books = new();
            for (int index = 0; index < 1000; index++)
            {
                books.Add(
                    new Book 
                    { 
                        BookId        = Guid.NewGuid(), 
                        PublisherId   = publishers.ElementAt(random.Next(1, publishers.Count())).PublisherId, 
                        CategoryId    = categories.ElementAt(random.Next(1, categories.Count())).CategoryId, 
                        AuthorId      = authors.ElementAt(random.Next(1, authors.Count())).AuthorId, 
                        PublishedDate = RandomDataGenerator.GenerateRandomDateOnly(new DateTime(1900, 1, 1), DateTime.Now), 
                        Title         = RandomDataGenerator.GenerateRandomBookTitle()
                    });
            }
            modelBuilder.Entity<Book>().HasData(books);


            // [Stock Aleatory Data]
            List<Stock> stocks = new();
            for (int index = 0; index < 1000; index++)
            {
                stocks.Add(
                    new Stock
                    {
                        StockId   = Guid.NewGuid(),
                        CountryId = countries.ElementAt(random.Next(1, countries.Count())).CountryId,
                        BookId    = books.ElementAt(random.Next(1, books.Count())).BookId,
                        Quantity  = RandomDataGenerator.GenerateRandomInt(0, 1000000),
                        Price     = RandomDataGenerator.GenerateRandomDecimal(0, 1500),

                    });
            }
            modelBuilder.Entity<Stock>().HasData(stocks);

            base.OnModelCreating(modelBuilder);
        }
    }
}