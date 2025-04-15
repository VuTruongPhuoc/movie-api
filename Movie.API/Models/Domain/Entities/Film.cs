using Movie.API.Models.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Movie.API.Models.Domain.Entities
{
    public class Film : BaseDomainEntity
    {
        public string Name { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? Image { get; set; } = default!;
        public string? Poster { get; set; } = default!;

        [Column("number_of_episodes")]
        public int NumberOfEpisodes { get; set; } = default!;
        public string OriginName { get; set; } = default!;
        public string Time { get; set; } = default!;
        public int Year { get; set; } = default!;
        public int Type { get; set; } = default!;
        public string? Trailer { get; set; } = default!;
        public int CountryId { get; set; } = default!;
        public int ScheduleId { get; set; } = default!;
        public Country? Country { get; set; } = default!;
        public Schedule? Schedule { get; set; } = default!;
        public virtual ICollection<Episode> Episodes { get; set; } = default!;
        public virtual ICollection<Review> Reviews { get; set; } = default!;
        public virtual ICollection<Track> Tracks { get; set; } = default!;
        public virtual ICollection<Comment> Comments { get; set; } = default!;
        public virtual ICollection<History> Histories { get; set; } = default!;
        public virtual ICollection<FilmCategory> FilmCategories { get; set; } = default!;

    }
}
