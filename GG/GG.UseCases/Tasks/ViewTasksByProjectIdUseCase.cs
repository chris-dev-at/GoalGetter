using GG.CoreBusiness;
using GG.UseCases.PluginInterfaces;
using GG.UseCases.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.Tasks
{
    public class ViewTasksByProjectIdUseCase : IViewTasksByProjectIdUseCase
    {
        private readonly IProjectsRepository projectsRepository;
        public ViewTasksByProjectIdUseCase(IProjectsRepository projectsRepository)
        {
            this.projectsRepository = projectsRepository;
        }

        public async Task<IEnumerable<ProjectTask>> ExecuteAsync(string name, int projectid)
        {
            var pro = await this.projectsRepository.GetProjectByIdAsync(projectid);
            return await this.projectsRepository.GetTaskByNameWithinList(name, pro.Tasks);
        }
    }
}
