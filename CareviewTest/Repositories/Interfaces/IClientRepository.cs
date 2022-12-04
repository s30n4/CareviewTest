using CareviewTest.Models;
using System.Linq;

namespace CareviewTest.Repositories.Interfaces
{
    public interface IClientRepository
    {
        IQueryable<Client> GetAllClients();
    }
}
