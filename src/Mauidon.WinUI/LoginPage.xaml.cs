// <copyright file="LoginPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.Tools;
using Mauidon.ViewModels;

namespace Mauidon.WinUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            this.InitializeComponent();
            this.DataContext = this.viewModel = Ioc.Default.ResolveWith<LoginViewModel>();
        }

        internal LoginViewModel viewModel { get; }
    }
}
