using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArtCollective.DataProcessor.ImportDTOs
{
    [XmlType("Feedback")]
    public class ImportFeedbackDtos
    {
        [XmlElement("Content")]
        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        public string? Content { get; set; }


        [XmlElement("Status")]
        [Required]
        public string Status { get; set; }


        [XmlElement("GroupId")]
        [Required]
        public int GroupId { get; set; }


        [XmlElement("ArtistId")]
        [Required]
        public int ArtistId { get; set; }


        [XmlAttribute("GivenOn")]
        [Required]
        public string GivenOn { get; set; }
    }
}
