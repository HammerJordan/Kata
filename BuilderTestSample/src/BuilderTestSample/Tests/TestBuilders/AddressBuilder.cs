using BuilderTestSample.Model;

namespace BuilderTestSample.Tests.TestBuilders
{
    public class AddressBuilder
    {
        private string city;
        private string country;
        private string postalcode;
        private string state;
        private string street1;
        private string street2;
        private string street3;

        public AddressBuilder WithStreet1(string name)
        {
            street1 = name;
            return this;
        }

        public AddressBuilder WithStreet2(string name)
        {
            street2 = name;
            return this;
        }

        public AddressBuilder WithStreet3(string name)
        {
            street3 = name;
            return this;
        }

        public AddressBuilder WithCity(string name)
        {
            city = name;
            return this;
        }

        public AddressBuilder WithState(string name)
        {
            state = name;
            return this;
        }

        public AddressBuilder WithPostalcode(string name)
        {
            postalcode = name;
            return this;
        }

        public AddressBuilder WithCountry(string name)
        {
            country = name;
            return this;
        }

        public Address Build()
        {
            return new()
            {
                City = city,
                Country = country,
                PostalCode = postalcode,
                State = state,
                Street1 = street1,
                Street2 = street2,
                Street3 = street3
            };
        }

        public static implicit operator Address(AddressBuilder addressBuilder)
        {
            return addressBuilder.Build();
        }
    }
}