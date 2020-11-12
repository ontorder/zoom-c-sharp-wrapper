using System;
using System.Windows.Forms;

namespace zoom_sdk_demo
{
    public partial class Commands : Form
    {
        public Commands()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var id = int.Parse(textBox1.Text);
            Program.zd.SetLayout(id);
        }
    }
}
