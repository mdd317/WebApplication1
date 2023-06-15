using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Studio
    {
        [Key]
        public int Id { get; set; }

        public string Studio_Name { get; set; }

        public string Owner { get; set; }

        //Relationships
        public List<Producer>? Producers { get; set; }




    }
}