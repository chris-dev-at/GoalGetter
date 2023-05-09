using System.ComponentModel.DataAnnotations;

namespace GG.CoreBusiness
{
    public class Project
    {
        [Required, StringLength(100, MinimumLength = 1, ErrorMessage = "Projectname must be between 1 and 100 characters")]
        public string Name { get; set; }
        public float Budget { get; set; }
        public Team assignedTeam { get; set; }
        public ProgressStatus status { get; set; }
    }
}
