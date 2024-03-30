namespace Loupedeck.AWTRIX3Plugin
{
    using System;

    // This class implements a command to jump to the Previous app.
    public class NextAppCommand : PluginDynamicCommand
    {
        // Initializes the command class.
        public NextAppCommand()
            : base(displayName: "Next App", description: "Jumps to the next app", groupName: "Commands")
        {
        }

        protected override void RunCommand(String actionParameter)
        {
            var success = HttpService.SendPostRequest("nextapp", "").Result;
        }

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapImage = EmbeddedResources.ReadImage("Loupedeck.AWTRIX3Plugin.Icons.NextApp.png");
            return bitmapImage;
        }

        // No change is needed in this method for displaying the command name.
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) =>
            "Next App";
    }
}
