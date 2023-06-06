using GG.CoreBusiness;

namespace GG.UseCases.Projects.Interfaces
{
    public interface IAddProjectUseCase
    {
        Task<StatusReport<EmptyVal>> ExecuteAsync(Project project);
    }
}