using Domain.Repositories;
using Raven.Client;
using Raven.Client.Document;
using System.Threading.Tasks;

namespace Infra.RavenDB.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DocumentStore documentStore;
        public IAsyncDocumentSession session { get; private set; }

        public UnitOfWork()
        {
            documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "easyBox"
            };
            documentStore.Initialize();
            session = documentStore.OpenAsyncSession();
        }

        public async Task Commit()
        {
            await session.SaveChangesAsync();
        }

        public void Dispose()
        {
            session?.Dispose();
            documentStore?.Dispose();
        }
    }
}
