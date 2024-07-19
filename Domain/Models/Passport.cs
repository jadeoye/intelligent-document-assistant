using System;
using Domain.Models.Base;

namespace Domain.Entities.Models
{
	public class Passport : DocumentModel
	{
        public string? CountryRegion { get; private set; }
        public DateTimeOffset? DateOfBirth { get; private set; }
        public DateTimeOffset? DateOfExpiration { get; private set; }
        public DateTimeOffset? DateOfIssue { get; private set; }
        public string? DocumentNumber { get; private set; }
        public string? DocumentType { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public MachineReadableZone? MachineReadableZone { get; private set; }
        public string? Nationality { get; private set; }
        public string? PersonalNumber { get; private set; }
        public string? PlaceOfBirth { get; private set; }
        public string? PlaceOfIssue { get; private set; }
        public char? Sex { get; private set; }

        public Passport() : base()
		{
            base.DocumentType = Enums.DocumentType.Passport;
		}

        public static Passport Create(string countryRegion, DateTimeOffset dateOfBirth, DateTimeOffset dateOfExpiration,
            DateTimeOffset dateOfIssue, string documentNumber, string documentType, string firstName, string lastName,
            MachineReadableZone machineReadableZone, string nationality, string personalNumber, string placeOfBirth,
            string placeOfIssue, char sex)
        {
            return new Passport
            {
                DateOfExpiration = dateOfExpiration,
                Sex = sex,
                CountryRegion = countryRegion,
                DateOfBirth = dateOfBirth,
                DateOfIssue = dateOfIssue,
                DocumentNumber = documentNumber,
                DocumentType = documentType,
                FirstName = firstName,
                LastName = lastName,
                MachineReadableZone = machineReadableZone,
                Nationality = nationality,
                PersonalNumber = personalNumber,
                PlaceOfBirth = placeOfBirth,
                PlaceOfIssue = placeOfIssue,
            };
        }
	}

    public class MachineReadableZone
    {
        public string? CountryRegion { get; private set; }
        public DateTimeOffset? DateOfBirth { get; private set; }
        public DateTimeOffset? DateOfExpiration { get; private set; }
        public string? DocumentNumber { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Nationality { get; private set; }
        public char? Sex { get; private set; }

        public static MachineReadableZone Create(string countryRegion, DateTimeOffset dateOfBirth, DateTimeOffset dateOfExpiration,
            string documentNumber, string firstName, string lastName, string nationality, char sex)
        {
            return new MachineReadableZone
            {
                DateOfBirth = dateOfBirth,
                Sex = sex,
                CountryRegion = countryRegion,
                DateOfExpiration = dateOfExpiration,
                DocumentNumber = documentNumber,
                FirstName = firstName,
                LastName = lastName,
                Nationality = nationality
            };
        }
    }
}

