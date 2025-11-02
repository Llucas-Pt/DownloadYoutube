using FFMpegCore;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownload.Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YoutubeDownloadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Download([FromBody] DownloadRequest request)
        {
            if (string.IsNullOrEmpty(request.Url) || string.IsNullOrEmpty(request.Format))
                return BadRequest("URL e formato são obrigatórios.");

            try
            {
                var cookies = Environment.GetEnvironmentVariable("YOUTUBE_COOKIES");
                var handler = new HttpClientHandler();
                handler.UseCookies = false;

                var http = new HttpClient(handler);
                http.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", cookies);
                Console.WriteLine("Cookies recebidos: " + cookies);


                var youtube = new YoutubeClient(http);



                var video = await youtube.Videos.GetAsync(request.Url);
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);

                var tempFile = Path.Combine(Path.GetTempPath(), $"{video.Id.Value}.{request.Format}");

                if (request.Format.ToLower() == "mp3")
                {
                    var audioStream = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                    if (audioStream == null)
                        return BadRequest("Nenhum stream de áudio disponível.");

                    await youtube.Videos.Streams.DownloadAsync(audioStream, tempFile);
                }
                else if (request.Format.ToLower() == "mp4")
                {
                    var muxedStream = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                    if (muxedStream == null)
                        return BadRequest("Nenhum stream de vídeo disponível.");

                    await youtube.Videos.Streams.DownloadAsync(muxedStream, tempFile);
                }
                else
                {
                    return BadRequest("Formato inválido");
                }

                var fileBytes = await System.IO.File.ReadAllBytesAsync(tempFile);
                System.IO.File.Delete(tempFile);

                return File(fileBytes, "application/octet-stream", $"{video.Title}.{request.Format}");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        public class DownloadRequest
        {
            public required string Url { get; set; }
            public required string Format { get; set; }
        }
    }
}
