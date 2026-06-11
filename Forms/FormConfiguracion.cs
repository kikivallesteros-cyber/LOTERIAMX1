namespace LOTERIAMX1.Forms;

using LOTERIAMX1.Domain;
using LOTERIAMX1.Domain.Enums;

/// <summary>
Formulario para configurar el juego antes de iniciarlo.
Permite seleccionar tamaño de tabla, cartas dobles, y formatos de victoria.
/// </summary>
public partial class FormConfiguracion : Form
{
    private ConfiguracionJuego _config;
    public ConfiguracionJuego ConfiguracionResultado { get; private set; }

    public FormConfiguracion(ConfiguracionJuego configInicial)
    {
        InitializeComponent();
        _config = configInicial ?? new ConfiguracionJuego();
        ConfiguracionResultado = _config;
        InicializarControles();
    }

    private void InicializarControles()
    {
        // Configurar colores temáticos
        BackColor = Color.FromArgb(254, 243, 210); // Amarillo cálido
        ForeColor = Color.FromArgb(40, 20, 10);    // Marrón oscuro
        Text = "⚙️ Configuración del Juego - LOTERIAMX1";
        Width = 700;
        Height = 900;
        StartPosition = FormStartPosition.CenterScreen;

        // Panel principal
        var panelPrincipal = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            BackColor = BackColor,
            Padding = new Padding(20)
        };

        // Título
        var lblTitulo = new Label
        {
            Text = "⚙️ Configuración del Juego",
            Font = new Font("Georgia", 18, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 20)
        };
        panelPrincipal.Controls.Add(lblTitulo);

        // Sección: Tamaño de tabla
        var grpTamaño = new GroupBox
        {
            Text = "📄 Tamaño de Tabla",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Margin = new Padding(0, 0, 0, 15),
            Padding = new Padding(15),
            Width = panelPrincipal.Width - 40,
            Height = 100,
            AutoSize = false
        };

