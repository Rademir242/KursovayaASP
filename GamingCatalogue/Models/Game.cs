namespace GamingCatalogue.Models
{
public class Game
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string Year { get; set; } 
        public string Description { get; set; } 

        public double GameplayScore { get; set; }
        public double GraphicsScore { get; set; }
        public double StoryScore { get; set; }
        public double MusicScore { get; set; }


        public string ImageUrl { get; set; } 
    }
}
