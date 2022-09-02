using Calabonga.OperationResults;
using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data;

public class GameRepository : IRepository<GameModel>
{
    private readonly GameDbContext _context;
    private readonly ILogger<GameRepository> _logger;


    public GameRepository(GameDbContext context, ILogger<GameRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void Dispose() => _context.Dispose();

    public async Task<OperationResult<IEnumerable<GameModel>>> GetAllAsync()
    {
        var result = OperationResult.CreateResult<IEnumerable<GameModel>>();
        try
        {
            _logger.LogInformation($"Getting all records from database");
            result.Result = await _context.Games.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while getting all records: {e.Message})");
            result.AddError(e);
        }

        return result;
    }

    public async Task<OperationResult<GameModel>> GetAsync(int id)
    {
        var result = OperationResult.CreateResult<GameModel>();
        try
        {
            _logger.LogInformation($"Getting game with id: {id}");
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                _logger.LogError($"No games with this id: {id}");
                result.AddError(new Exception("Game not found"));
                return result;
            }

            result.Result = game;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while getting game with id: {id}");
            result.AddError(e);
        }

        return result;
    }

    public async Task<OperationResult<bool>> CreateAsync(GameModel item)
    {
        var result = OperationResult.CreateResult<bool>();

        try
        {
            _logger.LogInformation($"Creating new game");
            await _context.Games.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while creating new game: {e.Message}");
            result.AddError(e);
        }

        return result;
    }

    public async Task<OperationResult<bool>> UpdateAsync(GameModel item)
    {
        var result = OperationResult.CreateResult<bool>();

        try
        {
            _logger.LogInformation($"Updating game with id: {item.Id}");
            _context.Games.Update(item);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while updating game: {e.Message}");
            result.AddError(e);
        }

        return result;
    }

    public async Task<OperationResult<bool>> DeleteAsync(GameModel item)
    {
        var result = OperationResult.CreateResult<bool>();

        try
        {
            _logger.LogInformation($"Deleting game with id: {item.Id}");
            _context.Games.Remove(item);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error while deleting game: {e.Message}");
            result.AddError(e);
        }

        return result;
    }
}