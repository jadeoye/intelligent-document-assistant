using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Enums;
using Domain.Entities.Models;
using Domain.Models.Base;

namespace Domain.Converters
{
    public class DocumentConverter : JsonConverter<DocumentModel>
    {
        private readonly DocumentType _documentType;
        public DocumentConverter(DocumentType documentType)
        {
            _documentType = documentType;
        }

        public override DocumentModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                return _documentType switch
                {
                    DocumentType.NationalIdentityCard => JsonSerializer.Deserialize<NationalIdentityCard>(doc.RootElement.GetRawText(), options),
                    _ => throw new Exception()
                };
            }
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DocumentModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.GetType(), options);
        }
    }
}

