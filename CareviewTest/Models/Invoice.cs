using System;
using System.Collections.Generic;

namespace CareviewTest.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; } = new List<InvoiceLineItem>();
    }
}
