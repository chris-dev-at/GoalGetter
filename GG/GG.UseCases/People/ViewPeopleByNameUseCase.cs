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
