namespace tp5.Repositories;

public class RepositorioUsuario : Repositorio<Usuario>
{
    
    public RepositorioUsuario(IConfiguration configuration) : base(configuration)
    {
    }

    public override Usuario? BuscarPorId(int id)
    {
        const string consulta = "select * from Usuario where id_pedido = @id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@id", id);
            conexion.Open();

            var salida = new Usuario();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
                salida = new Usuario
                {
                    Id = reader.GetInt32(0),
                    NombreUsuario = reader.GetString(1),
                    Contrasena = reader.GetString(2),
                    Rol = reader.GetString(2),
                };

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Logger.Debug("Error al buscar el pedido {Id} - {Error}", id, e.Message);
        }

        return null;
    }

    public override IEnumerable<Usuario> BuscarTodos()
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
                var pedido = new Usuario
                {
                    Id = reader.GetInt32(0),
                    NombreUsuario = reader.GetString(1),
                    Contrasena = reader.GetString(2),
                    Rol = reader.GetString(2),
                };
                salida.Add(pedido);
            }

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Logger.Debug("Error al buscar todos los pedidos - {Error}", e.Message);
        }

        return new List<Usuario>();
    }

    public override void Insertar(Usuario entidad)
    {
        const string consulta =
            "insert into Usuario (observacion, estado, id_cadete, id_cliente) values (@observacion, @estado, @cadete, @cliente)";
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            peticion.Parameters.AddWithValue("@observacion", entidad.Nombre);
            peticion.Parameters.AddWithValue("@estado", entidad.NombreUsuario);
            peticion.Parameters.AddWithValue("@cadete", entidad.Contrasena);
            peticion.Parameters.AddWithValue("@cliente", entidad.Rol);
            peticion.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            Logger.Debug("Error al insertar el pedido {Id} - {Error}", entidad.Id, e.Message);
        }
    }

    public override void Actualizar(Usuario entidad)
    {
        const string consulta =
            "update Usuario set observacion = @observacion, estado = @estado, id_cadete = @cadete, id_cliente = @cliente where id_pedido = @id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            peticion.Parameters.AddWithValue("@id", entidad.Id);
            peticion.Parameters.AddWithValue("@observacion", entidad.Nombre);
            peticion.Parameters.AddWithValue("@estado", entidad.NombreUsuario);
            peticion.Parameters.AddWithValue("@cadete", entidad.Contrasena);
            peticion.Parameters.AddWithValue("@cliente", entidad.Rol);
            peticion.ExecuteReader();
            conexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Logger.Debug("Error al insertar el pedido {Id} - {Error}", entidad.Id, e.Message);
        }
    }

    public override void Eliminar(int id)
    {
        const string consulta =
            "delete from Usuario where id_pedido = @id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@id", id);
            peticion.ExecuteReader();
            conexion.Close();
        }
        catch (Exception e)
        {
            Logger.Debug("Error al eliminar el pedido {Id} - {Error}", id, e.Message);
        }
    }

}