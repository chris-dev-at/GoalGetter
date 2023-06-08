using Newtonsoft.Json;

namespace GG.CoreBusiness
{
    public class Teammember 
    {
        public Teamrolle Role { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public Person person { get; set; }
        public int PersonId { get; set; } //used to restore link from person in project to person in contacts
    }
}
