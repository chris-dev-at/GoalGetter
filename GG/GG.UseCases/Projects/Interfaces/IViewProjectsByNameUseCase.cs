using GG.CoreBusiness;

namespace GG.UseCases.Projects.Interfaces
{
    public interface IViewProjectsByNameUseCase
    {
        Task<IEnumerable<Project>> ExecuteAsync(string name = "");
    }
}