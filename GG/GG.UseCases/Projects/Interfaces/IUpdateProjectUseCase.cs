using GG.CoreBusiness;

namespace GG.UseCases.Projects.Interfaces
{
    public interface IUpdateProjectUseCase
    {
        Task<StatusReport<EmptyVal>> ExecuteAsync(int projectid);
    }
}