using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Models;

public class GameModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Developer { get; set; } = null!;

    [NotMapped] 
    public string[] GenreArray { get; set; } = null!;

    public string Genres 
    {
        get => string.Join(";", GenreArray);
        set => GenreArray = value.Split(";", StringSplitOptions.TrimEntries);
    }

}