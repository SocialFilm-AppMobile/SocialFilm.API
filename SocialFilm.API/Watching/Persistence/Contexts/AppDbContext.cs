using Microsoft.EntityFrameworkCore;
using SocialFilm.API.Shared.Domain.Model;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Persistence.Contexts;

public class AppDbContext:DbContext
{
    public DbSet<Video> Videos { get; set; }
    public DbSet<BannerVideo> BannerVideos { get; set; } 
    public DbSet<Category> Categories { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Serie> Series { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    
    //Not implemented Yet
    //public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //VIDEO MODEL
        builder.Entity<Video>().ToTable("Videos");
        builder.Entity<Video>().HasKey(p => p.Id);
        builder.Entity<Video>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Video>().Property(p => p.VideoUrl).IsRequired();
        
        //BANNER VIDEO MODEL 
        builder.Entity<BannerVideo>().ToTable("BannerVideos");
        builder.Entity<BannerVideo>().HasKey(p => p.Id);
        builder.Entity<BannerVideo>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<BannerVideo>().Property(p => p.Banner).IsRequired();
        builder.Entity<BannerVideo>().Property(p => p.Billboard).IsRequired();

        // Relationships
       /*builder.Entity<Video>()
            .HasMany(p => p.Films)
            .WithOne(p => p.Video)
            .HasForeignKey(p => p.VideoId);
        builder.Entity<Video>()
            .HasMany(p => p.Episodes)
            .WithOne(p => p.Video)
            .HasForeignKey(p => p.VideoId);*/
        
        //CATEGORY MODEL
        builder.Entity<Category>().ToTable("Categories");
        builder.Entity<Category>().HasKey(p => p.Id);
        builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(p => p.Name).IsRequired();
        
        // Relationships
        builder.Entity<Category>()
            .HasMany(p => p.Films)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
        builder.Entity<Category>()
            .HasMany(p => p.Series)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
        
        //FILM MODEL    
        builder.Entity<Film>().ToTable("Films");
        builder.Entity<Film>().HasKey(p => p.Id);
        builder.Entity<Film>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Film>().Property(p => p.Title).IsRequired();
        builder.Entity<Film>().Property(p => p.Synopsis).IsRequired().HasMaxLength(500);

        builder.Entity<Film>()
            .HasOne(p => p.BannerVideo)
            .WithOne(p => p.Film)
            .HasForeignKey<BannerVideo>(p => p.FilmId);

        //SERIE MODEL    
        builder.Entity<Serie>().ToTable("Series");
        builder.Entity<Serie>().HasKey(p => p.Id);
        builder.Entity<Serie>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Serie>().Property(p => p.Title).IsRequired();
        builder.Entity<Serie>().Property(p => p.Synopsis).IsRequired().HasMaxLength(500);
        
        // Relationships
        builder.Entity<Serie>()
            .HasMany(p => p.Seasons)
            .WithOne(p => p.Serie)
            .HasForeignKey(p => p.SerieId);
        
        //SEASON MODEL    
        builder.Entity<Season>().ToTable("Seasons");
        builder.Entity<Season>().HasKey(p => p.Id);
        builder.Entity<Season>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Season>().Property(p => p.Title).IsRequired();
        builder.Entity<Season>().Property(p => p.Synopsis).IsRequired().HasMaxLength(500);
        
        // Relationships
        builder.Entity<Season>()
            .HasMany(p => p.Episodes)
            .WithOne(p => p.Season)
            .HasForeignKey(p => p.SeasonId);
        
        //EPISODE MODEL
        builder.Entity<Episode>().ToTable("Episodes");
        builder.Entity<Episode>().HasKey(p => p.Id);
        builder.Entity<Episode>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Episode>().Property(p => p.Title).IsRequired();
        builder.Entity<Episode>().Property(p => p.Synopsis).IsRequired().HasMaxLength(500);
        
        
    }
}