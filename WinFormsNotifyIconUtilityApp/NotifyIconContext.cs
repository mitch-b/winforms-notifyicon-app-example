using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace WinFormsNotifyIconUtilityApp
{
    /// <summary>
    /// Key components straight from https://www.codeproject.com/tips/627796/doing-a-notifyicon-program-the-right-way
    /// </summary>
    public class NotifyIconContext : ApplicationContext
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem quitMenuItem;
        private Form1 form;

        public NotifyIconContext()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            this.notifyIcon = new NotifyIcon();
            InitializeComponent();
            this.notifyIcon.Visible = true;
        }

        private void InitializeComponent()
        {
            notifyIcon.Text = "MyUtilityApp";
            notifyIcon.Icon = new Icon("Assets/azure-devops.ico");

            notifyIcon.MouseUp += NotifyIcon_MouseUp;

            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.SuspendLayout();
            contextMenuStrip.Name = "ContextMenu";
            contextMenuStrip.Size = new Size(153, 70);


            quitMenuItem = new ToolStripMenuItem();
            quitMenuItem.Name = "Quit";
            quitMenuItem.Size = new Size(152, 22);
            quitMenuItem.Text = "Quit";
            quitMenuItem.Click += new EventHandler(this.QuitMenuItem_Click);

            contextMenuStrip.Items.AddRange(new [] { this.quitMenuItem });

            contextMenuStrip.ResumeLayout(false);
            notifyIcon.ContextMenuStrip = contextMenuStrip;
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
        }

        private void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);
            }
            else
            {
                if (form == null || form.IsDisposed)
                {
                    form = new Form1();
                }
                form.Left = Screen.PrimaryScreen.WorkingArea.Right - form.Width;
                form.Top = Screen.PrimaryScreen.WorkingArea.Bottom - form.Height;
                form.Show();
            }
        }

        private void QuitMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
