using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtCollective.DataProcessor.ImportDTOs
{

    public class ImportArtworkDto
    {
        [JsonProperty("Title")]
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        [JsonProperty("Description")]
        [MaxLength(300)]
        [MinLength(10)]
        public string? Description { get; set; }

        [JsonProperty("CreatedOn")]
        [Required]
        public string CreatedOn { get; set; }

        [JsonProperty("ArtistId")]
        [Required]
        public int ArtistId { get; set; }
    }
}
