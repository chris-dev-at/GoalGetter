using GG.CoreBusiness;

namespace GG.UseCases.Tasks.Interfaces
{
    public interface IAddTaskUseCase
    {
        Task<StatusReport<EmptyVal>> ExecuteAsync(ProjectTask task, int projectid);
    }
}