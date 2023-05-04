namespace GG.CoreBusiness
{
    public class Project
    {
        public string Name { get; set; }
        public float Budget { get; set; }
        public Team assignedTeam { get; set; }
        public ProgressStatus status { get; set; }
    }
}
