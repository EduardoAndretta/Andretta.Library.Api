using Andretta.Library.Business.Interfaces.Repositories;
using Andretta.Library.Business.Models;
using Andretta.Library.Database.Migrations;
using PdfSharpCore.Drawing;
using PuppeteerSharp;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Andretta.Library.Database.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly InformationContext _context;
        public BookRepository(InformationContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GetBooks()
        {

            _context.Database.EnsureCreated();

            // [Get the highest Quantity of Stock of a Book]
            var highestQuantityStockBook = (from s in _context.Stocks
                              join b in _context.Books on s.BookId equals b.BookId
                              select new HighestQuantityStockBook { Quantity = s.Quantity.ToString(), Title = b.Title })
                 .OrderByDescending(x => x.Quantity)
                 .FirstOrDefault();

            // [Get the lowest Quantity of Stock of a Book]
            var lowestQuantityStockBook = (from s in _context.Stocks
                              join b in _context.Books on s.BookId equals b.BookId
                              select new LowestQuantityStockBook { Quantity = s.Quantity.ToString(), Title = b.Title })
                 .OrderBy(x => x.Quantity)
                 .FirstOrDefault();

            // [Get the highest and lowest Quantity of Stock of Books]
            var highestAndLowestQuantityStockBook = (from s in _context.Stocks
                              join b in _context.Books on s.BookId equals b.BookId
                              select new { Stock = s, Book = b })
                .GroupBy(x => x.Stock.Quantity)
                .Select(g => new
                {
                    MaxQuantityBook = g.OrderByDescending(x => x.Stock.Quantity).FirstOrDefault(),
                    MinQuantityBook = g.OrderBy(x => x.Stock.Quantity).FirstOrDefault()
                })
                .FirstOrDefault();

            // [Get the highest Unit Price of Stock of a Book]
            var highestUnitPriceStockBook = (from s in _context.Stocks
                              join b in _context.Books on s.BookId equals b.BookId
                              select new HighestUnitPriceStockBook { Price = s.Price.ToString("N2"), Title = b.Title })
                 .OrderByDescending(x => x.Price)
                 .FirstOrDefault();

            // [Get the lowest Unit Price of Stock of a Book]
            var lowestUnitPriceStockBook = (from s in _context.Stocks
                              join b in _context.Books on s.BookId equals b.BookId
                              select new LowestUnitPriceStockBook { Price = s.Price.ToString("N2"), Title = b.Title })
                 .OrderBy(x => x.Price)
                 .FirstOrDefault();

            // [Get the highest and lowest Unit Price of Stock of Books]
            var highestAndLowestUnitPriceStockBook = (from s in _context.Stocks
                              join b in _context.Books on s.BookId equals b.BookId
                              select new { Stock = s, Book = b })
                .GroupBy(x => x.Stock.Price)
                .Select(g => new
                {
                    MaxPriceBook = g.OrderByDescending(x => x.Stock.Price).FirstOrDefault(),
                    MinPriceBook = g.OrderBy(x => x.Stock.Price).FirstOrDefault()
                })
                .FirstOrDefault();

            // [Get the highest Total price of a Book]
            var highestTotalPriceBook = (from b in _context.Books
                              join s in _context.Stocks on b.BookId equals s.BookId
                              select 
                                   new HighestTotalPriceBook
                                   {
                                       Name = b.Title, 
                                       TotalPrice = (s.Price * s.Quantity).ToString("N2")
                                   })
                              .OrderByDescending(x => x.TotalPrice)
                              .FirstOrDefault();

            // [Get the lowest Total price of a Book]
            var lowestTotalPriceBook = (from b in _context.Books
                              join s in _context.Stocks on b.BookId equals s.BookId
                              select
                                   new LowestTotalPriceBook
                                   {
                                       Name = b.Title,
                                       TotalPrice = (s.Price * s.Quantity).ToString("N2")
                                   })
                              .OrderBy(x => x.TotalPrice)
                              .FirstOrDefault();

            // [Get the highest and lowest Total price of Books]
            var highestAndLowestTotalPriceBook = (from b in _context.Books
                              join s in _context.Stocks on b.BookId equals s.BookId
                              select new
                              {
                                  BookName = b.Title,
                                  TotalPrice = s.Price * s.Quantity
                              })
                .GroupBy(x => x.TotalPrice)
                .Select(g => new
                {
                    MaxTotalPriceBook = g.OrderByDescending(x => x.TotalPrice).FirstOrDefault(),
                    MinTotalPriceBook = g.OrderBy(x => x.TotalPrice).FirstOrDefault(),
                })
                .FirstOrDefault();


            // [Get the highest PublishDate Book]
            var highestPublishDateBook = (from b in _context.Books
                              join p in _context.Publishers on b.PublisherId equals p.PublisherId
                              join a in _context.Authors on b.AuthorId equals a.AuthorId
                              where b.PublishedDate == _context.Books.Max(x => x.PublishedDate)
                              select 
                                   new HighestPublishDateBook
                                   { 
                                       Name = $"{a.FirstName} {a.LastName}",
                                       Book = b.Title,
                                       PublishedDate = b.PublishedDate.ToString("d")
                                   })
                              .FirstOrDefault();

            // [Get the lowest PublishDate Book]
            var lowestPublishDateBook = (from b in _context.Books
                                          join p in _context.Publishers on b.PublisherId equals p.PublisherId
                                          join a in _context.Authors on b.AuthorId equals a.AuthorId
                                          where b.PublishedDate == _context.Books.Min(x => x.PublishedDate)
                                          select
                                               new LowestPublishDateBook
                                               {
                                                   Name = $"{a.FirstName} {a.LastName}",
                                                   Book = b.Title,
                                                   PublishedDate = b.PublishedDate.ToString("d")
                                               })
                              .FirstOrDefault();

            var content = new BookReport
            {
                HighestQuantityStockBook  = highestQuantityStockBook,
                LowestQuantityStockBook   = lowestQuantityStockBook,
                HighestUnitPriceStockBook = highestUnitPriceStockBook,
                LowestUnitPriceStockBook  = lowestUnitPriceStockBook,
                HighestTotalPriceBook     = highestTotalPriceBook,
                LowestTotalPriceBook      = lowestTotalPriceBook,
                HighestPublishDateBook    = highestPublishDateBook,
                LowestPublishDateBook     = lowestPublishDateBook
            };

            // [Report]
            var templateManager = new ResolvePathTemplateManager(new[] { "wwwroot/Templates/BookReport" });

            var config = new TemplateServiceConfiguration
            {
                TemplateManager = templateManager
            };

            Engine.Razor = RazorEngineService.Create(config);

            var pageContent = Engine.Razor.RunCompile("BookReport.cshtml", null, content);

            pageContent = pageContent.Replace("Andretta.Library.Business.Models.BookReport", "");

            return await GeneratePdfFromHtml(pageContent);
        }

        private static async Task<byte[]> GeneratePdfFromHtml(string htmlContent)
        {
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
            });

            using var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);

            return await page.PdfDataAsync(new PdfOptions
            {
                PrintBackground = true
            });
        }
    }
}
