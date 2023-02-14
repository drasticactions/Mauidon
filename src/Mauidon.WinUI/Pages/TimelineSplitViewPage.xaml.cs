// <copyright file="TimelineSplitViewPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauidon.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Mauidon.WinUI.Pages
{
    /// <summary>
    /// Timeline Split View Page.
    /// </summary>
    public sealed partial class TimelineSplitViewPage : Page
    {
        private TimelineViewModel vm;

        public TimelineSplitViewPage(TimelineViewModel vm)
        {
            this.InitializeComponent();
            this.DataContext = this.vm = vm;
        }
    }
}
