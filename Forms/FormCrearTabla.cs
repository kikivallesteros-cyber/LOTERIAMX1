namespace LOTERIAMX1.Forms;

using LOTERIAMX1.Domain;

/// <summary>
Formulario para crear una tabla personalizada manualmente.
/// El usuario puede agregar cartas manualmente o generar una aleatoria.
/// </summary>
public partial class FormCrearTabla : Form
{
    private Tabla _tablaResultado;
    private int _tamaño;
    private List<Carta> _cartasDisponibles;
    private Button[,] _botonesTabla;
    private List<Carta> _cartasSeleccionadas;

    public Tabla TablaResultado => _tablaResultado;

    public FormCrearTabla(int tamaño, List<Carta> cartasDisponibles)
    {
        InitializeComponent();
        _tamaño = tamaño;
        _cartasDisponibles = cartasDisponibles;
        _cartasSeleccionadas = new List<Carta>();
        _botonesTabla = new Button[_tamaño, _tamaño];
        InicializarFormulario();
    }

    private void InicializarFormulario()
    {
        BackColor = Color.FromArgb(254, 243, 210);
        ForeColor = Color.FromArgb(40, 20, 10);
        Text = $"Crear Tabla Personalizada {_tamaño}x{_tamaño}";
        Width = 900;
        Height = 700;
        StartPosition = FormStartPosition.CenterParent;

        var panelPrincipal = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(20)
        };

        // Título
        var lblTitulo = new Label
        {
            Text = $"📋 Crear Tabla {_tamaño}x{_tamaño} Personalizada",
            Font = new Font("Georgia", 16, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 20)
        };
        panelPrincipal.Controls.Add(lblTitulo);

        // Panel para la tabla de botones
        var panelTabla = new Panel
        {
            Width = _tamaño * 100 + 10,
            Height = _tamaño * 100 + 10,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.FromArgb(255, 255, 240),
            Margin = new Padding(0, 0, 0, 20)
        };

        // Crear botones para cada casilla
        for (int i = 0; i < _tamaño; i++)
        {
            for (int j = 0; j < _tamaño; j++)
            {
                var btn = new Button
                {
                    Width = 95,
                    Height = 95,
                    Left = j * 100 + 5,
                    Top = i * 100 + 5,
                    BackColor = Color.FromArgb(255, 255, 240),
                    ForeColor = Color.FromArgb(40, 20, 10),
                    Font = new Font("Segoe UI", 9),
                    Cursor = Cursors.Hand,
                    Text = $"[{i},{j}]",
                    Tag = (i, j)
                };
                btn.Click += (s, e) => SeleccionarCarta(btn, i, j);
                panelTabla.Controls.Add(btn);
                _botonesTabla[i, j] = btn;
            }
        }
        panelPrincipal.Controls.Add(panelTabla);

        // Panel de botones inferiores
        var panelBotones = new Panel
        {
            Width = panelPrincipal.Width - 40,
            Height = 50,
            Margin = new Padding(0, 20, 0, 0)
        };

        var btnGenerar = new Button
        {
            Text = "🎲 Generar Aleatoria",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            BackColor = Color.FromArgb(240, 185, 11),
            ForeColor = Color.FromArgb(40, 20, 10),
            Width = 150,
            Height = 40,
            Location = new Point(10, 5),
            Cursor = Cursors.Hand
        };
        btnGenerar.Click += (s, e) => GenerarAleatorio();

        var btnAceptar = new Button
        {
            Text = "✓ Aceptar",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            BackColor = Color.FromArgb(0, 104, 56),
            ForeColor = Color.White,
            Width = 120,
            Height = 40,
            Location = new Point(panelBotones.Width - 260, 5),
            Cursor = Cursors.Hand
        };
        btnAceptar.Click += (s, e) => AceptarTabla();

        var btnCancelar = new Button
        {
            Text = "✗ Cancelar",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            BackColor = Color.FromArgb(206, 17, 38),
            ForeColor = Color.White,
            Width = 120,
            Height = 40,
            Location = new Point(panelBotones.Width - 130, 5),
            Cursor = Cursors.Hand
        };
        btnCancelar.Click += (s, e) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        panelBotones.Controls.Add(btnGenerar);
        panelBotones.Controls.Add(btnAceptar);
        panelBotones.Controls.Add(btnCancelar);
        panelPrincipal.Controls.Add(panelBotones);

        Controls.Add(panelPrincipal);
    }

    private void SeleccionarCarta(Button btn, int fila, int columna)
    {
        // Aquí se implementaría un diálogo para seleccionar una carta
        MessageBox.Show($"Selecciona carta para [{fila},{columna}]", "Seleccionar Carta");
    }

    private void GenerarAleatorio()
    {
        var random = new Random();
        for (int i = 0; i < _tamaño; i++)
        {
            for (int j = 0; j < _tamaño; j++)
            {
                var carta = _cartasDisponibles[random.Next(_cartasDisponibles.Count)];
                _botonesTabla[i, j].Text = $"{carta.Numero}\n{carta.Nombre}";
                _botonesTabla[i, j].BackColor = Color.FromArgb(206, 17, 38);
                _botonesTabla[i, j].ForeColor = Color.White;
                _botonesTabla[i, j].Tag = carta;
            }
        }
        MessageBox.Show("✓ Tabla generada aleatoriamente", "Éxito");
    }

    private void AceptarTabla()
    {
        var casillas = new Carta[_tamaño, _tamaño];
        for (int i = 0; i < _tamaño; i++)
        {
            for (int j = 0; j < _tamaño; j++)
            {
                if (_botonesTabla[i, j].Tag is Carta carta)
                {
                    casillas[i, j] = carta;
                }
                else
                {
                    MessageBox.Show($"Por favor completa todas las casillas. Falta [{i},{j}]");
                    return;
                }
            }
        }

        _tablaResultado = Tabla.Vacia(_tamaño);
        for (int i = 0; i < _tamaño; i++)
        {
            for (int j = 0; j < _tamaño; j++)
            {
                _tablaResultado.Casillas[i, j] = casillas[i, j];
            }
        }

        DialogResult = DialogResult.OK;
        Close();
    }
}
