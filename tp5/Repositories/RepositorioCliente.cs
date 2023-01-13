namespace tp5.Repositories;

public class RepositorioCliente : Repositorio<Cliente>
{
    public RepositorioCliente(IConfiguration configuration) : base(configuration)
    {
    }

    public override Cliente? BuscarPorId(int id)
    {
        const string consulta = "select * from Usuario where id_usuario = @id and rol = @rol";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@id", id);
            peticion.Parameters.AddWithValue("@rol", Rol.Cliente);
            conexion.Open();

            var salida = new Cliente();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
                salida = new Cliente
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Direccion = reader.GetString(2),
                    Telefono = reader.GetString(3),
                    Rol = Rol.Cliente
                };

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Logger.Debug("Error al buscar el cliente {Id} - {Error}", id, e.Message);
        }

        return null;
    }

    public override IEnumerable<Cliente> BuscarTodos()
    {
        const string consulta = "select * from Usuario where rol = @rol";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@rol", Rol.Cliente);
            conexion.Open();

            var salida = new List<Cliente>();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
            {
                var cliente = new Cliente
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Direccion = reader.GetString(2),
                    Telefono = reader.GetString(3),
                    Rol = Rol.Cliente
                };

                salida.Add(cliente);
            }

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Logger.Debug("Error al buscar todos los clientes - {Error}", e.Message);
        }

        return null;
    }

    public override IEnumerable<Cliente> BuscarTodosPorId(int id)
    {
        throw new NotImplementedException();
    }

    public override void Insertar(Cliente entidad)
    {
        const string consulta =
            "insert into Usuario (nombre, usuario, clave, rol, direccion, telefono) values (@nombre, @usuario, @clave, @rol, @direccion, @telefono)";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            peticion.Parameters.AddWithValue("@nombre", entidad.Nombre);
            peticion.Parameters.AddWithValue("@usuario", entidad.NombreUsuario);
            peticion.Parameters.AddWithValue("@clave", entidad.Clave);
            peticion.Parameters.AddWithValue("@rol", entidad.Rol);
            peticion.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            Logger.Debug("Error al insertar el cliente {Id} - {Error}", entidad.Id, e.Message);
        }
    }

    public override void Actualizar(Cliente entidad)
    {
        const string consulta =
            "update Usuario set nombre = @nombre, direccion = @direccion, telefono = @telefono where id_usuario = @id and rol = @rol";
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            peticion.Parameters.AddWithValue("@id", entidad.Id);
            peticion.Parameters.AddWithValue("@nombre", entidad.Nombre);
            peticion.Parameters.AddWithValue("@direccion", entidad.Direccion);
            peticion.Parameters.AddWithValue("@telefono", entidad.Telefono);
            peticion.Parameters.AddWithValue("@rol", Rol.Cliente);
            peticion.ExecuteReader();
            conexion.Close();
        }
        catch (Exception e)
        {
            Logger.Debug("Error al actualizar el cliente {Id} - {Error}", entidad.Id, e.Message);
        }
    }

    public override void Eliminar(int id)
    {
        const string consulta =
            "delete from Usuario where id_usuario = @id and rol = @rol";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@id", id);
            peticion.Parameters.AddWithValue("@rol", Rol.Cliente);
            peticion.ExecuteReader();
            conexion.Close();
        }
        catch (Exception e)
        {
            Logger.Debug("Error al eliminar el cliente {Id} - {Error}", id, e.Message);
        }
    }
}