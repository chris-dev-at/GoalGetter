using GG.CoreBusiness;
using GG.UseCases.People.Interfaces;
using GG.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.People
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
            await projectsRepository.AddPersonAsync(person);
        }
    }
}
