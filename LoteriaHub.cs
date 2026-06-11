namespace LOTERIAMX1;

using LOTERIAMX1.Domain;
using LOTERIAMX1.Domain.Enums;
using LOTERIAMX1.Services;

/// <summary>
/// Hub central que orquesta toda la lógica del juego de Lotería.
/// Gestiona tablas, cartas, validación, desempates y puntajes.
/// </summary>
public class LoteriaHub
{
    private Tabla _tablaActual;
    private HistorialCartas _historial;
    private ConfiguracionJuego _config;
    private GestorPuntajes _puntajes;
    private ServicioDesempate _desempate;
    private ValidacionCartas _validacion;
    private List<Carta> _cartasDisponibles;
    private HashSet<int> _cartasCantadas;
    private Dictionary<string, bool> _fichasActivas;
    private List<string> _ganadoresActuales;

    public LoteriaHub(List<Carta> cartasDisponibles)
    {
        _cartasDisponibles = cartasDisponibles ?? throw new ArgumentNullException(nameof(cartasDisponibles));
        _historial = new HistorialCartas();
        _config = new ConfiguracionJuego();
        _puntajes = new GestorPuntajes();
        _desempate = new ServicioDesempate();
        _validacion = new ValidacionCartas();
        _cartasCantadas = new HashSet<int>();
        _fichasActivas = new Dictionary<string, bool>();
        _ganadoresActuales = new List<string>();

        GenerarTabla();
    }

    // ============ Configuración ============
    /// <summary>Obtiene la configuración actual del juego.</summary>
    public ConfiguracionJuego ObtenerConfiguracion() => _config;

    /// <summary>Actualiza la configuración del juego.</summary>
    public void ActualizarConfiguracion(ConfiguracionJuego nuevaConfig)
    {
        _config = nuevaConfig ?? throw new ArgumentNullException(nameof(nuevaConfig));
        GenerarTabla();
    }

    // ============ Tabla y Generación ============
    /// <summary>Obtiene la tabla actual.</summary>
    public Tabla ObtenerTabla() => _tablaActual;

    /// <summary>Genera una nueva tabla aleatoria con la configuración actual.</summary>
    public void GenerarTabla()
    {
        _tablaActual = Tabla.GenerarAleatoria(_cartasDisponibles, _config.TamañoTabla, _config.PermitirCartasDobles);
        _cartasCantadas.Clear();
        _historial.Limpiar();
        _ganadoresActuales.Clear();
    }

    /// <summary>Utiliza una tabla personalizada del usuario.</summary>
    public void GenerarTablaPersonalizada(Tabla tabla)
    {
        if (tabla == null)
            throw new ArgumentNullException(nameof(tabla));

        _tablaActual = tabla;
        _cartasCantadas.Clear();
        _historial.Limpiar();
        _ganadoresActuales.Clear();
    }

    // ============ Cartas Cantadas ============
    /// <summary>Canta una carta (la agrega al historial y marcas).</summary>
    public void CantarCarta(Carta carta)
    {
        if (carta == null)
            throw new ArgumentNullException(nameof(carta));

        if (!_cartasCantadas.Contains(carta.Numero))
        {
            _cartasCantadas.Add(carta.Numero);
            _historial.AgregarCarta(carta);
        }
    }

    /// <summary>Obtiene el conjunto de cartas cantadas.</summary>
    public HashSet<int> ObtenerCartasCantadas() => new(_cartasCantadas);

    /// <summary>Obtiene la carta actualmente mostrada en el historial.</summary>
    public Carta? ObtenerCartaActual() => _historial.CartaActual;

    /// <summary>Verifica si se puede retroceder en el historial.</summary>
    public bool PuedoRetroceder() => _historial.PuedoRetroceder;

    /// <summary>Verifica si se puede avanzar en el historial.</summary>
    public bool PuedoAvanzar() => _historial.PuedoAvanzar;

    /// <summary>Retrocede una posición en el historial.</summary>
    public Carta? RetrocederHistorial()
    {
        return _historial.Retroceder();
    }

    /// <summary>Avanza una posición en el historial.</summary>
    public Carta? AvanzarHistorial()
    {
        return _historial.Avanzar();
    }

    /// <summary>Obtiene la posición actual en el historial.</summary>
    public int ObtenerPosicionHistorial() => _historial.PosicionActual;

    /// <summary>Obtiene el total de cartas cantadas.</summary>
    public int ObtenerTotalCartasCantadas() => _historial.Total;

