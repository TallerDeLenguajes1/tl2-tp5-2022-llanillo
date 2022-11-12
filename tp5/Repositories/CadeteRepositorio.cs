namespace tp5.Repositories;

public class CadeteRepositorio : Repositorio<Cadete>
{
    public CadeteRepositorio(IConfiguration configuration) : base(configuration)
    {
    }

    public override Cadete? BuscarPorId(int id)
    {
        const string consulta = "select * from cadete C where C.id = id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            var salida = new Cadete();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
                salida = new Cadete
                {
                    Id = reader.GetInt32(0), Nombre = reader.GetString(1),
                    Direccion = reader.GetString(2),
                    Telefono = reader.GetInt32(3),
                    Cadeteria = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                };
            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al buscar el cadete: " + e.Message);
        }

        return null;
    }

    public override IEnumerable<Cadete> BuscarTodos()
    {
        const string consulta = "select * from cadete C";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            var salida = new List<Cadete>();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
            {
                var cadete = new Cadete
                {
                    Id = reader.GetInt32(0), Nombre = reader.GetString(1),
                    Direccion = reader.GetString(2),
                    Telefono = reader.GetInt32(3),
                    Cadeteria = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                };
                salida.Add(cadete);
            }

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al buscar todos los cadetes: " + e.Message);
        }

        return new List<Cadete>();
    }

    public override void Insertar(Cadete entidad)
    {
        const string consulta =
            "insert into cadete (nombre, direccion, telefono, id_cadeteria) values (@nombre, @direccion, @telefono, @cadeteria)";
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@nombre", entidad.Nombre);
            peticion.Parameters.AddWithValue("@direccion", entidad.Direccion);
            peticion.Parameters.AddWithValue("@telefono", entidad.Telefono);
            peticion.Parameters.AddWithValue("@cadeteria", entidad.Cadeteria);
            peticion.ExecuteReader();
            conexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al insertar el cadete: " + e.Message);
        }
    }

    public override void Actualizar(Cadete entidad)
    {
        throw new NotImplementedException();
    }

    public override void Eliminar(int id)
    {
        const string consulta =
            "delete from cadete C where C.id_cadete = @id";

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
            Console.WriteLine("Error al eliminar el cadete: " + e.Message);
        }
    }
}