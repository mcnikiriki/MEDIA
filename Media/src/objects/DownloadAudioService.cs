using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using MEDIA.src;

namespace Media.src.objects
{
    public class DownloadAudioService : IDownloadContentService
    {
        private readonly List<string> _audios;
        private readonly string _downloadPath;

        private const int MaxRetries = 3;
        private const int DelayOnRetry = 1000; // milliseconds

        public DownloadAudioService(List<string> audios, string downloadPath)
        {
            _audios = audios ?? throw new ArgumentException("Audios list cannot be null or empty.");
            _downloadPath = !string.IsNullOrWhiteSpace(downloadPath) ? downloadPath : throw new ArgumentException("Download path cannot be null or empty.");
        }

        public async Task DownloadContents()
        {
            foreach (var audio in _audios)
            {
                await DownloadAudio(audio);
            }
        }

        private async Task DownloadAudio(string audioUrl)
        {
            for (int attempt = 0; attempt < MaxRetries; attempt++)
            {
                try
                {
                    var audioFileName = Path.Combine(_downloadPath, $"{Guid.NewGuid()}.mp3");
                    var processStartInfo = new ProcessStartInfo
                    {
                        FileName = "yt-dlp",
                        Arguments = $"-x --audio-format mp3 -o \"{audioFileName}\" \"{audioUrl}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = false
                    };

                    using (var process = new Process { StartInfo = processStartInfo })
                    {
                        process.Start();

                        var totalSize = 100; // This should be set based on actual size if possible
                        var progress = new Progress<int>(value => UpdateProgressBar(value, totalSize));
                        
                        // Simulate reading output to update progress (replace with actual reading logic)
                        for (int i = 0; i <= totalSize; i++)
                        {
                            await Task.Delay(100); // Simulating download time
                            ((IProgress<int>)progress).Report(i);
                        }

                        await process.WaitForExitAsync();

                        if (process.ExitCode == 0)
                        {
                            Console.WriteLine($"Downloaded: {audioUrl}");
                            return; // Exit if successful
                        }
                        else
                        {
                            Console.WriteLine($"Error downloading {audioUrl}: {await process.StandardError.ReadToEndAsync()}");
                            throw new Exception($"yt-dlp exited with code {process.ExitCode}");
                        }
                    }
                }
                catch (Exception e) when (attempt < MaxRetries - 1)
                {
                    Console.WriteLine($"Attempt {attempt + 1} failed: {e.Message}");
                    await Task.Delay(DelayOnRetry); // Wait before retrying
                }
            }

            throw new Exception("Failed to download audio after multiple attempts.");
        }

        private void UpdateProgressBar(int value, int total)
        {
            Console.CursorLeft = 0;
            Console.Write("[");
            int width = 50; // Width of the progress bar
            int position = (int)((double)value / total * width);
            Console.Write(new string('#', position));
            Console.Write(new string(' ', width - position));
            Console.Write($"] {value}%");
        }
    }
}