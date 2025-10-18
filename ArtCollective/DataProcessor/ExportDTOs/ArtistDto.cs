using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtCollective.DataProcessor.ExportDTOs
{
    public class ArtistDto
    {
        public string Username { get; set; }
        public int Collaborations { get; set; }
        public List<ArtworkDto> Artworks { get; set; }
    }
}
