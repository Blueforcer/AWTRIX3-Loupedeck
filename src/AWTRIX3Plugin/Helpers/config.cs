namespace Loupedeck.HomeAssistant
{
    using System;
    using System.IO;

    using Loupedeck.Awtrix3Plugin;

    public class AwtrixConfig
    {
        public String Host { get; set; }
        public static AwtrixConfig FromString(String jsonConfig) => JsonHelpers.DeserializeAnyObject<AwtrixConfig>(jsonConfig);

        public static AwtrixConfig Read()
        {
            // Verwende das Plugin-Root-Verzeichnis für die Konfigurationsdatei
            var pluginDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var pluginRoot = Path.Combine(pluginDirectory, "Loupedeck", "Plugins", "AWTRIX3");

            var configFile = Path.Combine(pluginRoot, "config.json");

            if (!File.Exists(configFile))
            {
                PluginLog.Error("Configuration file is missing or unreadable.");
                return null;
            }

            var config = JsonHelpers.DeserializeAnyObjectFromFile<AwtrixConfig>(configFile);
            return config;
        }
    }
}
