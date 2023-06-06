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
    public class ViewTeamMembersByProjectIdUseCase : IViewTeamMembersByProjectIdUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public ViewTeamMembersByProjectIdUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<StatusReport<IEnumerable<Teammember>>> ExecuteAsync(string name, int projectid)
		{
			var pro = await this.projectsRepository.GetProjectByIdAsync(projectid);

			if(pro.Value != null) 
				return this.projectsRepository.GetTeammemberByNameWithinTeamAsync(name, pro.Value.assignedTeam).Result;
			else
				return new StatusReport<IEnumerable<Teammember>>(
						StatusState.Error,
						null,
						pro.Reason
					);
		}
	}
}
