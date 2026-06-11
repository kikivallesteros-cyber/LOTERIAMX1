# 🎲 Lotería Mexicana - LOTERIAMX1

Una aplicación completa en **C# .NET** para jugar Lotería Mexicana con soporte para múltiples jugadores, configuración personalizada, desempates avanzados y seguimiento de puntajes.

## ✨ Características Principales

### 🎮 Mecánica del Juego Mejorada
- **Tablas dinámicas**: Soporta tablas de 4x4, 5x5 y 10x10 (completamente personalizable)
- **Cartas cantadas**: Sistema de historial con navegación hacia atrás y adelante
- **Validación en tiempo real**: Verifica cartas válidas vs inválidas con alertas visuales
- **Detección de ganancias**: Múltiples formatos ganadores:
  - Línea horizontal
  - Línea vertical
  - Diagonales
  - Cruz (+ o X)
  - Cruzita (Plus)
  - Tabla llena

### 🃏 Cartas Dobles Personalizables
- Usuario elige si permite cartas repetidas en la tabla
- Generación aleatoria con o sin dobles
- Interfaz para seleccionar esta opción antes de jugar
- **Opción: Todos sin dobles** - todos los jugadores con cartas únicas
- **Opción: Permitir dobles** - misma carta puede repetirse

### 🏆 Sistema de Desempate Mejorado
- Cuando múltiples jugadores ganan simultáneamente
- Solo los ganadores pueden continuar en la siguiente ronda
- Fichas desactivadas para jugadores eliminados
- Soporte para múltiples rondas de desempate
- Ambos jugadores pueden seguir colocando fichas hasta que uno gane

### 📊 Gestión de Puntajes
- Seguimiento automático de victorias por partida
- Rankings en tiempo real
- Resumen de múltiples rondas
- Tabla de posiciones con emojis (🥇🥈🥉)
- Puntuación acumulativa entre partidas

### ⚙️ Configuración Personalizada
- **Tamaño de tabla**: 4x4, 5x5, 6x6... hasta 10x10
- **Cartas dobles**: Permitir o no cartas repetidas
- **Formas de ganar**: Seleccionar qué formatos son válidos
- **Crear tablas manualmente** o generar aleatoria
- **Juego sin dobles**: Todos los jugadores con cartas únicas
- **Botón de generación aleatoria**: Crear tablas con un clic

### 🎨 Interfaz Temática
- Diseño inspirado en la estética tradicional mexicana
- Colores: Rojo, verde, amarillo y tonos cálidos
- Fuentes elegantes (Georgia para títulos, Segoe UI para contenido)
- Botones intuitivos con iconos

## 📁 Estructura del Proyecto

```
LOTERIAMX1/
├── Domain/
│   ├── Carta.cs                    # Modelo de cartas
│   ├── Tabla.cs                    # Grid de juego dinámico
│   ├── ConfiguracionJuego.cs       # Configuración personalizable
│   ├── HistorialCartas.cs          # Navegación de cartas cantadas
│   └── Enums/
│       └── FormatoGanador.cs       # Tipos de patrones ganadores
├── Services/
│   ├── ValidacionCartas.cs         # Verificación de cartas válidas
│   ├── ServicioDesempate.cs        # Lógica de desempates
│   ├── GestorPuntajes.cs           # Seguimiento de puntajes
│   └── ServicioMultijugador.cs     # Gestión del multijugador
├── Forms/
│   ├── FormConfiguracion.cs        # Diálogo de configuración
│   ├── FormCrearTabla.cs           # Creador de tablas personalizado
│   └── (Otros formularios...)
├── LoteriaHub.cs                   # Orquestador central del juego
└── Program.cs                       # Punto de entrada
```

## 🎯 Clases Principales

### LoteriaHub
Centro de control del juego que orquesta:
- Generación y validación de tablas
- Gestión del historial de cartas
- Verificación de ganancias
- Desempates y puntajes

**Métodos clave:**
```csharp
// Configuración
ActualizarConfiguracion(ConfiguracionJuego config)
GenerarTabla()
GenerarTablaPersonalizada(Tabla tabla)

// Cartas
CantarCarta(Carta carta)
ObtenerCartasCantadas()
RetrocederHistorial() / AvanzarHistorial()
PuedoRetroceder() / PuedoAvanzar()

// Validación
EsCartaValida(int numero)
ObtenerCartasValidas(HashSet<int> cartas)
ObtenerCartasInvalidas(HashSet<int> cartas)

// Desempate
RegistrarGanador(string nombre)
HayDesempate()
ActivarFichasParaDesempate(List<string> jugadores)
EstaJugadorActivo(string nombre)

// Puntajes
ObtenerRanking()
ObtenerResumenPuntajes()
AgregarJugador(string nombre)
```

### ConfiguracionJuego
Define parámetros del juego:
- `TamañoTabla`: 4-10 (default: 5)
- `PermitirCartasDobles`: bool (default: true)
- `FormatosActivos`: List<FormatoGanador>

### ValidacionCartas
Métodos estáticos para validar:
```csharp
EsValida(int numero, HashSet<int> cantadas)
ObtenerCartasValidas(HashSet<int> colocadas, HashSet<int> cantadas)
ObtenerCartasInvalidas(HashSet<int> colocadas, HashSet<int> cantadas)
```

