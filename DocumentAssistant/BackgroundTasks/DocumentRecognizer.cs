using System;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Application.Common.Interfaces;
using DocumentAssistant.Helpers;
using Domain.Entities;
using DocumentAssistant.Constants;
using DocumentAssistant.App.Interfaces;
using Microsoft.Extensions.Configuration;
using Domain.Models.Base;

namespace DocumentAssistant.BackgroundTasks
{
    public class DocumentRecognizer : IDocumentRecognizer
    {
        private DocumentAnalysisClient _client;
        private readonly IAppDbContext _dbContext;

      
        public DocumentRecognizer(IAppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;

            string endpoint = configuration["RecognizerEndpoint"];
            string apiKey = configuration["RecognizerApiKey"];

            var credential = new AzureKeyCredential(apiKey);
            _client = new DocumentAnalysisClient(new Uri(endpoint), credential);
        }

        public async Task Recognize(string path)
        {
            try
            {
                AnalyzeDocumentOperation operation = await _client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-idDocument", new FileStream(path, FileMode.Open, FileAccess.Read));

                AnalyzeResult identityDocuments = operation.Value;

                AnalyzedDocument identityDocument = identityDocuments.Documents.Single();

                if (identityDocument.DocumentType == RecognizerDocumentType.NationalIdentityCard)
                    await ExtractNationalIdentityCardInformation(path, identityDocument);
                else if (identityDocument.DocumentType == RecognizerDocumentType.ResidencePermit)
                    await ExtractResidencePermitInformation(path, identityDocument);
                else if (identityDocument.DocumentType == RecognizerDocumentType.Passport)
                    await ExtractPassportInformation(path, identityDocument);
                else if (identityDocument.DocumentType == RecognizerDocumentType.DriverLicense)
                    await ExtractDriverLicenseInformation(path, identityDocument);
                else if (identityDocument.DocumentType == RecognizerDocumentType.SocialSecurityCard)
                    await ExtractSocialSecurityCardInformation(path, identityDocument);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PersistToDatabase(string path, DocumentModel model)
        {
            var document = Document.Create(path, model);
            await _dbContext.Documents.AddAsync(document);
            await _dbContext.SaveChangesAsync();
        }

        private async Task ExtractNationalIdentityCardInformation(string path, AnalyzedDocument identityDocument)
        {
            var nationalIdentityCard = NationalIdentityCardExtractor.Extract(identityDocument);
            await PersistToDatabase(path, nationalIdentityCard);
        }

        private async Task ExtractResidencePermitInformation(string path, AnalyzedDocument identityDocument)
        {
            var residencePermit = ResidencePermitExtractor.Extract(identityDocument);
            await PersistToDatabase(path, residencePermit);
        }

        private async Task ExtractPassportInformation(string path, AnalyzedDocument identityDocument)
        {
            var passport = PassportExtractor.Extract(identityDocument);
            await PersistToDatabase(path, passport);
        }

        private async Task ExtractDriverLicenseInformation(string path, AnalyzedDocument identityDocument)
        {
            var driverLicense = DriverLicenseExtractor.Extract(identityDocument);
            await PersistToDatabase(path, driverLicense);
        }

        private async Task ExtractSocialSecurityCardInformation(string path, AnalyzedDocument identityDocument)
        {
            var socialSecurityCard = SocialSecurityCardExtractor.Extract(identityDocument);
            await PersistToDatabase(path, socialSecurityCard);
        }
    }
}

