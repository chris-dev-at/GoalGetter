using GG.CoreBusiness;
using GG.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.Projects
{
	public class DeleteProjectUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public DeleteProjectUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		//public async Task<StatusReport<EmptyVal>> ExecuteAsync(int projectid)
		//{
		//	var pro = this.projectsRepository.Re(projectid).Result;
		//	return projectsRepository.ChangeProject(pro.Value).Result;
		//}
	}
}
