namespace LOTERIAMX1.Domain;

using LOTERIAMX1.Domain.Enums;

/// <summary>
/// Configuración personalizable del juego.
/// </summary>
public class ConfiguracionJuego
{
    /// <summary>
    /// Tamaño de la tabla (4, 5, 6, ... 10).
    /// </summary>
    public int TamañoTabla { get; set; } = 5;

    /// <summary>
    /// Si true, permite cartas dobles en la tabla.
    /// </summary>
    public bool PermitirCartasDobles { get; set; } = true;

    /// <summary>
    /// Formatos de victoria activos en este juego.
    /// </summary>
    public List<FormatoGanador> FormatosActivos { get; set; } = new()
    {
        FormatoGanador.LineaHorizontal,
        FormatoGanador.LineaVertical,
        FormatoGanador.Diagonal,
        FormatoGanador.DiagonalInvertida,
        FormatoGanador.Cruz,
        FormatoGanador.Cruzita,
        FormatoGanador.TablaLlena
    };

    public ConfiguracionJuego() { }

    public ConfiguracionJuego(int tamaño, bool dobles, List<FormatoGanador> formatos)
    {
        ValidarTamaño(tamaño);
        TamañoTabla = tamaño;
        PermitirCartasDobles = dobles;
        FormatosActivos = formatos ?? new List<FormatoGanador>();
    }

    private static void ValidarTamaño(int tamaño)
    {
        if (tamaño < 4 || tamaño > 10)
            throw new ArgumentException("El tamaño de la tabla debe estar entre 4 y 10.");
    }

    public string ObtenerResumen()
    {
        return $"Tabla {TamañoTabla}x{TamañoTabla} | Dobles: {(PermitirCartasDobles ? "Sí" : "No")} | Formas: {FormatosActivos.Count}";
    }
}
