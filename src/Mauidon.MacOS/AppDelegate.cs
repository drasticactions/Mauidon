using Drastic.Tray;

namespace Mauidon.MacOS;

[Register("AppDelegate")]
public class AppDelegate : NSApplicationDelegate
{

    private Drastic.Tray.TrayIcon? icon;

    public override void DidFinishLaunching(NSNotification notification)
    {
        var image = NSImage.ImageNamed("NSApplicationIcon");
        image!.Size = new CGSize(32, 32);
        var menuItems = new List<TrayMenuItem>();
        menuItems.Add(new TrayMenuItem("Quit", null, async () => { NSApplication.SharedApplication.Terminate(this); }, "q"));
        this.icon = new Drastic.Tray.TrayIcon("Mauidon", new Drastic.Tray.TrayImage(image!), menuItems, false);
        this.icon.RightClicked += (object? sender, TrayClickedEventArgs e) => this.icon.OpenMenu();
    }

    public override void WillTerminate(NSNotification notification)
    {
        // Insert code here to tear down your application
    }
}
