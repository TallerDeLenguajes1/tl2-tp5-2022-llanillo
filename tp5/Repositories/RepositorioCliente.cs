namespace tp5.Repositories;

public class RepositorioCliente : Repositorio<Cliente>
{
    public RepositorioCliente(IConfiguration configuration) : base(configuration)
    {
    }

    public override Cliente? BuscarPorId(int id)
    {
        const string consulta = "select * from Cliente where id_cliente = @id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@id", id);
            conexion.Open();

            var salida = new Cliente();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
                salida = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));

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
        const string consulta = "select * from Cliente";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            var salida = new List<Cliente>();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
            {
                var cliente = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3));
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

    public override void Insertar(Cliente entidad)
    {
        const string consulta =
            "insert into Cliente (nombre, direccion, telefono) values (@nombre,@direccion,@telefono)";
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();
            
            peticion.Parameters.AddWithValue("@nombre", entidad.Nombre);
            peticion.Parameters.AddWithValue("@direccion", entidad.Direccion);
            peticion.Parameters.AddWithValue("@telefono", entidad.Telefono);
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
            "update Cliente set nombre = @nombre, direccion = @direccion, telefono = @telefono where id_cliente = @id";
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            peticion.Parameters.AddWithValue("@id", entidad.Id);
            peticion.Parameters.AddWithValue("@nombre", entidad.Nombre);
            peticion.Parameters.AddWithValue("@direccion", entidad.Direccion);
            peticion.Parameters.AddWithValue("@telefono", entidad.Telefono);
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
            "delete from Cliente where id_cliente = @id";

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
            Logger.Debug("Error al eliminar el cliente {Id} - {Error}", id, e.Message);
        }
    }
}