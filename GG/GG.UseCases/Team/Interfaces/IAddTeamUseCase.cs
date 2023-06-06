using GG.CoreBusiness;

namespace GG.UseCases.Team.Interfaces
{
    public interface IAddTeamUseCase
    {
        Task<StatusReport<EmptyVal>> ExecuteAsync(Teammember person, int projectid);
    }
}