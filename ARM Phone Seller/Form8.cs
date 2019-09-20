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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            textBox11.KeyPress += textBox12_KeyPress;
            textBox2.KeyPress += textBox12_KeyPress;
            textBox16.KeyPress += textBox12_KeyPress;
            textBox3.KeyPress += textBox12_KeyPress;
        }
        public Form8(int idClient)
        {
            InitializeComponent();

            id = idClient;
            LoadData();
        }
        int id = -1;
        private void LoadData()
        {
            string query = $"SELECT name_,fam,fam2,gorod,ylica,dom,tel FROM Clients WHERE idClient = {id}";
            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();

            textBox11.Text = reader[0].ToString();
            textBox12.Text = reader[1].ToString();
            textBox2.Text = reader[2].ToString();
            textBox16.Text = reader[3].ToString();
            textBox3.Text = reader[4].ToString();
            textBox4.Text = reader[5].ToString();
            
            maskedTextBox1.Text = reader[6].ToString().Replace("+375 ","").Replace("(","").Replace(") ","");
            button2.Text = "Изменить";
        }
        

        private void insert()
        {
            string otch = textBox2.Text == "" ? " " : textBox2.Text;
            string query = $"INSERT INTO Clients (name_,fam,fam2,gorod,ylica,dom,skidka,tel) VALUES ('{textBox11.Text}'," +
                $"'{textBox12.Text}','{otch}','{textBox16.Text}','{textBox3.Text}',{textBox4.Text},0,'{"+375 " + maskedTextBox1.Text}')";


            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            command.ExecuteNonQuery();
        }
        private void Update()
        {
            string otch = textBox2.Text == "" ? " " : textBox2.Text;

            string query = $"UPDATE Clients SET name_='{textBox11.Text}',fam ='{textBox12.Text}',fam2='{otch}',gorod ='{textBox16.Text}'" +
                $",ylica ='{textBox3.Text}',dom = {textBox4.Text}, tel ='{"+375 " + maskedTextBox1.Text}' WHERE idClient = {id}";
            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            command.ExecuteNonQuery();
        }
        List<Label> labls = new List<Label>();
        int caunt = 0;
        private void CreatErrorPoint<T>(T point)
        {
            var valuePoint = point as dynamic; ;
            Label lb1 = new Label();
            //Slb1.Name = "mylabel1";
            lb1.Text = "•";
            lb1.ForeColor = Color.Red;
            if(point is MaskedTextBox)
                lb1.Left = valuePoint.Left + 102;
            else
                lb1.Left = valuePoint.Left + 155;
            lb1.Top = valuePoint.Top - 6;

            lb1.Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);
            lb1.Size = new System.Drawing.Size(30, 30);
            lb1.BackColor = panel1.BackColor;

            labls.Add(lb1);
            

            panel1.Controls.Add(lb1);
            //lb1.BringToFront();
            //MessageBox.Show("dsfsd");
        }
        private bool CheckText<T>(T item)
        {
           
            var value = item as dynamic;

            if (item is MaskedTextBox)
            {
                return maskedTextBox1.Text.Length <= 11 ? true : false;
            }

            if (value.Text == "")
            {
                return true;
            }
            return false;
        }
        
        private void DestroiAllErPoints()
        {
            foreach (var item in labls)
                item.Dispose();
        }
        private void DestroiErPoints<T>(T item)
        {
            var value = item as dynamic;
            int i = 0;
            foreach (var lab in labls)
            {

                if (lab.Top == value.Top - 6 && (lab.Left == value.Left + 155 || lab.Left == value.Left + 102))
                {
                    lab.Dispose();
                }
                i++;
            }
        }
        int countErrorPoints;
        private void CheckErrors()
        {
            //UserControl3.CombSetValue();
            foreach (var item in panel1.Controls)
            {
                if ((item is TextBox || item is ComboBox || item is MaskedTextBox) && item != textBox2)
                {
                    if (CheckText(item))
                    {
                        CreatErrorPoint(item);
                        countErrorPoints++;
                    }
                    else
                    {
                        DestroiErPoints(item);
                        countErrorPoints--;
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckErrors();
            if (countErrorPoints == 0)
            {
                timer1.Enabled = false;
            }
        }

        private void Form8_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = " ";
            }
            bool filledTextBox = panel1.Controls.OfType<TextBox>().All(textBox => textBox.Text != "");
            bool filledComboBox = panel1.Controls.OfType<ComboBox>().All(comboBox => comboBox.Text != "");
            if (filledTextBox && filledComboBox)
            {
                if (id != -1)
                {
                    Update();
                    Messege mesc = new Messege(Messege.messegeType.done, $"Клиент успешно изменён!");
                    mesc.ShowDialog();
                }
                else
                {
                    insert();
                    Messege mes = new Messege(Messege.messegeType.done, $"Клиент успешно добавлен!");
                    mes.ShowDialog();
                }
                Close();
            }
            else
            {
                timer1.Enabled = true;
                Messege mes = new Messege(Messege.messegeType.error, $"Не все поля заполнены");
                mes.ShowDialog();
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //char number = e.KeyChar;

            //if (!Char.IsDigit(number) && number != 8)
            //{
            //    e.Handled = true;
            //}
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.' )
            {
                e.Handled = true;
            }
        }
    }
}
