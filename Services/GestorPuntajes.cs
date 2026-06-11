namespace LOTERIAMX1.Services;

/// <summary>
/// Gestiona los puntajes y ranking de los jugadores.
/// Permite seguimiento de victorias a lo largo de múltiples rondas.
/// </summary>
public class GestorPuntajes
{
    private Dictionary<string, int> _puntajes = new();
    private int _rondasJugadas = 0;

    public GestorPuntajes()
    {
        Inicializar();
    }

    /// <summary>
    /// Reinicia los puntajes.
    /// </summary>
    public void Inicializar()
    {
        _puntajes.Clear();
        _rondasJugadas = 0;
    }

    /// <summary>
    /// Agrega un nuevo jugador con puntaje inicial de 0.
    /// </summary>
    public void AgregarJugador(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del jugador no puede estar vacío.");
        
        if (!_puntajes.ContainsKey(nombre))
            _puntajes[nombre] = 0;
    }

    /// <summary>
    /// Registra una victoria para un jugador.
    /// </summary>
    public void RegistrarVictoria(string nombre)
    {
        if (_puntajes.ContainsKey(nombre))
            _puntajes[nombre]++;
        else
            throw new KeyNotFoundException($"El jugador '{nombre}' no está registrado.");
    }

    /// <summary>
    /// Registra que se completó una ronda.
    /// </summary>
    public void RegistrarRonda()
    {
        _rondasJugadas++;
    }

    /// <summary>
    /// Obtiene el puntaje actual de un jugador.
    /// </summary>
    public int ObtenerPuntaje(string nombre)
    {
        return _puntajes.ContainsKey(nombre) ? _puntajes[nombre] : 0;
    }

    /// <summary>
    /// Obtiene un diccionario con todos los puntajes.
    /// </summary>
    public Dictionary<string, int> ObtenerTodosPuntajes()
    {
        return new Dictionary<string, int>(_puntajes);
    }

    /// <summary>
    /// Obtiene el ranking ordenado por puntaje descendente.
    /// </summary>
    public List<(string Nombre, int Puntaje)> ObtenerRanking()
    {
        return _puntajes
            .OrderByDescending(kv => kv.Value)
            .ThenBy(kv => kv.Key) // Orden alfabético como criterio de desempate
            .Select(kv => (kv.Key, kv.Value))
            .ToList();
    }

    /// <summary>
    /// Número de rondas jugadas.
    /// </summary>
    public int RondasJugadas => _rondasJugadas;

    /// <summary>
    /// Obtiene un resumen formateado de los puntajes.
    /// </summary>
    public string ObtenerResumen()
    {
        var ranking = ObtenerRanking();
        if (ranking.Count == 0)
            return "No hay datos de puntajes.";

        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"📊 Puntajes después de {_rondasJugadas} ronda(s):\n");

        int posicion = 1;
        foreach (var (nombre, puntaje) in ranking)
        {
            string medalla = posicion switch
            {
                1 => "🥇",
                2 => "🥈",
                3 => "🥉",
                _ => $"#{posicion}"
            };
            sb.AppendLine($"{medalla} {nombre}: {puntaje} victoria{(puntaje != 1 ? "s" : "")}");
            posicion++;
        }

        return sb.ToString();
    }

    /// <summary>
    /// Reinicia todos los puntajes a cero.
    /// </summary>
    public void ReiniciarPuntajes()
    {
        _puntajes.Clear();
        _rondasJugadas = 0;
    }
}
