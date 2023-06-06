using GG.CoreBusiness;

namespace GG.UseCases.Team.Interfaces
{
    public interface IViewPeopleIfNotInTeamUseCase
    {
        Task<StatusReport<IEnumerable<Person>>> ExecuteAsync(int projectid);
    }
}