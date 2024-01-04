using Raven.Client.Documents;

namespace POC_RavenDb_Docker.Clients.RavenClient.Interfaces
{
    public interface IDocumentStoreHolder
    {
        IDocumentStore Store();
    }
}
