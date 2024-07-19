using System;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Domain.Entities.Models;

namespace DocumentAssistant.Helpers
{
    public class PassportExtractor
    {
        public static Passport Extract(AnalyzedDocument identityDocument)
        {
            string countryRegion = string.Empty;
            DateTimeOffset dateOfBirth = default;
            DateTimeOffset dateOfExpiration = default;
            DateTimeOffset dateOfIssue = default;
            string documentNumber = string.Empty;
            string documentType = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;
            string nationality = string.Empty;
            string personalNumber = string.Empty;
            string placeOfBirth = string.Empty;
            string placeOfIssue = string.Empty;
            char sex = default;

            if (identityDocument.Fields.TryGetValue("CountryRegion", out DocumentField countryRegionField))
                if (countryRegionField.FieldType == DocumentFieldType.CountryRegion)
                    countryRegion = countryRegionField.Value.AsCountryRegion();

            if (identityDocument.Fields.TryGetValue("DateOfBirth", out DocumentField dateOfBirthField))
                if (dateOfBirthField.FieldType == DocumentFieldType.Date)
                    dateOfBirth = dateOfBirthField.Value.AsDate();

            if (identityDocument.Fields.TryGetValue("DateOfExpiration", out DocumentField dateOfExpirationField))
                if (dateOfExpirationField.FieldType == DocumentFieldType.Date)
                    dateOfExpiration = dateOfExpirationField.Value.AsDate();

            if (identityDocument.Fields.TryGetValue("DateOfIssue", out DocumentField dateOfIssueField))
                if (dateOfIssueField.FieldType == DocumentFieldType.Date)
                    dateOfIssue = dateOfIssueField.Value.AsDate();

            if (identityDocument.Fields.TryGetValue("DocumentNumber", out DocumentField documentNumberField))
                if (documentNumberField.FieldType == DocumentFieldType.String)
                    documentNumber = documentNumberField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("DocumentType", out DocumentField documentTypeField))
                if (documentTypeField.FieldType == DocumentFieldType.String)
                    documentType = documentTypeField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("FirstName", out DocumentField firstNameField))
                if (firstNameField.FieldType == DocumentFieldType.String)
                    firstName = firstNameField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("LastName", out DocumentField lastNameField))
                if (lastNameField.FieldType == DocumentFieldType.String)
                    lastName = lastNameField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("Nationality", out DocumentField nationalityField))
                if (nationalityField.FieldType == DocumentFieldType.String)
                    nationality = nationalityField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("PersonalNumber", out DocumentField personalNumberField))
                if (personalNumberField.FieldType == DocumentFieldType.String)
                    personalNumber = personalNumberField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("PlaceOfBirth", out DocumentField placeOfBirthField))
                if (placeOfBirthField.FieldType == DocumentFieldType.String)
                    placeOfBirth = placeOfBirthField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("PlaceOfIssue", out DocumentField placeOfIssueField))
                if (placeOfIssueField.FieldType == DocumentFieldType.String)
                    placeOfIssue = placeOfIssueField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("Sex", out DocumentField sexField))
                if (sexField.FieldType == DocumentFieldType.String)
                    sex = sexField.Value.AsString().First();

            var machineReadableZone = MachineReadableZone.Create(countryRegion, dateOfBirth, dateOfExpiration,
                documentNumber, firstName, lastName, nationality, sex);

            var passport = Passport.Create(countryRegion, dateOfBirth, dateOfExpiration, dateOfIssue, documentNumber,
                documentType, firstName, lastName, machineReadableZone, nationality, personalNumber, placeOfBirth,
                placeOfIssue, sex);

            return passport;
        }
    }
}

