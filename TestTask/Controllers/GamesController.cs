using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTask.Data;
using TestTask.Models;
using TestTask.ViewModels;

namespace TestTask.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class GamesController : ControllerBase
{
    private readonly IRepository<GameModel> _repository;

    private readonly ILogger<GamesController> _logger;

    private readonly IMapper _mapper;

    public GamesController(IRepository<GameModel> repository, ILogger<GamesController> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<GameViewModel>>> GetAllGames()
    {
        _logger.LogInformation($"Get request to {nameof(GetAllGames)}");
        var getRecordsResult = await _repository.GetAllAsync();
        if (!getRecordsResult.Ok)
            return NoContent();

        var result = getRecordsResult.Result!.Select(g => _mapper.Map<GameViewModel>(g));

        return CreatedAtAction(nameof(GetAllGames), result);
    }
}