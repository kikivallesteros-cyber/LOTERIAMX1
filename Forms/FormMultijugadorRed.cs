namespace LOTERIAMX1.Forms;

using System.Net;
using System.Net.Sockets;

/// <summary>
Formulario para modo multijugador en red.
Permite crear servidor (anfitrión) o conectarse a otro jugador.
/// </summary>
public partial class FormMultijugadorRed : Form
{
    private enum RolJugador { Anfitrion, Cliente }
    private RolJugador _rolSeleccionado = RolJugador.Anfitrion;
    public string DireccionIP { get; private set; }
    public string NombreJugador { get; private set; }
    public bool EsAnfitrion => _rolSeleccionado == RolJugador.Anfitrion;

    public FormMultijugadorRed()
    {
        InitializeComponent();
        InicializarFormulario();
    }

    private void InicializarFormulario()
    {
        BackColor = Color.FromArgb(254, 243, 210);
        ForeColor = Color.FromArgb(40, 20, 10);
        Text = "🌐 Multijugador en Red - LOTERIAMX1";
        Width = 800;
        Height = 650;
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
            Text = "🌐 Multijugador en Red",
            Font = new Font("Georgia", 20, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 20)
        };
        panel.Controls.Add(lblTitulo);

        // Sección: Rol del jugador
        var grpRol = new GroupBox
        {
            Text = "👤 Tu Rol en la Partida",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Width = 750,
            Height = 140,
            Padding = new Padding(15),
            Margin = new Padding(0, 0, 0, 15)
        };

        var rbAnfitrion = new RadioButton
        {
            Text = "🏠 ANFITRIÓN (Crear Partida)",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Checked = true,
            Location = new Point(15, 30),
            AutoSize = true,
            Width = 400
        };
        rbAnfitrion.CheckedChanged += (s, e) => _rolSeleccionado = RolJugador.Anfitrion;

        var lblAnfitrion = new Label
        {
            Text = "Eres el anfitrión. Otros jugadores se conectarán a tu IP.",
            Font = new Font("Segoe UI", 9),
            ForeColor = Color.FromArgb(120, 80, 40),
            Location = new Point(35, 55),
            AutoSize = true
        };

        var rbCliente = new RadioButton
        {
            Text = "📱 CLIENTE (Unirse a Partida)",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Checked = false,
            Location = new Point(15, 80),
            AutoSize = true,
            Width = 400
        };
        rbCliente.CheckedChanged += (s, e) => _rolSeleccionado = RolJugador.Cliente;

        var lblCliente = new Label
        {
            Text = "Te conectas a la IP del anfitrión para unirte a su partida.",
            Font = new Font("Segoe UI", 9),
            ForeColor = Color.FromArgb(120, 80, 40),
            Location = new Point(35, 105),
            AutoSize = true
        };

        grpRol.Controls.Add(rbAnfitrion);
        grpRol.Controls.Add(lblAnfitrion);
        grpRol.Controls.Add(rbCliente);
        grpRol.Controls.Add(lblCliente);
        panel.Controls.Add(grpRol);

        // Sección: Nombre del jugador
        var grpNombre = new GroupBox
        {
            Text = "👤 Tu Nombre",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Width = 750,
            Height = 80,
            Padding = new Padding(15),
            Margin = new Padding(0, 0, 0, 15)
        };

        var lblNombreJugador = new Label
        {
            Text = "¿Cuál es tu nombre?",
            Font = new Font("Segoe UI", 10),
            Location = new Point(15, 30),
            AutoSize = true
        };

        var txtNombre = new TextBox
        {
            Font = new Font("Segoe UI", 10),
            Width = 300,
            Location = new Point(250, 28)
        };

        grpNombre.Controls.Add(lblNombreJugador);
        grpNombre.Controls.Add(txtNombre);
        panel.Controls.Add(grpNombre);

