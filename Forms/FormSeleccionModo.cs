namespace LOTERIAMX1.Forms;

/// <summary>
Formulario para seleccionar el modo de juego:
- Partida Normal: todos los jugadores juegan
- Desempate: solo ganadores anteriores continúan
/// </summary>
public partial class FormSeleccionModo : Form
{
    public enum ModoJuego
    {
        Normal,
        Desempate
    }

    public ModoJuego ModoSeleccionado { get; private set; } = ModoJuego.Normal;

    public FormSeleccionModo()
    {
        InitializeComponent();
        InicializarFormulario();
    }

    private void InicializarFormulario()
    {
        BackColor = Color.FromArgb(254, 243, 210);
        ForeColor = Color.FromArgb(40, 20, 10);
        Text = "🎮 Seleccionar Modo de Juego - LOTERIAMX1";
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
            Text = "🎮 Selecciona el Modo de Juego",
            Font = new Font("Georgia", 20, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 30)
        };
        panel.Controls.Add(lblTitulo);

        // Opción 1: Partida Normal
        var panelNormal = CrearPanelOpcion(
            "PARTIDA NORMAL ✓",
            "Todos Juegan",
            "• Todos los jugadores participan\n" +
            "• Gana el primero que completa un formato\n" +
            "• Se pueden jugar múltiples rondas\n" +
            "• Sistema de puntuación acumulativo",
            Color.FromArgb(0, 104, 56),
            true
        );
        panelNormal.Margin = new Padding(0, 0, 0, 20);
        var rbNormal = panelNormal.Controls.OfType<RadioButton>().First();
        rbNormal.CheckedChanged += (s, e) =>
        {
            if (rbNormal.Checked)
                ModoSeleccionado = ModoJuego.Normal;
        };
        panel.Controls.Add(panelNormal);

        // Opción 2: Desempate
        var panelDesempate = CrearPanelOpcion(
            "CON DESEMPATE ⚔️",
            "Ganadores Continúan",
            "• Si 2+ ganan simultáneamente, hay desempate\n" +
            "• Solo los ganadores pueden continuar\n" +
            "• Los demás salen de la ronda\n" +
            "• Se puede jugar hasta encontrar un solo ganador",
            Color.FromArgb(206, 17, 38),
            false
        );
        panelDesempate.Margin = new Padding(0, 0, 0, 30);
        var rbDesempate = panelDesempate.Controls.OfType<RadioButton>().First();
        rbDesempate.CheckedChanged += (s, e) =>
        {
            if (rbDesempate.Checked)
                ModoSeleccionado = ModoJuego.Desempate;
        };
        panel.Controls.Add(panelDesempate);

        // Nota informativa
        var grpNota = new GroupBox
        {
            Text = "ℹ️ Información",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Width = 600,
            Height = 90,
            Padding = new Padding(10)
        };

        var lblNota = new Label
        {
            Text = "En modo Desempate, cuando múltiples jugadores ganan en la misma ronda, " +
                   "solo ellos pueden colocar fichas en la siguiente ronda. " +
                   "Los demás jugadores quedan eliminados hasta que haya un ganador único.",
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.FromArgb(40, 20, 10),
            AutoSize = true,
            MaximumSize = new Size(580, 100)
        };
        grpNota.Controls.Add(lblNota);
        panel.Controls.Add(grpNota);

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
}
