using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Match3
{
	public partial class StartForm : Form
	{
		public StartForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Hide();

			MainForm form = new MainForm();
			form.Show();

			form.FormClosing += MainFormClosing_Click;

		}

		private void MainFormClosing_Click(object sender, EventArgs e)
		{
			Show();
		}
	}
}
