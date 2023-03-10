// <copyright file="SettingsPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.Services;
using Mauidon.Models;
using Mauidon.Services;
using Mauidon.Translations;
using Mauidon.WinUI.Services;
using Mauidon.WinUI.Tools;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mauidon.WinUI.Pages
{
    /// <summary>
    /// Settings Page.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private IErrorHandlerService errorHandler;
        private IAppDispatcher dispatcher;
        private ApplicationSettingsService applicationSettingsService;
        private Window window;
        private DatabaseContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        /// <param name="mainWindow">Main Window.</param>
        public SettingsPage(Window mainWindow)
        {
            this.InitializeComponent();
            this.window = mainWindow;
            this.DataContext = this;
            this.context = Ioc.Default.GetService<DatabaseContext>()!;
            this.errorHandler = Ioc.Default.GetService<IErrorHandlerService>()!;
            this.dispatcher = Ioc.Default.GetService<IAppDispatcher>()!;
            this.applicationSettingsService = Ioc.Default.GetService<ApplicationSettingsService>()!;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets the current version of the app.
        /// </summary>
        public string Version => WinUIExtensions.GetAppVersion();

        /// <summary>
        /// Gets or sets the app theme.
        /// </summary>
        public AppTheme AppTheme
        {
            get
            {
                return this.applicationSettingsService.ApplicationElementTheme;
            }

            set
            {
                this.applicationSettingsService.ApplicationElementTheme = value;
            }
        }

        /// <summary>
        /// Gets or sets the element theme.
        /// </summary>
        public LanguageSetting LanguageSetting
        {
            get
            {
                return this.applicationSettingsService.ApplicationLanguageSetting;
            }

            set
            {
                this.applicationSettingsService.ApplicationLanguageSetting = value;
            }
        }

        /// <summary>
        /// Gets the Languages.
        /// </summary>
        public List<Tuple<string, AppTheme>> AppThemes { get; } = new List<Tuple<string, AppTheme>>()
        {
            new Tuple<string, AppTheme>(Common.DefaultThemeLabel, AppTheme.Default),
            new Tuple<string, AppTheme>(Common.DarkThemeLabel, AppTheme.Dark),
            new Tuple<string, AppTheme>(Common.LightThemeLabel, AppTheme.Light),
        };

        /// <summary>
        /// Gets the Languages.
        /// </summary>
        public List<Tuple<string, LanguageSetting>> Languages { get; } = new List<Tuple<string, LanguageSetting>>()
        {
            new Tuple<string, LanguageSetting>(Common.DefaultLanguage, LanguageSetting.Default),
            new Tuple<string, LanguageSetting>(Common.EnglishLanguage, LanguageSetting.English),
            new Tuple<string, LanguageSetting>(Common.JapaneseLanguage, LanguageSetting.Japanese),
        };

        /// <summary>
        /// On Property Changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = this.PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

#pragma warning disable SA1600 // Elements should be documented
        private bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action? onChanged = null)
#pragma warning restore SA1600 // Elements should be documented
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            return true;
        }

        private void LanguageComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            this.LanguageComboBox.SelectedIndex = this.Languages.IndexOf(this.Languages.First(n => n.Item2 == this.LanguageSetting));
        }

        private void LanguageComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.LanguageSetting = this.Languages[this.LanguageComboBox.SelectedIndex].Item2;
        }

        private void ThemeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.AppTheme = this.AppThemes[this.ThemeComboBox.SelectedIndex].Item2;
        }

        private void ThemeComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            this.ThemeComboBox.SelectedIndex = this.AppThemes.IndexOf(this.AppThemes.First(n => n.Item2 == this.AppTheme));
        }
    }
}
