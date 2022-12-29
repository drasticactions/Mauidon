// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.Services;
using Mastonet;
using Mauidon.Services;
using Mauidon.ViewModels;
using Mauidon.WinUI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Mauidon.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            Ioc.Default.ConfigureServices(
             new ServiceCollection()
             .AddSingleton<IAppDispatcher>(new AppDispatcher(dispatcherQueue))
             .AddSingleton<IErrorHandlerService, ErrorHandlerService>()
             .AddSingleton<IBrowserService, BrowserService>()
             .AddSingleton<IAuthorizationService, AuthorizationService>()
             .AddTransient<LoginViewModel>()
             .BuildServiceProvider());
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new BaseWindow(new LoginPage());
            m_window.Activate();
        }

        private Window? m_window;
    }
}
