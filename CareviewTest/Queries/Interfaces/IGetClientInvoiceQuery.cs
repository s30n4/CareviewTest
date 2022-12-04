using CareviewTest.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareviewTest.Queries.Interfaces
{
    public interface IGetClientInvoiceQuery
    {
        Task<List<ClientDto>> GetClientDtoAsync();
    }
}
