using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hmmm... You should really add a Name...")]
        [MaxLength(35)]
        public string Name { get; set; }
        public int NeighborhoodId { get; set; }

        [Required(ErrorMessage = "Hmmm... You should really add a pic of yourself...")]
        [MaxLength(35)]
        [DisplayName("Image")]
        public string ImageUrl { get; set; }
        public Neighborhood Neighborhood { get; set; }

        List<Walk> walks = new List<Walk>();
    }
}