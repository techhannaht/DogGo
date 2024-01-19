using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hmmm... You should really add a Name...")]
        [MaxLength(35)]
        public string Name { get; set; }
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "Hmmm... You should really add a Breed...")]
        [MaxLength(35)]
        public string Breed { get; set; }

        [Required(ErrorMessage = "Hmmm... You should really add a something nice about your pup...")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Hmmm... You should really add a pic of your pup...")]
        public string ImageUrl { get; set; }

        public Owner owner { get; set; }
    }
}
