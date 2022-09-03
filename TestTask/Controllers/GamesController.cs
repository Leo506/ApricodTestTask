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

    
    [HttpGet]
    public async Task<ActionResult<GameViewModel>> GetGameById(int id)
    {
        _logger.LogInformation($"Get request to {nameof(GetGameById)}");

        var getResult = await _repository.GetAsync(id);
        if (!getResult.Ok)
            return BadRequest();

        var result = _mapper.Map<GameViewModel>(getResult.Result);

        return CreatedAtAction(nameof(GetGameById), result);
    }

    
    [HttpPost]
    public async Task<ActionResult> CreateGame(GameViewModel gameViewModel)
    {
        var gameModel = _mapper.Map<GameModel>(gameViewModel);

        var creatingResult = await _repository.CreateAsync(gameModel);

        if (!creatingResult.Ok)
            return BadRequest();

        return Ok();
    }

    
    [HttpDelete]
    public async Task<ActionResult> DeleteGame(int id)
    {
        var gettingResult = await _repository.GetAsync(id);

        if (!gettingResult.Ok)
            return BadRequest();

        var deletingResult = await _repository.DeleteAsync(gettingResult.Result!);

        if (!deletingResult.Ok)
            return BadRequest();

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateGame(int id, GameViewModel gameViewModel)
    {
        var gameModel = _mapper.Map<GameModel>(gameViewModel);
        gameModel.Id = id;

        var updatingResult = await _repository.UpdateAsync(gameModel);

        if (!updatingResult.Ok)
            return BadRequest();

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameViewModel>>> GetByGenre(string genre)
    {
        var gettingAllRecords = await _repository.GetAllAsync();

        if (!gettingAllRecords.Ok)
            return BadRequest();

        if (gettingAllRecords.Result?.Count() == 0)
            return NoContent();
        
        var result = gettingAllRecords.Result?.Where(g => g.GenreArray.Contains(genre))
            .Select(g => _mapper.Map<GameViewModel>(g));

        return CreatedAtAction(nameof(GetByGenre), result);
    }
}