using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Palindromes.API.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Palindromes.API.Infrastructure
{
    public class PalindromesContext : DbContext
    {
        public PalindromesContext(DbContextOptions<PalindromesContext> options) : base(options)
        {
        }

        public DbSet<Palindrome> Palindromes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PalindromeEntityTypeConfiguration());
        }

    }

    class PalindromeEntityTypeConfiguration : IEntityTypeConfiguration<Palindrome>
    {
        public void Configure(EntityTypeBuilder<Palindrome> builder)
        {
            builder.ToTable("Palindrome");

            builder.Property(ci => ci.Id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired();

            builder.Property(ci => ci.Text)
                .IsRequired(true)
                .HasMaxLength(1000);

            builder.Property(ci => ci.CreatedDate)
                .IsRequired(true);

        }
    }


}
