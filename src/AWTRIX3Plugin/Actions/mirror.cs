using System;
using System.Timers;

using Loupedeck;

using static Loupedeck.AWTRIX3Plugin.HttpService;

public class MirrorCommand : PluginDynamicCommand
{
    private readonly Timer _updateTimer;

    public MirrorCommand()
        : base(displayName: "Mirror", description: "Mirrors AWTRIX screen", groupName: "Commands")
    {
        this.MakeProfileAction("text;Index:");

        // Initialisiere und starte den Timer
        this._updateTimer = new Timer(60); // Timer setzt auf 100ms
        this._updateTimer.Elapsed += this.OnTimedEvent;
        this._updateTimer.AutoReset = true;
        this._updateTimer.Enabled = true;
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e) => this.ActionImageChanged(null);

    protected override void RunCommand(String actionParameter)
    {
        // Diese Methode könnte genutzt werden, um bestimmte Aktionen auszuführen, 
        // wird hier aber nicht benötigt, wenn sie nur das Bild aktualisiert.
    }

    protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
    {
        if (actionParameter == null)
        {
            return null;
        }

        if (!Int32.TryParse(actionParameter, out var blockIndex))
        {
            // Wenn die Umwandlung fehlschlägt, wird kein Bild gerendert.
            return null;
        }

        var xOffset = blockIndex * 8; // Berechnet den X-Offset basierend auf dem actionParameter

        using (var bitmapBuilder = new BitmapBuilder(80, 80))
        {
            // Hintergrund initial schwarz zeichnen, um das Gitter zu erzeugen
            bitmapBuilder.Clear(new BitmapColor(0, 0, 0));

            // Nutze die Farben aus der GlobalLedData für das entsprechende 8x8-Segment
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var actualX = x + xOffset;
                    var color = GlobalLedData.Colors[actualX, y];

                    // Berechne die Positionen, unter Berücksichtigung des Gitters
                    var posX = x * 10; // 9 Pixel + 1 Pixel Gitter
                    var posY = y * 10; // 9 Pixel + 1 Pixel Gitter

                    // Zeichne das Rechteck mit der erhaltenen Farbe, 9x9 um Platz für das Gitter zu lassen
                    bitmapBuilder.FillRectangle(posX, posY, 9, 9, color);
                }
            }

            return bitmapBuilder.ToImage();
        }
    }

}
