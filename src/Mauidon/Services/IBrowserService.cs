// <copyright file="IBrowserService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Mauidon.Services
{
    public interface IBrowserService
    {
        Task OpenAsync(string url);
    }
}
