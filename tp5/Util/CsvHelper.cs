global using NLog;

namespace tp5.Util;

public static class CsvHelper
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private const string SeparadorCsv = ",";

    public static string VerRutaProyecto()
    {
        var envDir = Environment.CurrentDirectory;
        DirectoryInfo? pathProyecto = Directory.GetParent(envDir)?.Parent;
        return pathProyecto?.Parent != null ? pathProyecto.Parent.FullName : string.Empty;
    }

    public static List<Cadete> LeerCsvCadetes(string path)
    {
        if (!ExisteArchivo(path)) return new List<Cadete>();

        try
        {
            var cadetes = new List<Cadete>();
            var textoCsv = File.ReadLines(path).ToList();
            // textoCsv.RemoveAt(0); // Removemos la cabecera del excel

            foreach (var lineaSeparada in textoCsv.Select(linea => linea.Split(SeparadorCsv)))
            {
                var cadete = new Cadete
                {
                    Id = Convert.ToInt32(lineaSeparada[0]),
                    Nombre = lineaSeparada[1],
                    Direccion = lineaSeparada[2],
                    Telefono = Convert.ToInt32(lineaSeparada[3])
                };
                cadetes.Add(cadete);
            }

            return cadetes;
        }
        catch (Exception e)
        {
            Logger.Error(e, "Error al leer el {Path} CSV ", path);
            throw;
        }
    }

    public static void CrearCadetesCsv(string path, List<Cadete> cadete)
    {
        try
        {
            using var streamWriter = File.AppendText(path);

            cadete.ForEach(x =>
                streamWriter.WriteLine(string.Format(x.Id + SeparadorCsv + x.Nombre + SeparadorCsv + x.Direccion +
                                                     SeparadorCsv + x.Telefono)));
            streamWriter.Close();
        }
        catch (Exception e)
        {
            Logger.Error(e, "Excepción al tratar de escribir en el archivo");
        }

        Logger.Debug("Se escribió correctamente en el archivo: {Path}", path);
    }

    public static void AgregarCadeteCsv(string path, Cadete cadete)
    {
        if (!ExisteArchivo(path))
        {
            CrearCadetesCsv(path, new List<Cadete> { cadete });
        }
        else
        {
            using var fileStream = new FileStream(path, FileMode.Append);
            using var streamWriter = new StreamWriter(fileStream);

            var contenido = string.Format(cadete.Id + SeparadorCsv + cadete.Nombre + SeparadorCsv + cadete.Direccion +
                                          SeparadorCsv + cadete.Telefono);
            streamWriter.WriteLine(contenido);
            streamWriter.Close();
        }
    }

    public static void EliminarCsv(string path)
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