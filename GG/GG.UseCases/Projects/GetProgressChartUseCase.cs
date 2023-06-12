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
    public class GetProgressChartUseCase : IGetProgressChartUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public GetProgressChartUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<StatusReport<float>> ExecuteAsync(int projectid, ProgressStatus state)
		{
			var pro = this.projectsRepository.GetProjectByIdAsync(projectid).Result;
			return projectsRepository.GetPercentFromTaskState(state, pro.Value).Result;
		}
	}
}
