using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context.Configurations
{
    public class SuffixConfiguration : IEntityTypeConfiguration<Suffix>
    {
        public void Configure(EntityTypeBuilder<Suffix> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Value)
                .HasMaxLength(3);
            builder.HasMany(j => j.Drugs)
                .WithOne(u => u.Suffix)
                .HasForeignKey(j => j.SuffixId);
        }
    }
}
