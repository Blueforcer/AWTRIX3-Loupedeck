namespace Loupedeck.AWTRIX3Plugin
{
    using System;

    using Loupedeck.HomeAssistant;

    // This class contains the plugin-level logic of the Loupedeck plugin.

    public class AWTRIX3Plugin : Plugin
    {
        // Gets a value indicating whether this is an API-only plugin.
        public override Boolean UsesApplicationApiOnly => true;

        // Gets a value indicating whether this is a Universal plugin or an Application plugin.
        public override Boolean HasNoApplication => true;


        private readonly String Host;

        // Initializes a new instance of the plugin class.
        public AWTRIX3Plugin()
        {
            // Initialize the plugin log.
            PluginLog.Init(this.Log);

            // Initialize the plugin resources.
            PluginResources.Init(this.Assembly);
        }

        // This method is called when the plugin is loaded during the Loupedeck service start-up.
        public override void Load()
        {
            var Config = AwtrixConfig.Read();

            if (Config == null)
            {
                this.OnPluginStatusChanged(Loupedeck.PluginStatus.Error, "Configuration could not be read.", "https://github.com/schmic/Loupedeck-HomeAssistant", "Help");
                return;
            }
            else if (Config.Host.IsNullOrEmpty())
            {
                this.OnPluginStatusChanged(Loupedeck.PluginStatus.Error, "Configuration is missing url.", "https://github.com/schmic/Loupedeck-HomeAssistant", "Help");
                return;
            }
            HttpService.Host = Config.Host;
            HttpService.Initialize();
        }

        // This method is called when the plugin is unloaded during the Loupedeck service shutdown.
        public override void Unload()
        {
        }



    }
}
