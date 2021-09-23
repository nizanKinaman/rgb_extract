using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart2.Series[0].Color = Color.FromArgb(150, 255, 0, 0);
            chart2.Series[1].Color = Color.FromArgb(150, 0, 255, 0);
            chart2.Series[2].Color = Color.FromArgb(150, 0, 0, 255);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpeg;*.jpg;*.png";
                if (ofd.ShowDialog() == DialogResult.Cancel) return;
                pictureBox1.Image = new Bitmap(ofd.FileName);
                label1.Hide();
                label2.Hide();
                label3.Hide();
                label4.Hide();
                Bitmap[] channels = GetChannel((Bitmap)pictureBox1.Image);
                pictureBox2.Image = channels[0];
                pictureBox3.Image = channels[1];
                pictureBox4.Image = channels[2];
               
            }
        }

        public Bitmap[] GetChannel(Bitmap src)
        {
            Bitmap[] res = new Bitmap[] { new Bitmap(src.Width, src.Height), new Bitmap(src.Width, src.Height), new Bitmap(src.Width, src.Height) };
            int[] arrR = new int[256];
            int[] arrG = new int[256];
            int[] arrB = new int[256];
            for (int i = 0; i < src.Width; i++)
            {
                for (int j = 0; j < src.Height; j++)
                {
                    Color src_col = src.GetPixel(i, j);
                    res[0].SetPixel(i, j, Color.FromArgb(src_col.A, src_col.R, 0, 0));
                    res[1].SetPixel(i, j, Color.FromArgb(src_col.A, 0, src_col.G, 0));
                    res[2].SetPixel(i, j, Color.FromArgb(src_col.A, 0, 0, src_col.B));
                    arrR[src_col.R]++;
                    arrG[src_col.G]++;
                    arrB[src_col.B]++;
                }
            }
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            for (int i = 0; i < 256; i++)
            {
                chart2.Series[0].Points.AddXY(i, arrR[i]);
                chart2.Series[1].Points.AddXY(i, arrG[i]);
                chart2.Series[2].Points.AddXY(i, arrB[i]);
            }
            return res;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                chart2.Series[0].Enabled = true;
            else
                chart2.Series[0].Enabled = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
                chart2.Series[1].Enabled = true;
            else
                chart2.Series[1].Enabled = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
                chart2.Series[2].Enabled = true;
            else
                chart2.Series[2].Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
