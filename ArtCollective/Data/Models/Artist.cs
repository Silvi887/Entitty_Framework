using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtCollective.Data.Models
{
    public class Artist
    {

        public Artist()
        {
            Artworks= new List<Artwork>();
            Feedbacks= new List<Feedback>();
            ArtistsGroups= new List<ArtistGroup>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }

        public ICollection<Artwork> Artworks { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<ArtistGroup> ArtistsGroups { get; set; }
    }
}

//•	Id – integer, Primary Key
//•	Username – text with length [5, 30] (required)
//•	Email – text with length [6, 50] (required)
//•	Password – text with a minimum length of 4 (required)
//•	Artworks – a collection of type Artwork
//•	Feedbacks – a collection of type Feedback
//•	ArtistsGroups – a collection of type ArtistGroup
