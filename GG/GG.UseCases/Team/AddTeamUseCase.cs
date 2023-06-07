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
    public class AddTeamUseCase : IAddTeamUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public AddTeamUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<StatusReport<EmptyVal>> ExecuteAsync(Teammember person, int projectid)
		{
			var pro = await this.projectsRepository.GetProjectByIdAsync(projectid);

			if(pro.Value != null)
				return await this.projectsRepository.AddTeammemberToTeam(person, pro.Value);
			else
				return new StatusReport<EmptyVal>(
						StatusState.Error,
						EmptyVal.Empty,
						pro.Reason
					);
		}
	}
}
