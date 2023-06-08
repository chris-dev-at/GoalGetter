using GG.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.Tasks.Interfaces
{
	public interface IRemoveTaskUseCase
	{
		Task<StatusReport<EmptyVal>> ExecuteAsync(ProjectTask task, int projectid);
	}
}
