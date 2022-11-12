namespace tp5.Repositories.Cadete;

public interface IRepositorio <T>
{
    T BuscarPorId(int id);

    IEnumerable<T> BuscarTodos();

    void Insertar(T entidad);

    void Actualizar(T entidad);

    void Eliminar(int id);
}