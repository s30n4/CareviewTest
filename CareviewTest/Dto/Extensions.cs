using CareviewTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareviewTest.Dto
{
    public static class Extensions
    {
        public static ClientDto ToModel(this Client client)
        {
            var dto =  new ClientDto
            {
                Name = client.Name,
                NDISNumber = client.NDISNumber,
                DateOfBirth = client.DateOfBirth,
                TotalInvoices = client.Invoices.ToList().Count(),
            };
            var quantity = 0m;
            foreach(var inv in client.Invoices)
            {
                quantity += inv.InvoiceLineItems.Select(il => il.Quantity).First();
            }
            dto.InvoiceQuantity = quantity;
            return dto;
        }
    }
}
