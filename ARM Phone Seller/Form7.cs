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
    public partial class Form7 : Form
    {
        public int idCor;
        private int idPhone;
        public Form7(int idCor,int idPhone = -1)
        {
            InitializeComponent();
            this.idCor = idCor;
            this.idPhone = idPhone;
            LoadDataCors();
            LoadData();
        }
        public void UpdateForm()
        {
            string query = $"UPDATE Cors SET model='{textBox12.Text}',speed={textBox11.Text},nCors={comboBox8.Text},razProc={comboBox9.Text},sGPU={textBox16.Text},GAccelModl='{comboBox15.Text}' WHERE idProc={idCor}";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            command.ExecuteNonQuery();
        }
        public void LoadData()
        {
            string query = $"SELECT model,speed,nCors,razProc,sGPU,GAccelModl FROM Cors WHERE idProc={idCor}";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            
            reader.Read();
            label2.Text+= " "+reader[0].ToString();
            textBox12.Text = reader[0].ToString();
            textBox11.Text = reader[1].ToString();
            comboBox8.SelectedIndex = UserControl3.CombSetValue(comboBox8, reader[2].ToString());
            comboBox9.SelectedIndex = UserControl3.CombSetValue(comboBox9, reader[3].ToString());
            textBox16.Text = reader[4].ToString();
            comboBox15.Text = reader[5].ToString();

            reader.Close();
        }
        List<Label> labls = new List<Label>();
        int caunt = 0;
        public void LoadDataCors()
        {
            
            string query = "SELECT idProc, GAccelModl FROM Cors";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            int i = 0;
            comboBox15.Items.Clear();
            while (reader.Read())
            {
                
                if (int.Parse(reader[0].ToString()) != 1)
                {
                    comboBox15.Items.Add(reader[1].ToString());

                }
                i++;
            }

            reader.Close();
        }
        private void CreatErrorPoint<T>(T point)
        {
            var valuePoint = point as dynamic; ;
            Label lb1 = new Label();
            //Slb1.Name = "mylabel1";
            lb1.Text = "•";
            lb1.ForeColor = Color.Red;
            lb1.Left = valuePoint.Left + 294;
            lb1.Top = valuePoint.Top - 6;

            lb1.Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);
            lb1.Size = new System.Drawing.Size(30, 30);
            lb1.BringToFront();

            labls.Add(lb1);

            panel1.Controls.Add(lb1);
        }
        private bool CheckText<T>(T item)
        {
            var value = item as dynamic;
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

                if (lab.Top == value.Top - 6 && lab.Left == value.Left + 294)
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
                if (item is TextBox || item is ComboBox)
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckErrors();
            if (countErrorPoints == 0)
            {
                timer1.Enabled = false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bool filledTextBox = panel1.Controls.OfType<TextBox>().All(textBox => textBox.Text != "");
            bool filledComboBox = panel1.Controls.OfType<ComboBox>().All(comboBox => comboBox.Text != "");
            if (filledTextBox && filledComboBox)
            {
                Messege mes = new Messege(Messege.messegeType.warning, $"Вы действительно хотите изменить процессор {textBox12.Text}?");
                mes.ShowDialog();
                if (mes.isAssept)
                {
                    UpdateForm();
                    Close();
                }   
            }
            else
            {
                timer1.Enabled = true;
                Messege mes = new Messege(Messege.messegeType.error, $"Не все поля заполнены");
                mes.ShowDialog();
            }

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form7_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        public bool isDalate = false;
        private void button3_Click(object sender, EventArgs e)
        {

            string query = $"SELECT COUNT(b.idPhone) FROM  Phons b WHERE b.idProc = {idCor}";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            int count = int.Parse(command.ExecuteScalar().ToString());
            int j = 0;
            if (idPhone != -1)
                j = 1;
            if (count == j)
            {
                Messege mes = new Messege(Messege.messegeType.warning, $"Вы действительно хотите удалить процессор {textBox12.Text}?");
                mes.ShowDialog();
                if (mes.isAssept)
                {
                    if (idPhone!=-1)
                    {
                        query = $"UPDATE Phons SET idProc = {1} WHERE idPhone = {idPhone}";

                        command = new OleDbCommand(query, Form5.myConnection);

                        command.ExecuteNonQuery();
                    }
                    query = $"DELETE FROM Cors WHERE idProc = {idCor}";

                    command = new OleDbCommand(query, Form5.myConnection);

                    command.ExecuteNonQuery();
                    isDalate = true;
                    Close();

                }
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Данный процессор задействован в другом телефоне!");
                mes.ShowDialog();
            }
               
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
