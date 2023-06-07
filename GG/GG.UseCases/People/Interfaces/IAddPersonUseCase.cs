using GG.CoreBusiness;
using Microsoft.AspNetCore.Components.Forms;

namespace GG.UseCases.People.Interfaces
{
    public interface IAddPersonUseCase
    {
        Task<StatusReport<EmptyVal>> ExecuteAsync(Person person, IBrowserFile image);
    }
}