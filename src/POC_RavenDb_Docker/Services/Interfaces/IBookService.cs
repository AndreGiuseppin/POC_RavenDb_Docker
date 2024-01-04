using POC_RavenDb_Docker.DTOs.Requests;
using POC_RavenDb_Docker.DTOs.Responses;

namespace POC_RavenDb_Docker.Services.Interfaces
{
    public interface IBookService
    {
        Task Store(BookRequest obj);
        Task Delete(string id);
        Task<List<ResultDto<BookRequest>>> Load(int skip);
        Task<ResultDto<BookRequest>> Load(string id);
        Task Update(string id, BookRequest data);
    }
}
