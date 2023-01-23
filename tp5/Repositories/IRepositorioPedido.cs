namespace tp5.Repositories;

public interface IRepositorioPedido : IRepositorio<Pedido>
{
    IEnumerable<Pedido> BuscarTodosPorUsuarioYRol(int idUsuario, Rol rol);

    IEnumerable<Pedido> BuscarTodosPorId(int id);
}