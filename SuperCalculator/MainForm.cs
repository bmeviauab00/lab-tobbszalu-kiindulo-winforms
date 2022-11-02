using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SuperCalculator
{
	public partial class MainForm : Form
	{
		private DataFifo fifo = new DataFifo();

		public MainForm()
		{
			InitializeComponent();
			double p1 = 12.34;
			double p2 = 56.78;
			textBoxParam1.Text = p1.ToString();
			textBoxParam2.Text = p2.ToString();
		}

		private void ShowResult(double[] parameters, double result)
		{
			ListViewItem lvi = listViewResult.Items.Add(parameters[0] + " # " + parameters[1] + " = " + result);
			listViewResult.EnsureVisible(lvi.Index);
			listViewResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private void buttonCalcResult_Click(object sender, EventArgs e)
		{
			double p1, p2 = 0;
			if (Double.TryParse(textBoxParam1.Text, out p1) &&
				(Double.TryParse(textBoxParam2.Text, out p2)))
			{
				double[] parameters = new double[] { p1, p2 };
			}
			else MessageBox.Show(this, "Invalid parameter!", "Error");
		}
	}
}