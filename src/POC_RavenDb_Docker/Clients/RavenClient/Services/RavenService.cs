using POC_RavenDb_Docker.Clients.RavenClient.Interfaces;
using POC_RavenDb_Docker.DTOs.Requests;
using POC_RavenDb_Docker.DTOs.Responses;
using Raven.Client.Documents.Session;
using SessionOptions = Raven.Client.Documents.Session;

namespace POC_RavenDb_Docker.Clients.RavenClient.Services
{
    public class RavenService : IRavenService
    {
        private readonly IDocumentStoreHolder _documentStoreHolder;
        private SessionOptions.SessionOptions? _sessionOptions;

        public RavenService(IDocumentStoreHolder documentStoreHolder)
        {
            _documentStoreHolder = documentStoreHolder;
        }

        public IRavenService ConfigSessionOption(string database)
        {
            _sessionOptions = new()
            {
                Database = database,
                TransactionMode = TransactionMode.ClusterWide
            };
            return this;
        }

        public async Task Store<T>(T obj)
        {
            using (IAsyncDocumentSession asyncSession = _documentStoreHolder.Store().OpenAsyncSession(_sessionOptions))
            {
                await asyncSession.StoreAsync(obj);

                await asyncSession.SaveChangesAsync();
            }
        }

        public async Task Delete(string id)
        {
            using (IAsyncDocumentSession asyncSession = _documentStoreHolder.Store().OpenAsyncSession(_sessionOptions))
            {
                asyncSession.Delete(id);

                await asyncSession.SaveChangesAsync();
            }
        }

        public async Task<List<ResultDto<T>>> Load<T>(int skip)
        {
            using (IAsyncDocumentSession asyncSession = _documentStoreHolder.Store().OpenAsyncSession(_sessionOptions))
            {
                List<T> result = await asyncSession.Advanced.AsyncDocumentQuery<T>()
                    .Skip(skip)
                    .Take(10)
                    .ToListAsync();

                var resultWithIds = result.Select(dto => new ResultDto<T> { Id = asyncSession.Advanced.GetDocumentId(dto), Data = dto }).ToList();

                return resultWithIds;
            }
        }

        public async Task<ResultDto<T>> Load<T>(string id)
        {
            using (IAsyncDocumentSession asyncSession = _documentStoreHolder.Store().OpenAsyncSession(_sessionOptions))
            {
                var idReplaced = id.Replace("%2F", "/");
                var result = await asyncSession.LoadAsync<T>(idReplaced);

                var resultWithIds = new ResultDto<T> { Id = asyncSession.Advanced.GetDocumentId(result), Data = result };

                return resultWithIds;
            }
        }

        public async Task Update(string id, BookRequest data)
        {
            using (IAsyncDocumentSession asyncSession = _documentStoreHolder.Store().OpenAsyncSession(_sessionOptions))
            {
                var result = await asyncSession.LoadAsync<BookRequest>(id);

                result.Name = data.Name;
                result.Pages = data.Pages;

                await asyncSession.SaveChangesAsync();
            }
        }
    }
}