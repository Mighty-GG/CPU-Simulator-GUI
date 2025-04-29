using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;
using System.Drawing.Imaging;
using System.IO;

namespace CpuSchedulingWinForms
{
    public partial class CpuScheduler : Form
    {

        List<PerformanceMetrics> allMetrics = new List<PerformanceMetrics>();

        public CpuScheduler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dashBoardTab.Show();
            this.tabSelection.SelectTab(0);
            this.sidePanel.Height = btnDashBoard.Height;
            this.sidePanel.Top = btnDashBoard.Top;

            //this.btnProductCode.BackColor = Color.Transparent;
            //this.btnCpuScheduler.BackColor = Color.Transparent;
            //this.btnDashBoard.BackColor = Color.DimGray;
        }

        private void btnCpuScheduler_Click(object sender, EventArgs e)
        {
            //this.dashBoardTab.Show();
            this.tabSelection.SelectTab(1);
            this.sidePanel.Height = btnCpuScheduler.Height;
            this.sidePanel.Top = btnCpuScheduler.Top;

            //this.btnProductCode.BackColor = Color.Transparent;         
            //this.btnDashBoard.BackColor = Color.Transparent;
            //this.btnCpuScheduler.BackColor = Color.DimGray;
        }


        private void btnFCFS_Click(object sender, EventArgs e)
        {
            if (txtProcess.Text != "")
            {
                var metrics = Algorithms.fcfsAlgorithm(txtProcess.Text);
                if (metrics != null)
                {
                    allMetrics.Add(metrics);

                    string summary =
                        $"Algorithm: {metrics.AlgorithmName}\n" +
                        $"Avg Waiting Time: {metrics.AverageWaitingTime:F2} sec\n" +
                        $"Avg Turnaround Time: {metrics.AverageTurnaroundTime:F2} sec\n" +
                        $"CPU Utilization: {metrics.CPUUtilization:F2}%\n" +
                        $"Throughput: {metrics.Throughput:F2} processes/sec";

                    MessageBox.Show(summary, "FCFS Performance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Enter number of processes", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProcess.Focus();
            }
        }

        private void btnSJF_Click(object sender, EventArgs e)
        {
            if (txtProcess.Text != "")
            {
                var metrics = Algorithms.sjfAlgorithm(txtProcess.Text);
                if (metrics != null)
                {
                    allMetrics.Add(metrics);

                    string summary =
                        $"Algorithm: {metrics.AlgorithmName}\n" +
                        $"Avg Waiting Time: {metrics.AverageWaitingTime:F2} sec\n" +
                        $"Avg Turnaround Time: {metrics.AverageTurnaroundTime:F2} sec\n" +
                        $"CPU Utilization: {metrics.CPUUtilization:F2}%\n" +
                        $"Throughput: {metrics.Throughput:F2} processes/sec";

                    MessageBox.Show(summary, "SJF Performance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Enter number of processes", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProcess.Focus();
            }

        }

        private void btnPriority_Click(object sender, EventArgs e)
        {
            if (txtProcess.Text != "")
            {
                var metrics = Algorithms.priorityAlgorithm(txtProcess.Text);
                if (metrics != null)
                {
                    allMetrics.Add(metrics);

                    string summary =
                        $"Algorithm: {metrics.AlgorithmName}\n" +
                        $"Avg Waiting Time: {metrics.AverageWaitingTime:F2} sec\n" +
                        $"Avg Turnaround Time: {metrics.AverageTurnaroundTime:F2} sec\n" +
                        $"CPU Utilization: {metrics.CPUUtilization:F2}%\n" +
                        $"Throughput: {metrics.Throughput:F2} processes/sec";

                    MessageBox.Show(summary, "Priority Scheduling Performance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Enter number of processes", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProcess.Focus();
            }

        }

        private void txtProcess_TextChanged(object sender, EventArgs e)
        {

        }

        private void restartApp_Click(object sender, EventArgs e)
        {
            this.Hide();
            CpuScheduler cpuScheduler = new CpuScheduler();
            cpuScheduler.ShowDialog();
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            if (txtCodeInput.Text != "")
            {
                string barcode = txtCodeInput.Text;
                //Code128BarcodeDraw barcode = BarcodeDrawFactory.Code128WithChecksum;
                //pictureBoxCodeOutput.Image = barcode.Draw(barcodeInput, 30);
                //pictureBoxCodeOutput.Height = barcode.Draw(txtCodeInput.Text, 150).Height;
                //pictureBoxCodeOutput.Width = barcode.Draw(txtCodeInput.Text, 150).Width;

                Bitmap bitmap = new Bitmap(barcode.Length * 36, 109);   //40, 150
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    Font font = new Font("IDAutomationHC39M Free Version", 25);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush black = new SolidBrush(Color.Black);
                    SolidBrush white = new SolidBrush(Color.White);
                    graphics.FillRectangle(white, 0, 0, bitmap.Width, bitmap.Height);
                    graphics.DrawString(barcode, font, black, point);
                }
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    pictureBoxCodeOutput.Image = bitmap;
                    //pictureBoxCodeOutput.Height = bitmap.Height;
                    //pictureBoxCodeOutput.Width = bitmap.Width;
                }
            }
            else
            {
                MessageBox.Show("No Input", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodeInput.Focus();
            }
            
        }

        private void btnQrcode_Click(object sender, EventArgs e)
        {
            if (txtCodeInput.Text != "")
            {
                CodeQrBarcodeDraw codeQr = BarcodeDrawFactory.CodeQr;
                pictureBoxCodeOutput.Image = codeQr.Draw(txtCodeInput.Text, 100);
                //string barcode = txtCodeInput.Text;
                //Bitmap bitmap = new Bitmap(barcode.Length * 40, 150);
                //pictureBoxCodeOutput.Image = bitmap; 
            }
            else
            {
                MessageBox.Show("No Input", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodeInput.Focus();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pictureBoxCodeOutput.Image == null)
            {
                return;
            }
            else if (pictureBoxCodeOutput.Image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "PNG|*.png|JPEG|*.jpeg|ICON|*.ico"})
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBoxCodeOutput.Image.Save(saveFileDialog.FileName);
                    }
                }
            }           
        }

        private void btnProductCode_Click(object sender, EventArgs e)
        {
            //this.dashBoardTab.Show();
            this.tabSelection.SelectTab(2);
            this.sidePanel.Height = btnProductCode.Height;
            this.sidePanel.Top = btnProductCode.Top;
            
            //this.btnCpuScheduler.BackColor = Color.Transparent;
            //this.btnDashBoard.BackColor = Color.Transparent;
            //this.btnProductCode.BackColor = Color.DimGray;
        }

        private void CpuScheduler_Load(object sender, EventArgs e)
        {
            this.sidePanel.Height = btnDashBoard.Height;
            this.sidePanel.Top = btnDashBoard.Top;
            this.progressBar1.Increment(5);
            this.progressBar2.Increment(17);
            listView1.View = View.Details;
            listView1.GridLines = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Application.Exit();
            timer1.Start();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void btnRoundRobin_Click(object sender, EventArgs e)
        {
            if (txtProcess.Text != "")
            {
                var metrics = Algorithms.roundRobinAlgorithm(txtProcess.Text);
                if (metrics != null)
                {
                    allMetrics.Add(metrics);

                    string summary =
                        $"Algorithm: {metrics.AlgorithmName}\n" +
                        $"Avg Waiting Time: {metrics.AverageWaitingTime:F2} sec\n" +
                        $"Avg Turnaround Time: {metrics.AverageTurnaroundTime:F2} sec\n" +
                        $"CPU Utilization: {metrics.CPUUtilization:F2}%\n" +
                        $"Throughput: {metrics.Throughput:F2} processes/sec";

                    MessageBox.Show(summary, "Round Robin Performance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Enter number of processes", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProcess.Focus();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this.Opacity > 0.0)
            {
                this.Opacity -= 0.021;
            } else
            {
                timer1.Stop();
                Application.Exit();
            }
        }

        private void txtCodeInput_Click(object sender, EventArgs e)
        {
            this.txtCodeInput.Clear();
        }

        //---------------------------------------
        private void btnSRTF_Click(object sender, EventArgs e)
        {
            if (txtProcess.Text != "")
            {
                var metrics = Algorithms.srtfAlgorithm(txtProcess.Text);
                if (metrics != null)
                {
                    allMetrics.Add(metrics);

                    string summary =
                        $"Algorithm: {metrics.AlgorithmName}\n" +
                        $"Avg Waiting Time: {metrics.AverageWaitingTime:F2} sec\n" +
                        $"Avg Turnaround Time: {metrics.AverageTurnaroundTime:F2} sec\n" +
                        $"CPU Utilization: {metrics.CPUUtilization:F2}%\n" +
                        $"Throughput: {metrics.Throughput:F2} processes/sec";

                    MessageBox.Show(summary, "SRTF Performance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Enter number of processes", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProcess.Focus();
            }

        }

        private void btnMLFQ_Click(object sender, EventArgs e)
        {
            if (txtProcess.Text != "")
            {
                var metrics = Algorithms.mlfqAlgorithm(txtProcess.Text);
                if (metrics != null)
                {
                    allMetrics.Add(metrics);

                    string summary =
                        $"Algorithm: {metrics.AlgorithmName}\n" +
                        $"Avg Waiting Time: {metrics.AverageWaitingTime:F2} sec\n" +
                        $"Avg Turnaround Time: {metrics.AverageTurnaroundTime:F2} sec\n" +
                        $"CPU Utilization: {metrics.CPUUtilization:F2}%\n" +
                        $"Throughput: {metrics.Throughput:F2} processes/sec";

                    MessageBox.Show(summary, "MLFQ Performance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Enter number of processes", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProcess.Focus();
            }   

        }

        private void btnCompareAll_Click(object sender, EventArgs e)
        {
            ShowComparisonSummary(allMetrics);
        }

        private void ShowComparisonSummary(List<PerformanceMetrics> metricsList)
        {
            if (metricsList.Count == 0)
            {
                MessageBox.Show("No algorithm metrics available to compare. Please run some algorithms first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StringBuilder summary = new StringBuilder();
            summary.AppendLine("Algorithm Performance Comparison:\n");

            foreach (var metrics in metricsList)
            {
                summary.AppendLine($"{metrics.AlgorithmName}:");
                summary.AppendLine($" - Avg Waiting Time: {metrics.AverageWaitingTime:F2} sec");
                summary.AppendLine($" - Avg Turnaround Time: {metrics.AverageTurnaroundTime:F2} sec");
                summary.AppendLine($" - CPU Utilization: {metrics.CPUUtilization:F2}%");
                summary.AppendLine($" - Throughput: {metrics.Throughput:F2} processes/sec\n");
            }

            MessageBox.Show(summary.ToString(), "All Algorithms Comparison", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}