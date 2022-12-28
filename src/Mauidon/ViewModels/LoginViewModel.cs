// <copyright file="LoginViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Mauidon.ViewModels
{
    public class LoginViewModel : MauidonBaseViewModel
    {
        private string serverBaseUrl = "mastodon.social";

        public LoginViewModel(IServiceProvider services)
            : base(services)
        {
             this.StartLoginCommand = new AsyncCommand(
                async () => await this.ExecuteStartLoginCommand(),
                () => !string.IsNullOrEmpty(this.ServerBaseUrl),
                this.Dispatcher,
                this.ErrorHandler);
        }

        /// <summary>
        /// Gets or sets the server base url.
        /// </summary>
        public string ServerBaseUrl
        {
            get => this.serverBaseUrl;
            set
            {
                this.SetProperty(ref this.serverBaseUrl, value);
                this.StartLoginCommand?.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Start Login Command.
        /// </summary>
        public AsyncCommand StartLoginCommand { get; set; }

        private async Task ExecuteStartLoginCommand()
        {
            if (!string.IsNullOrEmpty(this.ServerBaseUrl))
            {
            }
        }
    }
}
