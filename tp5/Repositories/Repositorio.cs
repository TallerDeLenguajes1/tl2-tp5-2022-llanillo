namespace tp5.Repositories;

public abstract class Repositorio<T> : IRepositorio<T>
{
    protected readonly string CadenaConexion;

    public Repositorio(string cadenaConexion)
    {
        CadenaConexion = cadenaConexion;
    }

    public abstract T BuscarPorId(int id);

    public abstract IEnumerable<T> BuscarTodos();

    public abstract void Insertar(T entidad);

    public abstract void Actualizar(T entidad);

    public abstract void Eliminar(int id);
}