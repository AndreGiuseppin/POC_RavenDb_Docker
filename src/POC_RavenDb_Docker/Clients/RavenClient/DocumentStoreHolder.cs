using POC_RavenDb_Docker.Clients.RavenClient.Interfaces;
using Raven.Client.Documents;

namespace POC_RavenDb_Docker.Clients.RavenClient
{
    public class DocumentStoreHolder : IDocumentStoreHolder
    {
        // Use Lazy<IDocumentStore> to initialize the document store lazily. 
        // This ensures that it is created only once - when first accessing the public `Store` property.
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore()
            {
                // Define the cluster node URLs (required)
                Urls = new[] { "http://localhost:8080" },

                // Set conventions as necessary (optional)
                Conventions =
                {
                    MaxNumberOfRequestsPerSession = 10,
                    UseOptimisticConcurrency = false
                },

                // Define a default database (optional)
                //Database = "your_database_name",

                // Define a client certificate (optional)
                //Certificate = new X509Certificate2("C:\\path_to_your_pfx_file\\cert.pfx"),


            }.Initialize();

            return store;
        }

        IDocumentStore IDocumentStoreHolder.Store()
        {
            return Store;
        }
    }
}
