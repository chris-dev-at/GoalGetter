using Microsoft.AspNetCore.Components;

namespace GG.UseCases
{
    public interface IThemeService
    {
        bool IsDarkMode { get; set; }
        EventCallback<bool> OnThemeChanged { get; set; }
        Task SetTheme(bool isDarkMode);
    }
}