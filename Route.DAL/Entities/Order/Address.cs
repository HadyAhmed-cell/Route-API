﻿namespace Route.DAL.Entities.Order
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string firstName, string lastName, string country, string city, string street, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Street = street;
            ZipCode = zipCode;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}