        // Sección: IP (solo para cliente)
        var grpIP = new GroupBox
        {
            Text = "🌐 Dirección IP del Anfitrión",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 104, 56),
            Width = 750,
            Height = 130,
            Padding = new Padding(15),
            Margin = new Padding(0, 0, 0, 15),
            Visible = false // Se muestra solo si es cliente
        };

        var lblIPInfo = new Label
        {
            Text = "Pídele la IP al anfitrión e ingrésala aquí:",
            Font = new Font("Segoe UI", 10),
            Location = new Point(15, 30),
            AutoSize = true
        };

        var txtIP = new TextBox
        {
            Font = new Font("Segoe UI", 10),
            Width = 300,
            PlaceholderText = "Ej: 192.168.1.100",
            Location = new Point(15, 55)
        };

        var lblIPLocal = new Label
        {
            Text = "Tu IP local:",
            Font = new Font("Segoe UI", 9),
            Location = new Point(15, 85),
            AutoSize = true
        };

        var lblIPMostrada = new Label
        {
            Text = ObtenerIPLocal(),
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            Location = new Point(120, 85),
            AutoSize = true
        };

        grpIP.Controls.Add(lblIPInfo);
        grpIP.Controls.Add(txtIP);
        grpIP.Controls.Add(lblIPLocal);
        grpIP.Controls.Add(lblIPMostrada);
        panel.Controls.Add(grpIP);

        // Mostrar/ocultar IP según rol
        rbAnfitrion.CheckedChanged += (s, e) => grpIP.Visible = false;
        rbCliente.CheckedChanged += (s, e) => grpIP.Visible = true;

        // Sección: Mi IP (para anfitrión)
        var grpMiIP = new GroupBox
        {
            Text = "🏠 Mi IP (Comparte esta)",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(206, 17, 38),
            Width = 750,
            Height = 100,
            Padding = new Padding(15),
            Margin = new Padding(0, 0, 0, 20)
        };

        var lblMiIPDescripcion = new Label
        {
            Text = "📋 Copia esta IP y compártela con los otros jugadores para que se unan:",
            Font = new Font("Segoe UI", 10),
            Location = new Point(15, 30),
            AutoSize = true
        };

        var txtMiIP = new TextBox
        {
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            Width = 400,
            Text = ObtenerIPLocal(),
            ReadOnly = true,
            Location = new Point(15, 55),
            BackColor = Color.FromArgb(206, 17, 38),
            ForeColor = Color.White
        };

        var btnCopiar = new Button
        {
            Text = "📋 Copiar",
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            BackColor = Color.FromArgb(0, 104, 56),
            ForeColor = Color.White,
            Width = 80,
            Height = 30,
            Location = new Point(425, 55),
            Cursor = Cursors.Hand
        };
        btnCopiar.Click += (s, e) =>
        {
            Clipboard.SetText(txtMiIP.Text);
            MessageBox.Show("✓ IP copiada al portapapeles", "Éxito");
        };

        grpMiIP.Controls.Add(lblMiIPDescripcion);
        grpMiIP.Controls.Add(txtMiIP);
        grpMiIP.Controls.Add(btnCopiar);
        panel.Controls.Add(grpMiIP);

        // Ocultar/mostrar sección de MI IP según rol
        rbAnfitrion.CheckedChanged += (s, e) => grpMiIP.Visible = rbAnfitrion.Checked;
        rbCliente.CheckedChanged += (s, e) => grpMiIP.Visible = false;

        Controls.Add(panel);

        // Panel de botones
        var panelBotones = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 60,
            BackColor = BackColor,
            Padding = new Padding(10)
        };

        var btnConectar = new Button
        {
            Text = "✓ Conectar",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            BackColor = Color.FromArgb(0, 104, 56),
            ForeColor = Color.White,
            Width = 140,
            Height = 40,
            Location = new Point(10, 10),
            Cursor = Cursors.Hand
        };
        btnConectar.Click += (s, e) =>
        {
            NombreJugador = txtNombre.Text.Trim();
            DireccionIP = _rolSeleccionado == RolJugador.Anfitrion ? ObtenerIPLocal() : txtIP.Text.Trim();

            if (string.IsNullOrWhiteSpace(NombreJugador))
            {
                MessageBox.Show("⚠️ Por favor ingresa tu nombre.", "Campo Requerido");
                txtNombre.Focus();
                return;
            }

            if (_rolSeleccionado == RolJugador.Cliente && string.IsNullOrWhiteSpace(DireccionIP))
            {
                MessageBox.Show("⚠️ Por favor ingresa la IP del anfitrión.", "IP Requerida");
                txtIP.Focus();
                return;
            }

            MessageBox.Show($"✓ Conectado como {NombreJugador}\nIP: {DireccionIP}", "Conexión Exitosa");
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

        panelBotones.Controls.Add(btnConectar);
        panelBotones.Controls.Add(btnCancelar);
        Controls.Add(panelBotones);
    }

    private string ObtenerIPLocal()
    {
        try
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                if (socket.LocalEndPoint is IPEndPoint endPoint)
                {
                    return endPoint.Address.ToString();
                }
            }
        }
        catch
        {
            // Si falla, obtener del hostname
        }

        try
        {
            var hostname = Dns.GetHostName();
            var ips = Dns.GetHostAddresses(hostname);
            foreach (var ip in ips)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
        }
        catch { }

        return "127.0.0.1";
    }
}
