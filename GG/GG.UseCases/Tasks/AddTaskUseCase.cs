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
    public class AddTaskUseCase : IAddTaskUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public AddTaskUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<StatusReport<EmptyVal>> ExecuteAsync(ProjectTask task, int projectid)
		{
			var pro = await this.projectsRepository.GetProjectByIdAsync(projectid);

			if (pro.Value != null)
				return await this.projectsRepository.AddTaskToProject(task, pro.Value);
			else
				return new StatusReport<EmptyVal>(
						StatusState.Error,
						EmptyVal.Empty,
						pro.Reason
					);
		}
	}
}
