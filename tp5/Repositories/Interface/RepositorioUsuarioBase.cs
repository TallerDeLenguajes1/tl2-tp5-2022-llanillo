namespace tp5.Repositories.Interface;

public abstract class RepositorioUsuarioBase : Repositorio<Usuario>
{
    protected RepositorioUsuarioBase(IConfiguration configuration) : base(configuration)
    {
    }

    public abstract Usuario? Verificar(Usuario usuario);

    public abstract IEnumerable<Usuario> BuscarTodosPorRol(Rol rol);
}