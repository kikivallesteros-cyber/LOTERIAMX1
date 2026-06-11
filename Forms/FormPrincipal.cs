namespace LOTERIAMX1.Forms;

using LOTERIAMX1.Domain;

/// <summary>
Formulario principal del juego que muestra la tabla, cartas cantadas, y gestiona el flujo del juego.
/// </summary>
public partial class FormPrincipal : Form
{
    private LoteriaHub _hub;
    private Label[,] _labelsTabla;
    private Label _lblCartaActual;
    private Label _lblPosicion;
    private Label _lblGanadores;
    private Button _btnRetroceder;
    private Button _btnAvanzar;
    private Button _btnCantarCarta;
    private Button _btnValidar;
    private ListBox _lstPuntajes;

    public FormPrincipal()
    {
        InitializeComponent();
        InicializarJuego();
    }

    private void InicializarJuego()
    {
        // Cargar cartas disponibles (aquí van tus cartas)
        var cartas = CargarCartasMexicanas();
        _hub = new LoteriaHub(cartas);

        Text = "🎮 Lotería Mexicana - LOTERIAMX1";
        Width = 1200;
        Height = 800;
        BackColor = Color.FromArgb(254, 243, 210);
        StartPosition = FormStartPosition.CenterScreen;

        // Crear layout principal
        var panelPrincipal = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(10)
        };

        // Encabezado
        var panelEncabezado = new Panel
        {
            Dock = DockStyle.Top,
            Height = 60,
            BackColor = Color.FromArgb(206, 17, 38),
            Padding = new Padding(10)
        };
        var lblTitulo = new Label
        {
            Text = "🎮 Lotería Mexicana",
            Font = new Font("Georgia", 20, FontStyle.Bold),
            ForeColor = Color.White,
            AutoSize = true,
            Dock = DockStyle.Left
        };
        panelEncabezado.Controls.Add(lblTitulo);
        panelPrincipal.Controls.Add(panelEncabezado);

