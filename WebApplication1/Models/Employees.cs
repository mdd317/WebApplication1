using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication1.Models

{
    public class Employees
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }


        // Relationships
        public virtual Cinema Cinemas { get; set; }

        public int CinemaId { get; set; }


    }
}
