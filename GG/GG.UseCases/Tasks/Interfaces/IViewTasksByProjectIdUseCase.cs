using GG.CoreBusiness;

namespace GG.UseCases.Tasks.Interfaces
{
    public interface IViewTasksByProjectIdUseCase
    {
        Task<StatusReport<IEnumerable<ProjectTask>>> ExecuteAsync(string name, int projectid);
    }
}