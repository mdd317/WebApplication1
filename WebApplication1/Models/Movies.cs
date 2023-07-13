using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public  string original_language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Popularity { get; set; } 
        public double Price { get; set; }
        public DateTime release_date { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }

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
