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
		public async Task<IEnumerable<Project>> GetProjectsByNameAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return await Task.FromResult(Projects);
			return Projects.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
		}
		public Task AddProjectAsync(Project project)
		{
			if (Projects.Any(x => x.Name.Equals(project.Name, StringComparison.OrdinalIgnoreCase)))
				return Task.CompletedTask;

			var maxId = Projects.Max(x => x.Id);
			project.Id = maxId + 1;

			Projects.Add(project);
			return Task.CompletedTask;
		}

		public async Task<IEnumerable<Person>> GetPeopleByNameAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return await Task.FromResult(Contact);
			return Contact.Where(x => x.Firstname.Contains(name, StringComparison.OrdinalIgnoreCase));
		}

		public Task AddPersonAsync(Person person)
		{
			if (Contact.Any(x => x.Firstname.Equals(person.Firstname, StringComparison.OrdinalIgnoreCase)))
				return Task.CompletedTask;

			Contact.Add(person);
			return Task.CompletedTask;
		}

		Random randy = new Random();


        void CreateTestData()
		{
			Contact.Add(new Person()
			{
				Id = RandomString(),
				Firstname = "Rainer",
				Lastname = "Winkler",
				Address = "DE, Emskirchen 91448, Altschauerberg 8",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "r.winkler@htlkrems.at"
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
				Address = "AT, Gmünd 3950, Schremserstraße 69",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "contact@taylouni.me"
			});

			Contact.Add(new Person()
			{
				Id = RandomString(),
				Firstname = "Christian",
				Lastname = "Wiesinger",
				Address = "AT, Rappottenstein 3911, Burgunderweg 109",
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
			Team t1 = new Team() { Description = "Dream Team", Id = RandomString(), members = new List<Teammember>() };

			t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Designer", Role = Teamrolle.Worker });
			t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Programmer", Role = Teamrolle.Worker });
			t1.members.Add(new Teammember() { person = getRandomPerson(), Description = "Projektleiter", Role = Teamrolle.Projektleader });

			Project p1 = new Project() { Name = "Project 1", Budget = 99.50f, status = ProgressStatus.In_Progress, assignedTeam = t1, Description = "This project is for testing", Id = RandomString(), Tasks = new List<ProjectTask>() };

			p1.Tasks.Add(new ProjectTask() { Name = "Code Website", Description = "Make a website lol. xd. mega lol.", AssignedPerson = getRandomTPerson(t1), Deadline = RandomDay(), Id = RandomString() });
			p1.Tasks.Add(new ProjectTask() { Name = "Code Website 2", Description = "Make a website lol. xd. mega lol.", AssignedPerson = getRandomTPerson(t1), Deadline = RandomDay(), Id = RandomString() });
			p1.Tasks.Add(new ProjectTask() { Name = "Code Website 3", Description = "Make a website lol. xd. mega lol.", AssignedPerson = getRandomTPerson(t1), Deadline = RandomDay(), Id = RandomString() });
			p1.Tasks.Add(new ProjectTask() { Name = "Have FreeTime 1", Description = "Have Freetime lol. Very hard to understand", AssignedPerson = getRandomTPerson(t1), Deadline = RandomDay(), Id = RandomString() });
			p1.Tasks.Add(new ProjectTask() { Name = "Create Blazor App 1", Description = ".NET 7.0 CORE Framework needed. please install it and create blazorproject with it", AssignedPerson = getRandomTPerson(t1), Deadline = RandomDay(), Id = RandomString() });
			p1.Tasks.Add(new ProjectTask() { Name = "Create Blazor App 2", Description = ".NET 6.0 CORE Framework needed. please install it and create blazorproject with it", AssignedPerson = getRandomTPerson(t1), Deadline = RandomDay(), Id = RandomString() });

			Projects.Add(p1);


			//Project II
			Team t2 = new Team() { Description = "Die 4 netten Menschen", Id = RandomString(), members = new List<Teammember>() };

			t2.members.Add(new Teammember() { person = getRandomPerson(), Description = "Designer", Role = Teamrolle.Worker });
			t2.members.Add(new Teammember() { person = getRandomPerson(), Description = "Programmer", Role = Teamrolle.Worker });
			t2.members.Add(new Teammember() { person = Contact[0], Description = "Programmer", Role = Teamrolle.Administrator });
			t2.members.Add(new Teammember() { person = getRandomPerson(), Description = "Projektleiter", Role = Teamrolle.Projektleader });

			Project p2 = new Project() { Name = "Funky Munky", Budget = 99.50f, status = ProgressStatus.In_Progress, assignedTeam = t2, Tasks = new List<ProjectTask>() };

			p2.Tasks.Add(new ProjectTask() { Name = "Code Website", Description = "Make a website lol. xd. mega lol.", AssignedPerson = getRandomTPerson(t2), Deadline = RandomDay(), Id = RandomString() });
			p2.Tasks.Add(new ProjectTask() { Name = "Code Website 2", Description = "Make a website lol. xd. mega lol.", AssignedPerson = getRandomTPerson(t2), Deadline = RandomDay(), Id = RandomString() });
			p2.Tasks.Add(new ProjectTask() { Name = "Code Website 3", Description = "Make a website lol. xd. mega lol.", AssignedPerson = getRandomTPerson(t2), Deadline = RandomDay(), Id = RandomString() });
			p2.Tasks.Add(new ProjectTask() { Name = "Have FreeTime 1", Description = "Have Freetime lol. Very hard to understand", AssignedPerson = getRandomTPerson(t2), Deadline = RandomDay(), Id = RandomString() });
			p2.Tasks.Add(new ProjectTask() { Name = "Create Blazor App 1", Description = ".NET 7.0 CORE Framework needed. please install it and create blazorproject with it", AssignedPerson = getRandomTPerson(t2), Deadline = RandomDay(), Id = RandomString() });
			p2.Tasks.Add(new ProjectTask() { Name = "Create Blazor App 2", Description = ".NET 6.0 CORE Framework needed. please install it and create blazorproject with it", AssignedPerson = getRandomTPerson(t2), Deadline = RandomDay(), Id = RandomString() });

			Projects.Add(p2);

		}

		//Functions
		Person getRandomPerson()
		{
			return Contact.ToArray()[randy.Next(0, Contact.Count)];
		}
		Teammember getRandomTPerson(Team t)
		{
			return t.members.ToArray()[randy.Next(0, t.members.Count)];
		}
		DateTime RandomDay()
		{
			DateTime start = new DateTime(1995, 1, 1);
			int range = (DateTime.Today - start).Days;
			return start.AddDays(randy.Next(range));
		}
		string RandomString(int length = 8)
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