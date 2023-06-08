using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GG.CoreBusiness
{
    public class ProjectTask
    {
        public string Id { get; set; }
        public DateTime Deadline { get; set; }
        public ProgressStatus Status { get; set; }

        [JsonIgnore]
        public Teammember AssignedPerson { get; set; }
        public int PersonId { get; set; } //used to link memory with teammember instead of creating new one


        [Required, StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
        public string Name { get; set; }


        [StringLength(300, ErrorMessage = "Description can't be over 300 characters")]
        public string Description { get; set; }
    }
}
