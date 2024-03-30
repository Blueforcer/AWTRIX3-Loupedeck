namespace Loupedeck.Awtrix3Plugin
{
    using System;

    // This class implements a command to jump to the Previous app.
    public class DismissCommand : PluginDynamicCommand
    {
        // Initializes the command class.
        public DismissCommand()
            : base(displayName: "Dismiss", description: "Dismiss notification", groupName: "Commands")
        {
        }

        protected override void RunCommand(String actionParameter)
        {
            var success = HttpService.SendPostRequest("notify/dismiss", "").Result;
        }

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapImage = EmbeddedResources.ReadImage("Loupedeck.Awtrix3Plugin.Icons.Dismiss.png");
            return bitmapImage;
        }

        // No change is needed in this method for displaying the command name.
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) =>
            "Dismiss";
    }
}
