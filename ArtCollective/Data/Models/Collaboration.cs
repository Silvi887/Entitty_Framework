using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtCollective.Data.Models
{
    public class Collaboration
    {
        [Required]
        public int ArtistOneId { get; set; }

        [ForeignKey(nameof(ArtistOneId))]
        public Artist ArtistOne { get; set; }

        [Required]
        public int ArtistTwoId { get; set; }

        [ForeignKey(nameof(ArtistTwoId))]
        public Artist ArtistTwo { get; set; }
    }
}
//•	ArtistOneId – integer, Foreign Key(required)
//•	ArtistOne - Artist
//•	ArtistTwoId – integer, Foreign Key(required)
//•	ArtistTwo - Artist

