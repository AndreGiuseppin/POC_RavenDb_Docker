using POC_RavenDb_Docker.Clients.RavenClient.Interfaces;
using POC_RavenDb_Docker.DTOs.Requests;
using POC_RavenDb_Docker.DTOs.Responses;
using POC_RavenDb_Docker.Services.Interfaces;

namespace POC_RavenDb_Docker.Services
{
    public class BookService : IBookService
    {
        private readonly IRavenService _ravenService;

        public BookService(IRavenService ravenService)
        {
            _ravenService = ravenService ?? throw new ArgumentNullException(nameof(ravenService));
        }
        public async Task Store(BookRequest obj)
        {
            await _ravenService.ConfigSessionOption("BookDatabase").Store(obj);
        }
        public async Task<List<ResultDto<BookRequest>>> Load(int skip)
        {
            var result = await _ravenService.ConfigSessionOption("BookDatabase").Load<BookRequest>(skip);
            return result;
        }

        public async Task<ResultDto<BookRequest>> Load(string id)
        {
            var result = await _ravenService.ConfigSessionOption("BookDatabase").Load<BookRequest>(id);
            return result;
        }

        public async Task Delete(string id)
        {
            var idReplaced = id.Replace("%2F", "/");
            await _ravenService.ConfigSessionOption("BookDatabase").Delete(idReplaced);
        }

        public async Task Update(string id, BookRequest data)
        {
            var idReplaced = id.Replace("%2F", "/");

            await _ravenService.ConfigSessionOption("BookDatabase").Update(idReplaced, data);
        }
    }
}
