using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtCollective.Data.Models
{
    public class Artwork
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        
        [MaxLength(300)]
        [MinLength(10)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
       

        [Required]
        public int ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        public Artist Artist { get; set; }
    }
}

//•	Id – integer, Primary Key
//•	Title – text with length [3, 50] (required)
//•	Description – text with length [10, 300] (NOT required)
//•	CreatedOn – DateTime(required)
//•	ArtistId – integer, Foreign Key(required)
//•	Artist – Artist

