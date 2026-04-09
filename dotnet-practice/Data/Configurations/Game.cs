
using dotnet_practice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_practice.Data.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.Property(g => g.Name).IsRequired().HasMaxLength(100);

        builder.HasOne(g => g.Genre)
            .WithMany(g => g.Games)
            .HasForeignKey(g => g.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(g => g.Price).HasPrecision(10, 2);

        builder.HasIndex(g => g.Name).IsUnique();

    }


}
