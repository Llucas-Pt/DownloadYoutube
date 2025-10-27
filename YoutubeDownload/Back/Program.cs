using FFMpegCore;

var builder = WebApplication.CreateBuilder(args);

// CORS: libera para o front local e o do Render
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3005", // local
                "https://downloadyoutube-frontend.onrender.com" 
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Render define a variável PORT automaticamente
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"https://*:{port}");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// FFmpeg no Linux (Render)
GlobalFFOptions.Configure(new FFOptions
{
    BinaryFolder = "/usr/bin"
});

//  Log útil no console Render
Console.WriteLine($"🚀 API YoutubeDownload iniciada na porta {port}, ambiente: {app.Environment.EnvironmentName}");

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
