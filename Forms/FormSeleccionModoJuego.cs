namespace LOTERIAMX1.Forms;

/// <summary>
Formulario para seleccionar si jugar en modo local o multijugador.
/// </summary>
public partial class FormSeleccionModoJuego : Form
{
    public enum ModoConexion
    {
        Local,
        Multijugador
    }

    public ModoConexion ModoSeleccionado { get; private set; } = ModoConexion.Local;

    public FormSeleccionModoJuego()
    {
        InitializeComponent();
        InicializarFormulario();
    }

    private void InicializarFormulario()
    {
        BackColor = Color.FromArgb(254, 243, 210);
        ForeColor = Color.FromArgb(40, 20, 10);
        Text = "🎮 Seleccionar Modo - LOTERIAMX1";
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
            Text = "🎮 Modo de Juego",
            Font = new Font("Georgia", 22, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 30)
        };
        panel.Controls.Add(lblTitulo);

        var lblSubtitulo = new Label
        {
            Text = "¿Cómo deseas jugar?",
            Font = new Font("Segoe UI", 12),
            ForeColor = Color.FromArgb(40, 20, 10),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 30)
        };
        panel.Controls.Add(lblSubtitulo);

        // Opción 1: Local
        var panelLocal = CrearPanelOpcion(
            "🏠 MODO LOCAL",
            "Una Computadora",
            "• Todos los jugadores en una sola PC\n" +
            "• Turnan para jugar\n" +
            "• No requiere conexión a internet\n" +
            "• Ideal para jugar en familia",
            Color.FromArgb(0, 104, 56),
            true
        );
        panelLocal.Margin = new Padding(0, 0, 0, 20);
        var rbLocal = panelLocal.Controls.OfType<RadioButton>().First();
        rbLocal.CheckedChanged += (s, e) =>
        {
            if (rbLocal.Checked)
                ModoSeleccionado = ModoConexion.Local;
        };
        panel.Controls.Add(panelLocal);

        // Opción 2: Multijugador
        var panelMulti = CrearPanelOpcion(
            "🌐 MULTIJUGADOR EN RED",
            "Conexión por Internet",
            "• Jugadores en diferentes computadoras\n" +
            "• Conexión por IP\n" +
            "• Requiere conexión a internet\n" +
            "• Juega con amigos remotos",
            Color.FromArgb(206, 17, 38),
            false
        );
        panelMulti.Margin = new Padding(0, 0, 0, 30);
        var rbMulti = panelMulti.Controls.OfType<RadioButton>().First();
        rbMulti.CheckedChanged += (s, e) =>
        {
            if (rbMulti.Checked)
                ModoSeleccionado = ModoConexion.Multijugador;
        };
        panel.Controls.Add(panelMulti);

        Controls.Add(panel);

        // Panel de botones
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
            Text = "✕ Cancelar",
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

    private Panel CrearPanelOpcion(string titulo, string subtitulo, string descripcion, Color colorAccent, bool esPorDefecto)
    {
        var panel = new Panel
        {
            Width = 600,
            Height = 140,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.FromArgb(255, 255, 240),
            Padding = new Padding(15),
            Cursor = Cursors.Hand
        };

        var rb = new RadioButton
        {
            Text = titulo,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            ForeColor = colorAccent,
            Checked = esPorDefecto,
            Location = new Point(10, 5),
            AutoSize = true,
            Width = 500,
            Cursor = Cursors.Hand
        };

        var lblSubtitulo = new Label
        {
            Text = subtitulo,
            Font = new Font("Segoe UI", 10),
            ForeColor = colorAccent,
            Location = new Point(30, 30),
            AutoSize = true,
            Cursor = Cursors.Hand
        };

        var lblDescripcion = new Label
        {
            Text = descripcion,
            Font = new Font("Segoe UI", 9),
            ForeColor = Color.FromArgb(120, 80, 40),
            Location = new Point(30, 50),
            MaximumSize = new Size(550, 100),
            AutoSize = true,
            Cursor = Cursors.Hand
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
}
