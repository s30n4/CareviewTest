using Bogus;
using CareviewTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CareviewTest.Data
{
    internal class DatabaseInitializer : DropCreateDatabaseIfModelChanges<CareviewDbContext>
    {
        public DatabaseInitializer() { }
        protected override void Seed(CareviewDbContext context)
        {
            Faker<Client> clientFaker = new Faker<Client>()
                    .RuleFor(u => u.Name, (f, u) => f.Name.FindName())
                    .RuleFor(u => u.DateOfBirth, (f, u) => f.Date.Past())
                    .RuleFor(u => u.EmailAddress, (f, u) => f.Internet.Email());
            var client = clientFaker.Generate();
            var invoice = new Invoice
            {
                InvoiceDate = DateTime.Now,
                InvoiceNumber = "00010" + new Random().Next().ToString(),
                InvoiceLineItems = new List<InvoiceLineItem>()
                {
                    new InvoiceLineItem
                    {
                        Quantity= 10,
                        Service = new Service
                        {
                            Rate = 10.90m
                        }
                    }
                }
            };
            client.Invoices.Add(invoice);

            context.Clients.Add(client);

            context.SaveChanges();

        }
    }
}
