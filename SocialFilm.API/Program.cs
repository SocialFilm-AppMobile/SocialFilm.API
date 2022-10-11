using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SocialFilm.API.Security.Authorization.Handlers.Implementations;
using SocialFilm.API.Security.Authorization.Handlers.Interfaces;
using SocialFilm.API.Security.Authorization.Middleware;
using SocialFilm.API.Security.Authorization.Settings;
using SocialFilm.API.Security.Domain.Repositories;
using SocialFilm.API.Security.Domain.Services;
using SocialFilm.API.Security.Persistence.Repositories;
using SocialFilm.API.Security.Services;
using SocialFilm.API.Shared.Domain.Repositories;
using SocialFilm.API.Shared.Persistence.Contexts;
using SocialFilm.API.Shared.Persistence.Repositories;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Services;
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
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSwaggerGen(options =>
{
    //Add API Documentation Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "3m SocialFilm API",
        Description = "3m SocialFilm RESTful API",
        TermsOfService = new Uri("https://socialfilm-3m.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "3m.studio",
            Url = new Uri("https://3m.studio")
        },
        License = new OpenApiLicense
        {
            Name = "3m SocialFilm Resources License",
            Url = new Uri("https://socialfilm-3m.com/license")
        }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer Scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            Array.Empty<string>()
        }
    });
});

//*********************************************PART 1********************************************************
// ADD DATABASE CONNECTION
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine,LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// ADD LOWERCASE ROUTES
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// DEPENDENCY INJECTION CONFIGURATION
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ISerieRepository, SerieRepository>();
builder.Services.AddScoped<ISerieService, SerieService>();
builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
builder.Services.AddScoped<IEpisodeService, EpisodeService>();
builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<IBannerVideoRepository, BannerVideoRepository>();
builder.Services.AddScoped<IBannerVideoService, BannerVideoService>();



//AUTOMAPPER CONFIGURATION
builder.Services.AddAutoMapper(
    typeof(SocialFilm.API.Watching.Mapping.ModelToResourceProfile),
    typeof(SocialFilm.API.Watching.Mapping.ResourceToModelProfile),
    typeof(SocialFilm.API.Security.Mapping.ModelToResourceProfile),
    typeof(SocialFilm.API.Security.Mapping.ResourceToModelProfile));
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

}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("socialFilmCors");
// Configure Error Handler Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure JSON Web Token Handling Middleware
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();