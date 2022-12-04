using CareviewTest.Dto;
using CareviewTest.Queries.Interfaces;
using CareviewTest.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace CareviewTest.Queries
{
    public class GetClientInvoiceQuery : IGetClientInvoiceQuery
    {
        private readonly IClientRepository _clientRepository;

        public GetClientInvoiceQuery(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<ClientDto>> GetClientDtoAsync()
        {
            var clients = await _clientRepository.GetAllClients()
                  .AsNoTracking()
                  .Include("Invoices.InvoiceLineItems")
                  .Select(c => new ClientDto
                  {
                      Name = c.Name,
                      TotalInvoices = c.Invoices.Count(),
                      InvoiceQuantity = c.Invoices.SelectMany(i => i.InvoiceLineItems).Sum(x => x.Quantity)
                  }).ToListAsync();


            foreach (var client in clients)
            {
                Console.WriteLine(client.Name);
                Console.WriteLine(client.TotalInvoices);
                Console.WriteLine(client.InvoiceQuantity);
            }

            return clients;
        }
    }
}
