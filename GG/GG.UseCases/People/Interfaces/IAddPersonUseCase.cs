using GG.CoreBusiness;

namespace GG.UseCases.People.Interfaces
{
    public interface IAddPersonUseCase
    {
        Task ExecuteAsync(Person person);
    }
}