using AutoMapper;
using TestTask.ViewModels;

namespace TestTask.Models.Mapping;

public class GameMapping : Profile
{
    public GameMapping()
    {
        CreateMap<GameModel, GameViewModel>()
            .ForMember(gvm => gvm.Genres, s => s.MapFrom(x => x.GenreArray));
    }
}