using GG.CoreBusiness;

namespace GG.UseCases.Projects.Interfaces
{
    public interface IViewProjectByIdUseCase
    {
        Task<StatusReport<Project>> ExecuteAsync(int projectid);
    }
}