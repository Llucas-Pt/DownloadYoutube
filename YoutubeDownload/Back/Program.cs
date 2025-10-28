using FFMpegCore;

var builder = WebApplication.CreateBuilder(args);

// CORS: libera para o front local e o do Render
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3005", // ambiente local
                "https://downloadyoutube-frontend.onrender.com" // URL do front hospedado no Render
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Render define a variável PORT automaticamente
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do FFmpeg (Render usa containers Linux)
GlobalFFOptions.Configure(new FFOptions
{
    BinaryFolder = "/usr/bin"
});

// Log útil no console do Render
Console.WriteLine($"🚀 API YoutubeDownload iniciada na porta {port}, ambiente: {app.Environment.EnvironmentName}");

// Endpoint raiz (teste rápido no navegador)
app.MapGet("/", () => "✅ API YoutubeDownload rodando com sucesso!");

// Swagger — visível apenas se for ambiente local
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
