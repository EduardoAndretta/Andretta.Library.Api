using Andretta.Library.Business.Models;

namespace Andretta.Library.Business.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<byte[]> GetBooks();
    }
}
