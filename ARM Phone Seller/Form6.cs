using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class Form6 : Form
    {

        public Form6(int idPhone=-1)
        {
            this.id = idPhone;
            InitializeComponent();
            LoadColor();
            comboBox1.Items.Clear();
            for (int i = 0; i < colors.Count; i++)
            {
                comboBox1.Items.Add(colors[i]);
            }
            button5.Tag = 0 + "";
            MemComboBoxes.Add(comboBox5);
            ColorComboBoxes.Add(comboBox1);
            NumericUpDowns.Add(numericUpDown1);
            TextBoxes.Add(textBox1);
            Buttons.Add(button5);
            if (idPhone != -1)
            {
                LoadData(idPhone);
                Text = "Изменение Флэш-памяти";
                idDeleteStocks.Clear();
            }
            chengd = true;

        }
        public int id;
        public void LoadInfoMem()
        {

        }
        public static List<int> idStocks = new List<int>();
        public static List<int> idDeleteStocks = new List<int>();
        public void LoadData(int id=-1)
        {
            //this.MouseDown += panel1_MouseDown;
           
            this.id = id;
            string query = $"SELECT c.FleshMamari,b.color,a.colech,a.prise,a.idStock FROM STOCK a, Colors b,FleshMem c WHERE" +
                $" a.idPhone={id} AND c.idFleshMam = a.idFleshMem AND b.idColor=a.idColor AND chatged = FALSE ORDER BY a.idFleshMem";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            idStocks.Clear();
            if (reader.Read())
            {
                comboBox5.Text = reader[0].ToString();
                comboBox1.Text = reader[1].ToString();
                numericUpDown1.Value = int.Parse(reader[2].ToString());
                textBox1.Text = reader[3].ToString();
                
                idStocks.Add(int.Parse(reader[4].ToString()));
            }
            
            while (reader.Read())
            {
                count++;
                CreateComboBox(reader[0].ToString());
                CreateComboBoxColor(reader[1].ToString());
                CreateNumber(int.Parse(reader[2].ToString()));
                CreateTextBox(reader[3].ToString());
                CreateButton();
                idStocks.Add(int.Parse(reader[4].ToString()));

                if (count > 4)
                {
                    Height += 30;
                    panel1.Height += 30;
                    button2.Top += 30;
                }
                
            }

            UserControl3.MemComboBoxes = MemComboBoxes;
            UserControl3.ColorComboBoxes = ColorComboBoxes;
            UserControl3.NumericUpDowns = NumericUpDowns;
            UserControl3.TextBoxes = TextBoxes;

            UserControl3.LastMemComboBoxes = MemComboBoxes;
            UserControl3.LastColorComboBoxes = ColorComboBoxes;
            UserControl3.LastNumericUpDowns = NumericUpDowns;
            UserControl3.LastTextBoxes = TextBoxes;

        }
        List<ComboBox> MemComboBoxes = new List<ComboBox>();
        List<ComboBox> ColorComboBoxes = new List<ComboBox>();
        List<NumericUpDown> NumericUpDowns = new List<NumericUpDown>();
        List<TextBox> TextBoxes = new List<TextBox>();
        List<Button> Buttons = new List<Button>();

        
        int count;
        int interval=30;
        

        private void DeleteFlesh(int id)
        {
            if (count > 0)
            {
                MemComboBoxes[id].Dispose();
                MemComboBoxes.RemoveAt(id);

                ColorComboBoxes[id].Dispose();
                ColorComboBoxes.RemoveAt(id);

                NumericUpDowns[id].Dispose();
                NumericUpDowns.RemoveAt(id);

                TextBoxes[id].Dispose();
                TextBoxes.RemoveAt(id);
                if (this.id != -1)
                {
                    try {
                        idDeleteStocks.Add(idStocks[id]);
                        idStocks.RemoveAt(id);
                    } catch (Exception) { }
                    
                }


                Buttons[id].Dispose();
                Buttons.RemoveAt(id);
                for (int i = 0; i < NumericUpDowns.Count; i++)
                {
                    if (i >= id)
                    {
                        MemComboBoxes[i].Top -= 30;
                       
                        ColorComboBoxes[i].Top -= 30;
                        
                        NumericUpDowns[i].Top -= 30;
                        
                        TextBoxes[i].Top -= 30;
                       
                        Buttons[i].Top -= 30;
                    }
                    Buttons[i].Tag = i;
                }

                count--;
                if (count >= 4)
                {
                    Height -= 30;
                    panel1.Height -= 30;
                    button2.Top -= 30;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (count<25)
            {
                count++;
                CreateComboBox();
                CreateComboBoxColor();
                CreateNumber();
                CreateTextBox();
                CreateButton();
                if (count > 4)
                {
                    Height += 30;
                    panel1.Height += 30;
                    button2.Top += 30;
                }
            }
        }
        List<string> colors = new List<string>();
        private void LoadColor()
        {
            string quere = "SELECT color FROM Colors";
            OleDbCommand command = new OleDbCommand(quere,Form5.myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add(reader[0].ToString());
            }
        }
        public void CreateComboBox(string text="")
        {
            ComboBox Mcombo = new ComboBox();

            Mcombo.Top = MemComboBoxes[count - 1].Top;
            Mcombo.Left = MemComboBoxes[count - 1].Left;
            Mcombo.Font = MemComboBoxes[count - 1].Font;
            Mcombo.Size = MemComboBoxes[count - 1].Size;
            Mcombo.DropDownStyle = ComboBoxStyle.DropDownList;

            Mcombo.SelectedIndexChanged += comboBox5_SelectedIndexChanged;
            for (int i = 8; i < 500; i *= 2)
                Mcombo.Items.Add(i);
            if (text != "")
                Mcombo.SelectedIndex = UserControl3.CombSetValue(Mcombo, text);
            Mcombo.Name = count + "M";
            //if(isF)
            Mcombo.Top += interval;

            MemComboBoxes.Add(Mcombo);
            Controls.Add(Mcombo);
            Mcombo.BringToFront();
            //Controls.Add(Mcombo);
            isF = true;
        }
        bool isF = false;
        public void CreateComboBoxColor(string text="")
        {
            ComboBox Ccombo = new ComboBox();
            Ccombo.Top = ColorComboBoxes[count - 1].Top;
            Ccombo.Left = ColorComboBoxes[count - 1].Left;
            Ccombo.Font = ColorComboBoxes[count - 1].Font;
            Ccombo.Size = ColorComboBoxes[count - 1].Size;

            Ccombo.Name = count + "C";
            Ccombo.Top += interval;
            Ccombo.Text = text;
            for (int i = 0; i < colors.Count; i++)
            {
                Ccombo.Items.Add(colors[i]);
            }

            ColorComboBoxes.Add(Ccombo);
            Controls.Add(Ccombo);
            Ccombo.BringToFront();
        }
        public void CreateNumber(int value=0)
        {
            NumericUpDown Numer = new NumericUpDown();
            Numer.Top = NumericUpDowns[count - 1].Top;
            Numer.Left = NumericUpDowns[count - 1].Left;
            Numer.Font = NumericUpDowns[count - 1].Font;
            Numer.Size = NumericUpDowns[count - 1].Size;

            Numer.Top += interval;
            Numer.Name = count + "N";
            Numer.TextAlign = HorizontalAlignment.Center;
            Numer.Maximum = 1500;
            Numer.Value = value;

            NumericUpDowns.Add(Numer);
            Controls.Add(Numer);
            Numer.BringToFront();
        }
        public void CreateTextBox(string text="")
        {
            TextBox textBox = new TextBox();
            textBox.Top = TextBoxes[count - 1].Top;
            textBox.Left = TextBoxes[count - 1].Left;
            textBox.Font = TextBoxes[count - 1].Font;
            textBox.Size = TextBoxes[count - 1].Size;

            textBox.Top += interval;
            textBox.Name = count + "T";
            textBox.KeyPress += textBox1_KeyPress;
            textBox.Text = text;
            textBox.TextChanged += textBox1_TextChanged;

            TextBoxes.Add(textBox);
            Controls.Add(textBox);
            textBox.BringToFront();
        }
        public void CreateButton(string text = "")
        {
            Button button = new Button();
            button.Top = Buttons[count - 1].Top;
            button.Left = Buttons[count - 1].Left;
            //button.Font = Buttons[count - 1].Font;
            button.Size = Buttons[count - 1].Size;
            button.BackColor = Buttons[count - 1].BackColor;
            button.FlatStyle = Buttons[count - 1].FlatStyle;
            button.Image = Buttons[count - 1].Image;
            button.FlatAppearance.BorderSize = 0;
            button.Tag = count.ToString();
            button.Click += DeleteButtonCleeck;
            button.Top += interval;
            button.Name = count + "T";
            //button.KeyPress += textBox1_KeyPress;
            //button.TextChanged += textBox1_TextChanged;

            Buttons.Add(button);
            Controls.Add(button);
            button.BringToFront();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void DeleteButtonCleeck(object sender, EventArgs e)
        {
            DeleteFlesh(int.Parse((sender as Button).Tag.ToString()));
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (count > 0)
            {
                MemComboBoxes[MemComboBoxes.Count - 1].Dispose();
                MemComboBoxes.RemoveAt(MemComboBoxes.Count - 1);

                ColorComboBoxes[ColorComboBoxes.Count - 1].Dispose();
                ColorComboBoxes.RemoveAt(ColorComboBoxes.Count - 1);

                NumericUpDowns[NumericUpDowns.Count - 1].Dispose();
                NumericUpDowns.RemoveAt(NumericUpDowns.Count-1);

                TextBoxes[TextBoxes.Count - 1].Dispose();
                TextBoxes.RemoveAt(TextBoxes.Count - 1);

                count--;
                if (count >= 4)
                {
                    Height -= 30;
                    panel1.Height -= 30;
                    button2.Top -= 30;
                }
                
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (!CheckErrors())
            {
                UserControl3.MemComboBoxes = MemComboBoxes;
                UserControl3.ColorComboBoxes = ColorComboBoxes;
                UserControl3.NumericUpDowns = NumericUpDowns;
                UserControl3.TextBoxes = TextBoxes;

                Close();
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Не все поля заполнены");
                mes.ShowDialog();
            }
            
        }

        private bool CheckErrors()
        {
            bool filledTextBox = this.Controls.OfType<TextBox>().All(textBox => textBox.Text != "");
            bool filledComboBox = this.Controls.OfType<ComboBox>().All(comboBox => comboBox.Text != "");
            if (!filledTextBox|| !filledComboBox)
                return true;
            return false;
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }
        bool chengd=false;
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userIndex=0;
            if (chengd == true)
            {
                for (int i = 0; i < MemComboBoxes.Count; i++)
                {
                    if ((sender as ComboBox) == MemComboBoxes[i])
                    {
                        userIndex = i;
                    }
                    
                }
                for (int i = 0; i < MemComboBoxes.Count; i++)
                    if ((sender as ComboBox).Text == MemComboBoxes[i].Text )
                    {
                        TextBoxes[userIndex].Text = TextBoxes[i].Text;
                        break;
                    }
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int userIndex = 0;
            if (chengd == true)
            {
                for (int i = 0; i < MemComboBoxes.Count; i++)
                {
                    if ((sender as TextBox) == TextBoxes[i])
                    {
                        userIndex = i;
                    }

                }
                for (int i = 0; i < MemComboBoxes.Count; i++)
                    if (MemComboBoxes[userIndex].Text == MemComboBoxes[i].Text)
                    {
                        TextBoxes[i].Text = TextBoxes[userIndex].Text;
                    }
            }
        }

        private void Form6_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DeleteFlesh(int.Parse((sender as Button).Tag.ToString()));
        }
    }
}
