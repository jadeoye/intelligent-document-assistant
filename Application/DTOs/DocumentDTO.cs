using System;
using Domain.Converters;
using Domain.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Domain.Enums;

namespace Application.DTOs
{
    public class DocumentDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Data { get; set; }
        public DocumentType Type { get; set; }

        public DocumentModel Model
        {
            get => JsonSerializer.Deserialize<DocumentModel>(Data, new JsonSerializerOptions { Converters = { new DocumentConverter(Type) } });
            set => Data = JsonSerializer.Serialize(value, new JsonSerializerOptions { Converters = { new DocumentConverter(Type) } });
        }
    }
}

