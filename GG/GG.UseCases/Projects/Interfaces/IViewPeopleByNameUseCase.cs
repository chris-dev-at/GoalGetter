using GG.CoreBusiness;

namespace GG.UseCases.Projects.Interfaces
{
    public interface IViewPeopleByNameUseCase
    {
        Task<IEnumerable<Person>> ExecuteAsync(string name = "");
    }
}