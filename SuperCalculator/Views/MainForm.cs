using SuperCalculator.Data;

using System;
using System.Threading;
using System.Windows.Forms;

namespace SuperCalculator;

public partial class MainForm : Form
{
    private readonly DataFifo _fifo = new DataFifo();
    private bool _isClosed = false;

    public MainForm()
    {
        InitializeComponent();

        textBoxParam1.Text = 12.34.ToString();
        textBoxParam2.Text = 56.78.ToString();

        new Thread(WorkerThread) { Name = "Szal1" }.Start();
        new Thread(WorkerThread) { Name = "Szal2" }.Start();
        new Thread(WorkerThread) { Name = "Szal3" }.Start();
    }

    private void ShowResult(double[] parameters, double result)
    {
        if (_isClosed)
            return;

        if (InvokeRequired)
        {
            Invoke(ShowResult, new object[] { parameters, result });
        }
        else if (!IsDisposed)
        {
            var lvi = listViewResult.Items.Add($"{parameters[0]} #  {parameters[1]} = {result}");
            listViewResult.EnsureVisible(lvi.Index);
            listViewResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }

    private void buttonCalcResult_Click(object sender, EventArgs e)
    {
        if (double.TryParse(textBoxParam1.Text, out var p1) && double.TryParse(textBoxParam2.Text, out var p2))
        {
            var parameters = new double[] { p1, p2 };

            _fifo.Put(parameters);
        }
        else
        {
            MessageBox.Show(this, "Invalid parameter!", "Error");
        }
    }

    private void CalculatorThread(object arg)
    {
        var parameters = (double[])arg;
        var result = Algorithms.SuperAlgorithm.Calculate(parameters);
        ShowResult(parameters, result);
    }

    private void WorkerThread()
    {
        while (!_isClosed)
        {
            if (_fifo.TryGet(out var data))
            {
                double result = Algorithms.SuperAlgorithm.Calculate(data);
                ShowResult(data, result);
            }

            //Thread.Sleep(500);
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        _isClosed= true;
        _fifo.Release();
    }
}