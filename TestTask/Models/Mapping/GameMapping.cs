using AutoMapper;
using TestTask.ViewModels;

namespace TestTask.Models.Mapping;

public class GameMapping : Profile
{
    public GameMapping()
    {
        CreateMap<GameModel, GameViewModel>()
            .ForMember(gvm => gvm.Genres, s => s.MapFrom(x => x.GenreArray));

        CreateMap<GameViewModel, GameModel>()
            .ForMember(d => d.Genres, s => s.Ignore())
            .ForMember(d => d.GenreArray, s => s.MapFrom(x => x.Genres))
            .ForMember(d => d.Id, s => s.Ignore());
    }
}