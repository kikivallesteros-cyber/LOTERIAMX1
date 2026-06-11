namespace LOTERIAMX1.Domain.Enums;

/// <summary>
/// Enumeración de formatos de victoria posibles en la lotería.
/// </summary>
public enum FormatoGanador
{
    /// <summary>Completar una línea horizontal.</summary>
    LineaHorizontal,

    /// <summary>Completar una línea vertical.</summary>
    LineaVertical,

    /// <summary>Completar la diagonal principal (arriba-izq a abajo-der).</summary>
    Diagonal,

    /// <summary>Completar la diagonal inversa (arriba-der a abajo-izq).</summary>
    DiagonalInvertida,

    /// <summary>Completar una cruz (+ todas las líneas centrales).</summary>
    Cruz,

    /// <summary>Completar una cruzita (+ solo los extremos centrales).</summary>
    Cruzita,

    /// <summary>Completar todas las casillas del tablero.</summary>
    TablaLlena
}
