using ArtCollective.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtCollective.Data.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        public string Content { get; set; }

        [Required]
        public DateTime GivenOn { get; set; }
       
        [Required]
        public Status Status { get; set; }


        [Required]
        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }

        [Required]
        public int ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        public Artist Artist { get; set; }
    }
}

//•	Id – integer, Primary Key
//•	Content – text with length [3, 200] (required)
//•	GivenOn – DateTime(required)
//•	Status – enum Status(Pending = 0, Reviewed, Published, Rejected)(required)
//•	GroupId - integer, Foreign Key(required)
//•	Group - Group
//•	ArtistId - integer, Foreign Key(required)
//•	Artist – Artist
