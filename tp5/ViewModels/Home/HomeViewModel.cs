using tp5.ViewModels.Usuario.General;

namespace tp5.ViewModels.Home;

public class HomeViewModel
{
    public LoginViewModel LoginViewModel { get; set; }

    public List<UsuarioViewModel> UsuarioViewModels { get; set; }
}