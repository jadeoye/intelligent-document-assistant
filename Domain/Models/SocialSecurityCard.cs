using System;
using Domain.Models.Base;

namespace Domain.Entities.Models
{
	public class SocialSecurityCard : DocumentModel
	{
        public DateTimeOffset? DateOfIssue { get; private set; }
        public string? DocumentNumber { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }

        public SocialSecurityCard() : base()
		{
			DocumentType = Enums.DocumentType.SocialSecurityCard;
		}

		public static SocialSecurityCard Create(DateTimeOffset dateOfIssue, string documentNumber,
			string firstName, string lastName)
		{
			return new SocialSecurityCard
			{
				DateOfIssue = dateOfIssue,
				DocumentNumber = documentNumber,
				FirstName = firstName,
				LastName = lastName
			};
		}
	}
}

