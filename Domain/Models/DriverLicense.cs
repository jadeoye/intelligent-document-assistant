using System;
using Domain.Models.Base;
using Domain.Models.Common;

namespace Domain.Models
{
    public class DriverLicense : DocumentModel
    {
        public Address? Address { get; private set; }
        public string? CountryRegion { get; private set; }
        public DateTimeOffset? DateOfBirth { get; private set; }
        public DateTimeOffset? DateOfExpiration { get; private set; }
        public DateTimeOffset? DateOfIssue { get; private set; }
        public string? DocumentDiscriminator { get; private set; }
        public string? DocumentNumber { get; private set; }
        public string? Endorsements { get; private set; }
        public string? EyeColor { get; private set; }
        public string? FirstName { get; private set; }
        public string? Height { get; private set; }
        public string? LastName { get; private set; }
        public string? Region { get; private set; }
        public string? Restrictions { get; private set; }
        public char? Sex { get; private set; }
        public string? VehicleClassifications { get; private set; }
        public string? Weight { get; private set; }

        public DriverLicense() : base()
        {
            Type = Enums.DocumentType.DriversLicense;
        }

        public static DriverLicense Create(Address address, string countryRegion, DateTimeOffset dateOfBirth,
            DateTimeOffset dateOfExpiration, DateTimeOffset dateOfIssue, string documentDiscriminator,
            string documentNumber, string endorsements, string eyeColor, string firstName, string lastName,
            string height, string region, string restrictions, string vehicleClassifications, string weight, char sex)
        {
            return new DriverLicense
            {
                Address = address,
                Sex = sex,
                DateOfBirth = dateOfBirth,
                CountryRegion = countryRegion,
                DateOfExpiration = dateOfExpiration,
                DateOfIssue = dateOfIssue,
                DocumentDiscriminator = documentDiscriminator,
                DocumentNumber = documentNumber,
                Endorsements = endorsements,
                EyeColor = eyeColor,
                FirstName = firstName,
                Height = height,
                LastName = lastName,
                Region = region,
                Restrictions = restrictions,
                VehicleClassifications = vehicleClassifications,
                Weight = weight
            };
        }
    }
}
