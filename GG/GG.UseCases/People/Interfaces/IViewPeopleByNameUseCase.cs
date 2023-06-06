using GG.CoreBusiness;

namespace GG.UseCases.People.Interfaces
{
    public interface IViewPeopleByNameUseCase
    {
        Task<StatusReport<IEnumerable<Person>>> ExecuteAsync(string name = "");
    }
}