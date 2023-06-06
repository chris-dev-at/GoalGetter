using GG.CoreBusiness;

namespace GG.UseCases.Projects.Interfaces
{
    public interface IViewProjectsByNameUseCase
    {
        Task<StatusReport<IEnumerable<Project>>> ExecuteAsync(string name = "");
    }
}