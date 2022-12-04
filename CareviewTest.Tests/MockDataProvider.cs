using CareviewTest.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CareviewTest.Tests
{
    internal static class MockDataProvider
    {
        internal static List<Client> GetFakeClientList(decimal quantity = 10m)
        {
            var client = GetFakeClient(quantity);

            return new List<Client> { client };
        }
        internal static Client GetFakeClient(decimal quantity = 10m)
        {
            var client = new Client { Name = "test" };

            GetFakeInvoice(client, quantity);

            return client;
        }

        internal static Client GetFakeInvoice(Client client, decimal quantity = 10m)
        {
            var invoice = new Invoice
            {
                InvoiceDate = DateTime.Now,
                InvoiceNumber = "00010" + new Random().Next().ToString(),
                InvoiceLineItems = new List<InvoiceLineItem>()
                {
                    new InvoiceLineItem
                    {
                        Quantity=quantity,
                        Service = new Service
                        {
                            Rate = 10.90m
                        }
                    }
                }
            };

            client.Invoices.Add(invoice);

            return client;
        }

        public static DbSet<T> MockDbSet<T>(this IList<T> list) where T : class
        {
            var data = list.AsQueryable();

            var mock = new Mock<DbSet<T>>();

            mock.As<IDbAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));

            mock.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(data.Provider));

            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mock.Setup(m => m.AsNoTracking()).Returns(mock.Object);
            mock.Setup(m => m.Include(It.IsAny<string>())).Returns(mock.Object);

            mock.Setup(x => x.Add(It.IsAny<T>())).Returns<T>((input) =>
            {
                list.Add(input);
                return input;
            });

            return mock.Object;
        }

        internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;

            internal TestDbAsyncQueryProvider(IQueryProvider inner)
            {
                _inner = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestDbAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestDbAsyncEnumerable<TElement>(expression);
            }

            public object Execute(Expression expression)
            {
                return _inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return _inner.Execute<TResult>(expression);
            }

            public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute(expression));
            }

            public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute<TResult>(expression));
            }
        }

        internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
        {
            public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
                : base(enumerable)
            { }

            public TestDbAsyncEnumerable(Expression expression)
                : base(expression)
            { }

            public IDbAsyncEnumerator<T> GetAsyncEnumerator()
            {
                return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            }

            IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
            {
                return GetAsyncEnumerator();
            }

            IQueryProvider IQueryable.Provider
            {
                get { return new TestDbAsyncQueryProvider<T>(this); }
            }
        }

        internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestDbAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public void Dispose()
            {
                _inner.Dispose();
            }

            public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_inner.MoveNext());
            }

            public T Current
            {
                get { return _inner.Current; }
            }

            object IDbAsyncEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}
