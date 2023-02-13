namespace tp5.Repositories.Interface;

public abstract class RepositorioPedidoBase : Repositorio<Pedido>
{
    protected RepositorioPedidoBase(IConfiguration configuration) : base(configuration)
    {
    }

    public abstract IEnumerable<Pedido> BuscarTodosPorUsuarioYRol(int idUsuario, Rol rol);

    public abstract IEnumerable<Pedido> BuscarTodosPorId(int id);
}