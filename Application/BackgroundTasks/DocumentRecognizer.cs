using System;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Domain.Models.Base;
using Application.Interfaces;
using Application.Constants;
using Application.Helpers;

namespace Application.BackgroundTasks
{
    public class DocumentRecognizer : IDocumentRecognizer
    {
        private DocumentAnalysisClient _client;
        private readonly IAppDbContext _dbContext;

        public DocumentRecognizer(IAppDbContext dbContext, IConfiguration configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException("IConfiguration");

            if(dbContext is null)
                throw new ArgumentNullException("IDbContext");

            _dbContext = dbContext;

            string endpoint = configuration["DocumentRecognizerEndpoint"];
            string apiKey = configuration["DocumentRecognizerApiKey"];

            var credential = new AzureKeyCredential(apiKey);
            _client = new DocumentAnalysisClient(new Uri(endpoint), credential);
        }

        public async Task RecognizeAsync(string path)
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

