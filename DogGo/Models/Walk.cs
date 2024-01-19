using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walk
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime Date { get; set; }
        public int Duration { get; set; }


        [DisplayName("Duration")]
        public string FormattedDuration
        {
            get
            {
                int durationInMinutes = Duration / 60;
                return $"{durationInMinutes} min";
            }
        }

        public string TotalDuration(List<Walk> walks)
        {
            int totalMinutes = 0;

            foreach (var walk in walks)
            {
                totalMinutes += walk.Duration;
            }

            int minutes = totalMinutes / 60;
            int hours = totalMinutes % 60;

            return $" {hours} hours {minutes} minutes";
        }


        [DisplayName("Walker")]
        public int WalkerId { get; set; }


        [DisplayName("Dog")]
        public int DogId { get; set; }


        public Walker Walker { get; set; }


        public Dog Dog { get; set; }
    }
}
