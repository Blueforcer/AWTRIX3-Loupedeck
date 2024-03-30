namespace Loupedeck.AWTRIX3Plugin
{
    using System;

    // This class implements a command to jump to the Previous app.
    public class NotifyCommand : PluginDynamicCommand
    {
        // Initializes the command class.
        public NotifyCommand()
            : base(displayName: "Notification", description: "Sends a custom notification", groupName: "Commands")
        {
            this.MakeProfileAction("text;Notification data:");
        }

        protected override void RunCommand(String actionParameter)
        {
            // Verwende den HttpService, um die POST-Anfrage zu senden.
            var success = HttpService.SendPostRequest("notify", actionParameter).Result;
        }

        // No change is needed in this method for displaying the command name.
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize) =>
            "Notification";
    }
}
