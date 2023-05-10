using GG.CoreBusiness;

namespace GG.UseCases.Projects.Interfaces
{
    public interface IAddProjectUseCase
    {
        Task ExecuteAsync(Project project);
    }
}