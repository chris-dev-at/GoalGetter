using GG.CoreBusiness;
using GG.UseCases.PluginInterfaces;
using GG.UseCases.Projects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.Projects
{
    public class ViewProjectsByNameUseCase : IViewProjectsByNameUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public ViewProjectsByNameUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<IEnumerable<Project>> ExecuteAsync(string name = "")
		{
			return await projectsRepository.GetProjectsByNameAsync(name);
		}
	}
}
