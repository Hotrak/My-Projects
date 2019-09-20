using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BD
{
    public partial class UserControl1 : UserControl
    {
        List<Button> Buttons = new List<Button>();
        List<ComboBox> ColorComboBoxes = new List<ComboBox>();
        List<Label> LabelsCount = new List<Label>();
        List<Label> LabelsPrise = new List<Label>();
        List<NumericUpDown> Numers = new List<NumericUpDown>();
        public UserControl1()
        {
            InitializeComponent();

            //addFirst();

            //CreateButton("64");
            //CreateComboBoxColor();
            //CreateLableCount("12");
            //CreateLablePrise("10000");
        }
        public void addFirst()
        {
            button2.ForeColor = Color.FromArgb(248, 149, 16);
            Buttons.Add(button2);
            comboBox1.Items.Clear();
            ColorComboBoxes.Add(comboBox1);
            LabelsCount.Add(label49);
            LabelsPrise.Add(label23);
            numericUpDown1.Enabled = false;
            Numers.Add(numericUpDown1);
        }
        private void RemoveAll()
        {
            int countElem = Buttons.Count;
            for (int i = countElem - 1; i > 0; i--)
            {
                if (count > 0)
                {
                    Buttons[i].Dispose();
                    ColorComboBoxes[i].Dispose();
                    LabelsCount[i].Dispose();
                    LabelsPrise[i].Dispose();
                    Numers[i].Dispose();
                    count--;

                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool isFind = false;
            bool isHea = false;
            for (int i = 0; i < Numers.Count; i++)
            {
                if (Numers[i].Enabled && Numers[i].Value != 0)
                {

                    int id = idFleshMems[i][ColorComboBoxes[i].SelectedIndex];

                    int fleshMem = int.Parse(Buttons[i].Text.Replace(" ГБ", ""));
                    int count = int.Parse(Numers[i].Value.ToString());
                    int MaxCount = int.Parse(LabelsCount[i].Text);
                    double prise = double.Parse(LabelsPrise[i].Text);
                    string model = label2.Text;
                    string color = ColorComboBoxes[i].Text;

                    for (int j = 0; j < Form5.bascet.Count; j++)
                    {
                        if (Form5.bascet[j].fleshMem == fleshMem && Form5.bascet[j].color == color && Form5.bascet[j].idPhone == idPhone)
                        {
                            Form5.bascet[j].count += count;
                            isHea = true;
                        }
                    }
                    if (!isHea)
                        Form5.bascet.Add(new Bascet(id, count, prise, model, pictureBox1.Image, color, fleshMem, MaxCount, idPhone));
                    isFind = true;



                    string query = $"SELECT idColor FROM Colors WHERE color = '{color}'";
                    OleDbCommand command = new OleDbCommand(query, Form5.myConnection);
                    string idColor = command.ExecuteScalar().ToString();

                    query = $"SELECT idFleshMam FROM FleshMem WHERE FleshMamari = {fleshMem}";
                    command = new OleDbCommand(query, Form5.myConnection);
                    string idFlesh = command.ExecuteScalar().ToString();

                    query = $"UPDATE Stock SET colech = {int.Parse(LabelsCount[i].Text) - Numers[i].Value} WHERE idPhone = {idPhone} AND idFleshMem = {idFlesh} AND idColor = {idColor}";
                    command = new OleDbCommand(query, Form5.myConnection);
                    command.ExecuteNonQuery();
                }
            }
            if (isFind)
            {
                if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenBascket();
                }
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Вы не выбрали размер Флэш-памяти или не указали её количество");
                mes.ShowDialog();
            }


        }
        List<List<string>> counts = new List<List<string>>();
        List<List<int>> idFleshMems = new List<List<int>>();

        public int idPhone;
        public void LoadData(int id = 0)
        {
            idPhone = id;
            counts.Clear();
            counts = new List<List<string>>();
            idFleshMems.Clear();
            idFleshMems = new List<List<int>>();
            for (int i = 0; i < Buttons.Count; i++)
            {
                RemoveAll();
            }
            Buttons.Clear();
            ColorComboBoxes.Clear();
            LabelsCount.Clear();
            LabelsPrise.Clear();
            Numers.Clear();
            addFirst();
            string query = $"SELECT c.FleshMamari,a.colech,a.prise,b.color,c.idFleshMam FROM Stock a, Colors b,FleshMem c WHERE" +
                $" a.idPhone={id} AND c.idFleshMam = a.idFleshMem AND b.idColor=a.idColor AND a.chatged = FALSE ORDER BY c.FleshMamari";
            //string query = $"SELECT a.FleshMamari,b.colech,a.prise,b.color,a.idFleshMam FROM FleshMem a,Colors b  WHERE a.idPhone={id} AND b.idFleshMem = a.idFleshMam";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                button2.Text = reader[0].ToString() + " ГБ";
                comboBox1.Items.Add(reader[3].ToString());
                label49.Text = reader[1].ToString();
                label23.Text = reader[2].ToString();
                numericUpDown1.Maximum = int.Parse(reader[1].ToString());
                counts.Add(new List<string>());
                counts[count].Add(reader[1].ToString());

                idFleshMems.Add(new List<int>());
                idFleshMems[count].Add(int.Parse(reader[4].ToString()));

                comboBox1.SelectedIndex = 0;
            }

            while (reader.Read())
            {

                if (Buttons[count].Text != reader[0].ToString() + " ГБ")
                {
                    count++;
                    CreateButton(reader[0].ToString() + " ГБ");
                    CreateComboBoxColor(reader[3].ToString());
                    CreateLableCount(reader[1].ToString());
                    CreateLablePrise(reader[2].ToString());
                    CreateNumber(int.Parse(reader[1].ToString()));

                    counts.Add(new List<string>());
                    counts[count].Add(reader[1].ToString());

                    idFleshMems.Add(new List<int>());
                    idFleshMems[count].Add(int.Parse(reader[4].ToString()));

                }
                else
                {
                    ColorComboBoxes[count].Items.Add(reader[3].ToString());
                    counts[count].Add(reader[1].ToString());

                    idFleshMems[count].Add(int.Parse(reader[4].ToString()));
                }

            }

            query = $"SELECT  model, data, os, ram, sizeSc, fsim,Csim, hDesign, mDesign, scrin, sizeBat," +
                       $" componi, idProc, texScren, soatnoshStor,cam FROM Phons WHERE idPhone={id}";

            command = new OleDbCommand(query, Form5.myConnection);

            reader = command.ExecuteReader();
            //reader.Read();
            if (reader.Read())
            {
                label2.Text = reader[11].ToString() + " " + reader[0].ToString();//Модель
                label20.Text = reader[1].ToString();//Data
                label31.Text = reader[2].ToString();//Os
                label33.Text = reader[3].ToString();//Ram
                label26.Text = reader[4].ToString();
                label35.Text = reader[5].ToString();//Fsim
                label36.Text = reader[6].ToString();//Fsim
                label39.Text = reader[7].ToString();//Hdis
                label40.Text = reader[8].ToString().Replace(".", ",");//mDis
                label32.Text = reader[9].ToString();//razScrin
                label37.Text = reader[10].ToString();//batari
                label38.Text = reader[11].ToString();//compani
                idProcInPhone = int.Parse(reader[12].ToString());//idProcInPhone
                label41.Text = reader[13].ToString();//texScren
                label42.Text = reader[14].ToString();// soatnoshStor
                label25.Text = reader[15].ToString();// cam
            }


            pictureBox1.Image = DBmanager.GetImage(id);
            if (idProcInPhone != 1)
            {
                query = $"SELECT model,speed,nCors,razProc,sGPU,GAccelModl FROM Cors WHERE idProc={idProcInPhone}";

                command = new OleDbCommand(query, Form5.myConnection);

                reader = command.ExecuteReader();
                reader.Read();
                label43.Text = reader[0].ToString();
                label44.Text = reader[1].ToString();
                label45.Text = reader[2].ToString();
                label46.Text = reader[3].ToString();
                label47.Text = reader[4].ToString();
                label48.Text = reader[5].ToString();
                pictureBox2.Visible = false;
                reader.Close();
            }
            else
            {
                label43.Text = "Пусто";
                label44.Text = "Пусто";
                label45.Text = "Пусто";
                label46.Text = "Пусто";
                label47.Text = "Пусто";
                label48.Text = "Пусто";
                pictureBox2.Visible = true;
            }

        }
        int idProcInPhone;
        private void button2_Click(object sender, EventArgs e)
        {

        }
        int count = 0;
        int interval = 35;
        public void CreateButton(string text = "")
        {
            Button button = new Button();
            button.Top = Buttons[count - 1].Top;
            button.Left = Buttons[count - 1].Left;
            button.Font = Buttons[count - 1].Font;
            button.Size = Buttons[count - 1].Size;

            button.FlatStyle = FlatStyle.Flat;
            button.ForeColor = Buttons[count - 1].ForeColor;
            button.FlatAppearance.BorderSize = Buttons[count - 1].FlatAppearance.BorderSize;

            button.Click += button2_Click_1;

            button.Top += interval;

            button.Text = text;
            button.TextAlign = Buttons[count - 1].TextAlign;

            Buttons.Add(button);
            Controls.Add(button);
        }
        public void CreateComboBoxColor(string text = "")
        {
            ComboBox Ccombo = new ComboBox();
            Ccombo.Top = ColorComboBoxes[count - 1].Top;
            Ccombo.Left = ColorComboBoxes[count - 1].Left;
            Ccombo.Font = ColorComboBoxes[count - 1].Font;
            Ccombo.Size = ColorComboBoxes[count - 1].Size;
            Ccombo.DropDownStyle = ComboBoxStyle.DropDownList;

            Ccombo.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            Ccombo.Top += interval;
            Ccombo.Items.Add(text);
            //for (int i = 0; i < 3; i++)
            //{
            //    Ccombo.Items.Add(colorItems[i]);
            //}
            Ccombo.SelectedIndex = 0;
            ColorComboBoxes.Add(Ccombo);
            Controls.Add(Ccombo);
        }
        public void CreateLableCount(string text = "")
        {
            Label label = new Label();
            label.Top = LabelsCount[count - 1].Top;
            label.Left = LabelsCount[count - 1].Left;
            label.Font = LabelsCount[count - 1].Font;
            label.Size = LabelsCount[count - 1].Size;
            label.TextAlign = LabelsCount[count - 1].TextAlign;
            label.ForeColor = LabelsCount[count - 1].ForeColor;

            label.Top += interval;
            label.Text = text;

            LabelsCount.Add(label);
            Controls.Add(label);
        }
        public void CreateLablePrise(string text = "")
        {
            Label label = new Label();
            label.Top = LabelsPrise[count - 1].Top;
            label.Left = LabelsPrise[count - 1].Left;
            label.Font = LabelsPrise[count - 1].Font;
            label.Size = LabelsPrise[count - 1].Size;
            label.TextAlign = LabelsPrise[count - 1].TextAlign;
            label.ForeColor = LabelsPrise[count - 1].ForeColor;

            label.Top += interval;
            label.Text = text;

            LabelsPrise.Add(label);
            Controls.Add(label);
        }
        public void CreateNumber(int maxValue)
        {
            NumericUpDown Numer = new NumericUpDown();
            Numer.Top = Numers[count - 1].Top;
            Numer.Left = Numers[count - 1].Left;
            Numer.Font = Numers[count - 1].Font;
            Numer.Size = Numers[count - 1].Size;

            Numer.Top += interval;

            Numer.TextAlign = HorizontalAlignment.Center;

            Numer.Value = 0;
            Numer.Maximum = maxValue;

            Numer.Enabled = false;
            Numer.ReadOnly = true;

            Numers.Add(Numer);
            Controls.Add(Numer);
        }
        bool isPresd = true;
        string s;
        private void button2_Click_1(object sender, EventArgs e)
        {
            if ((sender as Button).ForeColor == Color.FromArgb(255, 255, 255))
            {
                (sender as Button).ForeColor = Color.FromArgb(248, 149, 16);
                for (int i = 0; i < Buttons.Count; i++)
                {
                    if ((sender as Button) == Buttons[i])
                    {
                        Numers[i].Enabled = false;
                        Numers[i].Minimum = 0;
                        Numers[i].Value = 0;
                    }
                }
            }
            else
            {
                (sender as Button).ForeColor = Color.FromArgb(255, 255, 255);
                for (int i = 0; i < Buttons.Count; i++)
                {
                    if ((sender as Button) == Buttons[i])
                    {
                        //MessageBox.Show("a");
                        Numers[i].Enabled = true;
                        if (Numers[i].Maximum != 0)
                            Numers[i].Value = 1;
                        else
                            Numers[i].Value = 0;

                        if (Numers[i].Value != 0)
                            Numers[i].Minimum = 1;
                        else
                            Numers[i].Minimum = 0;
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < ColorComboBoxes.Count; i++)
            {
                if ((sender as ComboBox) == ColorComboBoxes[i])
                {
                    LabelsCount[i].Text = counts[i][(sender as ComboBox).SelectedIndex];
                    //MessageBox.Show(idFleshMems[i][(sender as ComboBox).SelectedIndex].ToString());
                    Numers[i].Maximum = int.Parse(LabelsCount[i].Text);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenCheng(idPhone);
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            panel1.Visible = true;
        }

        private void UserControl1_MouseMove(object sender, MouseEventArgs e)
        {
            panel1.Visible = false;
        }
    }
    public class Bascet
    {
        public Bascet(int idFleshMem, int count, double prise, string model, Image img, string color, int fleshMem, int Maxcount, int idPhone)
        {
            this.model = model;
            this.idFleshMem = idFleshMem;
            this.count = count;
            this.prise = prise;
            this.img = img;
            this.color = color;
            this.fleshMem = fleshMem;
            this.Maxcount = Maxcount;
            this.idPhone = idPhone;
        }

        public int idPhone;
        public string model;
        public string color;
        public int fleshMem;
        public int idFleshMem;
        public int count;
        public int Maxcount;
        public double prise;
        public Image img;


    }
}
