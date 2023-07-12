using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Movie
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Description { get; set; }
        public double Price { get; set; }

        [JsonProperty("poster_path")]
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //Cinema
        public Cinema? Cinema { get; set; }

        public int CinemaId { get; set; }

        //Producer
        public List<Movie_Producer>? Movie_Producers { get; set; }

        /*      public List<ProducerEditViewModel>? EditMovie_Producers { get; set; }*/


        //Studio

        [NotMapped]
        public List<Studio?> Studios
        {
            get
            {
                List<Studio?> studios = new List<Studio?>();

                if (Movie_Producers != null && Movie_Producers.Any())
                {
                    foreach (var movieProducer in Movie_Producers)
                    {
                        if (movieProducer.Producer != null && movieProducer.Producer.Studio != null)
                        {
                            studios.Add(movieProducer.Producer.Studio);
                        }
                    }
                }

                return studios;
            }
        }


    }
}
