namespace tp5.Repositories;

public class ClienteRepositorio : Repositorio<Cliente>
{
    public ClienteRepositorio(IConfiguration configuration) : base(configuration)
    {
    }

    public override Cliente? BuscarPorId(int id)
    {
        const string consulta = "select * from cliente C where C.id = id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            var salida = new Cliente();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
                salida = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al buscar el cliente: " + e.Message);
        }

        return null;
    }

    public override IEnumerable<Cliente> BuscarTodos()
    {
        const string consulta = "select * from cliente C";

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
                    reader.GetInt32(3));
                salida.Add(cliente);
            }

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al buscar todos los clientes: " + e.Message);
        }

        return null;
    }

    public override void Insertar(Cliente entidad)
    {
        const string consulta =
            "insert into cliente (nombre, direccion, telefono) values (@nombre, @direccion, @telefono)";
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@nombre", entidad.Nombre);
            peticion.Parameters.AddWithValue("@direccion", entidad.Direccion);
            peticion.Parameters.AddWithValue("@telefono", entidad.Telefono);
            peticion.ExecuteReader();
            conexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al insertar el cliente: " + e.Message);
        }
    }

    public override void Actualizar(Cliente entidad)
    {
        throw new NotImplementedException();
    }

    public override void Eliminar(int id)
    {
        const string consulta =
            "delete from cliente C where C.id_cadete = @id";

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
            Console.WriteLine("Error al eliminar el cliente: " + e.Message);
        }
    }
}