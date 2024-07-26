using System;
using System.Net;
using Domain.Models.Base;
using Domain.Models.Common;

namespace Domain.Models
{
    public class NationalIdentityCard : DocumentModel
    {
        public Address? Address { get; private set; }
        public DateTimeOffset? DateOfBirth { get; private set; }
        public DateTimeOffset? DateOfExpiration { get; private set; }
        public DateTimeOffset? DateOfIssue { get; private set; }
        public string? DocumentDiscriminator { get; private set; }
        public string? DocumentNumber { get; private set; }
        public string? FirstName { get; private set; }
        public string? Height { get; private set; }
        public string? LastName { get; private set; }
        public string? PersonalNumber { get; private set; }
        public string? PlaceOfBirth { get; private set; }
        public char? Sex { get; private set; }

        public NationalIdentityCard() : base()
        {
            DocumentType = Enums.DocumentType.NationalIdentityCard;
        }

        public static NationalIdentityCard Create(Address address, DateTimeOffset dateOfBirth, DateTimeOffset dateOfExpiration,
            DateTimeOffset dateOfIssue, string documentDiscriminator, string documentNumber, string firstName, string lastName,
            string height, string personalNumber, string placeOfBirth, char sex)
        {
            return new NationalIdentityCard
            {
                Address = address,
                DateOfBirth = dateOfBirth,
                Sex = sex,
                DateOfExpiration = dateOfExpiration,
                DateOfIssue = dateOfIssue,
                DocumentDiscriminator = documentDiscriminator,
                DocumentNumber = documentNumber,
                FirstName = firstName,
                Height = height,
                LastName = lastName,
                PersonalNumber = personalNumber,
                PlaceOfBirth = placeOfBirth,
            };
        }
    }
}

