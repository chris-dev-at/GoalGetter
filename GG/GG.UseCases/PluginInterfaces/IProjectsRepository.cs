using GG.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.PluginInterfaces
{
	public interface IProjectsRepository
	{
		Task<IEnumerable<Project>> GetProjectsByNameAsync(string name);
		Task AddProjectAsync(Project project);
		Task<Project> GetProjectByIdAsync(int ProjectId);

		Task<IEnumerable<Person>> GetPeopleByNameAsync(string name);
		Task AddPersonAsync(Person person);

		Task <IEnumerable<Person>> GetPersonWithinPersonList(string person_name, IEnumerable<Person> persons);

		Task<IEnumerable<Teammember>> GetTeammemberByNameWithinTeamAsync(string person_name, Team team);
		Task<IEnumerable<ProjectTask>> GetTaskByNameWithinList(string task_name, IEnumerable<ProjectTask> tasks);
		Task RemovePersonCompletelyAsync(Person person);
		Task RemoveTeammemberFromTeamAsync(Teammember member, Team t);
		Task RemovePersonFromProjectAsync(Person person, Project p);

        //Task<bool> ExistsAsync(Project project);
        //Task UpdateProjectAsync(Project project);
        //Task DeleteProjectAsync(Project project);
    }
}
