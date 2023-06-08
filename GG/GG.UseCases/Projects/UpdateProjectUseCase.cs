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
    public class UpdateProjectUseCase : IUpdateProjectUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public UpdateProjectUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<StatusReport<EmptyVal>> ExecuteAsync(int projectid)
		{
			var pro = this.projectsRepository.GetProjectByIdAsync(projectid).Result;
			return projectsRepository.ChangeProject(pro.Value).Result;
		}
	}
}
