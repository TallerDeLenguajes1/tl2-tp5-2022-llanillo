namespace tp5.ViewModels;

public class PedidoAltaViewModel
{
    public readonly List<CadeteViewModel> Cadetes;

    public readonly List<ClienteViewModel> Clientes;

    public PedidoAltaViewModel(List<CadeteViewModel> cadetes, List<ClienteViewModel> clientes)
    {
        Cadetes = cadetes;
        Clientes = clientes;
    }
}