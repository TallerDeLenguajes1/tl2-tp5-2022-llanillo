using AutoMapper;
using tp5.Models;
using tp5.ViewModels.Cadete;

namespace tp5.Mappers;

public class MappingProfile : Profile
{
    protected MappingProfile()
    {
        CreateMap<Cadete, CadeteViewModel>().ReverseMap();
    }
}