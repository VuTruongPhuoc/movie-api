using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie.API.Infrastructure.Data.Configuration;
using Movie.API.Models.Domain.Entities;

namespace Movie.API.Infrastructure.Data
{
    public class MovieDbContext : IdentityDbContext<User, Role, string>
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Country> Countries { get; set; } = default!;
        public DbSet<Episode> Episodes { get; set; } = default!;
        public DbSet<EpisodeServer> EpisodeServers { get; set; } = default!;
        public DbSet<Film> Films { get; set; } = default!;
        public DbSet<FilmCategory> FilmCategories { get; set; } = default!;
        public DbSet<History> Histories { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public new DbSet<Role> Roles { get; set; } = default!;
        public DbSet<RolePrivileges> RolePrivileges { get; set; } = default!;
        public DbSet<Schedule> Schedules { get; set; } = default!;
        public DbSet<Section> Sections { get; set; } = default!;
        public DbSet<Server> Servers { get; set; } = default!;
        public DbSet<Track> Tracks { get; set; } = default!;
        public new DbSet<User> Users { get; set; } = default!;
        public DbSet<Feedback> Feedbacks { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FilmConfiguration());
            modelBuilder.ApplyConfiguration(new FilmCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new EpisodeConfiguration());
            modelBuilder.ApplyConfiguration(new HistoryConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePrivilegesConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new ServerConfiguration());
            modelBuilder.ApplyConfiguration(new TrackConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new EpisodeServerConfiguration());
            modelBuilder.ApplyConfiguration(new FeedbackConfiguration());

            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName is not null)
                    if (tableName.StartsWith("AspNet"))
                    {
                        entityType.SetTableName(tableName.Substring(6));
                    }
            }
        }

    }
}
