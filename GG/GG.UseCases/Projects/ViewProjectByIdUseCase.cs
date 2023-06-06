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
    public class ViewProjectByIdUseCase : IViewProjectByIdUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public ViewProjectByIdUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<Project> ExecuteAsync(int projectid)
		{
			return this.projectsRepository.GetProjectByIdAsync(projectid).Result.Value;
		}
	}
}
