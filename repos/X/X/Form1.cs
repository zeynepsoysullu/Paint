using System.Drawing.Imaging;

namespace X
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Width = 1200;
            this.Height = 700;
            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pictureBox1.Image = bm;
        }
        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;

        Pen pencil = new Pen(Color.Black, 1);
        int index;
        static int widthPen = 0;
        Pen eraser = new Pen(Color.White, widthPen);
        int x, y, sX, sY, cX, cY;


        ColorDialog cd = new ColorDialog();
        Color new_color;
        Image file;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;

            cX = e.X;
            cY = e.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Image = bm;
            index = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pictureBox1.BackColor = cd.Color;
            pencil.Color = cd.Color;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (file != null)
            {

                SaveFileDialog f = new SaveFileDialog();
                f.Filter = "JPG (*.JPG)|*.jpg";
                if (f.ShowDialog() == DialogResult.OK)
                {
                    file.Save(f.FileName);

                }

                file = null;


            }
            else
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "Image(*.jpg)| *.jpg|(*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Bitmap btm = bm.Clone(new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height), bm.PixelFormat);
                    btm.Save(sfd.FileName, ImageFormat.Jpeg);
                }
            }





        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG (*.JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                file = Image.FromFile(f.FileName);
                pictureBox1.Image = file;
            }
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
         
            pictureBox1.ImageLocation = f.FileName;

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = 6;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            pencil.Width = Int32.Parse(comboBox1.SelectedItem.ToString());

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if (index == 1)
                {
                    px = e.Location;
                    g.DrawLine(pencil, px, py);
                    py = px;
                }
                if (index == 2)
                {
                    px = e.Location;
                    g.DrawLine(eraser, px, py);
                    py = px;
                }

                pictureBox1.Refresh();

                x = e.X;
                y = e.Y;
                sX = e.X - cX;
                sY = e.Y - cY;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            sX = x - cX;
            sY = y - cY;
            if (index == 3)
            {
                g.DrawEllipse(pencil, cX, cY, sX, sY);
            }
            if (index == 4)
            {
                g.DrawRectangle(pencil, cX, cY, sX, sY);
            }
            if (index == 5)
            {
                g.DrawLine(pencil, cX, cY, x, y);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            index = 5;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (paint)

                if (index == 3)
                {
                    g.DrawEllipse(pencil, cX, cY, sX, sY);
                }
            if (index == 4)
            {
                g.DrawRectangle(pencil, cX, cY, sX, sY);
            }
            if (index == 5)
            {
                g.DrawLine(pencil, cX, cY, x, y);
            }
        }
    }
}