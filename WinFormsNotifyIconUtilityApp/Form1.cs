using System;
using System.Windows.Forms;

namespace WinFormsNotifyIconUtilityApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnCopyClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject("Hi from MyUtilityApp");
        }
    }
}
