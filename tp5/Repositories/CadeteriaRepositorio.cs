namespace tp5.Repositories;

public class CadeteriaRepositorio : Repositorio<Cadeteria>
{
    public CadeteriaRepositorio(IConfiguration configuration) : base(configuration)
    {
    }

    public override Cadeteria? BuscarPorId(int id)
    {
        const string consulta = "select * from cadeteria C where C.id = id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            var salida = new Cadeteria();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
                salida = new Cadeteria(reader.GetInt32(0), reader.GetString(1));

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al buscar la cadetería: " + e.Message);
        }

        return null;
    }

    public override IEnumerable<Cadeteria>? BuscarTodos()
    {
        const string consulta = "select * from cadeteria C";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            var salida = new List<Cadeteria>();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
            {
                var cafeteria = new Cadeteria(reader.GetInt32(0), reader.GetString(1));
                salida.Add(cafeteria);
            }

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al buscar todos las cadeterías: " + e.Message);
        }

        return null;
    }

    public override void Insertar(Cadeteria entidad)
    {
        const string consulta =
            "insert into cadeteria (nombre) values (@nombre)";
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@nombre", entidad.Nombre);
            peticion.ExecuteReader();
            conexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al insertarla cadetería: " + e.Message);
        }
    }

    public override void Actualizar(Cadeteria entidad)
    {
        throw new NotImplementedException();
    }

    public override void Eliminar(int id)
    {
        const string consulta =
            "delete from cadeteria C where C.id = @id";

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
            Console.WriteLine("Error al eliminar la cadetería:" + e.Message);
        }
    }
}