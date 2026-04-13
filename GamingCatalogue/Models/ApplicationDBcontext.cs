using GamingCatalogue.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Game> Games { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Genre> Genres { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "Open World" },
            new Tag { Id = 2, Name = "RPG" },
            new Tag { Id = 3, Name = "First Person" },
            new Tag { Id = 4, Name = "Action" }
        );
        modelBuilder.Entity<Game>().HasData(
            new Game
            {
                Id = 1, 
                Title = "The Witcher 3",
                Year = "2015",
                Description = "Вы- ведьмак Геральт из Ривии. Убийца чудовищ, путешествующий по миру, в котором бушует война. Спасите Цири - Дитя Предназначения, живое оружие, способное спасти или уничтожить мир. Из двух зол сумейте выбрать меньшее",
                GameplayScore = 9.0,
                GraphicsScore = 8.5,
                StoryScore = 9.2,
                MusicScore = 10.0,
                ImageUrl = "https://avatars.mds.yandex.net/i?id=e27a86862375d4e411a2fd746c50637e32718eb5-4571389-images-thumbs&n=13"
            },
            new Game
            {
                Id = 2,
                Title = "Cyberpunk 2077",
                Year = "2020",
                Description = "Доброе утро, Найт-сити! Сыграйте за наемника Ви, пытающегося выжить в городе будущего, где выше всего ценятся деньги",
                GameplayScore = 8.5,
                GraphicsScore = 10.0,
                StoryScore = 9.5,
                MusicScore = 10,
                ImageUrl = "https://avatars.mds.yandex.net/i?id=5c320c7935e0c26be3a814ee8d5d0d0b8ab4e6a6-6489204-images-thumbs&n=13"
            }
        );
    }

}
