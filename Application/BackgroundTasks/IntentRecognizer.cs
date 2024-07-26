using System;
using Application.Concretes;
using Application.Interfaces;
using Azure;
using Azure.AI.Language.Conversations;
using Azure.Core;
using Azure.Core.Serialization;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace Application.BackgroundTasks
{
	public class IntentRecognizer : IIntentRecognizer
	{
        private string _projectName;
        private string _deploymentName;
        private ConversationAnalysisClient _client;

		public IntentRecognizer(IConfiguration configuration)
		{
            if (configuration is null)
                throw new ArgumentNullException("IConfiguration");

            _projectName = configuration["IntentProjectName"];
            _deploymentName = configuration["IntentDeploymentName"];

            var endpoint = new Uri(configuration["IntentEndpoint"]);
            var credential = new AzureKeyCredential(configuration["IntentApiKey"]);

            _client = new ConversationAnalysisClient(endpoint, credential);
		}

        public async Task<RecognizedIntent> RecognizeAsync(string speech)
        {
            try
            {
                var data = new
                {
                    AnalysisInput = new
                    {
                        ConversationItem = new
                        {
                            Text = Normalize(speech),
                            Id = "1",
                            ParticipantId = "1"
                        }
                    },
                    Parameters = new
                    {
                        ProjectName = _projectName,
                        DeploymentName = _deploymentName,
                        StringIndexType = "Utf16CodeUnit"
                    },
                    Kind = "Conversation"
                };

                var response = await _client.AnalyzeConversationAsync(RequestContent.Create(data, JsonPropertyNames.CamelCase));

                dynamic conversationalTaskResult = response.Content.ToDynamicFromJson(JsonPropertyNames.CamelCase);
                dynamic conversationPrediction = conversationalTaskResult.Result.Prediction;

                foreach (dynamic intent in conversationPrediction.Intents)
                {
                    Console.WriteLine($"Category: {intent.Category}");
                    Console.WriteLine($"Confidence: {intent.ConfidenceScore}");

                    break;
                }

                RecognizedIntent recognizedIntent = new();

                foreach (var entity in conversationPrediction.Entities)
                {
                    if (entity.Category == "DocumentType")
                    {
                        recognizedIntent.DocumentType = entity.Text.ToString().ToLower() switch
                        {
                            "passport" => DocumentType.Passport,
                            "social security card" => DocumentType.SocialSecurityCard,
                            "driver's license" => DocumentType.DriversLicense,
                            "resident permit" => DocumentType.ResidencePermit,
                            "national id card" => DocumentType.NationalIdentityCard,
                            _ => throw new Exception()
                        };
                    }

                    if (entity.Category == "NameType")
                        recognizedIntent.NameType = entity.Text;

                    if (entity.Category == "FirstName" || entity.Category == "LastName")
                        recognizedIntent.Name = entity.Text;
                }

                return recognizedIntent;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string Normalize(string speech)
        {
            string _formattedSpeech = speech.ToLower()
                .Replace("passports", "passport")
                .Replace("national id cards", "national id card")
                .Replace("resident permits", "resident permit")
                .Replace("driver's licenses", "driver's license")
                .Replace("social security cards", "social security card");

            return _formattedSpeech;
        }
    }
}

