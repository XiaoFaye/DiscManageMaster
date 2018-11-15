using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DiscManageMaster.Core;

namespace DiscManageMaster
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Click += delegate(object sender, EventArgs e)
            {
                System.Diagnostics.Process.Start("iexplore.exe", "http://www.us-soft.com/buynow.html");
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Please enter your name!", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }

            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Please enter your registeration code!", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return;
            }

            if (PublicVar.IsKeyValue(textBox2.Text.Trim(), textBox1.Text.Trim()))
            {
                MessageBox.Show("Congratulations! Your copy of Personal Files Manager is licnesed.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("The registeration code is not valid! Please input again.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
