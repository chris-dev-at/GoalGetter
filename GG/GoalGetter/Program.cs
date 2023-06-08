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
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<IProjectsRepository, ProjectsRepository>();

builder.Services.AddTransient<IViewProjectsByNameUseCase, ViewProjectsByNameUseCase>();
builder.Services.AddTransient<IAddProjectUseCase, AddProjectUseCase>();
builder.Services.AddTransient<IViewProjectByIdUseCase, ViewProjectByIdUseCase>();
builder.Services.AddSingleton<IUpdateProjectUseCase, UpdateProjectUseCase>();

builder.Services.AddTransient<IViewPeopleByNameUseCase, ViewPeopleByNameUseCase>();
builder.Services.AddTransient<IAddPersonUseCase, AddPersonUseCase>();

// Team Services
builder.Services.AddTransient<IViewTeamMembersByProjectIdUseCase, ViewTeamMembersByProjectIdUseCase>();
builder.Services.AddTransient<IViewPeopleIfNotInTeamUseCase, ViewPeopleIfNotInTeamUseCase>();
builder.Services.AddTransient<IAddTeamUseCase, AddTeamUseCase>();

//Taks Services
builder.Services.AddTransient<IViewTasksByProjectIdUseCase, ViewTasksByProjectIdUseCase>();
builder.Services.AddTransient<IAddTaskUseCase, AddTaskUseCase>();
builder.Services.AddSingleton<IUpdateTaskUseCase, UpdateTaskUseCase>();
builder.Services.AddSingleton<IRemoveTaskUseCase, RemoveTaskUseCase>();

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