        // Área principal: Tabla + Lado derecho
        var panelContenido = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };

        // Tabla
        var panelTabla = CrearPanelTabla();
        panelContenido.Controls.Add(panelTabla);

        // Panel derecho: Cartas cantadas + Puntajes + Botones
        var panelDerecho = new Panel
        {
            Dock = DockStyle.Right,
            Width = 300,
            Padding = new Padding(10),
            AutoScroll = true
        };

        // Cartas cantadas
        var grpCartas = new GroupBox
        {
            Text = "📍 Cartas Cantadas",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Width = 280,
            Height = 150,
            Padding = new Padding(10)
        };

        _lblCartaActual = new Label
        {
            Text = "Esperando...",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(10, 30)
        };
        _lblPosicion = new Label
        {
            Text = "Posición: 0/0",
            Font = new Font("Segoe UI", 10),
            AutoSize = true,
            Location = new Point(10, 60)
        };
        grpCartas.Controls.Add(_lblCartaActual);
        grpCartas.Controls.Add(_lblPosicion);
        panelDerecho.Controls.Add(grpCartas);

        // Botones de control
        var panelBotonesControl = new Panel
        {
            Width = 280,
            Height = 100,
            Margin = new Padding(0, 10, 0, 0)
        };

        _btnRetroceder = new Button
        {
            Text = "◀ Retroceder",
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            BackColor = Color.FromArgb(240, 185, 11),
            ForeColor = Color.FromArgb(40, 20, 10),
            Width = 130,
            Height = 35,
            Location = new Point(10, 10),
            Cursor = Cursors.Hand
        };
        _btnRetroceder.Click += (s, e) => RetrocederHistorial();

        _btnAvanzar = new Button
        {
            Text = "Avanzar ▶",
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            BackColor = Color.FromArgb(240, 185, 11),
            ForeColor = Color.FromArgb(40, 20, 10),
            Width = 130,
            Height = 35,
            Location = new Point(150, 10),
            Cursor = Cursors.Hand
        };
        _btnAvanzar.Click += (s, e) => AvanzarHistorial();

        _btnCantarCarta = new Button
        {
            Text = "🎤 Cantar Carta",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            BackColor = Color.FromArgb(206, 17, 38),
            ForeColor = Color.White,
            Width = 280,
            Height = 40,
            Location = new Point(10, 50),
            Cursor = Cursors.Hand
        };
        _btnCantarCarta.Click += (s, e) => CantarCartaSiguiente();

        panelBotonesControl.Controls.Add(_btnRetroceder);
        panelBotonesControl.Controls.Add(_btnAvanzar);
        panelBotonesControl.Controls.Add(_btnCantarCarta);
        panelDerecho.Controls.Add(panelBotonesControl);

        // Puntajes
        var grpPuntajes = new GroupBox
        {
            Text = "🏆 Ranking",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Width = 280,
            Height = 200,
            Margin = new Padding(0, 10, 0, 0),
            Padding = new Padding(10)
        };

        _lstPuntajes = new ListBox
        {
            Width = 260,
            Height = 150,
            Font = new Font("Segoe UI", 10),
            Location = new Point(10, 30)
        };
        grpPuntajes.Controls.Add(_lstPuntajes);
        panelDerecho.Controls.Add(grpPuntajes);

        // Ganadores
        var grpGanadores = new GroupBox
        {
            Text = "👑 Ganadores",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            Width = 280,
            Height = 100,
            Margin = new Padding(0, 10, 0, 0),
            Padding = new Padding(10)
        };

        _lblGanadores = new Label
        {
            Text = "Esperando ganadores...",
            Font = new Font("Segoe UI", 10),
            AutoSize = true,
            Location = new Point(10, 30)
        };
        grpGanadores.Controls.Add(_lblGanadores);
        panelDerecho.Controls.Add(grpGanadores);

        panelContenido.Controls.Add(panelDerecho);
        panelPrincipal.Controls.Add(panelContenido);

        Controls.Add(panelPrincipal);
    }

    private Panel CrearPanelTabla()
    {
        var panelTabla = new Panel
        {
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.FromArgb(255, 255, 240),
            Padding = new Padding(10)
        };

        var tabla = _hub.ObtenerTabla();
        int tamaño = tabla.Filas;
        int btnSize = (panelTabla.Width - 40) / tamaño;

        _labelsTabla = new Label[tamaño, tamaño];

        for (int i = 0; i < tamaño; i++)
        {
            for (int j = 0; j < tamaño; j++)
            {
                var carta = tabla.Casillas[i, j];
                var lbl = new Label
                {
                    Text = $"{carta.Numero}\n{carta.Nombre}",
                    Width = btnSize,
                    Height = btnSize,
                    Left = j * btnSize + 10,
                    Top = i * btnSize + 10,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.FromArgb(255, 255, 240),
                    ForeColor = Color.FromArgb(40, 20, 10),
                    Font = new Font("Segoe UI", 8),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = carta.Numero,
                    Cursor = Cursors.Hand
                };
                lbl.Click += (s, e) => MarcarCarta(lbl, carta.Numero);
                panelTabla.Controls.Add(lbl);
                _labelsTabla[i, j] = lbl;
            }
        }

        return panelTabla;
    }

    private void MarcarCarta(Label lbl, int numeroCarta)
    {
        if (_hub.EsCartaValida(numeroCarta))
        {
            lbl.BackColor = lbl.BackColor == Color.FromArgb(255, 255, 240) 
                ? Color.FromArgb(206, 17, 38) 
                : Color.FromArgb(255, 255, 240);
        }
        else
        {
            MessageBox.Show($"❌ La carta {numeroCarta} no ha sido cantada", "Carta Inválida");
        }
    }

    private void RetrocederHistorial()
    {
        var carta = _hub.RetrocederHistorial();
        ActualizarCartaActual();
    }

    private void AvanzarHistorial()
    {
        var carta = _hub.AvanzarHistorial();
        ActualizarCartaActual();
    }

    private void CantarCartaSiguiente()
    {
        var cartas = _hub.ObtenerTabla().Casillas;
        Random random = new Random();
        int idx = random.Next(cartas.Length);
        var carta = cartas[idx / cartas.GetLength(1), idx % cartas.GetLength(1)];
        _hub.CantarCarta(carta);
        ActualizarCartaActual();
    }

    private void ActualizarCartaActual()
    {
        var carta = _hub.ObtenerCartaActual();
        if (carta != null)
        {
            _lblCartaActual.Text = $"{carta.Numero}. {carta.Nombre}\n{carta.Frase}";
            _lblPosicion.Text = $"Posición: {_hub.ObtenerPosicionHistorial()}/{_hub.ObtenerTotalCartasCantadas()}";
        }
    }

    private List<Carta> CargarCartasMexicanas()
    {
        // Aquí van las 54 cartas de la lotería mexicana
        return new List<Carta>
        {
            new Carta(1, "El Gallo", "¡El gallo canta!"),
            new Carta(2, "El Diablo", "¡Dale al diablo!"),
            new Carta(3, "La Dama", "¡La dama bonita!"),
            // ... agregar las 51 cartas restantes
        };
    }
}
