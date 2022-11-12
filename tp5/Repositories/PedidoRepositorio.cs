namespace tp5.Repositories;

public class PedidoRepositorio : Repositorio<Pedido>
{
    public PedidoRepositorio(IConfiguration configuration) : base(configuration)
    {
    }

    public override Pedido? BuscarPorId(int id)
    {
        const string consulta = "select * from pedido P where P.id = id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            var salida = new Pedido();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
                salida = new Pedido
                {
                    Id = reader.GetInt32(0),
                    Observacion = reader.GetString(1),
                    Estado = reader.GetString(2),
                    Cliente = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                    Cadete = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                };

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al buscar el pedido: " + e.Message);
        }

        return null;
    }

    public override IEnumerable<Pedido> BuscarTodos()
    {
        const string consulta = "select * from pedido P";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            var salida = new List<Pedido>();
            using var reader = peticion.ExecuteReader();
            while (reader.Read())
            {
                var pedido = new Pedido
                {
                    Id = reader.GetInt32(0),
                    Observacion = reader.GetString(1),
                    Estado = reader.GetString(2),
                    Cliente = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                    Cadete = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                };
                salida.Add(pedido);
            }

            conexion.Close();
            return salida;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al buscar todos los pedidos: " + e.Message);
        }

        return new List<Pedido>();
    }

    public override void Insertar(Pedido entidad)
    {
        const string consulta =
            "insert into pedido (observacion, estado, cliente, cadete) values (@observacion, @estado, @cliente, @cadete)";
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            peticion.Parameters.AddWithValue("@observacion", entidad.Observacion);
            peticion.Parameters.AddWithValue("@estado", entidad.Estado);
            peticion.Parameters.AddWithValue("@cliente", entidad.Cliente);
            peticion.Parameters.AddWithValue("@cadete", entidad.Cadete);
            peticion.ExecuteReader();
            conexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al insertar el pedido: " + e.Message);
        }
    }

    public override void Actualizar(Pedido entidad)
    {
        const string consulta =
            $"update pedido SET observacion = @observacion, estado = @estado, id_cadete = @cadete, id_cliente = @cliente WHERE id = @id";
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            peticion.Parameters.AddWithValue("@id", entidad.Id);
            peticion.Parameters.AddWithValue("@obs", entidad.Observacion);
            peticion.Parameters.AddWithValue("@estado", entidad.Estado);
            peticion.Parameters.AddWithValue("@idCadete", entidad.Cadete);
            peticion.Parameters.AddWithValue("@idCliente", entidad.Cliente);
            peticion.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
        }
    }

    public override void Eliminar(int id)
    {
        const string consulta =
            "delete from pedido P where P.id = @id";

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
            Console.WriteLine("Error al eliminar el pedido: " + e.Message);
        }
    }
}