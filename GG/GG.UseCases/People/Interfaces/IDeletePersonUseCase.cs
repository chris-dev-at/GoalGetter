using GG.CoreBusiness;

namespace GG.UseCases.People.Interfaces
{
    public interface IDeletePersonUseCase
    {
        Task<StatusReport<EmptyVal>> ExecuteAsync(Person person);
    }
}