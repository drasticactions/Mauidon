// <copyright file="AuthorizationService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet;
using Mastonet.Entities;

namespace Mauidon.Services
{
    /// <summary>
    /// Authorization Service.
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private string redirectUrl;

        private string hostUrl = string.Empty;
        private AppRegistration? appRegistration;
        private AuthenticationClient? authClient;
        private IBrowserService browserService;

        /// <inheritdoc/>
        public bool IsCodeAuth => this.redirectUrl == "urn:ietf:wg:oauth:2.0:oob";

        public AuthorizationService(IBrowserService browserService, string redirectUri = "urn:ietf:wg:oauth:2.0:oob")
        {
            this.browserService = browserService;
            this.redirectUrl = redirectUri;
        }

        /// <inheritdoc/>
        public async Task SetupLogin(string serverBase)
        {
            this.appRegistration = await this.GetAppRegistrationAsync(serverBase);
            this.authClient = new AuthenticationClient(this.appRegistration);
            var oauthUrl = this.authClient.OAuthUrl(this.redirectUrl);
            await this.browserService.OpenAsync(oauthUrl);
        }

        /// <inheritdoc/>
        public async Task<(MastodonClient Client, Account Account)> LoginWithCodeAsync(string code)
        {
            var auth = await this.authClient!.ConnectWithCode(code, this.redirectUrl);
            var client = new MastodonClient(this.appRegistration!.Instance, auth.AccessToken);
            var account = await client.GetCurrentUser();
            return (client, account);
        }

        /// <inheritdoc/>
        public async Task<MastodonClient> GenerateDefaultClientAsync()
        {
            var initAuthClient = new AuthenticationClient("mastodon.social");
            var test = await initAuthClient.CreateApp("Mauidon", Scope.Read);
            return new MastodonClient(test.Instance, string.Empty);
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
