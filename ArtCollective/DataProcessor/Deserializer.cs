using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using ArtCollective.Data;
using ArtCollective.Data.Models;
using ArtCollective.Data.Models.Enums;
using ArtCollective.DataProcessor.ImportDTOs;
using Newtonsoft.Json;

namespace ArtCollective.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format.";
        private const string DuplicatedData = "Data is duplicated.";
        private const string SuccessfullyImportedFeedbackEntity = "Successfully imported feedback (Given on: {0}, Status: {1})";
        private const string SuccessfullyImportedArtworkEntity = "Successfully imported artwork (Artist: {0}, Created on: {1})";

        public static string ImportFeedbacks(ArtCollectiveDbContext dbContext, string xmlString)
        {
            dbContext.Artists.laz.
            XmlSerializer xmlSerializer =
             new XmlSerializer(typeof(ImportFeedbackDtos[]), new XmlRootAttribute("Feedbacks"));

            using StringReader stringReader = new StringReader(xmlString);
            ImportFeedbackDtos[] FeedbackDtos = (ImportFeedbackDtos[])xmlSerializer.Deserialize(stringReader);
            //

            StringBuilder sb = new StringBuilder();
            List<Feedback> ListFeedbacks = new List<Feedback>();
            //

            int[] AllGroupIdDb = dbContext.Groups.Select(g => g.Id).ToArray();
            int[] AllArtistsIdDb = dbContext.Artists.Select(g => g.Id).ToArray();

            foreach (var FeedbackDto in FeedbackDtos)
            {
                if (!IsValid(FeedbackDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                string[] Allstatus = { "Pending", "Reviewed", "Published", "Rejected" };

               
                var st = FeedbackDto.Status;
                if (!Allstatus.Contains(st))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime dt1;
                var isValiddt = DateTime.TryParseExact(FeedbackDto.GivenOn, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1);
                if (!isValiddt)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
               

                if (!AllGroupIdDb.Contains(FeedbackDto.GroupId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!AllArtistsIdDb.Contains(FeedbackDto.ArtistId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (dbContext.Feedbacks.Any(f => f.Content == FeedbackDto.Content && f.ArtistId == FeedbackDto.ArtistId
                && f.Status == (Status)Enum.Parse(typeof(Status), FeedbackDto.Status) && f.GivenOn == dt1))

                {
                    sb.AppendLine(DuplicatedData);
                    continue;
                }

                if (ListFeedbacks.Any(f => f.Content == FeedbackDto.Content && f.ArtistId == FeedbackDto.ArtistId
               && f.Status == (Status)Enum.Parse(typeof(Status), FeedbackDto.Status) && f.GivenOn == dt1))

                {
                    sb.AppendLine(DuplicatedData);
                    continue;
                }

                Feedback feed1 = new Feedback()
                {
                    Content = FeedbackDto.Content,
                    GivenOn = dt1,
                    Status = (Status)Enum.Parse(typeof(Status), FeedbackDto.Status),
                    GroupId = FeedbackDto.GroupId,
                    ArtistId = FeedbackDto.ArtistId

                };
                ListFeedbacks.Add(feed1);
                sb.AppendLine(string.Format(SuccessfullyImportedFeedbackEntity, FeedbackDto.GivenOn, feed1.Status));



            }


            //
            dbContext.Feedbacks.AddRange(ListFeedbacks);
            dbContext.SaveChanges();
            return sb.ToString();
        }

        public static string ImportArtworks(ArtCollectiveDbContext dbContext, string jsonString)
        {
            ImportArtworkDto[] ImportArtworksDtos = JsonConvert.DeserializeObject<ImportArtworkDto[]>(jsonString);
            using StringReader stringReader = new StringReader(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Artwork> ListArtworks = new List<Artwork>();


            var AllAristId= dbContext.Artists.Select(a=> a.Id).ToArray();
            //
            foreach (var ImportArtworkDto in ImportArtworksDtos)
            {
                if (!IsValid(ImportArtworkDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                if(string.IsNullOrEmpty(ImportArtworkDto.Title)  || string.IsNullOrEmpty(ImportArtworkDto.CreatedOn))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                if (ListArtworks.Any(a=> a.Title == ImportArtworkDto.Title && a.ArtistId == ImportArtworkDto.ArtistId))
                {
                    sb.AppendLine(DuplicatedData);
                    continue;
                }

                DateTime dtCreated;
                var isvaliddt = DateTime.TryParseExact(ImportArtworkDto.CreatedOn, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtCreated);
                if (!isvaliddt)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                string dtstartmew = ImportArtworkDto.CreatedOn;

                if (!AllAristId.Contains(ImportArtworkDto.ArtistId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Artwork art1 = new Artwork()
                {
                    Title = ImportArtworkDto.Title,
                    Description = ImportArtworkDto.Description,
                    CreatedOn = dtCreated,
                    ArtistId = ImportArtworkDto.ArtistId
                };

                var artist = dbContext.Artists.FirstOrDefault(a => a.Id == ImportArtworkDto.ArtistId);

                ListArtworks.Add(art1);

                sb.AppendLine(String.Format(SuccessfullyImportedArtworkEntity, artist.Username, dtstartmew));

            }


            //
            dbContext.Artworks.AddRange(ListArtworks);
            dbContext.SaveChanges();
            return sb.ToString();

        }

        public static bool IsValid(object dto)
        {
            ValidationContext validationContext = new ValidationContext(dto);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            List<string> errorMessages = new List<string>();
            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            errorMessages = validationResults.Select(r => r.ErrorMessage!).ToList();

            return isValid;
        }
    }
}
