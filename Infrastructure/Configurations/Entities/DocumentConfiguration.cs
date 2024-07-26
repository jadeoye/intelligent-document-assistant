using System;
using System.Text.Json;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Entities
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.Property(e => e.Data).IsRequired();
            builder.Property(e => e.Type).IsRequired();
            builder.Property(e => e.Url).IsRequired();
            builder.Property(e => e.Name).IsRequired();
        }
    }
}

