using GG.CoreBusiness;

namespace GG.UseCases.Team.Interfaces
{
    public interface IViewTeamMembersByProjectIdUseCase
    {
        Task<IEnumerable<Teammember>> ExecuteAsync(string name, int projectid);
    }
}