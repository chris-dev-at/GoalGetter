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
    public class AddProjectUseCase : IAddProjectUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public AddProjectUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task ExecuteAsync(Project project)
		{
			await this.projectsRepository.AddProjectAsync(project);
		}
	}
}
