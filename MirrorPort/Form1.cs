using NetPs.Tcp;

namespace MirrorPort
{
    public partial class Form1 : Form
    {
        public const string STA_STOPPED = "stopped";
        public const string STA_RUNNING = "running";
        private IList<TcpServer> tcpServers = new List<TcpServer>();
        public Form1()
        {
            InitializeComponent();
            this.btn_start.Enabled = false;
            this.btn_stop.Enabled = false;
            this.lb_status.Text = STA_STOPPED;
            Initialize();

        }

        public async void Initialize()
        {
            using (var fs = new FileStream("mirror_config.ini", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                
                var reader = new StreamReader(fs);
                while(! reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (line == null) continue;
                    var blocks = line.Split(' ');
                    if (blocks.Length == 2)
                    {
                        try
                        {
                            var server = new TcpServer((_, client) =>
                            {
                                client.StartMirror(blocks[1]);
                            });
                            server.Run(blocks[0]);
                            tcpServers.Add(server);
                        }
                        catch
                        {
                            // ...
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
            foreach(var server in tcpServers)
            {
                server.Close();
                server.Dispose();
            }
            tcpServers.Clear();
            this.btn_start.Enabled = true;
            this.btn_stop.Enabled = false;
            this.lb_status.Text = STA_STOPPED;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Initialize();
        }
    }
}