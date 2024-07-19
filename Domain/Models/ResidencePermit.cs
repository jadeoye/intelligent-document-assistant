using System;
using Domain.Models.Base;

namespace Domain.Entities.Models
{
    public class ResidencePermit : DocumentModel
    {
        public ResidencePermit() : base()
        {
            DocumentType = Enums.DocumentType.ResidencePermit;
        }

        public string? Category { get; private set; }
        public string? CountryRegion { get; private set; }
        public DateTimeOffset? DateOfBirth { get; private set; }
        public DateTimeOffset? DateOfExpiration { get; private set; }
        public DateTimeOffset? DateOfIssue { get; private set; }
        public string? DocumentNumber { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? PlaceOfBirth { get; private set; }
        public char? Sex { get; private set; }

        public static ResidencePermit Create(string category, string countryRegion, DateTimeOffset dateOfBirth,
            DateTimeOffset dateOfExpiration, DateTimeOffset dateOfIssue, string documentNumber,
            string firstName, string lastName, string placeOfBirth, char sex)
        {
            return new ResidencePermit
            {
                Category = category,
                CountryRegion = countryRegion,
                DateOfBirth = dateOfBirth,
                Sex = sex,
                DateOfExpiration = dateOfExpiration,
                DateOfIssue = dateOfIssue,
                DocumentNumber = documentNumber,
                FirstName = firstName,
                LastName = lastName,
                PlaceOfBirth = placeOfBirth,
            };
        }
    }
}

