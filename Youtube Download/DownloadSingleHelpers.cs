using MediaToolkit;
using MediaToolkit.Model;
using VideoLibrary;

namespace Youtube_Download
{
    internal static class DownloadSingleHelpers
    {

        public static void DownloadSingleAsync(string url)
        {
            var source = @"c:\YoutubeDL\";
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(url);
            File.WriteAllBytes(source + vid.FullName, vid.GetBytes());

            var inputFile = new MediaFile { Filename = source + vid.FullName };
            var outputFile = new MediaFile { Filename = $"{source + vid.FullName}.mp3" };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);

                engine.Convert(inputFile, outputFile);
            }
        }
    }
}