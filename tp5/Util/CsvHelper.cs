using NLog;
using tp5.Models;

namespace tp5.Util;

public class CsvHelper
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    private const string SeparadorLinea = ",";

    public static List<Cadete> LeerCsv(string path)
    {
        if (!File.Exists(path))
        {
            Logger.Error("El CSV que se desea leer no existe");
            throw new FileNotFoundException(nameof(path));
        }

        try
        {
            var cadetes = new List<Cadete>();
            var textoCsv = File.ReadLines(path).ToList();
            textoCsv.RemoveAt(0); // Removemos la cabecera del excel

            foreach (var lineaSeparada in textoCsv.Select(linea => linea.Split(SeparadorLinea)))
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

    public static void EscribirCsv(string path, Cadete cadete)
    {
        using var fileStream = new FileStream(path, FileMode.Append);
        using var streamWriter = new StreamWriter(fileStream);

        var contenido = string.Format(cadete.Id + SeparadorLinea + cadete.Nombre + SeparadorLinea + cadete.Direccion +
                                      SeparadorLinea + cadete.Telefono);
        streamWriter.WriteLine(contenido);
        streamWriter.Close();
    }
}