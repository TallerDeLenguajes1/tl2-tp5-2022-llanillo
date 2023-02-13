using tp5.ViewModels.Home;
using tp5.ViewModels.Pedido;
using tp5.ViewModels.Usuario.General;

namespace tp5.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
        CreateMap<Usuario, UsuarioModificadoViewModel>().ReverseMap();
        CreateMap<Usuario, LoginViewModel>().ReverseMap();
        CreateMap<Pedido, PedidoViewModel>().ReverseMap();
        CreateMap<Pedido, PedidoAltaViewModel>().ReverseMap();
        CreateMap<Pedido, PedidoModificadoViewModel>().ReverseMap();
    }
}