using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.UseCases
{
    public class ThemeService : IThemeService
    {
        public bool IsDarkMode { get; set; }

        public EventCallback<bool> OnThemeChanged { get; set; }

        public async Task SetTheme(bool isDarkMode)
        {
            IsDarkMode = isDarkMode;
            await OnThemeChanged.InvokeAsync(IsDarkMode);
        }

    }
}
