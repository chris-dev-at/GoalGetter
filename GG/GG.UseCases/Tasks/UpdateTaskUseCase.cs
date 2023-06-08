using GG.CoreBusiness;
using GG.UseCases.PluginInterfaces;
using GG.UseCases.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.Tasks
{
    public class UpdateTaskUseCase : IUpdateTaskUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public UpdateTaskUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<StatusReport<EmptyVal>> ExecuteAsync(ProjectTask task, int projectid)
		{
			var pro = this.projectsRepository.GetProjectByIdAsync(projectid).Result;
			return projectsRepository.ChangeProjectTask(task, pro.Value).Result;
		}
	}
}
