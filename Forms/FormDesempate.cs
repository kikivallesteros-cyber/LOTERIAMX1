namespace LOTERIAMX1.Forms;

/// <summary>
Formulario de desempate que se muestra cuando múltiples jugadores ganan simultáneamente.
/// Permite que solo los ganadores continúen jugando.
/// </summary>
public partial class FormDesempate : Form
{
    private List<string> _ganadores;
    private List<string> _jugadoresActivos;

    public List<string> JugadoresActivos => _jugadoresActivos;

    public FormDesempate(string mensaje, List<string> ganadores)
    {
        InitializeComponent();
        _ganadores = ganadores;
        _jugadoresActivos = ganadores;
        InicializarFormulario(mensaje);
    }

    private void InicializarFormulario(string mensaje)
    {
        BackColor = Color.FromArgb(254, 243, 210);
        ForeColor = Color.FromArgb(40, 20, 10);
        Text = "⚔️ Desempate - Lotería Mexicana";
        Width = 500;
        Height = 350;
        StartPosition = FormStartPosition.CenterParent;

        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20),
            AutoScroll = true
        };

        // Icono y mensaje
        var lblMensaje = new Label
        {
            Text = "⚔️ DESEMPATE",
            Font = new Font("Georgia", 24, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 15)
        };
        panel.Controls.Add(lblMensaje);

        var lblDescripcion = new Label
        {
            Text = mensaje,
            Font = new Font("Segoe UI", 12),
            ForeColor = Color.FromArgb(40, 20, 10),
            AutoSize = true,
            MaximumSize = new Size(450, 100),
            Margin = new Padding(0, 0, 0, 20)
        };
        panel.Controls.Add(lblDescripcion);

        // Lista de ganadores
        var lblGanadores = new Label
        {
            Text = "Jugadores en desempate:",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 10)
        };
        panel.Controls.Add(lblGanadores);

        var lstGanadores = new ListBox
        {
            Width = 450,
            Height = 120,
            Font = new Font("Segoe UI", 11),
            Margin = new Padding(0, 0, 0, 20)
        };
        foreach (var ganador in _ganadores)
        {
            lstGanadores.Items.Add($"👑 {ganador}");
        }
        panel.Controls.Add(lstGanadores);

        // Botón de continuar
        var btnContinuar = new Button
        {
            Text = "⚔️ Continuar Desempate",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            BackColor = Color.FromArgb(206, 17, 38),
            ForeColor = Color.White,
            Width = 450,
            Height = 50,
            Cursor = Cursors.Hand,
            Margin = new Padding(0, 10, 0, 0)
        };
        btnContinuar.Click += (s, e) =>
        {
            DialogResult = DialogResult.OK;
            Close();
        };
        panel.Controls.Add(btnContinuar);

        Controls.Add(panel);
    }
}
