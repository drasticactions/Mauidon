// <copyright file="MauidonBaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauidon.Models;
using Mauidon.Services;

namespace Mauidon.ViewModels
{
    public class MauidonBaseViewModel : BaseViewModel
    {
        public MauidonBaseViewModel(IServiceProvider services)
            : base(services)
        {
            this.Authorization = services.GetService(typeof(AuthorizationService)) as AuthorizationService ?? throw new NullReferenceException(nameof(AuthorizationService));
            this.Authorization.OnNewSignIn += this.AuthorizationOnNewSignIn;
            this.MauidonClient = this.Authorization.DefaultClient;
        }

        /// <summary>
        /// Gets the Mauidon Client.
        /// </summary>

        public MauidonClient MauidonClient { get; internal set; }

        /// <summary>
        /// Gets the <see cref="IAuthorizationService"/>.
        /// </summary>
        public AuthorizationService Authorization { get; }

        /// <summary>
        /// Update Async.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        internal virtual Task UpdateAsync()
        {
            return Task.CompletedTask;
        }

        private void AuthorizationOnNewSignIn(object? sender, Events.NewSignInEventArgs e)
        {
            this.MauidonClient = e.Client;
            this.UpdateAsync().FireAndForgetSafeAsync();
        }
    }
}
