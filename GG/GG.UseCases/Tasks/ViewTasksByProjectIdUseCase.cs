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

        public async Task<StatusReport<IEnumerable<ProjectTask>>> ExecuteAsync(string name, int projectid)
        {
            var pro = await this.projectsRepository.GetProjectByIdAsync(projectid);
            if(pro.Value != null)
            {
                var tmp = this.projectsRepository.GetTaskByNameWithinList(name, pro.Value.Tasks).Result;

				if (tmp.Value != null)
                    return tmp;
                else
					return new StatusReport<IEnumerable<ProjectTask>>(
						StatusState.Normal,
						new List<ProjectTask>(),
						tmp.Reason
					);
			}
            else
				return new StatusReport<IEnumerable<ProjectTask>>(
						StatusState.Error,
						null,
						pro.Reason
					);
		}
    }
}
