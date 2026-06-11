namespace LOTERIAMX1.Forms;

using LOTERIAMX1.Domain;

/// <summary>
Formulario de selección de modo de partida.
Permite al usuario elegir entre:
- CON DOBLES: Todos los jugadores pueden tener cartas repetidas
- SIN DOBLES: Todos los jugadores tienen cartas únicas
/// </summary>
public partial class FormSeleccionDobles : Form
{
    public bool PermitirDobles { get; private set; } = true;

    public FormSeleccionDobles()
    {
        InitializeComponent();
        InicializarFormulario();
    }

    private void InicializarFormulario()
    {
        BackColor = Color.FromArgb(254, 243, 210);
        ForeColor = Color.FromArgb(40, 20, 10);
        Text = "🎰 Seleccionar Tipo de Partida - LOTERIAMX1";
        Width = 700;
        Height = 500;
        StartPosition = FormStartPosition.CenterScreen;
        MaximizeBox = false;
        MinimizeBox = false;

        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            Padding = new Padding(30)
        };

        // Título principal
        var lblTitulo = new Label
        {
            Text = "🎰 Selecciona el Tipo de Partida",
            Font = new Font("Georgia", 20, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 30)
        };
        panel.Controls.Add(lblTitulo);

        var lblSubtitulo = new Label
        {
            Text = "Elige cómo se generarán las tablas para TODOS los jugadores:",
            Font = new Font("Segoe UI", 12),
            ForeColor = Color.FromArgb(40, 20, 10),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 30)
        };
        panel.Controls.Add(lblSubtitulo);

        // Opción 1: CON DOBLES
        var panelConDobles = CrearPanelOpcion(
            "CON DOBLES ✓",
            "Cartas Repetidas Permitidas",
            "• Mismo número puede aparecer varias veces\n" +
            "• Cada jugador tiene tabla diferente\n" +
            "• Mayor variedad en el juego\n" +
            "• Recomendado para más de 2 jugadores",
            Color.FromArgb(0, 104, 56),
            true
        );
        panelConDobles.Margin = new Padding(0, 0, 0, 20);
        var rbConDobles = panelConDobles.Controls.OfType<RadioButton>().First();
        rbConDobles.CheckedChanged += (s, e) =>
        {
            if (rbConDobles.Checked)
                PermitirDobles = true;
        };
        panel.Controls.Add(panelConDobles);

        // Opción 2: SIN DOBLES
        var panelSinDobles = CrearPanelOpcion(
            "SIN DOBLES ✗",
            "Todas las Cartas Únicas",
            "• Cada número solo aparece una vez\n" +
            "• Todas las tablas son diferentes\n" +
            "• Mayor desafío y equilibrio\n" +
            "• Perfecto para 2 jugadores",
            Color.FromArgb(206, 17, 38),
            false
        );
        panelSinDobles.Margin = new Padding(0, 0, 0, 30);
        var rbSinDobles = panelSinDobles.Controls.OfType<RadioButton>().First();
        rbSinDobles.CheckedChanged += (s, e) =>
        {
            if (rbSinDobles.Checked)
                PermitirDobles = false;
        };
        panel.Controls.Add(panelSinDobles);

        // Advertencia
        var grpAdvertencia = new GroupBox
        {
            Text = "⚠️ Importante",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            Width = 600,
            Height = 80,
            Padding = new Padding(10),
            Margin = new Padding(0, 0, 0, 20)
        };

        var lblAdvertencia = new Label
        {
            Text = "Esta configuración se aplicará a TODOS los jugadores en la partida. " +
                   "No se puede cambiar una vez iniciado el juego.",
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.FromArgb(40, 20, 10),
            AutoSize = true,
            MaximumSize = new Size(580, 100)
        };
        grpAdvertencia.Controls.Add(lblAdvertencia);
        panel.Controls.Add(grpAdvertencia);

        Controls.Add(panel);
    }

    private Panel CrearPanelOpcion(string titulo, string subtitulo, string descripcion, Color colorAccent, bool esPorDefecto)
    {
        var panel = new Panel
        {
            Width = 600,
            Height = 140,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.FromArgb(255, 255, 240),
            Padding = new Padding(15)
        };

        var rb = new RadioButton
        {
            Text = titulo,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            ForeColor = colorAccent,
            Checked = esPorDefecto,
            Location = new Point(10, 5),
            AutoSize = true,
            Width = 500
        };

        var lblSubtitulo = new Label
        {
            Text = subtitulo,
            Font = new Font("Segoe UI", 10),
            ForeColor = colorAccent,
            Location = new Point(30, 30),
            AutoSize = true
        };

        var lblDescripcion = new Label
        {
            Text = descripcion,
            Font = new Font("Segoe UI", 9),
            ForeColor = Color.FromArgb(120, 80, 40),
            Location = new Point(30, 50),
            MaximumSize = new Size(550, 100),
            AutoSize = true
        };

        panel.Controls.Add(rb);
        panel.Controls.Add(lblSubtitulo);
        panel.Controls.Add(lblDescripcion);

        // Hacer que el click en el panel seleccione el radio button
        panel.Click += (s, e) => rb.Checked = true;
        foreach (var ctrl in panel.Controls)
        {
            ctrl.Click += (s, e) => rb.Checked = true;
        }

        return panel;
    }

    protected override void CreateHandle()
    {
        base.CreateHandle();
        
        // Crear botones inferiores
        var panelBotones = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 60,
            BackColor = Color.FromArgb(254, 243, 210),
            Padding = new Padding(30, 10, 30, 10)
        };

        var btnContinuar = new Button
        {
            Text = "✓ Continuar",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            BackColor = Color.FromArgb(0, 104, 56),
            ForeColor = Color.White,
            Width = 150,
            Height = 40,
            Cursor = Cursors.Hand
        };
        btnContinuar.Click += (s, e) =>
        {
            DialogResult = DialogResult.OK;
            Close();
        };

        var btnCancelar = new Button
        {
            Text = "✗ Cancelar",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            BackColor = Color.FromArgb(206, 17, 38),
            ForeColor = Color.White,
            Width = 150,
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
        Controls.Add(panelBotones);
    }
}
