using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Mapping;
using SocialFilm.API.Watching.Persistence.Contexts;
using SocialFilm.API.Watching.Persistence.Repositories;
using SocialFilm.API.Watching.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add CORS Service
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"socialFilmCors", builder =>
    {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen();

//*********************************************PART 1********************************************************
// ADD DATABASE CONNECTION
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine,LogLevel.Information)
        .EnableDetailedErrors());

// ADD LOWERCASE ROUTES
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// DEPENDENCY INJECTION CONFIGURATION
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<ISerieRepository, SerieRepository>();
builder.Services.AddScoped<ISerieService, SerieService>();
builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
builder.Services.AddScoped<IEpisodeService, EpisodeService>();
builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<IBannerVideoRepository, BannerVideoRepository>();
builder.Services.AddScoped<IBannerVideoService, BannerVideoService>();



//AUTOMAPPER CONFIGURATION
builder.Services.AddAutoMapper(typeof(ModelToResourceProfile), typeof(ResourceToModelProfile));
//***********************************************************************************************************

var app = builder.Build();

//*********************************************PART 2********************************************************
//VALIDATION FOR ENSURING DATABASE OBJECTS ARE CREATED
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}
//***********************************************************************************************************

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("socialFilmCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();