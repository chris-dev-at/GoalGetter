using GG.CoreBusiness;

namespace GG.UseCases.Projects.Interfaces
{
    public interface IGetProgressChartUseCase
    {
        Task<StatusReport<float>> ExecuteAsync(int projectid, ProgressStatus state);
    }
}