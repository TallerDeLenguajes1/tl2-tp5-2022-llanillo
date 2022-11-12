namespace tp5.Repositories;

public class CadeteRepositorio : Repositorio<Cadete>
{
    public CadeteRepositorio(string cadenaConexion) : base(cadenaConexion)
    {
    }

    public override Cadete BuscarPorId(int id)
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
                salida = new Cadete(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));

            conexion.Close();
            return salida;
        }
        catch
        {
            Console.WriteLine("Error al buscar el cadete: " + id);
        }

        return new Cadete();
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
                var cadete = new Cadete(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetInt32(3));
                salida.Add(cadete);
            }

            conexion.Close();
            return salida;
        }
        catch
        {
            Console.WriteLine("Error al buscar todos los cadetes");
        }

        return new List<Cadete>();
    }

    public override void Insertar(Cadete entidad)
    {
        const string consulta =
            "insert into cadete (nombre, direccion, telefono) values (@nombre, @direccion, @telefono)";
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
        catch
        {
            Console.WriteLine("Error al insertar el cadete: " + entidad.Nombre);
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
        catch
        {
            Console.WriteLine("Error al eliminar el cadete: " + id);
        }
    }
}