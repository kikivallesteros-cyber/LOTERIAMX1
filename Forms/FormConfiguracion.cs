namespace LOTERIAMX1.Forms;

using LOTERIAMX1.Domain;
using LOTERIAMX1.Domain.Enums;

/// <summary>
Formulario para configurar el juego antes de iniciarlo.
/// Permite seleccionar tamaño de tabla, cartas dobles, y formatos de victoria.
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
            Text = "📏 Tamaño de Tabla",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Margin = new Padding(0, 0, 0, 15),
            Padding = new Padding(15),
            Width = panelPrincipal.Width - 40,
            Height = 100
        };

        var cmbTamaño = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 10),
            Width = 200,
            Location = new Point(15, 30)
        };
        cmbTamaño.Items.AddRange(new object[] { "4x4", "5x5 (Recomendado)", "6x6", "7x7", "8x8", "9x9", "10x10" });
        cmbTamaño.SelectedIndex = _config.TamañoTabla - 4;
        cmbTamaño.SelectedIndexChanged += (s, e) => _config.TamañoTabla = 4 + cmbTamaño.SelectedIndex;
        grpTamaño.Controls.Add(cmbTamaño);
        panelPrincipal.Controls.Add(grpTamaño);

        // Sección: Cartas Dobles
        var grpDobles = new GroupBox
        {
            Text = "🃏 Cartas Dobles",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Margin = new Padding(0, 0, 0, 15),
            Padding = new Padding(15),
            Width = panelPrincipal.Width - 40,
            Height = 120
        };

        var rbDoblesSí = new RadioButton
        {
            Text = "✓ Permitir cartas repetidas (Dobles)",
            Font = new Font("Segoe UI", 10),
            Checked = _config.PermitirCartasDobles,
            Location = new Point(15, 30),
            AutoSize = true
        };
        rbDoblesSí.CheckedChanged += (s, e) => _config.PermitirCartasDobles = rbDoblesSí.Checked;

        var rbDobblesNo = new RadioButton
        {
            Text = "✗ Sin dobles (todas únicas)",
            Font = new Font("Segoe UI", 10),
            Checked = !_config.PermitirCartasDobles,
            Location = new Point(15, 60),
            AutoSize = true
        };

        grpDobles.Controls.Add(rbDoblesSí);
        grpDobles.Controls.Add(rbDobblesNo);
        panelPrincipal.Controls.Add(grpDobles);

        // Sección: Formatos Ganadores
        var grpFormatos = new GroupBox
        {
            Text = "🏆 Formas de Ganar",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Margin = new Padding(0, 0, 0, 15),
            Padding = new Padding(15),
            Width = panelPrincipal.Width - 40,
            Height = 250
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
                AutoSize = true
            };
            chk.CheckedChanged += (s, e) => ActualizarFormatos();
            chkFormatos[formato] = chk;
            grpFormatos.Controls.Add(chk);
            posY += 30;
        }
        panelPrincipal.Controls.Add(grpFormatos);

        // Botones
        var panelBotones = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 50,
            BackColor = BackColor,
            Padding = new Padding(10)
        };

        var btnAceptar = new Button
        {
            Text = "✓ Aceptar",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            BackColor = Color.FromArgb(0, 104, 56),
            ForeColor = Color.White,
            Width = 120,
            Height = 35,
            Location = new Point(10, 10),
            Cursor = Cursors.Hand
        };
        btnAceptar.Click += (s, e) =>
        {
            DialogResult = DialogResult.OK;
            Close();
        };

        var btnCancelar = new Button
        {
            Text = "✗ Cancelar",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            BackColor = Color.FromArgb(206, 17, 38),
            ForeColor = Color.White,
            Width = 120,
            Height = 35,
            Location = new Point(140, 10),
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

    private void ActualizarFormatos()
    {
        // Actualizar lista de formatos seleccionados
        _config.FormatosActivos.Clear();
        foreach (var chk in Controls.OfType<GroupBox>().LastOrDefault()?.Controls.OfType<CheckBox>() ?? Array.Empty<CheckBox>())
        {
            if (chk.Checked)
            {
                var texto = chk.Text;
                if (texto.Contains("Horizontal")) _config.FormatosActivos.Add(FormatoGanador.LineaHorizontal);
                else if (texto.Contains("Vertical")) _config.FormatosActivos.Add(FormatoGanador.LineaVertical);
                else if (texto.Contains("Principal")) _config.FormatosActivos.Add(FormatoGanador.Diagonal);
                else if (texto.Contains("Inversa")) _config.FormatosActivos.Add(FormatoGanador.DiagonalInvertida);
                else if (texto.Contains("Cruz")) _config.FormatosActivos.Add(FormatoGanador.Cruz);
                else if (texto.Contains("Plus")) _config.FormatosActivos.Add(FormatoGanador.Cruzita);
                else if (texto.Contains("Llena")) _config.FormatosActivos.Add(FormatoGanador.TablaLlena);
            }
        }
    }
}
