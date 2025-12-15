using Carsales.RickAndMorty.BFF.Clients;
using Carsales.RickAndMorty.BFF.Endpoints;
using Carsales.RickAndMorty.BFF.Middleware;
using Carsales.RickAndMorty.BFF.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración tipada para la API externa
builder.Services.Configure<RickAndMortyApiOptions>(
    builder.Configuration.GetSection(RickAndMortyApiOptions.SectionName));

// Cliente HTTP hacia Rick and Morty
builder.Services.AddHttpClient<IRickAndMortyApiClient, RickAndMortyApiClient>();

// Servicios de dominio
builder.Services.AddScoped<IEpisodeService, EpisodeService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS para frontend Angular dev
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware global de manejo de errores
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularDev");

// Endpoints minimal API
EpisodeEndpoints.Map(app);

app.Run();