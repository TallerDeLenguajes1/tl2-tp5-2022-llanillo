namespace tp5.Repositories;

public class RepositorioCadete : Repositorio<Cadete>
{
    public RepositorioCadete(IConfiguration configuration) : base(configuration)
    {
    }

    public override Cadete? BuscarPorId(int id)
    {
        // const string consulta = "select * from Usuario where id_usuario = @id and rol = @rol";
        const string consulta = "select * from Usuario where id_usuario = @id";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@id", id);
            peticion.Parameters.AddWithValue("@rol", Rol.Cadete);
            conexion.Open();

            Cadete salida = null; 
            using var reader = peticion.ExecuteReader();

            while (reader.Read())
                salida = new Cadete()
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    NombreUsuario = reader.GetString(2),
                    Rol = Rol.Cadete,
                    Direccion = reader.GetString(5),
                    Telefono = reader.GetString(6)
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
        const string consulta = "select * from Usuario where rol = @rol";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@rol", Rol.Cadete);
            conexion.Open();

            var salida = new List<Cadete>();
            using var reader = peticion.ExecuteReader();

            while (reader.Read())
            {
                var cadete = new Cadete
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Direccion = reader.GetString(5),
                    Telefono = reader.GetString(6),
                    Rol = Rol.Cadete
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

    public override IEnumerable<Cadete> BuscarTodosPorId(int id)
    {
        throw new NotImplementedException();
    }

    public override void Insertar(Cadete entidad)
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
            peticion.Parameters.AddWithValue("@direccion", entidad.Direccion);
            peticion.Parameters.AddWithValue("@telefono", entidad.Telefono);
            peticion.ExecuteNonQuery();
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
            peticion.Parameters.AddWithValue("@rol", Rol.Cadete);
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
            "delete from Usuario where id_usuario = @id and rol = @rol";

        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            peticion.Parameters.AddWithValue("@id", id);
            peticion.Parameters.AddWithValue("@rol", Rol.Cadete);
            peticion.ExecuteReader();
            conexion.Close();
        }
        catch (Exception e)
        {
            Logger.Debug("Error al eliminar el cadete {Id} - {Error}", id, e.Message);
        }
    }
}