using POC_RavenDb_Docker.DTOs.Requests;
using POC_RavenDb_Docker.DTOs.Responses;

namespace POC_RavenDb_Docker.Clients.RavenClient.Interfaces
{
    public interface IRavenService
    {
        IRavenService ConfigSessionOption(string database);
        Task Store<T>(T obj);
        Task Delete(string id);
        Task<List<ResultDto<T>>> Load<T>(int skip);
        Task<ResultDto<T>> Load<T>(string id);
        Task Update(string id, BookRequest data);
    }
}
