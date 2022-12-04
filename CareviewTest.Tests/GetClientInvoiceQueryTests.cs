using CareviewTest.Data;
using CareviewTest.Models;
using CareviewTest.Queries;
using CareviewTest.Repositories;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareviewTest.Tests
{
    [TestFixture(Description = "GetClientInvoiceQueryTests", Category = "Unit")]
    public class GetClientInvoiceQueryTests
    {
        private Mock<CareviewDbContext> mockContext;

        [SetUp]
        public void Init()
        {
            mockContext = new Mock<CareviewDbContext>();
        }

        private GetClientInvoiceQuery GetRequest(ClientRepository clientRepository)
        {
            return new GetClientInvoiceQuery(clientRepository);
        }

        [Test(Description = "1 Invoice - Quantity 10  ")]
        public async Task Handle_QueryForClient_ShouldReturnTotalInvoices_1_InvoiceQuantity_10()
        {
            //Arrange
            mockContext.Setup(c => c.Clients).Returns(MockDataProvider.MockDbSet(MockDataProvider.GetFakeClientList()));

            var subject = GetRequest(new ClientRepository(mockContext.Object));

            //Act

            var result = await subject.GetClientDtoAsync();

            //Assert
            //
            result.Count().ShouldBe(1);
            result.First().TotalInvoices.ShouldBe(1);
            result.First().InvoiceQuantity.ShouldBe(10m);

        }

        [Test(Description = "1 Invoice - Quantity 7 ")]
        public async Task Handle_QueryForClient_ShouldReturnTotalInvoices_1_InvoiceQuantity_7()
        {
            //Arrange
            mockContext.Setup(c => c.Clients).Returns(MockDataProvider.MockDbSet(MockDataProvider.GetFakeClientList(7m)));

            var subject = GetRequest(new ClientRepository(mockContext.Object));

            //Act
            var result = await subject.GetClientDtoAsync();

            //Assert
            result.Count().ShouldBe(1);
            result.First().TotalInvoices.ShouldBe(1);
            result.First().InvoiceQuantity.ShouldBe(7m);

        }

        [Test(Description = "3 Invoice - InvoiceQuantity 95 ")]
        public async Task Handle_QueryForClient_ShouldReturnTotalInvoices_3_InvoiceQuantity_95()
        {
            //Arrange
            var client = MockDataProvider.GetFakeClient();
            MockDataProvider.GetFakeInvoice(client, 70);
            MockDataProvider.GetFakeInvoice(client, 15);

            mockContext.Setup(c => c.Clients).Returns(MockDataProvider.MockDbSet(new List<Client> { client }));

            var subject = GetRequest(new ClientRepository(mockContext.Object));

            //Act
            var result = await subject.GetClientDtoAsync();

            //Assert
            result.Count().ShouldBe(1);
            result.First().TotalInvoices.ShouldBe(3);
            result.First().InvoiceQuantity.ShouldBe(95m);

        }

        [Test(Description = "2 Clients ")]
        public async Task Handle_QueryForClient_ShouldReturn_2Clients()
        {
            //Arrange
            mockContext.Setup(c => c.Clients).Returns(MockDataProvider.MockDbSet(new List<Client>
            {
                MockDataProvider.GetFakeClient(),
                MockDataProvider.GetFakeClient(5m)
            }));

            var subject = GetRequest(new ClientRepository(mockContext.Object));

            //Act
            var result = await subject.GetClientDtoAsync();

            //Assert
            result.Count().ShouldBe(2);
            result[0].TotalInvoices.ShouldBe(1);
            result[0].InvoiceQuantity.ShouldBe(10m);
            result[1].TotalInvoices.ShouldBe(1);
            result[1].InvoiceQuantity.ShouldBe(5m);

        }

    }
}