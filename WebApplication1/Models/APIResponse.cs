namespace WebApplication1.Models
{
    public class APIResponse
    {
        public List<MovieItem> Results { get; set; }
    }

    public class MovieItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public int original_language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Popularity { get; set; }
        public double Price { get; set; }
        public string poster_path { get; set; }
        public DateTime release_date { get; set; }
        public int original_title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }

    }
}
