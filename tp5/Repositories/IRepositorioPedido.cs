namespace tp5.Repositories;

public interface IRepositorioPedido : IRepositorio<Pedido>
{
    IEnumerable<Pedido> BuscarTodosPorCliente(int id);

    IEnumerable<Pedido> BuscarTodosPorCadete(int id);
}