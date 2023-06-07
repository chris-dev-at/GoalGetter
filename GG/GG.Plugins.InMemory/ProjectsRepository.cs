using GG.CoreBusiness;
using GG.UseCases.PluginInterfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using static MudBlazor.Colors;

namespace GG.Plugins.InMemory
{
	public class ProjectsRepository : IProjectsRepository
	{
		private List<Project> Projects = new List<Project>();
		private List<Person> Contact = new List<Person>();

		public ProjectsRepository()
		{
			//Load Data
			CreateTestData();
		}

		//CRUD for Project Entitys
		public async Task<StatusReport<IEnumerable<Project>>> GetProjectsByNameAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return new StatusReport<IEnumerable<Project>>(
						StatusState.Normal,
						await Task.FromResult(Projects),
						"Projects have been found"
					);

			IEnumerable<Project> result = Projects.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
			return new StatusReport<IEnumerable<Project>>(
						result.Any() ? StatusState.Normal : StatusState.Warning,
						result,
						result.Any() ? "Projects have been found" : "Projects have been found"
					);
		}

		public Task<StatusReport<EmptyVal>> AddProjectAsync(Project project)
		{
			if (Projects.Any(x => x.Name.Equals(project.Name, StringComparison.OrdinalIgnoreCase)))
				return Task.FromResult(new StatusReport<EmptyVal>(
					StatusState.Error,
					EmptyVal.Empty,
					"There is already a project with this name"
				));

			var maxId = Projects.Max(x => x.Id);
			project.Id = maxId + 1;

			Projects.Add(project);
			return Task.FromResult(new StatusReport<EmptyVal>(
					StatusState.Success,
					EmptyVal.Empty,
					"Project has been added"
				));
		}

		public async Task<StatusReport<Project>> GetProjectByIdAsync(int ProjectId)
		{
			var pro = Projects.FirstOrDefault(x => x.Id == ProjectId);
			return new StatusReport<Project>(
					pro == null ? StatusState.Normal : StatusState.Warning,
					pro,
					pro == null ? "No projects have been found" : "Project has been found"
				);
		}

		//CRUD for People Entitys

		public async Task<StatusReport<IEnumerable<Person>>> GetPeopleByNameAsync(string name)
		{
			IEnumerable<Person> result = GetPersonWithinPersonList(name, Contact).Result.Value;
			return new StatusReport<IEnumerable<Person>>(
					result.Any() ? StatusState.Normal : StatusState.Warning,
					result,
					result.Any() ? "No people have been found" : "People have been found"
				);
		}

		public async Task<StatusReport<IEnumerable<Person>>> GetPersonWithinPersonList(string person_name, IEnumerable<Person> personlist)
		{
			if (string.IsNullOrWhiteSpace(person_name))
				return new StatusReport<IEnumerable<Person>>(
						StatusState.Normal,
						await Task.FromResult(personlist),
						"People have been found"
					);

			IEnumerable<Person> result = Contact.Where(x =>
					x.Firstname.Contains(person_name, StringComparison.OrdinalIgnoreCase) ||
					x.Lastname.Contains(person_name, StringComparison.OrdinalIgnoreCase));

			return new StatusReport<IEnumerable<Person>>(
					result.Any() ? StatusState.Normal : StatusState.Warning,
					result,
					result.Any() ? "No people have been found" : "People have been found"
				);
		}

		public async Task<StatusReport<IEnumerable<Teammember>>> GetTeammemberByNameWithinTeamAsync(string person_name, Team team)
		{
			if (team.members == null)
				return new StatusReport<IEnumerable<Teammember>>(
						StatusState.Warning,
						new List<Teammember>(),
						"No people have been found"
					);
			if (string.IsNullOrWhiteSpace(person_name))
				return new StatusReport<IEnumerable<Teammember>>(
						StatusState.Normal,
						await Task.FromResult(team.members),
						"People have been found"
					);

			IEnumerable<Teammember> result = team.members.Where(x =>
					x.person.Firstname.Contains(person_name, StringComparison.OrdinalIgnoreCase) ||
					x.person.Lastname.Contains(person_name, StringComparison.OrdinalIgnoreCase));

			return new StatusReport<IEnumerable<Teammember>>(
						result.Any() ? StatusState.Normal : StatusState.Warning,
						result,
						result.Any() ? "No people have been found" : "People have been found"
					);

		}

