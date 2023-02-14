// <copyright file="WinUIExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace Mauidon.WinUI.Tools
{
    /// <summary>
    /// WinUI Extensions.
    /// </summary>
    public static class WinUIExtensions
    {
        /// <summary>
        /// Small Icon.
        /// </summary>
        private const int ICONSMALL = 0;

        /// <summary>
        /// Big Icon.
        /// </summary>
        private const int ICONBIG = 1;

        /// <summary>
        /// Icon Small 2.
        /// </summary>
        private const int ICONSMALL2 = 2;

        /// <summary>
        /// Get Icon.
        /// </summary>
        private const int WMGETICON = 0x007F;

        /// <summary>
        /// Set Icon.
        /// </summary>
        private const int WMSETICON = 0x0080;

        private const long APPMODELERRORNOPACKAGE = 15700L;

        /// <summary>
        /// Is the app running as a UWP.
        /// </summary>
        /// <returns>bool.</returns>
        public static bool IsRunningAsUwp()
        {
            int length = 0;
            StringBuilder sb = new StringBuilder(0);
            int result = GetCurrentPackageFullName(ref length, sb);

            sb = new StringBuilder(length);
            result = GetCurrentPackageFullName(ref length, sb);

            return result != APPMODELERRORNOPACKAGE;
        }

        /// <summary>
        /// Get the current version of app. Returns the store version if UWP. Returns the assembly version if unpackaged.
        /// </summary>
        /// <returns>String.</returns>
        public static string GetAppVersion()
        {
            if (IsRunningAsUwp())
            {
                Package package = Package.Current;
                PackageId packageId = package.Id;
                PackageVersion version = packageId.Version;

                return string.Format("{0}.{1}.{2}.{3}-Store", version.Major, version.Minor, version.Build, version.Revision);
            }

            return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Missing";
        }

        /// <summary>
        /// Create a random access stream from a byte array.
        /// </summary>
        /// <param name="array">The byte array.</param>
        /// <returns><see cref="IRandomAccessStream"/>.</returns>
        public static IRandomAccessStream ToRandomAccessStream(this byte[] array)
        {
            InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
            using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
            {
                writer.WriteBytes(array);
                writer.StoreAsync().GetResults();
            }

            return ms;
        }

        /// <summary>
        /// Find all ascendant elements of the specified element. This method can be chained with
        /// LINQ calls to add additional filters or projections on top of the returned results.
        /// <para>
        /// This method is meant to provide extra flexibility in specific scenarios and it should not
        /// be used when only the first item is being looked for. In those cases, use one of the
        /// available <see cref="FindAscendant{T}(DependencyObject)"/> overloads instead, which will
        /// offer a more compact syntax as well as better performance in those cases.
        /// </para>
        /// </summary>
        /// <param name="element">The root element.</param>
        /// <returns>All the descendant <see cref="DependencyObject"/> instance from <paramref name="element"/>.</returns>
        public static IEnumerable<DependencyObject> FindAscendants(this DependencyObject element)
        {
            while (true)
            {
                DependencyObject? parent = VisualTreeHelper.GetParent(element);

                if (parent is null)
                {
                    yield break;
                }

                yield return parent;

                element = parent;
            }
        }

        /// <summary>
        /// Determines if a rectangle intersects with another rectangle.
        /// </summary>
        /// <param name="rect1">The first rectangle to test.</param>
        /// <param name="rect2">The second rectangle to test.</param>
        /// <returns>This method returns <see langword="true"/> if there is any intersection, otherwise <see langword="false"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IntersectsWith(this Rect rect1, Rect rect2)
        {
            if (rect1.IsEmpty || rect2.IsEmpty)
            {
                return false;
            }

            return (rect1.Left <= rect2.Right) &&
                   (rect1.Right >= rect2.Left) &&
                   (rect1.Top <= rect2.Bottom) &&
                   (rect1.Bottom >= rect2.Top);
        }

        /// <summary>
        /// Set the Icon for this <see cref="Window"/> out from the current process, which is the same as the ApplicationIcon set in the *.csproj.
        /// </summary>
        /// <param name="window">Window.</param>
        public static void SetIconFromApplicationIcon(this Window window)
        {
            if (IsRunningAsUwp())
            {
                return;
            }

            // https://learn.microsoft.com/en-us/answers/questions/822928/app-icon-windows-app-sdk.html
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            string sExe = System.Diagnostics.Process.GetCurrentProcess().MainModule!.FileName;
            var ico = System.Drawing.Icon.ExtractAssociatedIcon(sExe);
            SendMessage(hWnd, WMSETICON, ICONBIG, ico!.Handle);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder packageFullName);

        /// <summary>
        /// Send Message to App.
        /// </summary>
        /// <param name="hWnd">Pointer.</param>
        /// <param name="msg">Message.</param>
        /// <param name="wParam">W Parameter.</param>
        /// <param name="lParam">L Parameter.</param>
        /// <returns>Int.</returns>
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, IntPtr lParam);
    }
}
