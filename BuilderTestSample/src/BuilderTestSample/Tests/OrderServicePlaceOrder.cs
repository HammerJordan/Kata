using System;
using System.Collections.Generic;
using BuilderTestSample.Exceptions;
using BuilderTestSample.Model;
using BuilderTestSample.Services;
using BuilderTestSample.Tests.TestBuilders;
using Xunit;

namespace BuilderTestSample.Tests
{
    public class OrderServicePlaceOrder
    {
        private readonly OrderService OrderService = new();

        public static CustomerBuilder ValidCustomerBuilder()
        {
            return new CustomerBuilder()
                .WithId(1)
                .WithAddress(ValidAddressBuilder())
                .WithName("First", "Last")
                .WithCreditRating(201)
                .WithTotalPurchases(1);
        }

        public static OrderBuilder ValidOrderBuilder()
        {
            return new OrderBuilder()
                .WithId(0)
                .WithAmount(100)
                .WithCustomer(ValidCustomerBuilder());
        }

        public static AddressBuilder ValidAddressBuilder()
        {
            return new AddressBuilder()
                .WithStreet1("Street1")
                .WithCity("City")
                .WithState("State")
                .WithPostalcode("Postal")
                .WithCountry("Canada");
        }

        [Fact]
        public void ValidOrder()
        {
            OrderService.PlaceOrder(ValidOrderBuilder().Build());
        }

        public static IEnumerable<object[]> InvalidOrderData => new List<object[]>
        {
            new object[]
            {
                ValidOrderBuilder()
                    .WithId(123)
                    .Build()
            },
            new object[]
            {
                ValidOrderBuilder()
                    .WithAmount(0)
                    .Build()
            },
            new object[]
            {
                ValidOrderBuilder()
                    .WithCustomer(null)
                    .Build()
            }
        };

        [Theory]
        [MemberData(nameof(InvalidOrderData))]
        public void InvalidOrderThrowsInvalidOrderException(Order order)
        {
            Assert.Throws<InvalidOrderException>(() => OrderService.PlaceOrder(order));
        }

        public static IEnumerable<object[]> InvalidCustomerData => new List<object[]>
        {
            new object[]
            {
                ValidCustomerBuilder()
                    .WithId(0),
                typeof(InvalidCustomerException)
            },
            new object[]
            {
                ValidCustomerBuilder()
                    .WithAddress(null),
                typeof(InvalidCustomerException)
            },
            new object[]
            {
                ValidCustomerBuilder()
                    .WithName(null, ""),
                typeof(InvalidCustomerException)
            },
            new object[]
            {
                ValidCustomerBuilder()
                    .WithCreditRating(200),
                typeof(InsufficientCreditException)
            },
            new object[]
            {
                ValidCustomerBuilder()
                    .WithTotalPurchases(0),
                typeof(InvalidCustomerException)
            }
        };

        [Theory]
        [MemberData(nameof(InvalidCustomerData))]
        public void InvalidCustomerThrowsException(Customer customer, Type exception)
        {
            var order = ValidOrderBuilder()
                .WithCustomer(customer)
                .Build();
            Assert.Throws(exception, () => OrderService.PlaceOrder(order));
        }

        public static IEnumerable<object[]> InvalidAddressData => new List<object[]>
        {
            new object[]
            {
                ValidAddressBuilder()
                    .WithStreet1("")
            },
            new object[]
            {
                ValidAddressBuilder()
                    .WithCity(null)
            },
            new object[]
            {
                ValidAddressBuilder()
                    .WithState("")
            },
            new object[]
            {
                ValidAddressBuilder()
                    .WithPostalcode(null)
            },
            new object[]
            {
                ValidAddressBuilder()
                    .WithCountry("")
            }
        };

        [Theory]
        [MemberData(nameof(InvalidAddressData))]
        public void InvalidAddressThrowsException(Address address)
        {
            var validCustomer = ValidCustomerBuilder().WithAddress(address);
            var order = ValidOrderBuilder()
                .WithCustomer(validCustomer)
                .Build();
            Assert.Throws<InvalidAddressException>(() => OrderService.PlaceOrder(order));
        }
        
        [Fact]
        public void FlagIsExpeditedOnLargeOrdersWithGoodCreditRating()
        {
            var validCustomer = ValidCustomerBuilder()
                .WithCreditRating(501)
                .WithTotalPurchases(5001);
            var order = ValidOrderBuilder()
                .WithCustomer(validCustomer)
                .Build();
            
            OrderService.PlaceOrder(order);
            
            Assert.True(order.IsExpedited);
        }
        
        
        [Fact]
        public void ValidOrdersAddedToCustomer()
        {
            var validCustomer = ValidCustomerBuilder();
            var order = ValidOrderBuilder()
                .WithCustomer(validCustomer)
                .Build();
            
            OrderService.PlaceOrder(order);
            
           Assert.Contains(order, order.Customer.OrderHistory);
        }
        
        [Fact]
        public void ValidOrdersIncreaseCustomerTotalPurchases()
        {
            var validCustomer = ValidCustomerBuilder();
            var order = ValidOrderBuilder()
                .WithCustomer(validCustomer)
                .Build();

            decimal old = order.Customer.TotalPurchases;
            
            OrderService.PlaceOrder(order);
            
            Assert.True(order.Customer.TotalPurchases == old + 1);
        }
    }
}