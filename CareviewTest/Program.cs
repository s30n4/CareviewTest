using Autofac;
using CareviewTest.Data;
using CareviewTest.Queries;
using CareviewTest.Queries.Interfaces;
using CareviewTest.Repositories;
using CareviewTest.Repositories.Interfaces;
using System;

namespace CareviewTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GetClientInvoiceQuery>()
                .As<IGetClientInvoiceQuery>()
                .OnActivating(async s => await s.Instance.GetClientDtoAsync())
                .AutoActivate();

            builder.RegisterType<CareviewDbContext>()
             .InstancePerLifetimeScope();


            var container = builder.Build();

            Console.ReadLine();
        }
    }
}
