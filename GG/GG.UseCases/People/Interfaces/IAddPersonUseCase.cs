using GG.CoreBusiness;

namespace GG.UseCases.People.Interfaces
{
    public interface IAddPersonUseCase
    {
        Task<StatusReport<EmptyVal>> ExecuteAsync(Person person);
    }
}