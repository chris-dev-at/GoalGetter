using GG.CoreBusiness;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.PluginInterfaces
{
	public interface IProjectsRepository
	{
		Task<StatusReport<IEnumerable<Project>>> GetProjectsByNameAsync(string name);
		Task<StatusReport<EmptyVal>> AddProjectAsync(Project project);
		Task<StatusReport<Project>> GetProjectByIdAsync(int ProjectId);
		Task<StatusReport<EmptyVal>> ChangeProjectStatus(Project project, ProgressStatus state);
		Task<StatusReport<IEnumerable<Person>>> GetPeopleByNameAsync(string name);
		Task<StatusReport<EmptyVal>> AddPersonAsync(Person person, IBrowserFile image);
		Task<StatusReport<Person>> GetPersonById(int id);
		Task<StatusReport<string>> SaveFileToServer(string fileDir, string fileName, IBrowserFile file);
		Task<StatusReport<EmptyVal>> SaveTextToFile(string fileDir, string fileName, string text);
		Task<StatusReport<string>> ReadTextFromFile(string fileDir, string fileName);
		Task<StatusReport<EmptyVal>> ChangePerson(Person person);
		Task<StatusReport<EmptyVal>> ChangeProject(Project project);
		Task<StatusReport<EmptyVal>> ChangeProjectTask(ProjectTask task, Project project);
		Task<StatusReport<EmptyVal>> ChangeTeammember(Teammember teammember, Project project);
		Task<StatusReport<IEnumerable<Person>>> GetPersonWithinPersonList(string person_name, IEnumerable<Person> persons);
		Task<StatusReport<IEnumerable<Teammember>>> GetTeammemberByNameWithinTeamAsync(string person_name, GG.CoreBusiness.Team team);
		Task<StatusReport<IEnumerable<ProjectTask>>> GetTaskByNameWithinList(string task_name, IEnumerable<ProjectTask> tasks);
		Task<StatusReport<EmptyVal>> RemovePersonCompletelyAsync(Person person);
		Task<StatusReport<EmptyVal>> RemoveTaskFromProject(ProjectTask task, Project p);
		Task<StatusReport<EmptyVal>> RemoveTeammemberFromTeamAsync(Teammember member, Project project);
		Task<StatusReport<EmptyVal>> RemovePersonFromProjectAsync(Person person, Project p);
		Task<StatusReport<EmptyVal>> RemoveProject(Project p);
		Task<StatusReport<IEnumerable<Person>>> GetAllPersonsIfnotAlreadyInTeamAsync(GG.CoreBusiness.Team team);
		Task<StatusReport<bool>> PersonAlreadyInTeam(Person person, GG.CoreBusiness.Team team);
		Task<StatusReport<EmptyVal>> AddTeammemberToTeam(Teammember member, Project project);
		Task<StatusReport<EmptyVal>> AddTaskToProject(ProjectTask task, Project project);
		Task<StatusReport<EmptyVal>> SaveProjectToFile(Project p);
		Task<StatusReport<EmptyVal>> SaveAllProjectToFile();
		Task<StatusReport<EmptyVal>> SaveContacts();
		Task<StatusReport<EmptyVal>> DeleteFile(string fileDir, string fileName);
		bool IsDirectoryNameAllowed(string directoryName);

		//Task<bool> ExistsAsync(Project project);
		//Task UpdateProjectAsync(Project project);
		//Task DeleteProjectAsync(Project project);
	}
}