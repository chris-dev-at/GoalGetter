using GG.CoreBusiness;
using GG.UseCases.PluginInterfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Libraries.DistanceAddressCalculator;
using static MudBlazor.Colors;
using System.IO.Enumeration;
using System.IO;
using Newtonsoft.Json;
using MudBlazor;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GG.Plugins.InMemory
{
	public class ProjectsRepository : IProjectsRepository
	{
		private List<Project> Projects = new List<Project>();
		private List<Person> Contact = new List<Person>();

		public ProjectsRepository()
		{
			//Load Data
			var status = LoadData();
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

		public async Task<StatusReport<EmptyVal>> AddProjectAsync(Project project)
		{
			if (Projects.Any(x => x.Name.Equals(project.Name, StringComparison.OrdinalIgnoreCase)))
				return new StatusReport<EmptyVal>(
					StatusState.Error,
					EmptyVal.Empty,
					"There is already a project with this name"
				);

			if (!IsDirectoryNameAllowed(project.Name))
				return new StatusReport<EmptyVal>(
					StatusState.Error,
					EmptyVal.Empty,
					"Projectname contains invalid characters (\\/:*?\"<>|)"
				);

			int maxId = 0;
			if(Projects.Count > 0)
			{
				maxId = Projects.Max(x => x.Id);
			}
			project.Id = maxId+1;

			Projects.Add(project);

			await SaveProjectToFile(project);

			return new StatusReport<EmptyVal>(
					StatusState.Success,
					EmptyVal.Empty,
					"Project has been added"
				);
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

		public async Task<StatusReport<EmptyVal>> ChangeProjectStatus(Project project, ProgressStatus state)
		{
			project.status = state;

			await SaveProjectToFile(project);

			return new StatusReport<EmptyVal>(
					StatusState.Success,
					EmptyVal.Empty,
					"ProgressState has been changed"
				);
		}

		//CRUD for Updates
		public async Task<StatusReport<EmptyVal>> ChangeProjectTask(ProjectTask task, Project project)
		{
			await SaveProjectToFile(project);
			return new StatusReport<EmptyVal>(
					StatusState.Success,
					EmptyVal.Empty,
					"Task has been updated"
				);
		}

		public async Task<StatusReport<EmptyVal>> ChangeProject(Project project)
		{
			await SaveProjectToFile(project);
			return new StatusReport<EmptyVal>(
					StatusState.Success,
					EmptyVal.Empty,
					"Project has been updated"
				);
		}

		public async Task<StatusReport<EmptyVal>> ChangePerson(Person person)
		{
			await SaveContacts();
			return new StatusReport<EmptyVal>(
					StatusState.Success,
					EmptyVal.Empty,
					"Person has been updated"
				);
		}
		public async Task<StatusReport<EmptyVal>> ChangeTeammember(Teammember teammember, Project project)
		{
			await SaveProjectToFile(project);
			return new StatusReport<EmptyVal>(
					StatusState.Success,
					EmptyVal.Empty,
					"Teammember has been updated"
				);
		}


		//CRUD for People Entitys
		public async Task<StatusReport<Person>> GetPersonById(int id)
		{
			var person = Contact.Where(x => x.Id == id).FirstOrDefault();
			return new StatusReport<Person>(
					person == null ? StatusState.Normal : StatusState.Error,
					person,
					person == null ? "No person has been found" : "Person has been found"
				);
		}

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

		public async Task<StatusReport<EmptyVal>> RemoveTaskFromProject(ProjectTask task, Project p)
		{
			p.Tasks.Remove(task);

			await SaveProjectToFile(p);

			return new StatusReport<EmptyVal>(
							StatusState.Success,
							EmptyVal.Empty,
							"Task has been removed"
						);
		}

		public async Task<StatusReport<EmptyVal>> RemoveTeammemberFromTeamAsync(Teammember member, Project p)
		{
			if (p.assignedTeam.members.Contains(member))
			{
				p.assignedTeam.members.Remove(member);
				return new StatusReport<EmptyVal>(
							StatusState.Success,
							EmptyVal.Empty,
							"Teammember has been removed from Team"
						);
			}

			await SaveProjectToFile(p);

			return new StatusReport<EmptyVal>(
						StatusState.Failed,
						EmptyVal.Empty,
						"Member was not found within Team"
					);
		}

		public async Task<StatusReport<EmptyVal>> RemoveProject(Project p)
		{
			Projects.Remove(p);
			var status = await DeleteFile(Path.Combine("ApplicationData", "projects"), $"{p.Name}.json");

			return new StatusReport<EmptyVal>(
								status.State,
								EmptyVal.Empty,
								status.State == StatusState.Success ? "Project has been deleted" : "Project does not exist in ApplicationData"
							);
		}

		public async Task<StatusReport<EmptyVal>> RemovePersonCompletelyAsync(Person person)
		{
			if (!Contact.Contains(person))
			{
				return new StatusReport<EmptyVal>(
						StatusState.Failed,
						EmptyVal.Empty,
						"Member was not found within Contacts"
					);
			}

			//Remove out of all Projects
			foreach (Project p in Projects)
			{
				await RemovePersonFromProjectAsync(person, p);
			}

			//Remove from Contacts
			Contact.Remove(person);

			await SaveContacts();

			return new StatusReport<EmptyVal>(
						StatusState.Success,
						EmptyVal.Empty,
						"Person has been removed"
					);
		}
		public async Task<StatusReport<EmptyVal>> RemovePersonFromProjectAsync(Person person, Project p)
		{
			//Check if person is within Team
			if (!p.assignedTeam.members.Select(t => t.person).Contains(person))
			{
				return new StatusReport<EmptyVal>(
						StatusState.Failed,
						EmptyVal.Empty,
						"Member was not found within Project"
					);
			}

			foreach (Teammember member in p.assignedTeam.members)
			{
				if (member.person == person)
				{
					await RemoveTeammemberFromTeamAsync(member, p);

					//Remove from all Assigned Tasks
					foreach (ProjectTask task in p.Tasks)
					{
						if (task.AssignedPerson == member)
							await RemoveTaskFromProject(task, p);
					}
				}
			}

			await SaveProjectToFile(p);

			return new StatusReport<EmptyVal>(
						StatusState.Success,
						EmptyVal.Empty,
						"Person has been removed from project"
					);

		}

		//Continue Status Reporting
		public async Task<StatusReport<EmptyVal>> AddPersonAsync(Person person, IBrowserFile image)
		{
			if (Contact.Any(x => x.Firstname.Equals(person.Firstname, StringComparison.OrdinalIgnoreCase))
				& Contact.Any(x => x.Lastname.Equals(person.Lastname, StringComparison.OrdinalIgnoreCase)))
				return new StatusReport<EmptyVal>(
						StatusState.Error,
						EmptyVal.Empty,
						"Person already exists"
					);

			if(!IsDirectoryNameAllowed(person.Firstname+person.Lastname))
				return new StatusReport<EmptyVal>(
					StatusState.Error,
					EmptyVal.Empty,
					"Personname contains invalid characters (\\/:*?\"<>|)"
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


			await SaveContacts();

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

		public async Task<StatusReport<EmptyVal>> AddTeammemberToTeam(Teammember member, Project p)
		{
			if (PersonAlreadyInTeam(member.person, p.assignedTeam).Result.Value)
				return new StatusReport<EmptyVal>(
						StatusState.Error,
						EmptyVal.Empty,
						"Person already within Team"
					);

			member.PersonId = member.person.Id;
			p.assignedTeam.members.Add(member);

			await SaveProjectToFile(p);


			return new StatusReport<EmptyVal>(
						StatusState.Success,
						EmptyVal.Empty,
						"Person has been added"
					);
		}

		public async Task<StatusReport<EmptyVal>> AddTaskToProject(ProjectTask task, Project project)
		{
			task.PersonId = task.AssignedPerson.person.Id;
			project.Tasks.Add(task);

			await SaveProjectToFile(project);

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
				Directory.CreateDirectory(directory);

			//overwrite Profile Picture
			if(File.Exists(filePath))
				File.Delete(filePath);

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

		public async Task<StatusReport<EmptyVal>> SaveTextToFile(string fileDir, string fileName, string text)
		{
			string directory = Path.Combine(Directory.GetCurrentDirectory(), fileDir);
			string filePath = Path.Combine(directory, fileName);

			//ensure Directory exists
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			//ensure File exists
			if(!File.Exists(filePath))
				File.Create(filePath).Close();

			using (var sw = new StreamWriter(filePath, false))
			{
				await sw.WriteLineAsync(text);
			}

			return new StatusReport<EmptyVal>(
							StatusState.Success,
							EmptyVal.Empty,
							$"File has been saved"
						);
		}

		public async Task<StatusReport<string>> ReadTextFromFile(string fileDir, string fileName)
		{
			string directory = Path.Combine(Directory.GetCurrentDirectory(), fileDir);
			string filePath = Path.Combine(directory, fileName);

			if (!File.Exists(filePath))
			{
				return new StatusReport<string>(
								StatusState.Error,
								null,
								$"File does not exist"
							);
			}

			return new StatusReport<string>(
								StatusState.Normal,
								await File.ReadAllTextAsync(filePath),
								$"File has been read"
							);
		}

		public async Task<StatusReport<EmptyVal>> SaveProjectToFile(Project p)
		{
			var jsonString = JsonConvert.SerializeObject(p, Formatting.Indented);

			await SaveTextToFile(Path.Combine("ApplicationData", "projects"), $"{p.Name}.json", jsonString);

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
			var jsonString = JsonConvert.SerializeObject(Contact, Formatting.Indented);

			await SaveTextToFile("ApplicationData", "ContactList.json", jsonString);

			return new StatusReport<EmptyVal>(
							StatusState.Success,
							EmptyVal.Empty,
							$"Contact has been saved"
						);
		}

		public async Task<StatusReport<EmptyVal>> DeleteFile(string fileDir, string fileName)
		{
			string directory = Path.Combine(Directory.GetCurrentDirectory(), fileDir);
			string filePath = Path.Combine(directory, fileName);

			if (!File.Exists(filePath))
			{
				return new StatusReport<EmptyVal>(
								StatusState.Error,
								EmptyVal.Empty,
								$"File does not exist"
							);
			}
			File.Delete(filePath);

			return new StatusReport<EmptyVal>(
								StatusState.Success,
								EmptyVal.Empty,
								$"File has been deleted"
							);
		}

		public async Task<StatusReport<EmptyVal>> LoadData()
		{
			Contact = JsonConvert.DeserializeObject<List<Person>>((await ReadTextFromFile("ApplicationData", "ContactList.json")).Value);

			string directory = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "ApplicationData"), "projects");

			foreach (var item in Directory.GetFiles(directory))
			{
				Project current = JsonConvert.DeserializeObject<Project>(await File.ReadAllTextAsync(Path.Combine(directory, item)));

				//restore links to people from contacts
				if (current == null) throw new NullReferenceException("Faulty json could not be loaded");
				foreach (var member in current.assignedTeam.members)
				{
					Person tmp = GetPersonById(member.PersonId).Result.Value;
					if (tmp == null) throw new NullReferenceException($"Faulty json could not be loaded (Person with ID: {member.PersonId} does not exist)");
					member.person = tmp;
				}
				foreach (var task in current.Tasks)
				{
					Teammember tmp = current.assignedTeam.members.Where(x => x.person.Id == task.PersonId).FirstOrDefault();
					if (tmp == null) throw new NullReferenceException($"Faulty json could not be loaded (Person with ID: {task.PersonId} does not exist)");
					task.AssignedPerson = tmp;
				}

				Projects.Add(current);
			}

			return new StatusReport<EmptyVal>(
						StatusState.Success,
						EmptyVal.Empty,
						$"Data has been loaded"
					);
		}

		public bool IsDirectoryNameAllowed(string directoryName)
		{
			// Define the pattern for allowed directory names
			string pattern = @"^[a-zA-Z0-9_\-\. ]+$";

			// Check if the directory name matches the pattern
			bool isMatch = Regex.IsMatch(directoryName, pattern);

			return isMatch;
		}

		public async Task<StatusReport<float>> GetPercentFromTaskState(ProgressStatus state, Project p)
		{
			float state_task_count = p.Tasks.Where(x => x.Status == state).Count();

			if(p.Tasks.Count == 0 || state_task_count == 0)
			{
				return new StatusReport<float>(
						StatusState.Success,
						0,
						$"Percentage has been calculated [divide by 0 skipped]"
					);
			}

			float percent = state_task_count / p.Tasks.Count;

			return new StatusReport<float>(
						StatusState.Success,
						percent,
						$"Percentage has been calculated"
					);
		}


		/*
		 Task<StatusReport<int>> GetPercentUpcommingFromProject(Project p);
		Task<StatusReport<int>> GetPercentInProgressFromProject(Project p);
		 * */
	}
}