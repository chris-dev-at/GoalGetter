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
	public class ViewPeopleByNameUseCase : IViewPeopleByNameUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public ViewPeopleByNameUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task<IEnumerable<Person>> ExecuteAsync(string name = "")
		{
			return await projectsRepository.GetPeopleByNameAsync(name);
		}
	}
}
