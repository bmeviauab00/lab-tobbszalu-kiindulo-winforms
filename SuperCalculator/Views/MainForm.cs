using SuperCalculator.Data;

using System;
using System.Windows.Forms;

namespace SuperCalculator;

public partial class MainForm : Form
{
    private readonly DataFifo _fifo = new DataFifo();

    public MainForm()
    {
        InitializeComponent();

        textBoxParam1.Text = 12.34.ToString();
        textBoxParam2.Text = 56.78.ToString();
    }

    private void ShowResult(double[] parameters, double result)
    {
        var lvi = listViewResult.Items.Add($"{parameters[0]} #  {parameters[1]} = {result}");
        listViewResult.EnsureVisible(lvi.Index);
        listViewResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    }

    private void buttonCalcResult_Click(object sender, EventArgs e)
    {
        if (double.TryParse(textBoxParam1.Text, out var p1) && double.TryParse(textBoxParam2.Text, out var p2))
        {
            var parameters = new double[] { p1, p2 };

            // TODO Call Algorithms.dll
        }
        else
        {
            MessageBox.Show(this, "Invalid parameter!", "Error");
        }
    }
}