using BuilderTestSample.Exceptions;
using BuilderTestSample.Model;

namespace BuilderTestSample.Services
{
    public class OrderService
    {
        public void PlaceOrder(Order order)
        {
            ValidateOrder(order);

            ExpediteOrder(order);

            AddOrderToCustomerHistory(order);
        }

        private void ValidateOrder(Order order)
        {
            // throw InvalidOrderException unless otherwise noted.

            // TODO: order ID must be zero (it's a new order)
            if (order.Id != 0)
                throw new InvalidOrderException("Order ID must be 0.");
            // TODO: order amount must be greater than zero
            if (order.TotalAmount <= 0)
                throw new InvalidOrderException("Order Amount Must be greater then 0");
            // TODO: order must have a customer (customer is not null)
            if (order.Customer == null)
                throw new InvalidOrderException("Order Must have a customer");

            ValidateCustomer(order.Customer);
        }

        private void ValidateCustomer(Customer customer)
        {
            // throw InvalidCustomerException unless otherwise noted
            // create a CustomerBuilder to implement the tests for these scenarios

            // TODO: customer must have an ID > 0
            if (customer.Id <= 0)
                throw new InvalidCustomerException("Customer Id must be greater then 0");
            // TODO: customer must have an address (it is not null)
            if (customer.HomeAddress == null)
                throw new InvalidCustomerException("Customer Must have a valid address");
            // TODO: customer must have a first and last name
            if (string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName))
                throw new InvalidCustomerException("Customer Must have a valid Name");
            // TODO: customer must have credit rating > 200 (otherwise throw InsufficientCreditException)
            if (customer.CreditRating <= 200)
                throw new InsufficientCreditException("Customer must have credit rating > 200");
            // TODO: customer must have total purchases >= 0
            if (customer.TotalPurchases <= 0)
                throw new InvalidCustomerException("Customer must have total purchases more then 0");
            ValidateAddress(customer.HomeAddress);
        }

        private void ValidateAddress(Address homeAddress)
        {
            // throw InvalidAddressException unless otherwise noted
            // create an AddressBuilder to implement the tests for these scenarios

            // TODO: street1 is required (not null or empty)
            if (string.IsNullOrEmpty(homeAddress.Street1))
                throw new InvalidAddressException("Address must have a street1");
            // TODO: city is required (not null or empty)
            if (string.IsNullOrEmpty(homeAddress.City))
                throw new InvalidAddressException("Address must have a City");
            // TODO: state is required (not null or empty)
            if (string.IsNullOrEmpty(homeAddress.State))
                throw new InvalidAddressException("Address must have a State");
            // TODO: postalcode is required (not null or empty)
            if (string.IsNullOrEmpty(homeAddress.PostalCode))
                throw new InvalidAddressException("Address must have a PostalCode");
            // TODO: country is required (not null or empty)
            if (string.IsNullOrEmpty(homeAddress.Country))
                throw new InvalidAddressException("Address must have a Country");
        }

        private void ExpediteOrder(Order order)
        {
            // TODO: if customer's total purchases > 5000 and credit rating > 500 set IsExpedited to true
            if (order.Customer.TotalPurchases > 5000 && order.Customer.CreditRating > 500)
                order.IsExpedited = true;
        }

        private void AddOrderToCustomerHistory(Order order)
        {
            // TODO: add the order to the customer
            order.Customer.OrderHistory.Add(order);
            // TODO: update the customer's total purchases property
            order.Customer.TotalPurchases++;
            

        }
    }
}