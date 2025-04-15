using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Movie.API.Models.Domain.Entities;
using System.Linq;

namespace Movie.API.Infrastructure.Data.Configuration
{
    public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.ToTable("Episodes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Link).IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.Film)
                .WithMany(x => x.Episodes)
                .HasForeignKey(x => x.FilmId);
            builder.HasMany(x => x.Servers).WithMany(x => x.Episodes)
            .UsingEntity<EpisodeServer>(join =>
            join.HasOne(j => j.Server).WithMany(w => w.EpisodeServers).HasForeignKey(x => x.ServerId).HasPrincipalKey(s => s.Id).OnDelete(DeleteBehavior.Cascade),
            join => join.HasOne(s => s.Episode).WithMany(w => w.EpisodeServers).HasForeignKey(x => x.EpisodeId).HasPrincipalKey(s => s.Id).OnDelete(DeleteBehavior.Cascade));
        }
    }
}
