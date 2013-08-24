using System.Drawing;
using System.Windows.Forms;
using STM.UI.Common;
using STM.UI.Properties;

namespace STM.UI.Forms.MainForm
{
    public partial class MainForm : Form
    {
        private readonly NotifyIconManager trayManager;

        public MainForm()
        {
            this.InitializeComponent();

            this.trayManager = new NotifyIconManager(this);
            this.trayManager.NotifyIcon.Icon = Resources.CrossCircleIco;
            this.trayManager.NotifyIcon.Text = "some text for ni text";

            var showHideMenuItem = new ToolStripMenuItem("Show/Hide");
            showHideMenuItem.Font = new Font(showHideMenuItem.Font, FontStyle.Bold);
            showHideMenuItem.Click += (s, a) => this.trayManager.SwitchFormState();

            this.trayManager.NotifyIcon.ContextMenuStrip.Items.Add(showHideMenuItem);
            this.trayManager.NotifyIcon.ContextMenuStrip.Items.Add("-");
            this.trayManager.NotifyIcon.ContextMenuStrip.Items.Add("Exit", null, (s, a) => this.trayManager.CloseForm());
        }
    }
}
