using System;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Domain.Entities.Models;
using Domain.Models;
using Domain.Models.Common;

namespace DocumentAssistant.Helpers
{
	public class DriverLicenseExtractor
	{
		public static DriverLicense Extract(AnalyzedDocument identityDocument)
		{
            Address address = default;
            string countryRegion = string.Empty;
            DateTimeOffset dateOfBirth = default;
            DateTimeOffset dateOfExpiration = default;
            DateTimeOffset dateOfIssue = default;
            string documentDecriminator = string.Empty;
            string documentNumber = string.Empty;
            string endorsements = string.Empty;
            string eyeColor = string.Empty;
            string firstName = string.Empty;
            string height = string.Empty;
            string lastName = string.Empty;
            string region = string.Empty;
            string restrictions = string.Empty;
            string vehicleClassifications = string.Empty;
            string weight = string.Empty;
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

            if (identityDocument.Fields.TryGetValue("DocumentDiscriminator", out DocumentField documentDescriminatorField))
                if (documentDescriminatorField.FieldType == DocumentFieldType.String)
                    documentDecriminator = documentDescriminatorField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("DocumentNumber", out DocumentField documentNumberField))
                if (documentNumberField.FieldType == DocumentFieldType.String)
                    documentNumber = documentNumberField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("Endorsements", out DocumentField endorsementsField))
                if (endorsementsField.FieldType == DocumentFieldType.String)
                    endorsements = endorsementsField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("EyeColor", out DocumentField eyeColorField))
                if (eyeColorField.FieldType == DocumentFieldType.String)
                    eyeColor = eyeColorField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("FirstName", out DocumentField firstNameField))
                if (firstNameField.FieldType == DocumentFieldType.String)
                    firstName = firstNameField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("Height", out DocumentField heightField))
                if (heightField.FieldType == DocumentFieldType.String)
                    height = heightField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("LastName", out DocumentField lastNameField))
                if (lastNameField.FieldType == DocumentFieldType.String)
                    lastName = lastNameField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("Region", out DocumentField regionField))
                if (regionField.FieldType == DocumentFieldType.String)
                    region = regionField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("Restrictions", out DocumentField restrictionsField))
                if (restrictionsField.FieldType == DocumentFieldType.String)
                    restrictions = restrictionsField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("VehicleClassifications", out DocumentField vehicleClassificationsField))
                if (vehicleClassificationsField.FieldType == DocumentFieldType.String)
                    vehicleClassifications = vehicleClassificationsField.Value.AsString();

            if (identityDocument.Fields.TryGetValue("Sex", out DocumentField sexField))
                if (sexField.FieldType == DocumentFieldType.String)
                    sex = sexField.Value.AsString().First();

            var driverLicense = DriverLicense.Create(address, countryRegion, dateOfBirth, dateOfExpiration, dateOfIssue,
                documentDecriminator, documentNumber, endorsements, eyeColor, firstName, lastName, height, region,
                restrictions, vehicleClassifications, weight, sex);

            return driverLicense;
        }
    }
}

