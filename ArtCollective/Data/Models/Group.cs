using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtCollective.Data.Models
{
    public class Group
    {

        public Group()
        {
            Feedbacks = new List<Feedback>();
            ArtistsGroups= new List<ArtistGroup>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        public DateTime StartedOn { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<ArtistGroup> ArtistsGroups { get; set; }
    }
}

//•	Id – integer, Primary Key
//•	Title – text with length [3, 50] (required)
//•	StartedOn – DateTime(required)
//•	Feedbacks – a collection of type Feedback
//•	ArtistsGroups – a collection of type ArtistGroup

