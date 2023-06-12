using GG.CoreBusiness;
using GG.UseCases.PluginInterfaces;
using GG.UseCases.Team.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.Team
{
    public class RemoveTeammemberFromTeamUseCase : IRemoveTeammemberFromTeamUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public RemoveTeammemberFromTeamUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<StatusReport<EmptyVal>> ExecuteAsync(Teammember t, int projectid)
		{
			var pro = await this.projectsRepository.GetProjectByIdAsync(projectid);

			if (pro.Value != null)
				return this.projectsRepository.RemovePersonFromProjectAsync(t.person, pro.Value).Result;
			else
				return new StatusReport<EmptyVal>(
						StatusState.Error,
						EmptyVal.Empty,
						pro.Reason
					);
		}
	}
}
