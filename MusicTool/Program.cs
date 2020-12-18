using System;
using System.IO;
using System.Linq;

namespace MusicTool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var musicDir = new DirectoryInfo("C:\\Users\\alex\\Downloads\\vk music");

            foreach (var file in musicDir.EnumerateFiles().Where(f => f.Extension == ".mp3"))
            {
                var mp3File = TagLib.File.Create(file.FullName);

                if (!string.IsNullOrEmpty(mp3File.Tag.Title))
                    continue;

                var mp3FileInfo = file.Name
                    .Substring(0, file.Name.IndexOf(file.Extension, StringComparison.InvariantCulture))
                    .Split('-')
                    .Select(v => v.TrimEnd())
                    .ToArray();

                var (performers, title) = (mp3FileInfo[0], mp3FileInfo[1]);

                mp3File.Tag.Title = title;
                mp3File.Tag.Performers = new[] { performers };

                mp3File.Save();
            }
        }
    }
}
