using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class Messege : Form
    {
        public Messege(messegeType type, string messege)
        {
            InitializeComponent();
            isAssept = false;
            switch (type)
            {
                case messegeType.done:
                    label1.Text = "Готово";
                    pictureBox1.Image = imageList1.Images[0];
                    break;
                case messegeType.error:
                    label1.Text = "Ошибка";
                    pictureBox1.Image = imageList1.Images[1];
                    break;
                case messegeType.warning:
                    label1.Text = "Удаление";
                    pictureBox1.Image = imageList1.Images[2];
                    break;
            }
            label2.Text = messege;
        }
        public bool isAssept=false;
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            isAssept = true;
        }
        public enum messegeType
        {
            done, error, warning
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
