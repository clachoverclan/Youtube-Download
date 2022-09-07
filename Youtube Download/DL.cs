using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YoutubeExplode;
using YoutubeExplode.Playlists;
using MediaToolkit.Model;
using MediaToolkit;
using VideoLibrary;

namespace Youtube_Download
{
    class DL
    {   
        public static void Startup()
        {
            // Specify the directory you want to manipulate.
            string path = @"c:\YoutubeDL";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}", Directory.GetCreationTime(path));
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }

        public static void DownloadSingle(string url)
        {
            
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(url);
            string path = $@"c:\YoutubeDL\{vid.Info.Author}\";
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}", Directory.GetCreationTime(path));
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            File.WriteAllBytes(path + vid.FullName, vid.GetBytes());

            var inputFile = new MediaFile { Filename = path + vid.FullName };
            var outputFile = new MediaFile { Filename = $"{path + vid.FullName}.mp3" };

            using var engine = new Engine();
            engine.GetMetadata(inputFile);
            engine.Convert(inputFile, outputFile);
            File.Delete(path + vid.FullName);
        }

        public static async Task DownloadPlaylistAsync(string url)
        {
            var youtube = YouTube.Default;
            var ytb = new YoutubeClient();
            await foreach (var id in ytb.Playlists.GetVideosAsync(url)) {
                var vid = youtube.GetVideo($"https://www.youtube.com/watch?v={id.Id}");
                string path = $@"c:\YoutubeDL\{vid.Info.Author}\";
                try
                {
                    // Determine whether the directory exists.
                    if (Directory.Exists(path))
                    {
                        Console.WriteLine("That path exists already.");
                        return;
                    }

                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    Console.WriteLine("The directory was created successfully at {0}", Directory.GetCreationTime(path));
                }
                catch (Exception e)
                {
                    MessageBox.Show("The process failed: {0} "+ e.ToString());
                }
                File.WriteAllBytes(path + vid.FullName, vid.GetBytes());

                var inputFile = new MediaFile { Filename = path + vid.FullName };
                var outputFile = new MediaFile { Filename = $"{path + vid.FullName}.mp3" };

                using var engine = new Engine();
                engine.GetMetadata(inputFile);
                engine.Convert(inputFile, outputFile);
                File.Delete(path + vid.FullName);
            }
            
        }
    }
}
