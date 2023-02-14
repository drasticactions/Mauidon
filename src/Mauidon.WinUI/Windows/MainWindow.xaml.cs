// <copyright file="MainWindow.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.Modal;
using Mauidon.Services;
using Mauidon.ViewModels;
using Mauidon.WinUI.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.ApplicationSettings;
using WinUIEx;

namespace Mauidon.WinUI.Windows
{
    /// <summary>
    /// Main Window.
    /// </summary>
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        private AuthorizationService authorizationService;
        private SettingsPage settingsPage;
        private TimelineSplitViewPage publicTimelineSplitViewPage;
        private bool isLoggedIn;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.authorizationService = Ioc.Default.GetService<AuthorizationService>()!;
            this.isLoggedIn = this.authorizationService.DefaultClient?.IsLoggedIn ?? false;
            this.MainWindowGrid.DataContext = this;

            this.settingsPage = new SettingsPage(this);
            this.publicTimelineSplitViewPage = new TimelineSplitViewPage(Ioc.Default.GetService<PublicTimelineViewModel>()!);
            this.NavigationFrame.Content = this.publicTimelineSplitViewPage;

            this.ExtendsContentIntoAppTitleBar(true);
            this.SetTitleBar(this.AppTitleBar);

            this.GetAppWindow().TitleBar.ButtonBackgroundColor = global::Windows.UI.Color.FromArgb(0, 0, 0, 0);
            this.GetAppWindow().TitleBar.ButtonInactiveBackgroundColor = global::Windows.UI.Color.FromArgb(0, 0, 0, 0);

            var manager = WinUIEx.WindowManager.Get(this);
            manager.Backdrop = new WinUIEx.MicaSystemBackdrop();
        }

        /// <summary>
        /// Gets the app logo path.
        /// </summary>
        public string AppLogo => "Icon.favicon.ico";

        /// <summary>
        /// Gets the list of navigation items.
        /// </summary>
        public ObservableCollection<NavigationViewItemBase> Items { get; } = new ObservableCollection<NavigationViewItemBase>();

        private void NavViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem nav)
            {
                var value = nav.Tag?.ToString() ?? string.Empty;
                switch (value)
                {
                    case "Settings":
                        this.NavigationFrame.Content = this.settingsPage;
                        break;
                    default:
                        break;
                }

                return;
            }
        }

        private void LaunchModalLoginWindow()
        {
            var position = this.GetPosition();
            var size = new global::Windows.Graphics.SizeInt32(500, 600);
            var testModal = new LoginModalWindow(this, new ModalWindowOptions() { MinSize = size, Position = this.PositionModalInCenter(size) });
            testModal.Activate();
            testModal.GetAppWindow().MoveInZOrderAtTop();
        }
    }
}
