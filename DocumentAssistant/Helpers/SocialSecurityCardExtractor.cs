using System;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Domain.Entities.Models;

namespace DocumentAssistant.Helpers
{
	public class SocialSecurityCardExtractor
	{
		public static SocialSecurityCard Extract(AnalyzedDocument identityDocument)
		{
            DateTimeOffset dateOfIssue = default;
            string documentNumber = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;

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

            var socialSecurityCard = SocialSecurityCard.Create(dateOfIssue, documentNumber, firstName, lastName);

            return socialSecurityCard;
        }
    }
}

