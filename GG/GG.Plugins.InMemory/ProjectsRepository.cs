using GG.CoreBusiness;
using GG.UseCases.PluginInterfaces;

namespace GG.Plugins.InMemory
{
	public class ProjectsRepository : IProjectsRepository
	{
		private List<Project> Projects = new List<Project>();
		private List<Person> Contact = new List<Person>();

		public ProjectsRepository() 
		{
			CreateTestData();
		}
		public Task<IEnumerable<Project>> GetProjectsByNameAsync(string name)
		{
			throw new NotImplementedException();
		}

		


		public void CreateTestData()
		{
			Contact.Add(new Person()
			{
				Id = "0K9P3G9V",
				Firstname = "Rainer",
				Lastname = "Winkler",
				Address = "DE, Emskirchen 91448, Altschauerberg 8",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "r.winkler@htlkrems.at"
			});

			Contact.Add(new Person()
			{
				Id = "DE2876YI",
				Firstname = "Ilse",
				Lastname = "Nigischer",
				Address = "AT Waidhofen a. T. 3830, Buxdihudenstraße 1",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "i.nigischer@htlkrems.at"
			});

			Contact.Add(new Person()
			{
				Id = "M8A557WC",
				Firstname = "Herwig",
				Lastname = "Macho",
				Address = "AT Zwettl 3910, Propstei 8",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "h.macho@htlkrems.at"
			});

			Contact.Add(new Person()
			{
				Id = "QMRBJH1U",
				Firstname = "Lukas",
				Lastname = "Kolinsky",
				Address = "AT, Zwettl 3910, Propstei 7",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "lukas.kolinsky1@gmail.com"
			});

			Contact.Add(new Person()
			{
				Id = "WLA9PUCX",
				Firstname = "Mulham",
				Lastname = "Taylouni",
				Address = "AT, Gmünd 3950, Schremserstraße 51",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "contact@taylouni.me"
			});

			Contact.Add(new Person()
			{
				Id = "AHR44LMZ",
				Firstname = "Christian",
				Lastname = "Wiesinger",
				Address = "AT, Rappottenstein 3911, Marbach am Walde 79",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "contact@taylouni.me"
			});

			Contact.Add(new Person()
			{
				Id = "AHR44LMZ",
				Firstname = "Clemens",
				Lastname = "Schmid",
				Address = "AT, Zwettl 3910,Hammerweg 3",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "c.schmid@htlkrems.at"
			});


			//Project I
			Team t1 = new Team() { Description = "Dream Team" };

			t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Designer", Role = Teamrolle.Worker });
			t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Programmer", Role = Teamrolle.Worker });
			t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Projektleiter", Role = Teamrolle.Projektleader });

			Project p1 = new Project() { Name = "Project 1", Budget = 99.50f, status = ProgressStatus.In_Progress, assignedTeam = t1 };


			//Project II
			Team t2 = new Team() { Description = "Die 4 netten Menschen" };

			t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Designer", Role = Teamrolle.Worker });
			t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Programmer", Role = Teamrolle.Worker });
			t1.members.Add(new Teammember() { person = Contact[0], Description = "Programmer", Role = Teamrolle.Administrator });
			t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Projektleiter", Role = Teamrolle.Projektleader });

			Project p2 = new Project() { Name = "Funky Munky", Budget = 99.50f, status = ProgressStatus.In_Progress, assignedTeam = t2 };

		}

		public Person getRandomPerson()
		{
			Random randy = new Random();
			return Contact.ToArray()[randy.Next(0, Contact.Count)];
		}
	}
}