// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

#nullable disable

using System.Runtime.InteropServices;
using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;

namespace Mauidon.WinUI
{
    /// <summary>
    /// Program Entry Point.
    /// </summary>
    internal class Program
    {
        private const string AppKey = "7317741F-805D-4586-999A-9F971DFE1396";
        private static IntPtr redirectEventHandle = IntPtr.Zero;

        [STAThread]
        private static void Main(string[] args)
        {
            WinRT.ComWrappersSupport.InitializeComWrappers();
            bool isRedirect = DecideRedirection();
            if (!isRedirect)
            {
                Microsoft.UI.Xaml.Application.Start((p) =>
                {
                    var context = new DispatcherQueueSynchronizationContext(
                        DispatcherQueue.GetForCurrentThread());
                    SynchronizationContext.SetSynchronizationContext(context);
                    new App();
                });
            }
        }

        private static bool DecideRedirection()
        {
            bool isRedirect = false;

            AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
            ExtendedActivationKind kind = args.Kind;

            try
            {
                AppInstance keyInstance = AppInstance.FindOrRegisterForKey(AppKey);

                if (keyInstance.IsCurrent)
                {
                    keyInstance.Activated += OnActivated;
                }
                else
                {
                    isRedirect = true;
                    RedirectActivationTo(args, keyInstance);
                }
            }
            catch (Exception)
            {
            }

            return isRedirect;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateEvent(
            IntPtr lpEventAttributes,
            bool bManualReset,
            bool bInitialState,
            string lpName);

        [DllImport("kernel32.dll")]
        private static extern bool SetEvent(IntPtr hEvent);

        [DllImport("ole32.dll")]
        private static extern uint CoWaitForMultipleObjects(
            uint dwFlags,
            uint dwMilliseconds,
            ulong nHandles,
            IntPtr[] pHandles,
            out uint dwIndex);

        // Do the redirection on another thread, and use a non-blocking
        // wait method to wait for the redirection to complete.
        private static void RedirectActivationTo(
            AppActivationArguments args, AppInstance keyInstance)
        {
            redirectEventHandle = CreateEvent(IntPtr.Zero, true, false, null);
            Task.Run(() =>
            {
                keyInstance.RedirectActivationToAsync(args).AsTask().Wait();
                SetEvent(redirectEventHandle);
            });
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
            uint CWMO_DEFAULT = 0;
            uint INFINITE = 0xFFFFFFFF;
            _ = CoWaitForMultipleObjects(
               CWMO_DEFAULT,
               INFINITE,
               1,
               new IntPtr[] { redirectEventHandle },
               out uint handleIndex);
        }
#pragma warning restore SA1312 // Variable names should begin with lower-case letter

        private static void OnActivated(object sender, AppActivationArguments args)
        {
            ExtendedActivationKind kind = args.Kind;
        }
    }
}
