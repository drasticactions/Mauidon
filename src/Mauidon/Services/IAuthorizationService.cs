﻿// <copyright file="IAuthorizationService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet;
using Mastonet.Entities;

namespace Mauidon.Services
{
    /// <summary>
    /// Authorization Service.
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Gets a value indicating if the user needs to copy a code.
        /// </summary>
        public bool IsCodeAuth { get; }

        /// <summary>
        /// Setup Login for Mastodon.
        /// </summary>
        /// <param name="serverBase">The base server login.</param>
        /// <returns><see cref="Task"/>.</returns>
        public Task SetupLogin(string serverBase);

        /// <summary>
        /// Login with Mastodon OAuth Code.
        /// </summary>
        /// <param name="code">Code to Login.</param>
        /// <returns><see cref="Task"/>.</returns>
        public Task<(MastodonClient Client, Account Account)> LoginWithCodeAsync(string code);

        /// <summary>
        /// Generates the default client.
        /// </summary>
        /// <returns>Mastodon Client.</returns>
        public Task<MastodonClient> GenerateDefaultClientAsync();
    }
}
