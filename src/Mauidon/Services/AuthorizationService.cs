// <copyright file="AuthorizationService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using Mastonet;
using Mastonet.Entities;
using Mauidon.Models;

namespace Mauidon.Services
{
    /// <summary>
    /// Authorization Service.
    /// </summary>
    public class AuthorizationService
    {
        private string redirectUrl;

        private string hostUrl = string.Empty;
        private AppRegistration? appRegistration;
        private AuthenticationClient? authClient;
        private IBrowserService browserService;
        private DatabaseContext databaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationService"/> class.
        /// </summary>
        /// <param name="database">Database Context.</param>
        /// <param name="browserService">Browser Service.</param>
        /// <param name="redirectUri">Redirect URL.</param>
        public AuthorizationService(DatabaseContext database, IBrowserService browserService, string redirectUri = "urn:ietf:wg:oauth:2.0:oob")
        {
            this.databaseContext = database;
            this.browserService = browserService;
            this.redirectUrl = redirectUri;
        }

        /// <summary>
        /// Gets a value indicating whether gets a value indicating if the user needs to copy a code.
        /// </summary>
        public bool IsCodeAuth => this.redirectUrl == "urn:ietf:wg:oauth:2.0:oob";

        /// <summary>
        /// Setup Login for Mastodon.
        /// </summary>
        /// <param name="serverBase">The base server login.</param>
        /// <returns><see cref="Task"/>.</returns>
        public async Task SetupLogin(string serverBase)
        {
            this.appRegistration = await this.GetAppRegistrationAsync(serverBase);
            this.authClient = new AuthenticationClient(this.appRegistration);
            var oauthUrl = this.authClient.OAuthUrl(this.redirectUrl);
            await this.browserService.OpenAsync(oauthUrl);
        }

        /// <summary>
        /// Login with Mastodon OAuth Code.
        /// </summary>
        /// <param name="code">Code to Login.</param>
        /// <returns><see cref="Task"/>.</returns>
        public async Task<MauidonClient> LoginWithCodeAsync(string code)
        {
            Auth? auth = await this.authClient!.ConnectWithCode(code, this.redirectUrl);
            var client = new MastodonClient(this.appRegistration!.Instance, auth.AccessToken);
            var account = await client.GetCurrentUser();
            var mauidonAccount = this.databaseContext.MauidonAccounts!.FirstOrDefault(n => n.AccountName == account.AccountName) ?? new MauidonAccount();
            mauidonAccount.Update(account, auth);
            this.databaseContext.AddOrUpdateMauidonAccount(mauidonAccount).FireAndForgetSafeAsync();
            return new MauidonClient(client, mauidonAccount);
        }

        /// <summary>
        /// Gets the default client async.
        /// </summary>
        /// <returns>MauidonClient.</returns>
        public async Task<MauidonClient> GetDefaultClientAsync()
        {
            var account = this.databaseContext.MauidonAccounts!.FirstOrDefault(n => n.IsDefault);
            if (account is null)
            {
                return await this.GetDefaultClientAsync();
            }

            var client = new MastodonClient(account.Instance, account.AccessToken);

            return new MauidonClient(client, account);
        }

        /// <summary>
        /// Generates the default client.
        /// </summary>
        /// <returns>Mastodon Client.</returns>
        public async Task<MauidonClient> GenerateDefaultClientAsync()
        {
            var initAuthClient = new AuthenticationClient("mastodon.social");
            var test = await initAuthClient.CreateApp("Mauidon", Scope.Read);
            return new MauidonClient(new MastodonClient(test.Instance, string.Empty));
        }

        private async Task<AppRegistration> GetAppRegistrationAsync(string serverBase)
        {
            Uri.TryCreate(serverBase, UriKind.RelativeOrAbsolute, out Uri? serverBaseUri);
            if (serverBaseUri == null)
            {
                throw new InvalidServerUriException(serverBase);
            }

            this.hostUrl = serverBaseUri.IsAbsoluteUri ? serverBaseUri.Host : serverBaseUri.OriginalString;
            AppRegistration appRegistration;
            try
            {
                var initAuthClient = new AuthenticationClient(this.hostUrl);
                appRegistration = await initAuthClient.CreateApp("Mauidon", Scope.Read | Scope.Write | Scope.Follow, null, this.redirectUrl);
            }
            catch (Exception ex)
            {
                throw new AppRegistrationCreationFailureException(ex.Message, ex);
            }

            return appRegistration;
        }
    }
}
