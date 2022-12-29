// <copyright file="BaseWindow.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUIEx;

namespace Mauidon.WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BaseWindow : Window
    {
        private Page page;

        public BaseWindow(Page page, SystemBackdrop? backdrop = default)
        {
            this.InitializeComponent();

            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(this.AppTitleBar);

            this.MainFrame.Content = this.page = page;
            page.DataContextChanged += this.Page_DataContextChanged;
            var manager = WindowManager.Get(this);
            manager.Backdrop = backdrop ?? new MicaSystemBackdrop();
        }

        private void Page_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            this.MainGrid.DataContext = args.NewValue;
        }
    }
}
