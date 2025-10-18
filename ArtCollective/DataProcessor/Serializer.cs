using System.Xml.Serialization;
using System.Xml;
using ArtCollective.Data;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ArtCollective.DataProcessor.ExportDTOs;
using System.Xml.Linq;

namespace ArtCollective.DataProcessor
{
    public class Serializer
    {
        public static string ExportArtistsWithCollaborationsCountAndTheirArtworks(ArtCollectiveDbContext dbContext)
        {
            // Query data and map it to DTOs
            var artistDtos = dbContext.Artists
                .Select(artist => new ArtistDto
                {
                    Username = artist.Username,
                    Collaborations = dbContext.Collaborations
                        .Count(c => c.ArtistOneId == artist.Id || c.ArtistTwoId == artist.Id),
                    Artworks = artist.Artworks
                        .OrderBy(artwork => artwork.Id)
                        .Select(artwork => new ArtworkDto
                        {
                            Title = artwork.Title,
                            CreatedOn = artwork.CreatedOn.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        }).ToList()
                })
                .OrderBy(artistDto => artistDto.Username)
                .ToList();

            // Generate the XML document
            var xmlDocument = new XElement("Artists",
                artistDtos.Select(artistDto => new XElement("Artist",
                    new XAttribute("Collaborations", artistDto.Collaborations),
                    new XElement("Username", artistDto.Username),
                    new XElement("Artworks",
                        artistDto.Artworks.Select(artworkDto => new XElement("Artwork",
                            new XElement("Title", artworkDto.Title),
                            new XElement("CreatedOn", artworkDto.CreatedOn)
                        ))
                    )
                ))
            );

            return xmlDocument.ToString();
        }
        public static string ExportGroupsWithFeedbacksChronologically(ArtCollectiveDbContext dbContext)
        {
            var AllGroups = dbContext.Groups.AsEnumerable()
                .OrderBy(g => g.StartedOn)
                 .Select(g => new
                 {
                    Id=g.Id,
                    Title=g.Title,
                    StartedOn=g.StartedOn.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Feedbacks=g.Feedbacks
                    .OrderBy(f=> f.GivenOn)
                    .Select(f=> new
                    {
                        Content = f.Content,
                        GivenOn =f.GivenOn.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Status= f.Status,
                        ArtistUsername=f.Artist.Username

                    }).ToArray()


                 }).ToArray();




            return JsonConvert.SerializeObject(AllGroups, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
