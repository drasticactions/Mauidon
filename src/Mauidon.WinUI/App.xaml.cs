// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Reflection;
using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.Services;
using Mauidon.Services;
using Mauidon.ViewModels;
using Mauidon.WinUI.Services;
using Mauidon.WinUI.Tools;
using Mauidon.WinUI.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Mauidon.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? window;
        private WindowService windowService;
        private ApplicationSettingsService applicationSettingsService;

        public App()
        {
            this.InitializeComponent();
            var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            string databaseField = WinUIExtensions.IsRunningAsUwp() ? System.IO.Path.Combine(global::Windows.Storage.ApplicationData.Current.LocalFolder.Path, "mauidon-database.db") : Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly()!.Location!)!, "mauidon-database.db");
            this.RemoveDatabase(databaseField);

            Ioc.Default.ConfigureServices(
             new ServiceCollection()
             .AddSingleton<IAppDispatcher>(new AppDispatcher(dispatcherQueue))
             .AddSingleton<IErrorHandlerService, ErrorHandlerService>()
             .AddSingleton<IBrowserService, BrowserService>()
             .AddSingleton<AuthorizationService>()
             .AddSingleton<DatabaseContext>(new DatabaseContext(databaseField))
             .AddSingleton<WindowsPlatformService>()
             .AddSingleton<WindowService>()
             .AddSingleton<ApplicationSettingsService>()
             .AddSingleton<ThemeSelectorService>()
             .AddTransient<LoginViewModel>()
             .BuildServiceProvider());

            this.windowService = Ioc.Default.GetService<WindowService>()!;
            this.applicationSettingsService = Ioc.Default.GetService<ApplicationSettingsService>()!;
            this.applicationSettingsService.UpdateCulture();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            this.window = this.windowService.AddWindow<MainWindow>();
            this.window.Activate();
            this.window.SetIconFromApplicationIcon();

            Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().Activated += this.AppActivated;
        }

        private void AppActivated(object? sender, Microsoft.Windows.AppLifecycle.AppActivationArguments e)
        {
            // From https://github.com/andrewleader/RedirectActivationWinUI3Sample/blob/main/RedirectActivationWinUI3Sample/App.xaml.cs#L71-L88
            var hwnd = (global::Windows.Win32.Foundation.HWND)WinRT.Interop.WindowNative.GetWindowHandle(this.window);

            global::Windows.Win32.PInvoke.ShowWindow(hwnd, global::Windows.Win32.UI.WindowsAndMessaging.SHOW_WINDOW_CMD.SW_RESTORE);

            global::Windows.Win32.PInvoke.SetForegroundWindow(hwnd);
        }

        private void RemoveDatabase(string db)
        {
            var filename = Path.GetFileNameWithoutExtension(db);
            var directory = Path.GetDirectoryName(db);
            var files = Directory.EnumerateFiles(directory!).Where(n => n.Contains(filename));
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
