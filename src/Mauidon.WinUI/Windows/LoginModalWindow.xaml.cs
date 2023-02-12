// <copyright file="LoginModalWindow.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Modal;
using Microsoft.UI.Xaml;
using WinUIEx;

namespace Mauidon.WinUI.Windows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginModalWindow : Drastic.Modal.ModalWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginModalWindow"/> class.
        /// </summary>
        /// <param name="parent">Parent Window.</param>
        /// <param name="options">Options.</param>
        public LoginModalWindow(Window parent, ModalWindowOptions? options = default)
            : base(parent, options: options)
        {
            this.InitializeComponent();

            this.MainWindowGrid.DataContext = this;

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
    }
}