		public async Task<StatusReport<IEnumerable<ProjectTask>>> GetTaskByNameWithinList(string task_name, IEnumerable<ProjectTask> tasks)
		{
			if (string.IsNullOrWhiteSpace(task_name))
				return new StatusReport<IEnumerable<ProjectTask>>(
							StatusState.Normal,
							await Task.FromResult(tasks),
							"Tasks have been found"
						);

			IEnumerable<ProjectTask> result = tasks.Where(x =>
					x.Name.Contains(task_name, StringComparison.OrdinalIgnoreCase) ||
					x.Description.Contains(task_name, StringComparison.OrdinalIgnoreCase));

			return new StatusReport<IEnumerable<ProjectTask>>(
						result.Any() ? StatusState.Normal : StatusState.Warning,
						result,
						result.Any() ? "No tasks have been found" : "Tasks have been found"
					);
		}

		public async Task<StatusReport<EmptyVal>> RemoveTeammemberFromTeamAsync(Teammember member, Team t)
		{
			if (t.members.Contains(member))
			{
				t.members.Remove(member);
				return new StatusReport<EmptyVal>(
							StatusState.Success,
							EmptyVal.Empty,
							"Teammember has been removed from Team"
						);
			}

			return new StatusReport<EmptyVal>(
						StatusState.Failed,
						EmptyVal.Empty,
						"Member was not found within Team"
					);
		}
		public async Task<StatusReport<EmptyVal>> RemovePersonCompletelyAsync(Person person)
		{
			if (Contact.Contains(person))
			{
				//Remove out of all Projects
				foreach (Project p in Projects)
				{
					await RemovePersonFromProjectAsync(person, p);
				}

				//Remove from Contacts
				Contact.Remove(person);

				return new StatusReport<EmptyVal>(
							StatusState.Success,
							EmptyVal.Empty,
							"Person has been removed"
						);
			}

			return new StatusReport<EmptyVal>(
						StatusState.Failed,
						EmptyVal.Empty,
						"Member was not found within Contacts"
					);


		}
		public async Task<StatusReport<EmptyVal>> RemovePersonFromProjectAsync(Person person, Project p)
		{
			//Check if person is within Team
			if (p.assignedTeam.members.Select(t => t.person).Contains(person))
			{
				foreach (Teammember member in p.assignedTeam.members)
				{
					if (member.person == person)
					{
						await RemoveTeammemberFromTeamAsync(member, p.assignedTeam);

						//Remove from all Assigned Tasks
						foreach (ProjectTask task in p.Tasks)
						{
							if (task.AssignedPerson == member)
								task.AssignedPerson = null;
						}
					}
				}
				return new StatusReport<EmptyVal>(
							StatusState.Success,
							EmptyVal.Empty,
							"Person has been removed from project"
						);
			}

			return new StatusReport<EmptyVal>(
						StatusState.Failed,
						EmptyVal.Empty,
						"Member was not found within Project"
					);

		}

		//Continue Status Reporting
		public async Task<StatusReport<EmptyVal>> AddPersonAsync(Person person, IBrowserFile image)
		{
			if (Contact.Any(x => x.Firstname.Equals(person.Firstname, StringComparison.OrdinalIgnoreCase)))
				return new StatusReport<EmptyVal>(
						StatusState.Error,
						EmptyVal.Empty,
						"Person already exists"
					);

			//Handle ID
			var maxId = Contact.Max(x => x.Id);
			person.Id = maxId + 1;

			//Handle Avatar
			if (image != null)
			{
				if (image.Size > 5 * 1024 * 1024) // 5MB in bytes
				{
					return new StatusReport<EmptyVal>(
							StatusState.Error,
							EmptyVal.Empty,
							"The Avatar you selected is to big (<5MB)"
						);
				}

				string fileName = $"{person.Id}_{person.Lastname}{person.Firstname}.{image.Name.Split(".")[image.Name.Split(".").Length-1]}";
				

				person.AvatarPath = (await SaveFileToServer("profiles", fileName, image)).Value;
			}

			Contact.Add(person);
			return new StatusReport<EmptyVal>(
						StatusState.Success,
						EmptyVal.Empty,
						"Person has been added"
					);
		}

		public async Task<StatusReport<IEnumerable<Person>>> GetAllPersonsIfnotAlreadyInTeamAsync(Team team)
		{
			IEnumerable<Person> result = Contact.Where(x => !team.members.Select(t => t.person).Contains(x));
			return new StatusReport<IEnumerable<Person>>(
						result.Any() ? StatusState.Normal : StatusState.Warning,
						result,
						result.Any() ? "No people have been found" : "People have been found"
					);
		}

