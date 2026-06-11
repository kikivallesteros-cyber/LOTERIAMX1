namespace LOTERIAMX1.Domain;

/// <summary>
/// Representa un tablero de lotería como una matriz de cartas.
/// Soporta tamaños dinámicos de 4x4 hasta 10x10.
/// </summary>
public class Tabla
{
    /// <summary>
    /// Número de filas del tablero.
    /// </summary>
    public int Filas { get; }

    /// <summary>
    /// Número de columnas del tablero.
    /// </summary>
    public int Columnas { get; }

    /// <summary>
    /// Total de casillas en el tablero.
    /// </summary>
    public int TotalCasillas => Filas * Columnas;

    /// <summary>
    /// Matriz de cartas que componen el tablero.
    /// </summary>
    public Carta[,] Casillas { get; }

    private Tabla(Carta[,] casillas)
    {
        Casillas = casillas ?? throw new ArgumentNullException(nameof(casillas));
        Filas = casillas.GetLength(0);
        Columnas = casillas.GetLength(1);
    }

    /// <summary>
    /// Crea un tablero vacío del tamaño especificado.
    /// </summary>
    public static Tabla Vacia(int tamaño = 5)
    {
        if (tamaño < 4 || tamaño > 10)
            throw new ArgumentException("El tamaño debe estar entre 4 y 10.");
        return new Tabla(new Carta[tamaño, tamaño]);
    }

    /// <summary>
    /// Genera un tablero aleatorio.
    /// </summary>
    /// <param name="todas">Colección de todas las cartas disponibles.</param>
    /// <param name="tamaño">Tamaño del tablero (4-10).</param>
    /// <param name="permitirDobles">Si true, permite cartas repetidas.</param>
    public static Tabla GenerarAleatoria(IEnumerable<Carta> todas, int tamaño = 5, bool permitirDobles = true)
    {
        if (tamaño < 4 || tamaño > 10)
            throw new ArgumentException("El tamaño debe estar entre 4 y 10.");

        int totalCasillas = tamaño * tamaño;
        var cartasDisponibles = todas.ToList();

        if (cartasDisponibles.Count < totalCasillas && !permitirDobles)
            throw new InvalidOperationException(
                $"No hay suficientes cartas únicas para una tabla de {tamaño}x{tamaño}. "
                + $"Se necesitan {totalCasillas} pero solo hay {cartasDisponibles.Count}.");

        List<Carta> seleccion;

        if (permitirDobles)
        {
            // Con dobles: seleccionar aleatorio, puede haber duplicadas
            seleccion = new List<Carta>();
            var random = new Random();

            for (int i = 0; i < totalCasillas; i++)
            {
                var carta = cartasDisponibles[random.Next(cartasDisponibles.Count)];
                seleccion.Add(carta);
            }
        }
        else
        {
            // Sin dobles: todas las cartas son únicas
            seleccion = cartasDisponibles
                .OrderBy(_ => Random.Shared.Next())
                .Take(totalCasillas)
                .ToList();
        }

        // Barajar la selección final
        seleccion = seleccion.OrderBy(_ => Random.Shared.Next()).ToList();

        var casillas = new Carta[tamaño, tamaño];
        for (int i = 0; i < totalCasillas; i++)
            casillas[i / tamaño, i % tamaño] = seleccion[i];

        return new Tabla(casillas);
    }

    /// <summary>
    /// Obtiene el índice lineal de una posición (fila, columna).
    /// </summary>
    public int ObtenerIndice(int fila, int columna)
    {
        if (fila < 0 || fila >= Filas || columna < 0 || columna >= Columnas)
            throw new ArgumentOutOfRangeException("Posición fuera de rango.");
        return fila * Columnas + columna;
    }
}
