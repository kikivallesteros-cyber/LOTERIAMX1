namespace LOTERIAMX1.Forms;

/// <summary>
Formulario para gestionar jugadores en modo local.
Permite agregar, eliminar y ver lista de jugadores.
/// </summary>
public partial class FormJugadoresLocal : Form
{
    private List<string> _jugadores = new List<string>();
    private ListBox _lstJugadores;
    private TextBox _txtNombreJugador;

    public List<string> Jugadores => _jugadores;

    public FormJugadoresLocal()
    {
        InitializeComponent();
        InicializarFormulario();
    }

    private void InicializarFormulario()
    {
        BackColor = Color.FromArgb(254, 243, 210);
        ForeColor = Color.FromArgb(40, 20, 10);
        Text = "👥 Agregar Jugadores - LOTERIAMX1";
        Width = 600;
        Height = 600;
        StartPosition = FormStartPosition.CenterScreen;

        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(20)
        };

        // Título
        var lblTitulo = new Label
        {
            Text = "👥 Agregar Jugadores",
            Font = new Font("Georgia", 18, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 20)
        };
        panel.Controls.Add(lblTitulo);

        // Sección: Agregar jugador
        var grpAgregar = new GroupBox
        {
            Text = "➕ Nuevo Jugador",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Width = 560,
            Height = 100,
            Padding = new Padding(15),
            Margin = new Padding(0, 0, 0, 15)
        };

        var lblNombre = new Label
        {
            Text = "Nombre del jugador:",
            Font = new Font("Segoe UI", 10),
            Location = new Point(15, 30),
            AutoSize = true
        };

        _txtNombreJugador = new TextBox
        {
            Font = new Font("Segoe UI", 10),
            Width = 300,
            Location = new Point(15, 55),
            Padding = new Padding(5)
        };
        _txtNombreJugador.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Return)
            {
                AgregarJugador();
                e.Handled = true;
            }
        };

        var btnAgregar = new Button
        {
            Text = "✓ Agregar",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            BackColor = Color.FromArgb(0, 104, 56),
            ForeColor = Color.White,
            Width = 100,
            Height = 30,
            Location = new Point(325, 55),
            Cursor = Cursors.Hand
        };
        btnAgregar.Click += (s, e) => AgregarJugador();

        grpAgregar.Controls.Add(lblNombre);
        grpAgregar.Controls.Add(_txtNombreJugador);
        grpAgregar.Controls.Add(btnAgregar);
        panel.Controls.Add(grpAgregar);

        // Sección: Lista de jugadores
        var grpLista = new GroupBox
        {
            Text = "📋 Jugadores en la Partida",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Width = 560,
            Height = 280,
            Padding = new Padding(15),
            Margin = new Padding(0, 0, 0, 15)
        };

        _lstJugadores = new ListBox
        {
            Font = new Font("Segoe UI", 11),
            Width = 530,
            Height = 180,
            Location = new Point(15, 30),
            BackColor = Color.FromArgb(255, 255, 240),
            ForeColor = Color.FromArgb(40, 20, 10)
        };
        grpLista.Controls.Add(_lstJugadores);

        var btnEliminar = new Button
        {
            Text = "✕ Eliminar Seleccionado",
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            BackColor = Color.FromArgb(206, 17, 38),
            ForeColor = Color.White,
            Width = 220,
            Height = 30,
            Location = new Point(15, 220),
            Cursor = Cursors.Hand
        };
        btnEliminar.Click += (s, e) => EliminarJugador();

        var lblContador = new Label
        {
            Text = "Total: 0 jugadores",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Location = new Point(250, 225),
            AutoSize = true,
            Tag = "lblContador"
        };

        grpLista.Controls.Add(btnEliminar);
        grpLista.Controls.Add(lblContador);
        panel.Controls.Add(grpLista);

        // Panel de botones inferiores
        var panelBotones = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 60,
            BackColor = BackColor,
            Padding = new Padding(10)
        };

        var btnContinuar = new Button
        {
            Text = "✓ Continuar",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            BackColor = Color.FromArgb(0, 104, 56),
            ForeColor = Color.White,
            Width = 140,
            Height = 40,
            Location = new Point(10, 10),
            Cursor = Cursors.Hand
        };
        btnContinuar.Click += (s, e) =>
        {
            if (_jugadores.Count < 2)
            {
                MessageBox.Show("⚠️ Se necesitan al menos 2 jugadores para jugar.", "Cantidad Insuficiente");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        };

        var btnCancelar = new Button
        {
            Text = "✕ Cancelar",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            BackColor = Color.FromArgb(206, 17, 38),
            ForeColor = Color.White,
            Width = 140,
            Height = 40,
            Location = new Point(160, 10),
            Cursor = Cursors.Hand
        };
        btnCancelar.Click += (s, e) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        panelBotones.Controls.Add(btnContinuar);
        panelBotones.Controls.Add(btnCancelar);

        Controls.Add(panel);
        Controls.Add(panelBotones);
    }

    private void AgregarJugador()
    {
        var nombre = _txtNombreJugador.Text.Trim();

        if (string.IsNullOrWhiteSpace(nombre))
        {
            MessageBox.Show("⚠️ Por favor ingresa el nombre del jugador.", "Campo Vacío");
            _txtNombreJugador.Focus();
            return;
        }

        if (_jugadores.Contains(nombre))
        {
            MessageBox.Show($"⚠️ El jugador '{nombre}' ya está en la lista.", "Duplicado");
            return;
        }

        _jugadores.Add(nombre);
        _lstJugadores.Items.Add($"👤 {nombre}");
        _txtNombreJugador.Clear();
        _txtNombreJugador.Focus();

        ActualizarContador();
    }

    private void EliminarJugador()
    {
        if (_lstJugadores.SelectedIndex == -1)
        {
            MessageBox.Show("⚠️ Selecciona un jugador para eliminar.", "Ninguno Seleccionado");
            return;
        }

        var indice = _lstJugadores.SelectedIndex;
        _jugadores.RemoveAt(indice);
        _lstJugadores.Items.RemoveAt(indice);

        ActualizarContador();
    }

    private void ActualizarContador()
    {
        var lblContador = Controls.OfType<Panel>().First().Controls.OfType<GroupBox>().Last().Controls.OfType<Label>().First(l => l.Tag?.ToString() == "lblContador");
        lblContador.Text = $"Total: {_jugadores.Count} jugador{(_jugadores.Count != 1 ? "es" : "")}";
    }
}
