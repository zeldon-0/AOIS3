using Core.Domain.Models;
using Data.Context.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class DrugContext : DbContext
    {
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Suffix> Suffixes { get; set; }
        public DrugContext(DbContextOptions<DrugContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DrugContext() : base()
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DrugConfiguration());
            modelBuilder.ApplyConfiguration(new SuffixConfiguration());
        }

    }
}
