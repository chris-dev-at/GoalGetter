using GoalGetter.Authentication;
using GG.Plugins.InMemory;
using GG.UseCases.People;
using GG.UseCases.People.Interfaces;
using GG.UseCases.PluginInterfaces;
using GG.UseCases.Projects;
using GG.UseCases.Projects.Interfaces;
using GG.UseCases.Tasks;
using GG.UseCases.Tasks.Interfaces;
using GG.UseCases.Team;
using GG.UseCases.Team.Interfaces;
using GoalGetter.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<UserAccountService>();

builder.Services.AddSingleton<IProjectsRepository, ProjectsRepository>();

<<<<<<< HEAD
=======
//Project
>>>>>>> 4f16cf8c54fdd354925749203b3062c6f990bf2f
builder.Services.AddTransient<IViewProjectsByNameUseCase, ViewProjectsByNameUseCase>();
builder.Services.AddTransient<IAddProjectUseCase, AddProjectUseCase>();
builder.Services.AddTransient<IViewProjectByIdUseCase, ViewProjectByIdUseCase>();
builder.Services.AddSingleton<IUpdateProjectUseCase, UpdateProjectUseCase>();
<<<<<<< HEAD
=======
builder.Services.AddSingleton<IGetProgressChartUseCase, GetProgressChartUseCase>();
>>>>>>> 4f16cf8c54fdd354925749203b3062c6f990bf2f

builder.Services.AddTransient<IViewPeopleByNameUseCase, ViewPeopleByNameUseCase>();
builder.Services.AddTransient<IAddPersonUseCase, AddPersonUseCase>();

// Team Services
builder.Services.AddTransient<IViewTeamMembersByProjectIdUseCase, ViewTeamMembersByProjectIdUseCase>();
builder.Services.AddTransient<IViewPeopleIfNotInTeamUseCase, ViewPeopleIfNotInTeamUseCase>();
builder.Services.AddTransient<IAddTeamUseCase, AddTeamUseCase>();
<<<<<<< HEAD
builder.Services.AddSingleton<IRemoveTeammemberFromTeamUseCase, RemoveTeammemberFromTeamUseCase>();
=======
>>>>>>> 4f16cf8c54fdd354925749203b3062c6f990bf2f

//Taks Services
builder.Services.AddTransient<IViewTasksByProjectIdUseCase, ViewTasksByProjectIdUseCase>();
builder.Services.AddTransient<IAddTaskUseCase, AddTaskUseCase>();
builder.Services.AddSingleton<IUpdateTaskUseCase, UpdateTaskUseCase>();
builder.Services.AddSingleton<IRemoveTaskUseCase, RemoveTaskUseCase>();
builder.Services.AddSingleton<IRemoveProjectUseCase, RemoveProjectUseCase>();

builder.Services.AddMudServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
