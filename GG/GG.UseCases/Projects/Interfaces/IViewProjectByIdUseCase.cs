using GG.CoreBusiness;

namespace GG.UseCases.Projects.Interfaces
{
    public interface IViewProjectByIdUseCase
    {
        Task<Project> ExecuteAsync(int projectid);
    }
}