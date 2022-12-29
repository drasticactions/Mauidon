// <copyright file="BrowserService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauidon.Services;
using Windows.System;

namespace Mauidon.WinUI.Services
{
    public class BrowserService : IBrowserService
    {
        public async Task OpenAsync(string url)
        {
            await Launcher.LaunchUriAsync(new Uri(url)!);
        }
    }
}
