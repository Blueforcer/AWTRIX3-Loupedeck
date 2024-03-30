namespace Loupedeck.Awtrix3Plugin
{
    using System;

    // This class implements a command to jump to the Previous app.
    public class PreviousAppCommand : PluginDynamicCommand
    {
        // Initializes the command class.
        public PreviousAppCommand()
            : base(displayName: "Previous App", description: "Jumps to the Previous app", groupName: "Commands")
        {
        }

        protected override void RunCommand(String actionParameter) => _ = HttpService.SendPostRequest("previousapp", "").Result;

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapImage = EmbeddedResources.ReadImage("Loupedeck.Awtrix3Plugin.Icons.PreviousApp.png");
            return bitmapImage;
        }

        // No change is needed in this method for displaying the command name.
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) =>
            "Previous App";
    }
}