        var cmbTamaño = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 10),
            Width = 250,
            Location = new Point(15, 30)
        };
        cmbTamaño.Items.AddRange(new object[] { "4x4", "5x5 (Recomendado)", "6x6", "7x7", "8x8", "9x9", "10x10" });
        cmbTamaño.SelectedIndex = _config.TamañoTabla - 4;
        cmbTamaño.SelectedIndexChanged += (s, e) => _config.TamañoTabla = 4 + cmbTamaño.SelectedIndex;
        grpTamaño.Controls.Add(cmbTamaño);
        panelPrincipal.Controls.Add(grpTamaño);

        // Sección: Tipo de Partida (CON o SIN DOBLES)
        var grpTipoPartida = new GroupBox
        {
            Text = "🎰 Tipo de Partida - Cartas Dobles",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            Margin = new Padding(0, 0, 0, 15),
            Padding = new Padding(15),
            Width = panelPrincipal.Width - 40,
            Height = 180,
            AutoSize = false
        };

        var rbConDobles = new RadioButton
        {
            Text = "✓ CON DOBLES - Permite cartas repetidas en la tabla",
            Font = new Font("Segoe UI", 10),
            Checked = _config.PermitirCartasDobles,
            Location = new Point(15, 30),
            AutoSize = true,
            Width = 600
        };
        rbConDobles.CheckedChanged += (s, e) =>
        {
            if (rbConDobles.Checked)
                _config.PermitirCartasDobles = true;
        };

        var lblExplicacionDobles = new Label
        {
            Text = "→ Misma carta puede aparecer varias veces en tu tabla",
            Font = new Font("Segoe UI", 9),
            ForeColor = Color.FromArgb(120, 80, 40),
            Location = new Point(35, 55),
            AutoSize = true
        };

        var rbSinDobles = new RadioButton
        {
            Text = "✗ SIN DOBLES - Todas las cartas son únicas en la tabla",
            Font = new Font("Segoe UI", 10),
            Checked = !_config.PermitirCartasDobles,
            Location = new Point(15, 85),
            AutoSize = true,
            Width = 600
        };
        rbSinDobles.CheckedChanged += (s, e) =>
        {
            if (rbSinDobles.Checked)
                _config.PermitirCartasDobles = false;
        };

        var lblExplicacionSinDobles = new Label
        {
            Text = "→ Cada carta solo aparece una vez (todas diferentes)",
            Font = new Font("Segoe UI", 9),
            ForeColor = Color.FromArgb(120, 80, 40),
            Location = new Point(35, 110),
            AutoSize = true
        };

        var lblAdvertencia = new Label
        {
            Text = "⚠️ Nota: Esta opción se aplica a TODOS los jugadores",
            Font = new Font("Segoe UI", 9, FontStyle.Italic),
            ForeColor = Color.FromArgb(206, 17, 38),
            Location = new Point(15, 135),
            AutoSize = true
        };

        grpTipoPartida.Controls.Add(rbConDobles);
        grpTipoPartida.Controls.Add(lblExplicacionDobles);
        grpTipoPartida.Controls.Add(rbSinDobles);
        grpTipoPartida.Controls.Add(lblExplicacionSinDobles);
        grpTipoPartida.Controls.Add(lblAdvertencia);
        panelPrincipal.Controls.Add(grpTipoPartida);

        // Sección: Formatos Ganadores
        var grpFormatos = new GroupBox
        {
            Text = "🏆 Formas de Ganar",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Margin = new Padding(0, 0, 0, 15),
            Padding = new Padding(15),
            Width = panelPrincipal.Width - 40,
            Height = 300,
            AutoSize = false
        };

        var chkFormatos = new Dictionary<FormatoGanador, CheckBox>();
        var formatos = new[]
        {
            (FormatoGanador.LineaHorizontal, "↔️ Línea Horizontal"),
            (FormatoGanador.LineaVertical, "↕️ Línea Vertical"),
            (FormatoGanador.Diagonal, "↘️ Diagonal Principal"),
            (FormatoGanador.DiagonalInvertida, "↙️ Diagonal Inversa"),
            (FormatoGanador.Cruz, "✚ Cruz (Líneas centrales)"),
            (FormatoGanador.Cruzita, "+ Plus (Extremos centrales)"),
            (FormatoGanador.TablaLlena, "⬜ Tabla Llena")
        };

        int posY = 30;
        foreach (var (formato, nombre) in formatos)
        {
            var chk = new CheckBox
            {
                Text = nombre,
                Font = new Font("Segoe UI", 10),
                Checked = _config.FormatosActivos.Contains(formato),
                Location = new Point(15, posY),
                AutoSize = true,
                Width = 500
            };
            chk.CheckedChanged += (s, e) => ActualizarFormatos(chkFormatos);
            chkFormatos[formato] = chk;
            grpFormatos.Controls.Add(chk);
            posY += 35;
        }
        panelPrincipal.Controls.Add(grpFormatos);

        // Botones
        var panelBotones = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 60,
            BackColor = BackColor,
            Padding = new Padding(10)
        };

        var btnAceptar = new Button
        {
            Text = "✓ Aceptar",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            BackColor = Color.FromArgb(0, 104, 56),
            ForeColor = Color.White,
            Width = 140,
            Height = 40,
            Location = new Point(10, 10),
            Cursor = Cursors.Hand
        };
        btnAceptar.Click += (s, e) =>
        {
            ConfiguracionResultado = _config;
            DialogResult = DialogResult.OK;
            Close();
        };

        var btnCancelar = new Button
        {
            Text = "✗ Cancelar",
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

        panelBotones.Controls.Add(btnAceptar);
        panelBotones.Controls.Add(btnCancelar);

        Controls.Add(panelPrincipal);
        Controls.Add(panelBotones);
    }

    private void ActualizarFormatos(Dictionary<FormatoGanador, CheckBox> chkFormatos)
    {
        _config.FormatosActivos.Clear();
        foreach (var (formato, chk) in chkFormatos)
        {
            if (chk.Checked)
            {
                _config.FormatosActivos.Add(formato);
            }
        }
    }
}