		public async Task<StatusReport<bool>> PersonAlreadyInTeam(Person person, Team team)
		{
			bool result = team.members.Select(t => t.person).Contains(person);
			return new StatusReport<bool>(
						StatusState.Normal,
						result,
						result != null ? "Person was not found within Team" : "Person was found within Team"
					);
		}

		public async Task<StatusReport<EmptyVal>> AddTeammemberToTeam(Teammember member, Team team)
		{
			if (PersonAlreadyInTeam(member.person, team).Result.Value)
				return new StatusReport<EmptyVal>(
						StatusState.Error,
						EmptyVal.Empty,
						"Person already within Team"
					);


			team.members.Add(member);

			return new StatusReport<EmptyVal>(
						StatusState.Success,
						EmptyVal.Empty,
						"Person has been added"
					);
		}

		public async Task<StatusReport<EmptyVal>> AddTaskToProject(ProjectTask task, Project projects)
		{
			projects.Tasks.Add(task);

			return new StatusReport<EmptyVal>(
						StatusState.Success,
						EmptyVal.Empty,
						"Task has been added"
					);
		}

		public async Task<StatusReport<string>> SaveFileToServer(string fileDir, string fileName, IBrowserFile file)
		{
			string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
			string directory = Path.Combine(wwwrootPath, fileDir);
			string filePath = Path.Combine(directory, fileName);

			//ensure Directory exists
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.OpenReadStream().CopyToAsync(stream);
			}

			return new StatusReport<string>(
							StatusState.Success,
							Path.Combine(fileDir, fileName),
							$"File was successfully uploaded"
						);
		}

		public async Task<StatusReport<EmptyVal>> SaveProjectToFile(Project p)
		{
			//Todo save Projects
			//Todo Save Address not string of address in Person

			return new StatusReport<EmptyVal>(
							StatusState.Success,
							EmptyVal.Empty,
							$"Project has been saved"
						);
		}

		public async Task<StatusReport<EmptyVal>> SaveAllProjectToFile()
		{
			foreach (var project in Projects)
			{
				await SaveProjectToFile(project);
			}

			return new StatusReport<EmptyVal>(
							StatusState.Success,
							EmptyVal.Empty,
							$"All Projects have been saved"
						);
		}

		public async Task<StatusReport<EmptyVal>> SaveContacts()
		{
			//Todo save Contacts
			return new StatusReport<EmptyVal>(
							StatusState.Success,
							EmptyVal.Empty,
							$"Contact has been saved"
						);
		}

		#region Testdata

		Random randy = new Random();


		void CreateTestData()
		{
			Contact.Add(new Person()
			{
				Id = 1,
				Firstname = "Rainer",
				Lastname = "Winkler",
				Address = "DE, Emskirchen 91448, Altschauerberg 8",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "r.winkler@htlkrems.at"
			});

			Contact.Add(new Person()
			{
				Id = 2,
				Firstname = "Ilse",
				Lastname = "Nigischer",
				Address = "AT Waidhofen a. T. 3830, Buxdihudenstraße 1",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "i.nigischer@htlkrems.at"
			});

			Contact.Add(new Person()
			{
				Id = 3,
				Firstname = "Herwig",
				Lastname = "Macho",
				Address = "AT Zwettl 3910, Propstei 8",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "h.macho@htlkrems.at"
			});

			Contact.Add(new Person()
			{
				Id = 4,
				Firstname = "Lukas",
				Lastname = "Kolinsky",
				Address = "AT, Zwettl 3910, Propstei 7",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "lukas.kolinsky1@gmail.com"
			});

			Contact.Add(new Person()
			{
				Id = 5,
				Firstname = "Mulham",
				Lastname = "Taylouni",
				Address = "AT, Gmünd 3950, Schremserstraße 69",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "contact@taylouni.me"
			});

			Contact.Add(new Person()
			{
				Id = 6,
				Firstname = "Christian",
				Lastname = "Wiesinger",
				Address = "AT, Rappottenstein 3911, Burgunderweg 109",
				AvatarPath = @"https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png",
				Email = "contact@taylouni.me"
			});

			Contact.Add(new Person()
			{
				Id = 7,
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

			Project p1 = new Project() { Name = "Project 1", Budget = 99.50f, status = ProgressStatus.In_Progress, assignedTeam = t1, Description = "This project is for testing", Id = 1, Tasks = new List<ProjectTask>() };

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

			Project p2 = new Project() { Name = "Funky Munky", Budget = 99.50f, status = ProgressStatus.In_Progress, assignedTeam = t2, Id = 2, Tasks = new List<ProjectTask>() };

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
		#endregion
		//
	}
}