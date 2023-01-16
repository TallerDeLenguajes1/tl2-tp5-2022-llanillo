namespace tp5.Repositories;

public class RepositorioUsuario : IRepositorioUsuario
{
    protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    private readonly IConfiguration _configuration;
    protected readonly string? CadenaConexion;

    public RepositorioUsuario(IConfiguration configuration)
    {
        _configuration = configuration;
        CadenaConexion = _configuration.GetConnectionString("ConnectionString");
    }

    public Usuario? Verificar(Usuario usuario)
    {
        var consulta =
            $"select * from Usuario where usuario = '{usuario.NombreUsuario}' and clave = '{usuario.Clave}'";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            using var reader = peticion.ExecuteReader();
            var salida = new Usuario();

            while (reader.Read())
                salida = new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    NombreUsuario = reader.GetString(2),
                    Clave = reader.GetString(3),
                    Rol = (Rol)reader.GetInt32(4),
                    Direccion = reader.GetString(5),
                    Telefono = reader.GetString(6)
                };

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Logger.Debug("Error al buscar el usuario {Id} - {Error}", usuario.Id, e.Message);
        }

        return null;
    }

    public IEnumerable<Usuario> BuscarTodos()
    {
        const string consulta = "select * from Usuario";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            var salida = new List<Usuario>();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
            {
                var usuario = new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    NombreUsuario = reader.GetString(2),
                    Clave = reader.GetString(3),
                    Rol = (Rol)reader.GetInt32(4),
                    Direccion = reader.GetString(5),
                    Telefono = reader.GetString(6)
                };

                salida.Add(usuario);
            }

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Logger.Debug("Error al buscar todos los usuarios - {Error}", e.Message);
        }

        return null;
    }
}