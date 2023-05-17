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
    public class AddPersonUseCase : IAddPersonUseCase
	{
		private readonly IProjectsRepository projectsRepository;
		public AddPersonUseCase(IProjectsRepository projectsRepository)
		{
			this.projectsRepository = projectsRepository;
		}
		public async Task ExecuteAsync(Person person)
		{
			await this.projectsRepository.AddPersonAsync(person);
		}
	}
}
