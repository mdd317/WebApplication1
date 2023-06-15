namespace WebApplication1.Models
{
    public class Movie_Producer
    {    
        public int Id { get; set; }

        //Relationships
        public Movie? Movie { get; set; }

        public int MovieId { get; set; }

        //producer
        public Producer? Producer { get; set; }

        public int ProducerId { get; set; }
    }
}
