using dotnet_practice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_practice.Data.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(g => g.Name)
            .IsUnique();

        builder.HasData(
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "RPG" },
            new Genre { Id = 3, Name = "Strategy" },
            new Genre { Id = 4, Name = "Sports" }
        );
    }
}