    // ============ Validación de Cartas ============
    /// <summary>Obtiene las cartas inválidas del conjunto colocado.</summary>
    public List<int> ObtenerCartasInvalidas(HashSet<int> cartasColocadas)
    {
        return ValidacionCartas.ObtenerCartasInvalidas(cartasColocadas, _cartasCantadas);
    }

    /// <summary>Obtiene las cartas válidas del conjunto colocado.</summary>
    public List<int> ObtenerCartasValidas(HashSet<int> cartasColocadas)
    {
        return ValidacionCartas.ObtenerCartasValidas(cartasColocadas, _cartasCantadas);
    }

    /// <summary>Verifica si una carta específica es válida.</summary>
    public bool EsCartaValida(int numeroCarta)
    {
        return ValidacionCartas.EsValida(numeroCarta, _cartasCantadas);
    }

    // ============ Verificación de Ganancias ============
    /// <summary>Obtiene los formatos ganadores activos.</summary>
    public List<FormatoGanador> ObtenerFormatosActivos() => new(_config.FormatosActivos);

    // ============ Desempate ============
    /// <summary>Registra un jugador como ganador de la ronda actual.</summary>
    public void RegistrarGanador(string nombreJugador)
    {
        if (string.IsNullOrWhiteSpace(nombreJugador))
            throw new ArgumentException("El nombre del jugador no puede estar vacío.");

        if (!_ganadoresActuales.Contains(nombreJugador))
            _ganadoresActuales.Add(nombreJugador);

        _puntajes.RegistrarVictoria(nombreJugador);
    }

    /// <summary>Obtiene la lista de ganadores de la ronda actual.</summary>
    public List<string> ObtenerGanadoresRonda() => new(_ganadoresActuales);

    /// <summary>Verifica si hay desempate (múltiples ganadores).</summary>
    public bool HayDesempate() => ServicioDesempate.HayDesempate(_ganadoresActuales);

    /// <summary>Obtiene el mensaje de desempate formateado.</summary>
    public string ObtenerMensajeDesempate()
    {
        return ServicioDesempate.ObtenerMensajeDesempate(_ganadoresActuales);
    }

    /// <summary>Activa fichas solo para los ganadores del desempate.</summary>
    public void ActivarFichasParaDesempate(List<string> todosLosJugadores)
    {
        _fichasActivas = ServicioDesempate.GenerarFichasActivas(_ganadoresActuales, todosLosJugadores);
    }

    /// <summary>Verifica si un jugador está activo (puede seguir jugando).</summary>
    public bool EstaJugadorActivo(string nombre)
    {
        if (_fichasActivas.Count == 0)
            return true; // Si no hay desempate, todos están activos

        return _fichasActivas.ContainsKey(nombre) && _fichasActivas[nombre];
    }

    /// <summary>Obtiene el estado de fichas activas.</summary>
    public Dictionary<string, bool> ObtenerFichasActivas() => new(_fichasActivas);

    // ============ Puntajes ============
    /// <summary>Agrega un nuevo jugador con puntaje inicial 0.</summary>
    public void AgregarJugador(string nombre)
    {
        _puntajes.AgregarJugador(nombre);
    }

    /// <summary>Obtiene el puntaje de un jugador.</summary>
    public int ObtenerPuntaje(string nombre)
    {
        return _puntajes.ObtenerPuntaje(nombre);
    }

    /// <summary>Obtiene todos los puntajes.</summary>
    public Dictionary<string, int> ObtenerTodosPuntajes()
    {
        return _puntajes.ObtenerTodosPuntajes();
    }

    /// <summary>Obtiene el ranking ordenado por puntaje descendente.</summary>
    public List<(string Nombre, int Puntaje)> ObtenerRanking()
    {
        return _puntajes.ObtenerRanking();
    }

    /// <summary>Obtiene un resumen formateado de puntajes.</summary>
    public string ObtenerResumenPuntajes()
    {
        return _puntajes.ObtenerResumen();
    }

    /// <summary>Registra el fin de una ronda y limpia el estado de ganadores.</summary>
    public void RegistrarRonda()
    {
        _puntajes.RegistrarRonda();
        _ganadoresActuales.Clear();
        _fichasActivas.Clear();
    }

    /// <summary>Reinicia todos los puntajes.</summary>
    public void ReiniciarPuntajes()
    {
        _puntajes.ReiniciarPuntajes();
    }

    // ============ Información General ============
    /// <summary>Obtiene un resumen del estado actual del juego.</summary>
    public string ObtenerInfoJuego()
    {
        return $"Tabla: {_config.TamañoTabla}x{_config.TamañoTabla} | " +
               $"Cartas cantadas: {_cartasCantadas.Count} | " +
               $"Jugadores: {_puntajes.ObtenerTodosPuntajes().Count}";
    }
}
