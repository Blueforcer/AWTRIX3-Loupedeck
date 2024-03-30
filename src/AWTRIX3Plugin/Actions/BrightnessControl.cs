namespace Loupedeck.Awtrix3Plugin
{
    using System;
    using System.Threading;

    public class BrightnessAdjustment : PluginDynamicAdjustment
    {
        private int _brightnessPercentage = 50; // Prozentwert der Helligkeit.
        private bool _autoBrightness = false; // Status der automatischen Helligkeit.
        private Timer _timer;
        private const int Delay = 300; // Verzögerung von 2 Sekunden.

        public BrightnessAdjustment()
            : base(displayName: "Brightness", description: "Toggles auto/manual brightness", groupName: "Adjustments", hasReset: true) // 'hasReset' auf false, da nicht benötigt.
        {
        }

        protected override void ApplyAdjustment(String actionParameter, Int32 diff)
        {
            if (this._autoBrightness)
            {
                return; // Änderungen ignorieren, wenn sich im Automodus befindet.
            }

            this._brightnessPercentage += diff;
            this._brightnessPercentage = Math.Max(0, Math.Min(100, this._brightnessPercentage));

            this.AdjustmentValueChanged();
            this._timer = new Timer(_ => SendBrightnessAdjustment(), null, Timeout.Infinite, Timeout.Infinite);
            this._timer.Change(Delay, Timeout.Infinite);
        }

        protected override void RunCommand(String actionParameter)
        {
            // Toggle zwischen automatischer und manueller Helligkeitseinstellung.
            this._autoBrightness = !this._autoBrightness;

            this.AdjustmentValueChanged();
            this.SendAutoBrightnessToggle();
        }

        protected override String GetAdjustmentValue(String actionParameter) => this._autoBrightness ? "AUTO" : $"{this._brightnessPercentage}%";

        private void SendBrightnessAdjustment()
        {
            if (this._autoBrightness)
            {
                return; // Sende keine manuellen Helligkeitsänderungen im Automodus.
            }

            int brightnessValue = (int)Math.Round(this._brightnessPercentage * 2.55);

            var payload = $"{{\"BRI\":{brightnessValue}}}";
            var success = HttpService.SendPostRequest("settings", payload).Result;
        }

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmapImage = EmbeddedResources.ReadImage("Loupedeck.Awtrix3Plugin.Icons.Brightness.png");
            return bitmapImage;
        }

        private void SendAutoBrightnessToggle()
        {
            var payload = $"{{\"ABRI\":{this._autoBrightness.ToString().ToLower()}}}";
            var success = HttpService.SendPostRequest("settings", payload).Result;
        }
    }
}
