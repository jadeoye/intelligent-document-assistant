using System;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Domain.Models;
using Domain.Models.Common;

namespace Application.Helpers
{
    public class NationalIdentityCardExtractor
    {
        public static NationalIdentityCard Extract(AnalyzedDocument identityDocument)
        {
            Address address = default;
            DateTimeOffset dateOfBirth = default;
            DateTimeOffset dateOfExpiration = default;
            DateTimeOffset dateOfIssue = default;
            string documentDecriminator = string.Empty;
            string documentNumber = string.Empty;
            string firstName = string.Empty;
            string height = string.Empty;
            string lastName = string.Empty;
            string personalNumber = string.Empty;
            string placeOfBirth = string.Empty;
            char sex = default;

            if (identityDocument.Fields.TryGetValue("Address", out DocumentField addressField))
            {
                if (addressField.FieldType == DocumentFieldType.String)
                {
                    var _address = addressField.Value.AsAddress();
                    address = Address.Create(_address.City, _address.CityDistrict, _address.CountryRegion,
                        _address.House, _address.HouseNumber, _address.Level, _address.PoBox, _address.PostalCode,
                        _address.Road, _address.State, _address.StateDistrict, _address.StreetAddress, _address.Suburb, _address.Unit);
                }
            }

            if (identityDocument.Fields.TryGetValue("DateOfBirth", out DocumentField dateOfBirthField))
                if (dateOfBirthField.FieldType == DocumentFieldType.Date)
                    dateOfBirth = dateOfBirthField.Value.AsDate();

            if (identityDocument.Fields.TryGetValue("DateOfExpiration", out DocumentField dateOfExpirationField))
                if (dateOfExpirationField.FieldType == DocumentFieldType.Date)
                    dateOfExpiration = dateOfExpirationField.Value.AsDate();

            if (identityDocument.Fields.TryGetValue("DateOfIssue", out DocumentField dateOfIssueField))
                if (dateOfIssueField.FieldType == DocumentFieldType.Date)
                    dateOfIssue = dateOfIssueField.Value.AsDate();

            if (identityDocument.Fields.TryGetValue("DocumentDiscriminator", out DocumentField documentDescriminatorField))
                if (documentDescriminatorField.FieldType == DocumentFieldType.String)
                    documentDecriminator = documentDescriminatorField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("DocumentNumber", out DocumentField documentNumberField))
                if (documentNumberField.FieldType == DocumentFieldType.String)
                    documentNumber = documentNumberField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("FirstName", out DocumentField firstNameField))
                if (firstNameField.FieldType == DocumentFieldType.String)
                    firstName = firstNameField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("Height", out DocumentField heightField))
                if (heightField.FieldType == DocumentFieldType.String)
                    height = heightField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("LastName", out DocumentField lastNameField))
                if (lastNameField.FieldType == DocumentFieldType.String)
                    lastName = lastNameField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("PersonalNumber", out DocumentField personalNumberField))
                if (personalNumberField.FieldType == DocumentFieldType.String)
                    personalNumber = personalNumberField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("PlaceOfBirth", out DocumentField placeOfBirthField))
                if (placeOfBirthField.FieldType == DocumentFieldType.String)
                    placeOfBirth = placeOfBirthField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("Sex", out DocumentField sexField))
                if (sexField.FieldType == DocumentFieldType.String)
                    sex = sexField.Value.AsString().First();

            var nationalIdentityCard = NationalIdentityCard.Create(address, dateOfBirth, dateOfExpiration, dateOfIssue,
                documentDecriminator, documentNumber, firstName, lastName, height, personalNumber, placeOfBirth, sex);

            return nationalIdentityCard;
        }
    }
}

