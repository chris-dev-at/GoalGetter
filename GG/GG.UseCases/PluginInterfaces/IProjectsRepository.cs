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
		//Task<Project> GetInventoryByIdAsync(int ProjectId);
		//Task AddProjectAsync(Project project);
		//Task<bool> ExistsAsync(Project project);
		//Task UpdateProjectAsync(Project project);
		//Task DeleteProjectAsync(Project project);
	}
}
