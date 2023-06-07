using GG.CoreBusiness;
using GG.UseCases.People.Interfaces;
using GG.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudBlazor;
using Microsoft.AspNetCore.Components.Forms;

namespace GG.UseCases.People
{
    public class AddPersonUseCase : IAddPersonUseCase
    {
        private readonly IProjectsRepository projectsRepository;
        public AddPersonUseCase(IProjectsRepository projectsRepository)
        {
            this.projectsRepository = projectsRepository;
        }
        public async Task<StatusReport<EmptyVal>> ExecuteAsync(Person person, IBrowserFile image)
        {
            person.AvatarColor = await GetRandomColor();
            
            return await projectsRepository.AddPersonAsync(person, image);
        }
        public async Task<Color> GetRandomColor()
        {
			Random random = new Random();
			Type type = typeof(Color);
			Array values = type.GetEnumValues();
			int index = random.Next(values.Length);

			return (Color)values.GetValue(index);
		}
    }
}
