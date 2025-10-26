using FFMpegCore;

var builder = WebApplication.CreateBuilder(args);

// CORS: front local ou URL do Render
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3005") // localmente
                                                      //.WithOrigins("https://front-render-url.onrender.com") // produção
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Pega a porta que o Render define, ou usa 8080 como fallback
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// FFmpeg no container Linux
GlobalFFOptions.Configure(new FFOptions
{
    BinaryFolder = "/usr/bin"
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();
app.Run();





/*using System.IO;
using FFMpegCore;
using YoutubeDownload;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3005") // endereço do seu front-end React
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configura FFmpeg embutido
var ffmpegPath = Path.Combine(AppContext.BaseDirectory, "Back", "ffmpeg", "bin", "ffmpeg.exe");
if (!File.Exists(ffmpegPath))
{
    throw new FileNotFoundException($"ffmpeg.exe não encontrado em: {ffmpegPath}");
}

GlobalFFOptions.Configure(new FFOptions
{
    BinaryFolder = Path.Combine(AppContext.BaseDirectory, "Back", "ffmpeg", "bin")
});




// Ativa o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp"); // CORS
app.UseAuthorization();
app.MapControllers();
app.Run();
*/