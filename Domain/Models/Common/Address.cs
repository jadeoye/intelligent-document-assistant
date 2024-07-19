using System;
namespace Domain.Models.Common
{
    public class Address
    {
        public string? City { get; private set; }
        public string? CityDistrict { get; private set; }
        public string? CountryRegion { get; private set; }
        public string? House { get; private set; }
        public string? HouseNumber { get; private set; }
        public string? Level { get; private set; }
        public string? PoBox { get; private set; }
        public string? PostalCode { get; private set; }
        public string? Road { get; private set; }
        public string? State { get; private set; }
        public string? StateDistrict { get; private set; }
        public string? StreetAddress { get; private set; }
        public string? Suburb { get; private set; }
        public string? Unit { get; private set; }

        public static Address Create(string city, string cityDistrict, string countryRegion, string house,
            string houseNumber, string level, string poBox, string postalCode, string road, string state,
            string stateDistrict, string streetAddress, string suburb, string unit)
        {
            return new Address
            {
                City = city,
                CityDistrict = cityDistrict,
                CountryRegion = countryRegion,
                House = house,
                HouseNumber = houseNumber,
                Level = level,
                PoBox = poBox,
                PostalCode = postalCode,
                Road = road,
                State = state,
                StateDistrict = stateDistrict,
                StreetAddress = streetAddress,
                Suburb = suburb,
                Unit = unit
            };
        }
    }
}

