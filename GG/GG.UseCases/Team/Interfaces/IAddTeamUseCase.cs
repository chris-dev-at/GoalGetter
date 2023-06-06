using GG.CoreBusiness;

namespace GG.UseCases.Team.Interfaces
{
    public interface IAddTeamUseCase
    {
        Task ExecuteAsync(Teammember person, int projectid);
    }
}