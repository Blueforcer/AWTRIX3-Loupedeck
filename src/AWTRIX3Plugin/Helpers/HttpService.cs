using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loupedeck.Awtrix3Plugin
{
    public class HttpService
    {
        private static Timer _timer;
        public static string Host { get; set; }
        private static readonly HttpClient _httpClient = new HttpClient();

        // Definiere vier globale BitmapImages
        public static BitmapImage[] LedImages { get; private set; } = new BitmapImage[4];

        public static class GlobalLedData
        {
            public static BitmapColor[,] Colors { get; private set; } = new BitmapColor[32, 8];
        }

        public static void Initialize()
        {
            // Setze den Timer, um alle 2 Sekunden die DownloadLedData Methode aufzurufen.
            _timer = new Timer(async _ =>
            {
                await DownloadLedData(); // Ersetze 'api/led-data' mit deinem tatsächlichen Endpunkt.
            },
            null,
            TimeSpan.Zero,
            TimeSpan.FromMilliseconds(60));
        }

        public static async Task<Boolean> SendPostRequest(String apiEndpoint, String payload)
        {
            if (String.IsNullOrEmpty(Host))
            {
                Console.WriteLine("Host is not set.");
                return false;
            }

            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var fullUri = $"http://{Host}/api/{apiEndpoint}";

            try
            {
                var response = await _httpClient.PostAsync(fullUri, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending POST request to {fullUri}: {e.Message}");
                return false;
            }
        }


        public static async Task DownloadLedData()
        {
            var fullUri = $"http://{Host}/api/screen";

            try
            {
                var responseString = await _httpClient.GetStringAsync(fullUri);
                var colorInts = System.Text.Json.JsonSerializer.Deserialize<int[]>(responseString);
                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 32; x++)
                    {
                        var colorValue = colorInts[y * 32 + x]; // Korrigiere den Index für zeilenweise gespeicherte Daten
                        GlobalLedData.Colors[x, y] = new BitmapColor(
                            (Byte)((colorValue >> 16) & 0xFF),
                            (Byte)((colorValue >> 8) & 0xFF),
                            (Byte)(colorValue & 0xFF));
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error downloading or processing LED data: {e.Message}");
            }
        }


    }
}
