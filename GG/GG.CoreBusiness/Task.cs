namespace GG.CoreBusiness
{
    public class Task
    {
        public string Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProgressStatus Status { get; set; }
        public Teammember AssignedPerson { get; set; }
    }
}
