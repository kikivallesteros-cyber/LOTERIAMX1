namespace LOTERIAMX1.Services;

/// <summary>
/// Servicio para validar cartas colocadas contra cartas cantadas.
/// </summary>
public class ValidacionCartas
{
    /// <summary>
    /// Valida si una carta colocada en la tabla es válida (ha sido cantada).
    /// </summary>
    public static bool EsValida(int numeroCarta, HashSet<int> cartasCantadas)
    {
        return cartasCantadas.Contains(numeroCarta);
    }

    /// <summary>
    /// Obtiene todas las cartas inválidas que el jugador ha colocado.
    /// </summary>
    public static List<int> ObtenerCartasInvalidas(HashSet<int> cartasColocadas, HashSet<int> cartasCantadas)
    {
        return cartasColocadas
            .Where(numero => !cartasCantadas.Contains(numero))
            .OrderBy(x => x)
            .ToList();
    }

    /// <summary>
    /// Obtiene todas las cartas válidas que el jugador ha colocado.
    /// </summary>
    public static List<int> ObtenerCartasValidas(HashSet<int> cartasColocadas, HashSet<int> cartasCantadas)
    {
        return cartasColocadas
            .Where(numero => cartasCantadas.Contains(numero))
            .OrderBy(x => x)
            .ToList();
    }
}
