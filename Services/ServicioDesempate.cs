namespace LOTERIAMX1.Services;

/// <summary>
/// Servicio para manejar desempates cuando múltiples jugadores ganan simultáneamente.
/// </summary>
public class ServicioDesempate
{
    /// <summary>
    /// Filtra los jugadores que ganaron en esta ronda.
    /// </summary>
    public static List<string> ObtenerGanadores(List<string> todosLosJugadores,
                                                   List<string> ganadoresDeEstRonda)
    {
        if (todosLosJugadores == null || ganadoresDeEstRonda == null)
            throw new ArgumentNullException();

        return todosLosJugadores
            .Where(j => ganadoresDeEstRonda.Contains(j))
            .ToList();
    }

    /// <summary>
    /// Desactiva fichas para jugadores que no ganaron en desempate.
    /// </summary>
    public static Dictionary<string, bool> GenerarFichasActivas(List<string> ganadores,
                                                                  List<string> todosJugadores)
    {
        var fichasActivas = new Dictionary<string, bool>();

        foreach (var jugador in todosJugadores ?? new List<string>())
        {
            fichasActivas[jugador] = (ganadores ?? new List<string>()).Contains(jugador);
        }

        return fichasActivas;
    }

    /// <summary>
    /// Verifica si hay desempate (más de un ganador).
    /// </summary>
    public static bool HayDesempate(List<string> ganadores)
    {
        return ganadores != null && ganadores.Count > 1;
    }

    /// <summary>
    /// Obtiene el mensaje de desempate formateado.
    /// </summary>
    public static string ObtenerMensajeDesempate(List<string> ganadores)
    {
        if (ganadores == null || ganadores.Count == 0)
            return "No hay ganadores en esta ronda.";

        if (ganadores.Count == 1)
            return $"¡{ganadores[0]} ganó! 🎉";

        var nombresFormateados = string.Join(", ", ganadores);
        return $"¡Desempate! {nombresFormateados} ganaron al mismo tiempo.\n" +
               "Solo ustedes pueden continuar jugando en la siguiente ronda.";
    }

    /// <summary>
    /// Verifica si un jugador específico está activo (ganador en desempate).
    /// </summary>
    public static bool EstaActivo(string nombreJugador, Dictionary<string, bool> fichasActivas)
    {
        return fichasActivas != null && fichasActivas.ContainsKey(nombreJugador) && fichasActivas[nombreJugador];
    }
}
