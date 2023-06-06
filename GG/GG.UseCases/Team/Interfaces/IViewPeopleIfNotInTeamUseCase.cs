using GG.CoreBusiness;

namespace GG.UseCases.Team.Interfaces
{
    public interface IViewPeopleIfNotInTeamUseCase
    {
        Task<IEnumerable<Person>> ExecuteAsync(int projectid);
    }
}