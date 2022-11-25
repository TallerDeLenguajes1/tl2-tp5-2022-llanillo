global using NLog;

namespace tp5.Util;

public static class CsvHelper
{
    private const string SeparadorCsv = ",";
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public static string VerRutaProyecto()
    {
        var envDir = Environment.CurrentDirectory;
        var pathProyecto = Directory.GetParent(envDir)?.Parent;
        return pathProyecto?.Parent != null ? pathProyecto.Parent.FullName : string.Empty;
    }

    public static List<Cadete> LeerArchivoCadete(string path)
    {
        if (!ExisteArchivo(path)) return new List<Cadete>();

        try
        {
            var salida = new List<Cadete>();
            var textoCsv = File.ReadLines(path).ToList();
            // textoCsv.RemoveAt(0); // Removemos la cabecera del excel

            foreach (var lineaSeparada in textoCsv.Select(linea => linea.Split(SeparadorCsv)))
            {
                var cadete = new Cadete
                {
                    Id = Convert.ToInt32(lineaSeparada[0]),
                    Nombre = lineaSeparada[1],
                    Direccion = lineaSeparada[2],
                    Telefono = lineaSeparada[3]
                };
                salida.Add(cadete);
            }

            return salida;
        }
        catch (Exception e)
        {
            Logger.Error(e, "Error al leer el {Path} CSV ", path);
            throw;
        }
    }

    public static List<Pedido> LeerArchivoPedido(string path)
    {
        if (!ExisteArchivo(path)) return new List<Pedido>();

        try
        {
            var salida = new List<Pedido>();
            var textoCsv = File.ReadLines(path).ToList();
            // textoCsv.RemoveAt(0); // Removemos la cabecera del excel

            foreach (var lineaSeparada in textoCsv.Select(linea => linea.Split(SeparadorCsv)))
            {
                var pedido = new Pedido(Convert.ToInt32(lineaSeparada[0]), lineaSeparada[1], lineaSeparada[2],
                    Convert.ToInt32(lineaSeparada[3]), Convert.ToInt32(lineaSeparada[4]));
                salida.Add(pedido);
            }

            return salida;
        }
        catch (Exception e)
        {
            Logger.Error(e, "Error al leer el {Path} CSV ", path);
            throw;
        }
    }

    public static void CrearArchivo<T>(string path, IEnumerable<T> entidades)
    {
        try
        {
            using var streamWriter = File.AppendText(path);

            foreach (var contenido in from entidad in entidades
                     select entidad?.ToString()?.Split(" ")
                     into arregloEntidad
                     where arregloEntidad != null
                     select string.Join(SeparadorCsv, arregloEntidad))
                streamWriter.WriteLine(contenido);

            // streamWriter.WriteLine(string.Format(x.Id + SeparadorCsv + x.Nombre + SeparadorCsv + x.Direccion +
            // SeparadorCsv + x.Telefono)));
            streamWriter.Close();
        }
        catch (Exception e)
        {
            Logger.Error(e, "Excepción al tratar de escribir en el archivo");
        }

        Logger.Debug("Se escribió correctamente en el archivo: {Path}", path);
    }

    public static void AgregarEntidad<T>(string path, T entidad)
    {
        if (!ExisteArchivo(path))
        {
            CrearArchivo(path, new List<T> { entidad });
        }
        else
        {
            using var fileStream = new FileStream(path, FileMode.Append);
            using var streamWriter = new StreamWriter(fileStream);

            var arregloEntidad = entidad?.ToString()?.Split(" ");
            if (arregloEntidad != null)
            {
                var contenido = string.Join(SeparadorCsv, arregloEntidad);
                streamWriter.WriteLine(contenido);
            }

            streamWriter.Close();
        }
    }

    public static void EliminarArchivo(string path)
    {
        if (!File.Exists(path)) return;
        File.Delete(path);
        Logger.Debug("Archibo borrado exitosamente");
    }

    private static bool ExisteArchivo(string path)
    {
        if (File.Exists(path)) return true;
        Logger.Info("El archivo: {Path} no existe, se procederá a crear", path);
        return false;
    }
}