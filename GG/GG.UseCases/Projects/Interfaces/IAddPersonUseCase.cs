using GG.CoreBusiness;

namespace GG.UseCases.Projects.Interfaces
{
    public interface IAddPersonUseCase
    {
        Task ExecuteAsync(Person person);
    }
}