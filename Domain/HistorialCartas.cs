namespace LOTERIAMX1.Domain;

/// <summary>
/// Gestiona el historial de cartas cantadas permitiendo navegar hacia atrás y adelante.
/// </summary>
public class HistorialCartas
{
    private readonly List<Carta> _cartas = new();
    private int _indiceActual = -1;

    /// <summary>
    /// Carta actualmente mostrada en el historial.
    /// </summary>
    public Carta? CartaActual => _indiceActual >= 0 && _indiceActual < _cartas.Count ? _cartas[_indiceActual] : null;

    /// <summary>
    /// Indica si se puede retroceder en el historial.
    /// </summary>
    public bool PuedoRetroceder => _indiceActual > 0;

    /// <summary>
    /// Indica si se puede avanzar en el historial.
    /// </summary>
    public bool PuedoAvanzar => _indiceActual < _cartas.Count - 1;

    /// <summary>
    /// Total de cartas en el historial.
    /// </summary>
    public int Total => _cartas.Count;

    /// <summary>
    /// Posición actual en el historial (1-basada).
    /// </summary>
    public int PosicionActual => _indiceActual + 1;

    /// <summary>
    /// Agrega una nueva carta al historial.
    /// </summary>
    public void AgregarCarta(Carta carta)
    {
        if (carta == null)
            throw new ArgumentNullException(nameof(carta));

        // Si estamos en el medio del historial, eliminar todo lo que viene después
        if (_indiceActual >= 0 && _indiceActual < _cartas.Count - 1)
            _cartas.RemoveRange(_indiceActual + 1, _cartas.Count - _indiceActual - 1);

        _cartas.Add(carta);
        _indiceActual = _cartas.Count - 1;
    }

    /// <summary>
    /// Retrocede una posición en el historial.
    /// </summary>
    public Carta? Retroceder()
    {
        if (PuedoRetroceder)
        {
            _indiceActual--;
            return CartaActual;
        }
        return null;
    }

    /// <summary>
    /// Avanza una posición en el historial.
    /// </summary>
    public Carta? Avanzar()
    {
        if (PuedoAvanzar)
        {
            _indiceActual++;
            return CartaActual;
        }
        return null;
    }

    /// <summary>
    /// Obtiene todas las cartas del historial.
    /// </summary>
    public List<Carta> ObtenerTodas() => new(_cartas);

    /// <summary>
    /// Limpia el historial.
    /// </summary>
    public void Limpiar()
    {
        _cartas.Clear();
        _indiceActual = -1;
    }
}
