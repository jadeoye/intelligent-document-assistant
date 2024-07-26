using System;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Domain.Models;

namespace Application.Helpers
{
	public class ResidencePermitExtractor
	{
		public static ResidencePermit Extract(AnalyzedDocument identityDocument)
		{
			string category = string.Empty;
			string countryRegion = string.Empty;
			DateTimeOffset dateOfBirth = default;
			DateTimeOffset dateOfExpiration = default;
			DateTimeOffset dateOfIssue = default;
			string documentNumber = string.Empty;
			string firstName = string.Empty;
			string lastName = string.Empty;
			string placeOfBirth = string.Empty;
			char sex = default;
            
            if (identityDocument.Fields.TryGetValue("Category", out DocumentField categoryField))
                if (categoryField.FieldType == DocumentFieldType.String)
                    category = categoryField.Value.AsString();

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

            if (identityDocument.Fields.TryGetValue("FirstName", out DocumentField firstNameField))
                if (firstNameField.FieldType == DocumentFieldType.String)
                    firstName = firstNameField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("LastName", out DocumentField lastNameField))
                if (lastNameField.FieldType == DocumentFieldType.String)
                    lastName = lastNameField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("PlaceOfBirth", out DocumentField placeOfBirthField))
                if (placeOfBirthField.FieldType == DocumentFieldType.String)
                    placeOfBirth = placeOfBirthField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("Sex", out DocumentField sexField))
                if (sexField.FieldType == DocumentFieldType.String)
                    sex = sexField.Value.AsString().First();

            var residencePermit = ResidencePermit.Create(category, countryRegion, dateOfBirth, dateOfExpiration,
                dateOfIssue, documentNumber, firstName, lastName, placeOfBirth, sex);

            return residencePermit;
        }
    }
}

