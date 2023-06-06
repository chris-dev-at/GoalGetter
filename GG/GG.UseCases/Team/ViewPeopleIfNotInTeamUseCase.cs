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
    public class ViewPeopleIfNotInTeamUseCase : IViewPeopleIfNotInTeamUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public ViewPeopleIfNotInTeamUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<StatusReport<IEnumerable<Person>>> ExecuteAsync(int projectid)
		{
			var pro = await this.projectsRepository.GetProjectByIdAsync(projectid);
<<<<<<< HEAD
			return this.projectsRepository.GetAllPersonsIfnotAlreadyInTeamAsync(pro.Value.assignedTeam).Result.Value;
			//return this.projectsRepository.GetPeopleByNameAsync("").Result.Value;
=======
			if (pro.Value != null)
				return this.projectsRepository.GetAllPersonsIfnotAlreadyInTeamAsync(pro.Value.assignedTeam).Result;
			else
				return new StatusReport<IEnumerable<Person>>(
						StatusState.Error,
						null,
						pro.Reason
					);
>>>>>>> 0ee8f39a992afeef4ad33834d55523db3520e928
		}
	}
}
