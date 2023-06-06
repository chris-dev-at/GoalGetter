using GG.CoreBusiness;

namespace GG.UseCases.Team.Interfaces
{
    public interface IViewTeamMembersByProjectIdUseCase
    {
        Task<StatusReport<IEnumerable<Teammember>>> ExecuteAsync(string name, int projectid);
    }
}