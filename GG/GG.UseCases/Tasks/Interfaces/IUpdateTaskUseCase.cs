using GG.CoreBusiness;

namespace GG.UseCases.Tasks.Interfaces
{
    public interface IUpdateTaskUseCase
    {
        Task<StatusReport<EmptyVal>> ExecuteAsync(ProjectTask task, int projectid);
    }
}