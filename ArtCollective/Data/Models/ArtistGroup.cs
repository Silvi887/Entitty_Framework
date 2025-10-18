using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtCollective.Data.Models
{
    public class ArtistGroup
    {
        [Required]
        public int ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        public Artist Artist{ get; set; }

        [Required]
        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }
    }
}

//•	ArtistId – integer, Primary Key, Foreign Key (required)
//•	Artist – Artist
//•	GroupId – integer, Primary Key, Foreign Key (required)
//•	Group – Group