### ServicioDesempate
Maneja múltiples ganadores:
```csharp
HayDesempate(List<string> ganadores) → bool
ObtenerMensajeDesempate(List<string> ganadores) → string
GenerarFichasActivas(List<string> ganadores, List<string> todos) → Dict
ObtenerGanadores(List<string> todos, List<string> ganadores) → List
```

### GestorPuntajes
Seguimiento de victorias:
```csharp
AgregarJugador(string nombre)
RegistrarVictoria(string nombre)
ObtenerRanking() → List<(string, int)>
ObtenerResumen() → string
ReiniciarPuntajes()
```

### HistorialCartas
Navegación por cartas cantadas:
```csharp
AgregarCarta(Carta carta)
Retroceder() / Avanzar()
ObtenerTodas() → List<Carta>
PosicionActual, PuedoRetroceder, PuedoAvanzar
Limpiar()
```

### Tabla
Grid dinámico de cartas:
```csharp
Tabla.GenerarAleatoria(List<Carta> todas, int tamaño, bool dobles)
Tabla.Vacia(int tamaño)
ObtenerIndice(int fila, int columna) → int
```

## 🎯 Flujo de Desempate

```
1. Múltiples jugadores ganan (mismo patrón, mismo momento)
   └─> 2. LoteriaHub.RegistrarGanador() para cada uno
   └─> 3. HayDesempate() retorna true
   └─> 4. Mostrar mensaje: "Desempate! Juan, María ganaron..."
   └─> 5. ActivarFichasParaDesempate(todos los jugadores)
   └─> 6. EstaJugadorActivo(nombre) → true solo para ganadores
   └─> 7. Solo activos pueden colocar fichas
   └─> 8. Continuar ronda hasta nuevo ganador
   └─> 9. RegistrarRonda() limpia estado
```

## 🎨 Tema de Colores

| Elemento | Color | RGB |
|----------|-------|-----|
| Fondo | Amarillo cálido | 254, 243, 210 |
| Superficie | Blanco roto | 255, 255, 240 |
| Rojo primario | Rojo mexicano | 206, 17, 38 |
| Verde | Verde oscuro | 0, 104, 56 |
| Amarillo | Dorado | 240, 185, 11 |
| Texto primario | Marrón oscuro | 40, 20, 10 |
| Texto secundario | Marrón claro | 120, 80, 40 |

## 📋 Uso Básico

```csharp
// Crear el hub del juego
var cartas = CargarCartasDisponibles(); // Tu fuente de cartas
var hub = new LoteriaHub(cartas);

// Configurar el juego
var config = hub.ObtenerConfiguracion();
config.TamañoTabla = 6;
config.PermitirCartasDobles = true;
hub.ActualizarConfiguracion(config);

// Agregar jugadores
hub.AgregarJugador("Juan");
hub.AgregarJugador("María");

// Cantar cartas
var carta = cartas[0];
hub.CantarCarta(carta);

// Verificar cartas válidas
var colocadas = new HashSet<int> { 1, 5, 12 };
var validas = hub.ObtenerCartasValidas(colocadas);
var invalidas = hub.ObtenerCartasInvalidas(colocadas);

if (invalidas.Any())
{
    MessageBox.Show($"Cartas inválidas detectadas: {string.Join(", ", invalidas)}");
}

// Registrar ganador
hub.RegistrarGanador("Juan");

// Verificar desempate
if (hub.HayDesempate())
{
    var mensaje = hub.ObtenerMensajeDesempate();
    MessageBox.Show(mensaje);
    
    hub.ActivarFichasParaDesempate(new List<string> { "Juan", "María" });
}

// Obtener ranking
var ranking = hub.ObtenerRanking();
Console.WriteLine(hub.ObtenerResumenPuntajes());
```

## 🔧 Dependencias

- **.NET 6.0+**
- **Windows Forms** (incluido en .NET)

## 📦 Compilación y Ejecución

```bash
# Restaurar dependencias
dotnet restore

# Compilar
dotnet build

# Ejecutar
dotnet run
```

## 📝 Enumeraciones

### FormatoGanador
```csharp
public enum FormatoGanador
{
    LineaHorizontal,
    LineaVertical,
    Diagonal,
    DiagonalInvertida,
    Cruz,
    Cruzita,
    TablaLlena
}
```

## ✅ Validación de Entrada

- **Tamaño de tabla**: 4-10 (rango permitido)
- **Cartas dobles**: Verificadas en generación de tabla
- **Jugadores**: No duplicados en lista
- **Cartas colocadas**: Validadas contra cantadas
- **Alertas visuales**: Notificación de cartas inválidas

## 🚀 Características Nuevas en v2.0

✅ Tamaño dinámico de tabla (4x4, 5x5, 10x10)
✅ Cartas dobles personalizables
✅ Sistema de desempate mejorado
✅ Múltiples formatos ganadores
✅ Validación en tiempo real con alertas
✅ Navegación de historial (atrás/adelante)
✅ Gestión de puntajes acumulativa
✅ Interfaz temática mexicana
✅ Multijugador mejorado
✅ Tablas personalizadas
✅ Generación aleatoria de tablas
✅ Sistema de puntuación por ronda

## 📚 Documentación Adicional

Cada clase contiene comentarios XML (`///`) con descripción de métodos públicos.

## 👨‍💻 Autor

Creado para Lotería Mexicana - Sistema de Juego Digital Mejorado

---

**Versión**: 2.0.0 (Mejorada)
**Última actualización**: 2026-06-11
**Estado**: ✅ Completo con todas las características solicitadas