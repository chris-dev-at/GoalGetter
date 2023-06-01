using GG.CoreBusiness;

namespace GG.UseCases.Tasks.Interfaces
{
    public interface IViewTasksByProjectIdUseCase
    {
        Task<IEnumerable<ProjectTask>> ExecuteAsync(string name, int projectid);
    }
}