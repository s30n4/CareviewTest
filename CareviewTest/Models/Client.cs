using System;
using System.Collections.Generic;

namespace CareviewTest.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NDISNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    }
}
