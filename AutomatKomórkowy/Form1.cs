using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace AutomatKomórkowy
{
    public partial class Form1 : Form
    {
        Automat2D automat;
        BackgroundWorker worker = new BackgroundWorker();
        Bitmap bitmap;
        Graphics graphics;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
                textBox1.Text = "600";
            if (textBox2.TextLength == 0)
                textBox2.Text = "600";
            if (textBox3.TextLength == 0)
                textBox3.Text = "100";

            pictureBox1.Height = Convert.ToInt32(textBox1.Text);
            pictureBox1.Width = Convert.ToInt32(textBox2.Text);

            progressBar1.Location = new Point(pictureBox1.Location.X + (pictureBox1.Width / 4), pictureBox1.Location.Y - 30);

            int size1 = (1 * Convert.ToInt32(textBox1.Text) / 3);
            int size2 = (1 * Convert.ToInt32(textBox2.Text) / 3);

            bitmap = new Bitmap(size1,size2);
            graphics = Graphics.FromImage(bitmap);

            automat = new Automat2D(size1, size2,comboBox1.SelectedIndex);

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            progressBar1.Maximum = Convert.ToInt32(textBox3.Text);
   
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            automat.Iterate(Convert.ToInt32(textBox3.Text), bitmap, pictureBox1,progressBar1,graphics);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            automat.stop();
        }
    }
}
