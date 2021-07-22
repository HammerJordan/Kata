using System.Collections.Generic;
using BuilderTestSample.Model;

namespace BuilderTestSample.Tests.TestBuilders
{
    public class CustomerBuilder
    {
        private int creditRating;
        private string firstName;
        private Address homeAddress;
        private int id;
        private string lastName;
        private readonly List<Order> orderHistory;
        private decimal totalPurches;

        public CustomerBuilder()
        {
            orderHistory = new List<Order>();
        }

        public CustomerBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public CustomerBuilder WithAddress(Address address)
        {
            homeAddress = address;
            return this;
        }

        public CustomerBuilder WithName(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            return this;
        }

        public CustomerBuilder WithCreditRating(int credit)
        {
            creditRating = credit;
            return this;
        }

        public CustomerBuilder WithTotalPurchases(int purches)
        {
            totalPurches = purches;
            return this;
        }

        public Customer Build()
        {
            return new(id)
            {
                FirstName = firstName,
                LastName = lastName,
                CreditRating = creditRating,
                HomeAddress = homeAddress,
                OrderHistory = orderHistory,
                TotalPurchases = totalPurches
            };
        }

        public static implicit operator Customer(CustomerBuilder builder)
        {
            return builder.Build();
        }
    }
}