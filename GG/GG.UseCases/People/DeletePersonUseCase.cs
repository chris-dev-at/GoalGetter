using GG.CoreBusiness;
using GG.UseCases.People.Interfaces;
using GG.UseCases.PluginInterfaces;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.People
{
    public class DeletePersonUseCase : IDeletePersonUseCase
    {
        private readonly IProjectsRepository projectsRepository;
        public DeletePersonUseCase(IProjectsRepository projectsRepository)
        {
            this.projectsRepository = projectsRepository;
        }
        public async Task<StatusReport<EmptyVal>> ExecuteAsync(Person person)
        {
            return await projectsRepository.RemovePersonCompletelyAsync(person);
        }
    }
}
