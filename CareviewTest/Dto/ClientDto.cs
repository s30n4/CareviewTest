using CareviewTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareviewTest.Dto
{
    public class ClientDto
    {
        public string Name { get;  set; }
        public string NDISNumber { get;  set; }
        public DateTime DateOfBirth { get;  set; }
        public int TotalInvoices { get; internal set; }
        public decimal InvoiceQuantity { get; internal set; }
    }
}
