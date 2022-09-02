using TestTask.Models;

namespace TestTask.Data;

public class DataSeeder
{
    private readonly IRepository<GameModel> _repository;

    public DataSeeder(IRepository<GameModel> repository) => _repository = repository;

    public async Task Seed()
    {
        await _repository.CreateAsync(new GameModel()
        {
            Name = "Metro 2033",
            Developer = "4A Games",
            GenreArray = new []
            {
                "Horror",
                "FPS"
            }
        });

        await _repository.CreateAsync(new GameModel()
        {
            Name = "Stalker:Shadow of Chernobyl",
            Developer = "GSC game world",
            GenreArray = new []
            {
                "Horror",
                "FPS"
            }
        });
    }
}