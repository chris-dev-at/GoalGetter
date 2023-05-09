using GG.CoreBusiness;
using System;

namespace UnitTests_CoreBusiness
{
    [TestClass]
    public class UnitTest1
    {
        public List<Project> Projects = new List<Project>();
        public List<Person> Contact = new List<Person>();

        [TestMethod]
        public void TestPerson()
        {
            CreateTestData();
            Assert.IsTrue(true);
        }

        public void CreateTestData()
        {
            Contact.Add(new Person() {
                Id= RandomString(),
                Firstname = "Rainer",
                Lastname = "Winkler",
                Address= "DE, Emskirchen 91448, Altschauerberg 8",
                AvatarPath= @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
                Email= "r.winkler@htlkrems.at"
            });

            Contact.Add(new Person()
            {
                Id = RandomString(),
                Firstname = "Ilse",
                Lastname = "Nigischer",
                Address = "AT Waidhofen a. T. 3830, Buxdihudenstraße 1",
                AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
                Email = "i.nigischer@htlkrems.at"
            });

            Contact.Add(new Person()
            {
                Id = RandomString(),
                Firstname = "Herwig",
                Lastname = "Macho",
                Address = "AT Zwettl 3910, Propstei 8",
                AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
                Email = "h.macho@htlkrems.at"
            });

            Contact.Add(new Person()
            {
                Id = RandomString(),
                Firstname = "Lukas",
                Lastname = "Kolinsky",
                Address = "AT, Zwettl 3910, Propstei 7",
                AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
                Email = "lukas.kolinsky1@gmail.com"
            });

            Contact.Add(new Person()
            {
                Id = RandomString(),
                Firstname = "Mulham",
                Lastname = "Taylouni",
                Address = "AT, Gmünd 3950, Schremserstraße 51",
                AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
                Email = "contact@taylouni.me"
            });

            Contact.Add(new Person()
            {
                Id = RandomString(),
                Firstname = "Christian",
                Lastname = "Wiesinger",
                Address = "AT, Rappottenstein 3911, Marbach am Walde 79",
                AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
                Email = "contact@taylouni.me"
            });

            Contact.Add(new Person()
            {
                Id = RandomString(),
                Firstname = "Clemens",
                Lastname = "Schmid",
                Address = "AT, Zwettl 3910,Hammerweg 3",
                AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
                Email = "c.schmid@htlkrems.at"
            });


            //Project I
            Team t1 = new Team() { Description="Dream Team" };

            t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Designer", Role=Teamrolle.Worker });
            t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Programmer", Role=Teamrolle.Worker });
            t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Projektleiter", Role=Teamrolle.Projektleader });

            Project p1 = new Project() { Name="Project 1", Budget=99.50f, status=ProgressStatus.In_Progress, assignedTeam=t1, Description="This project is for testing", Id=RandomString()};

            p1.Tasks.Add(new ProjectTask() { Name="Code Website", Description = "Make a website lol. xd. mega lol.", AssignedPerson =getRandomPerson(t1), Deadline=RandomDay(), Id= RandomString() });
            p1.Tasks.Add(new ProjectTask() { Name="Code Website 2", Description = "Make a website lol. xd. mega lol.", AssignedPerson =getRandomPerson(t1), Deadline=RandomDay(), Id= RandomString() });
            p1.Tasks.Add(new ProjectTask() { Name="Code Website 3", Description = "Make a website lol. xd. mega lol.", AssignedPerson =getRandomPerson(t1), Deadline=RandomDay(), Id= RandomString() });
            p1.Tasks.Add(new ProjectTask() { Name="Have FreeTime 1", Description = "Have Freetime lol. Very hard to understand", AssignedPerson =getRandomPerson(t1), Deadline=RandomDay(), Id= RandomString() });
            p1.Tasks.Add(new ProjectTask() { Name="Create Blazor App 1", Description = ".NET 7.0 CORE Framework needed. please install it and create blazorproject with it", AssignedPerson =getRandomPerson(t1), Deadline=RandomDay(), Id= RandomString() });
            p1.Tasks.Add(new ProjectTask() { Name="Create Blazor App 2", Description = ".NET 6.0 CORE Framework needed. please install it and create blazorproject with it", AssignedPerson =getRandomPerson(t1), Deadline=RandomDay(), Id= RandomString() });



            //Project II
            Team t2 = new Team() { Description = "Die 4 netten Menschen" };

            t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Designer", Role = Teamrolle.Worker });
            t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Programmer", Role = Teamrolle.Worker });
            t1.members.Add(new Teammember() { person = Contact[0], Description = "Programmer", Role = Teamrolle.Administrator });
            t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Projektleiter", Role = Teamrolle.Projektleader });

            Project p2 = new Project() { Name = "Funky Munky", Budget = 99.50f, status = ProgressStatus.In_Progress, assignedTeam = t2 };

            p2.Tasks.Add(new ProjectTask() { Name = "Code Website", Description = "Make a website lol. xd. mega lol.", AssignedPerson = getRandomPerson(t1), Deadline = RandomDay(), Id = RandomString() });
            p2.Tasks.Add(new ProjectTask() { Name = "Code Website 2", Description = "Make a website lol. xd. mega lol.", AssignedPerson = getRandomPerson(t1), Deadline = RandomDay(), Id = RandomString() });
            p2.Tasks.Add(new ProjectTask() { Name = "Code Website 3", Description = "Make a website lol. xd. mega lol.", AssignedPerson = getRandomPerson(t1), Deadline = RandomDay(), Id = RandomString() });
            p2.Tasks.Add(new ProjectTask() { Name = "Have FreeTime 1", Description = "Have Freetime lol. Very hard to understand", AssignedPerson = getRandomPerson(t1), Deadline = RandomDay(), Id = RandomString() });
            p2.Tasks.Add(new ProjectTask() { Name = "Create Blazor App 1", Description = ".NET 7.0 CORE Framework needed. please install it and create blazorproject with it", AssignedPerson = getRandomPerson(t1), Deadline = RandomDay(), Id = RandomString() });
            p2.Tasks.Add(new ProjectTask() { Name = "Create Blazor App 2", Description = ".NET 6.0 CORE Framework needed. please install it and create blazorproject with it", AssignedPerson = getRandomPerson(t1), Deadline = RandomDay(), Id = RandomString() });


        }

        //Functions
        Random randy = new Random();
        public Person getRandomPerson()
        {
            return Contact.ToArray()[randy.Next(0,Contact.Count)];
        }
        public Teammember getRandomPerson(Team t)
        {
            return t.members.ToArray()[randy.Next(0, t.members.Count)];
        }
        public DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(randy.Next(range));
        }
        private string RandomString(int length=8)
        {
            string ret = "";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            ret = new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[randy.Next(s.Length)]).ToArray());
                ret = new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[randy.Next(s.Length)]).ToArray());
            return ret;
        }
    }
}