using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GamingCatalogue.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameplayScore = table.Column<double>(type: "float", nullable: false),
                    GraphicsScore = table.Column<double>(type: "float", nullable: false),
                    StoryScore = table.Column<double>(type: "float", nullable: false),
                    MusicScore = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GameplayScore", "GraphicsScore", "ImageUrl", "MusicScore", "StoryScore", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "Вы- ведьмак Геральт из Ривии. Убийца чудовищ, путешествующий по миру, в котором бушует война. Спасите Цири - Дитя Предназначения, живое оружие, способное спасти или уничтожить мир. Из двух зол сумейте выбрать меньшее", 9.0, 8.5, "https://avatars.mds.yandex.net/i?id=e27a86862375d4e411a2fd746c50637e32718eb5-4571389-images-thumbs&n=13", 10.0, 9.1999999999999993, "The Witcher 3", "2015" },
                    { 2, "Доброе утро, Найт-сити! Сыграйте за наемника Ви, пытающегося выжить в городе будущего, где выше всего ценятся деньги", 8.5, 10.0, "https://avatars.mds.yandex.net/i?id=5c320c7935e0c26be3a814ee8d5d0d0b8ab4e6a6-6489204-images-thumbs&n=13", 10.0, 9.5, "Cyberpunk 2077", "2020" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
