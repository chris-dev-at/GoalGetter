using GG.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases.Projects.Interfaces
{
	public interface IRemoveProjectUseCase
	{
		Task<StatusReport<EmptyVal>> ExecuteAsync(int projectid);
	}
}
