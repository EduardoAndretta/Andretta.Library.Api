using Andretta.Library.Business.Interfaces.Repositories;
using Andretta.Library.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Andretta.Library.Api.Controllers
{
    [Route("API/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;
        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            return File(await _repository.GetBooks(), "application/pdf", "bookReport.pdf");
        }
    }
}
