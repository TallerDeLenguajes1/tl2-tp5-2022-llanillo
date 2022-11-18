using System.Drawing.Printing;

namespace tp5.Repositories;

public class RepositorioCadete : Repositorio<Cadete>
{
    public RepositorioCadete(IConfiguration configuration) : base(configuration)
    {
    }

    public override Cadete? BuscarPorId(int id)
    {
        const string consulta = "select * from cadete C where C.id = @id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@id", id);
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
            Logger.Debug("Error al buscar el cadete {Id} - {Error}", id, e.Message);
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
            Logger.Debug("Error al buscar todos los cadetes - {Error}", e.Message);
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
            Logger.Debug("Error al insertar el cadete {Id} - {Error}", entidad.Id, e.Message);
        }
    }

    public override void Actualizar(Cadete entidad)
    {
        const string consulta =
            "update cadete SET nombre = @nombre, direccion = @direccion, telefono = @telefono where id = @id";
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
            Logger.Debug("Error al insertar el pedido {Id} - {Error}", entidad.Id, e.Message);
        }
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
            Logger.Debug("Error al eliminar el cadete {Id} - {Error}", id, e.Message);
        }
    }
}