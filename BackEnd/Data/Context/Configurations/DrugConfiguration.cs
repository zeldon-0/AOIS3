using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context.Configurations
{
    public class DrugConfiguration : IEntityTypeConfiguration<Drug>
    {
        public void Configure(EntityTypeBuilder<Drug> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name)
                .HasMaxLength(30);
            builder.HasOne(d => d.Suffix)
                .WithMany(d => d.Drugs)
                .HasForeignKey(d => d.SuffixId);
        }
    }
}
