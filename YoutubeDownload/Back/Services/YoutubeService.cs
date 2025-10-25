// Services/DownloadService.cs
using AngleSharp.Media;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;


namespace YoutubeDownload.Back.Models
{
    public class YoutubeService
    {
        public string Url { get; set; }
        public string Format { get; set; }
    }
}
