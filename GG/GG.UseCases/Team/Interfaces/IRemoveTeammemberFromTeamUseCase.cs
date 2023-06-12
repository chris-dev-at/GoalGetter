using GG.CoreBusiness;

namespace GG.UseCases.Team.Interfaces
{
    public interface IRemoveTeammemberFromTeamUseCase
    {
        Task<StatusReport<EmptyVal>> ExecuteAsync(Teammember t, int projectid);
    }
}