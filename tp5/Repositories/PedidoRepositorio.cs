namespace tp5.Repositories;

public class PedidoRepositorio : Repositorio<Pedido>
{
    public PedidoRepositorio(string cadenaConexion) : base(cadenaConexion)
    {
    }

    public override Pedido BuscarPorId(int id)
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
                salida = new Pedido(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));

            conexion.Close();
            return salida;
        }
        catch
        {
            Console.WriteLine("Error al buscar el pedido: " + id);
        }

        return new Pedido();
    }

    public override IEnumerable<Pedido> BuscarTodos()
    {
        throw new NotImplementedException();
    }

    public override void Insertar(Pedido entidad)
    {
        throw new NotImplementedException();
    }

    public override void Actualizar(Pedido entidad)
    {
        throw new NotImplementedException();
    }

    public override void Eliminar(int id)
    {
        throw new NotImplementedException();
    }
}