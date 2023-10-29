namespace MirrorPort
{
    using NetPs.Socket;
    using NetPs.Tcp;
    using NetPs.Udp;
    using System.Security.Cryptography;

    public partial class Form1 : Form
    {
        public const string STA_STOPPED = "stopped";
        public const string STA_RUNNING = "running";
        private IList<SocketCore> Sockets = new List<SocketCore>();
        public Form1()
        {
            InitializeComponent();
            this.Text = "TcpUdp 转发器";
            this.btn_start.Text = "开始转发";
            this.btn_stop.Text = "结束转发";
            this.btn_start.Enabled = false;
            this.btn_stop.Enabled = false;
            this.lb_status.Text = STA_STOPPED;
            this.check_logshow.Text = "是否输出";
            this.check_logshow.Checked = true;
            Initialize();
            tb_print.TextChanged += Tb_print_TextChanged;

        }

        private void Tb_print_TextChanged(object sender, EventArgs e)
        {
            tb_print.SelectionStart = tb_print.Text.Length;
            tb_print.ScrollToCaret();
        }

        public async void Initialize()
        {
            using (var fs = new FileStream("mirror_config.ini", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {

                var reader = new StreamReader(fs);
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (line == null) continue;
                    var blocks = line.Split(' ');
                    if (blocks.Length == 3)
                    {
                        try
                        {
                            switch (blocks[0].ToLower())
                            {
                                case "udp":
                                    var host = new UdpHost(blocks[1]);
                                    host.Rx.NoBufferReceived += (buffer, length, address) =>
                                    {
                                        var c = host.Clone(address);
                                        c.StartHub(new UdpMirrorHub(c, blocks[2], 10 << 20));
                                        this.Log($"[{blocks[1]}] 接收到 udp {address}\n");
                                        return false;
                                    };
                                    host.SocketClosed += (o, e) =>
                                    {
                                        this.Log($"结束转发 udp [{blocks[2]}] --到> [{blocks[1]}]\n");
                                    };
                                    host.StartReceive();
                                    Sockets.Add(host);
                                    this.Log($"开始转发 udp [{blocks[2]}] --到> [{blocks[1]}]\n");
                                    break;
                                default:
                                case "tcp":
                                    var server = new TcpServer((_, client) =>
                                    {
                                        try
                                        {
                                            client.StartHub(new TcpMirrorHub(client, blocks[2], this.GetLimit()));
                                            this.Log($"[{blocks[1]}] 接收到 tcp {client.RemoteIPEndPoint}\n");
                                        }
                                        catch
                                        {
                                        }
                                    });
                                    server.SocketClosed += (o, e) =>
                                    {
                                        this.Log($"结束转发 tcp [{blocks[2]}] --到> [{blocks[1]}]\n");
                                    };
                                    server.Run(blocks[1]);
                                    Sockets.Add(server);
                                    this.Log($"开始转发 tcp [{blocks[2]}] --到> [{blocks[1]}]\n");
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log($"转发失败：${ex.Message}\n");
                        }
                    }
                }
                this.btn_start.Enabled = false;
                this.btn_stop.Enabled = true;
                this.lb_status.Text = STA_RUNNING;
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            foreach (var server in Sockets)
            {
                server.Dispose();
            }
            Sockets.Clear();
            this.btn_start.Enabled = true;
            this.btn_stop.Enabled = false;
            this.lb_status.Text = STA_STOPPED;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Log(string message)
        {
            if (!this.check_logshow.Checked) return;
            if (this.tb_print.InvokeRequired)
            {
                this.tb_print.Invoke(() =>
                {
                    this.tb_print.Text += message;
                });
            }
            else
            {
                this.tb_print.Text += message;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var check = sender as CheckBox;
            if (check != null && !check.Checked)
            {
                this.tb_print.Text = string.Empty;
            }

        }

        private int GetLimit()
        {
            var limit = this.n_limit.Value;
            if (limit == 0)
            {
                return -1;
            }
            return (int)limit << 20;
        }
    }
}