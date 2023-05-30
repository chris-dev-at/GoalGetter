using GG.CoreBusiness;

namespace GG.UseCases.People.Interfaces
{
    public interface IViewPeopleByNameUseCase
    {
        Task<IEnumerable<Person>> ExecuteAsync(string name = "");
    }
}