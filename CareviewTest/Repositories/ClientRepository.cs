using CareviewTest.Data;
using CareviewTest.Models;
using CareviewTest.Repositories.Interfaces;
using System.Linq;

namespace CareviewTest.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly CareviewDbContext _dbContext;

        public ClientRepository(CareviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Client> GetAllClients()
        {
            return _dbContext.Clients;
        }
    }
}
