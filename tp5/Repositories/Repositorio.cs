namespace tp5.Repositories;

public abstract class Repositorio<T> : IRepositorio<T>
{
    private readonly IConfiguration _configuration;
    protected readonly string? CadenaConexion;

    protected Repositorio(IConfiguration configuration)
    {
        _configuration = configuration;
        CadenaConexion = _configuration.GetConnectionString("ConnectionString");
    }

    public abstract T? BuscarPorId(int id);

    public abstract IEnumerable<T> BuscarTodos();

    public abstract void Insertar(T entidad);

    public abstract void Actualizar(T entidad);

    public abstract void Eliminar(int id);
}