namespace TestTask.Models;

public class GameModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public GenreModel[] Genres { get; set; }
}