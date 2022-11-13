﻿namespace tp5.Repositories;

public class RepositorioPedido : Repositorio<Pedido>, IRepositorioPedido
{
    public RepositorioPedido(IConfiguration configuration) : base(configuration)
    {
    }

    public override Pedido? BuscarPorId(int id)
    {
        const string consulta = "select * from pedido P where P.id = @id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@id", id);
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
            Logger.Debug("Error al buscar el pedido {Id} - {Error}", id, e.Message);
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
            Logger.Debug("Error al buscar todos los pedidos - {Error}", e.Message);
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
            Logger.Debug("Error al insertar el pedido {Id} - {Error}", entidad.Id, e.Message);
        }
    }

    public override void Actualizar(Pedido entidad)
    {
        const string consulta =
            "update pedido set observacion = @observacion, estado = @estado, id_cadete = @cadete, id_cliente = @cliente where id = @id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();

            peticion.Parameters.AddWithValue("@id", entidad.Id);
            peticion.Parameters.AddWithValue("@observacion", entidad.Observacion);
            peticion.Parameters.AddWithValue("@estado", entidad.Estado);
            peticion.Parameters.AddWithValue("@cadete", entidad.Cadete);
            peticion.Parameters.AddWithValue("@cliente", entidad.Cliente);
            peticion.ExecuteNonQuery();
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
            Logger.Debug("Error al eliminar el pedido {Id} - {Error}", id, e.Message);
        }
    }

    public IEnumerable<Pedido> BuscarTodosPorCliente(int id)
    {
        const string consulta = "select * from pedido P where P.id_cliente = @id";
        return BuscarTodosPorEntidad(id, consulta);
    }

    public IEnumerable<Pedido> BuscarTodosPorCadete(int id)
    {
        const string consulta = "select * from pedido P where P.id_cadete = @id";
        return BuscarTodosPorEntidad(id, consulta);
    }

    private IEnumerable<Pedido> BuscarTodosPorEntidad(int id, string consulta)
    {
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@id", id);
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
            Logger.Debug("Error al buscar todos los pedidos del cliente {Id}- {Error}", id, e.Message);
        }

        return new List<Pedido>();
    }
}