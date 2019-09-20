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
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
            
        }
        public Operation oper;
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                
                dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";


                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    
                    pictureBox1.Image = new Bitmap(dlg.FileName);
                    pictureBox2.Image = new Bitmap(dlg.FileName);
                    textBox20.Text = dlg.FileName;
                }
                
            }
        }

        public void ResetForm()
        {
            timer1.Enabled = false;
            DestroiAllErPoints();
            foreach (Control item in this.Controls)
            {
                countOpen = 0;
                if (item is TextBox)
                {
                    ((TextBox)item).Clear();
                }
                else if (item is ComboBox)
                {
                    comboBox1.SelectedIndex = 0;
                    ((ComboBox)item).Text = "";
                    if((ComboBox)item!= comboBox2)
                        ((ComboBox)item).SelectedItem = null;
                }
            }
            //<a href="../Main.html">Link</a>
            Image img = Image.FromFile(@"images/Вопрос.png");
            pictureBox1.Image = img;
            pictureBox2.Image = img;
        }
        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number == 44)
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

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
       
        List<Label> labls = new List<Label>();
        int caunt=0;
        private void CreatErrorPoint<T>(T point)
        {
            var valuePoint = point as dynamic; ;
            Label lb1 = new Label();
            //Slb1.Name = "mylabel1";
            lb1.Text = "•";
            lb1.ForeColor = Color.Red;
            lb1.Left = valuePoint.Left + 299;
            lb1.Top = valuePoint.Top - 6;
            
            lb1.Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);
            lb1.Size = new System.Drawing.Size(30, 30);
            lb1.BringToFront();

            labls.Add(lb1);
            
            this.Controls.Add(lb1);
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
                
                if (lab.Top  == value.Top - 6 && lab.Left  == value.Left + 299)
                {
                    lab.Dispose();
                }
                i++;
            }
        }
        int countErrorPoints;
        private void CheckErrors()
        {
            foreach (var item in Controls)
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
            //if (countErrorPoints == 0)
            //{
            //    timer1.Enabled = false;
            //}
            
        }

        public static int CombSetValue(ComboBox comboBox,string str)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                comboBox.SelectedIndex = i;
                if (comboBox.Text ==str)
                {
                    return i;
                }
            }
            return -1;
        }

        public int idProcInPhone;
        public int idPhone;
        public string data;
        public string model;
        public string os;
        public double scrinSize;
        public string razScrin;
        public long ram;
        //public int mamori;
        public string fSim;
        public int cSim;
        public int batari;
        public string compani;

        public int idCors;
        public string proc;
        public int speed;
        public int countCors;
        public int razProс;
        public int sGPU;
        public string GAccelModl;

        public string hDis;
        public string mDis;
       
        public string texScren;
        public string soatnoshStor;

        public double minPrise;
        //public int inStock;

        Image image;
        List<int> idProc = new List<int>();

        public static List<ComboBox> MemComboBoxes = new List<ComboBox>();
        public static List<ComboBox> ColorComboBoxes = new List<ComboBox>();
        public static List<NumericUpDown> NumericUpDowns = new List<NumericUpDown>();
        public static List<TextBox> TextBoxes = new List<TextBox>();

        public static List<ComboBox> LastMemComboBoxes = new List<ComboBox>();
        public static List<ComboBox> LastColorComboBoxes = new List<ComboBox>();
        public static List<NumericUpDown> LastNumericUpDowns = new List<NumericUpDown>();
        public static List<TextBox> LastTextBoxes = new List<TextBox>();
        void LoadInfo(int id)
        {
            comboBox2.SelectedIndex = 0;
            string query = $"SELECT  model, data, os, ram, sizeSc, fsim,Csim, hDesign, mDesign, scrin, sizeBat," +
                        $" componi, idProc, texScren, soatnoshStor, cam FROM Phons WHERE idPhone={id}";
            
            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();

            textBox2.Text = reader[0].ToString();//Модель
            comboBox14.SelectedIndex = CombSetValue(comboBox14, reader[1].ToString());//Data
            comboBox1.SelectedIndex = CombSetValue(comboBox1, reader[2].ToString());//Os
            comboBox4.SelectedIndex = CombSetValue(comboBox4, reader[3].ToString());//Ram
            textBox8.Text = reader[4].ToString();
            comboBox6.SelectedIndex = CombSetValue(comboBox6, reader[5].ToString());//Fsim
            comboBox7.SelectedIndex = CombSetValue(comboBox7, reader[6].ToString());//Fsim
            comboBox10.SelectedIndex = CombSetValue(comboBox10, reader[7].ToString());//Hdis
            comboBox11.SelectedIndex = CombSetValue(comboBox11, reader[8].ToString().Replace(".",","));//mDis
            //textBox3.Text = reader[9].ToString();//DrazScrin
            comboBox16.Text = reader[9].ToString();//DrazScrin
            textBox19.Text = reader[10].ToString();//batari
            //comboBox13.SelectedIndex = CombSetValue(comboBox13, reader[11].ToString());//compani
            comboBox13.Text = reader[11].ToString();//compani
            idProcInPhone =int.Parse(reader[12].ToString());//idProcInPhone
            comboBox12.SelectedIndex = CombSetValue(comboBox12, reader[13].ToString());//texScren
            comboBox3.SelectedIndex = CombSetValue(comboBox3, reader[14].ToString());// soatnoshStor
            textBox1.Text = reader[15].ToString();// Cam
            for (int i = 0; i < idProc.Count;i++)
            if (idProc[i]== idProcInPhone)
            {
                    comboBox2.SelectedIndex = i + 1;
                    break;
            }
            pictureBox1.Image = DBmanager.GetImage(id);
            pictureBox2.Image = pictureBox1.Image;
            textBox20.Text = "Добавлено";
            textBox5.Text = "Добавлено";
            label2.Text = "Изменение";
            //mamori = int.Parse(comboBox5.Text);

            
        }
        public void LoadDataCors()
        {
            comboBox2.Items.Clear();
            comboBox15.Items.Clear();
            idProc.Clear();
            comboBox2.Items.Add("Новый");
            comboBox2.SelectedIndex = 0;
            string query = "SELECT idProc,model,GAccelModl FROM Cors";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                if (int.Parse(reader[0].ToString()) != 1)
                {

                    idProc.Add(Convert.ToInt32(reader[0].ToString()));
                    comboBox2.Items.Add(reader[1].ToString());
                    comboBox15.Items.Add(reader[2].ToString());
                }
                i++;
            }

            reader.Close();
        }
        public void LoadData(int id = 0)
        {


            idPhone = id;
            //comboBox2.SelectedIndex = 0;
            LoadDataCors();

            comboBox14.Items.Clear();
            int year = DateTime.Now.Year;
            year -= 2000;
            //MessageBox.Show(year.ToString());
            for (int i = 16; i <= year; i++)
            {
                comboBox14.Items.Add(2000 + i);
            }

            //query = $"SELECT GAccelModl FROM Cors";

            //command = new OleDbCommand(query, Form5.myConnection);

            //reader = command.ExecuteReader();
            //comboBox15.Items.Clear();
            //while (reader.Read())
            //    comboBox15.Items.Add(reader[0].ToString());


            string query = $"SELECT componi FROM Phons GROUP BY componi";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            comboBox13.Items.Clear();
            while (reader.Read())
                comboBox13.Items.Add(reader[0].ToString());

            if (Operation.insert == oper)
                button5.Text = "Добавить";
            else
            {
                button5.Text = "Изменить";
                form6 = new Form6(id);
                LoadInfo(id);
            }

        }

        bool error = false;


        private void button5_Click(object sender, EventArgs e)
        {
            string query;
            OleDbCommand command;
            //if (Operation.update == oper)
            //{
            //    query = $"DELETE FROM Stock WHERE idPhone={idPhone}";
            //    command = new OleDbCommand(query, Form5.myConnection);

            //    command.ExecuteNonQuery();
            //}

            bool filledTextBox = this.Controls.OfType<TextBox>().All(textBox => textBox.Text != "");
            bool filledComboBox = this.Controls.OfType<ComboBox>().All(comboBox => comboBox.Text != "");
            //Task.Run(() => { while (true) CheckErrors(); });


            //DestroiAllErPoints();
           
            if (!filledComboBox || !filledTextBox)
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Не все поля заполнены");
                mes.ShowDialog();
                timer1.Enabled = true;
            }
            else
            if (!CheckModel("Phons", textBox2.Text)&& oper == Operation.insert)
            {
                CreatErrorPoint(textBox2);
                error = true;
                Messege mes = new Messege(Messege.messegeType.error, $"Телефон с моделью {textBox2.Text} уже зарегистрирован");
                mes.ShowDialog();
            }
            else
            {
                
                data = comboBox14.Text;
                model = textBox2.Text;
                os = comboBox1.Text;
                //scrinSize = double.Parse(textBox8.Text);
                razScrin = comboBox16.Text;
                ram = int.Parse(comboBox4.Text);
                //mamori = int.Parse(comboBox5.Text);
                fSim = comboBox6.Text;
                cSim = int.Parse(comboBox7.Text);
                batari = int.Parse(textBox19.Text);
                compani = comboBox13.Text;
                
                if (comboBox2.Text == "Новый")
                {
                    proc = textBox12.Text;
                    speed = int.Parse(textBox11.Text);
                    countCors = int.Parse(comboBox8.Text);
                    razProс = int.Parse(comboBox9.Text);
                    sGPU = int.Parse(textBox16.Text);
                    GAccelModl = comboBox15.Text;
                    
                    idCors = DBmanager.GetId("Cors", "idProc");
                    query = $"INSERT INTO Cors (idProc,model,speed,nCors,razProc,sGPU,GAccelModl) VALUES ({idCors},'{proc}',{speed},{countCors},{razProс},{sGPU},'{GAccelModl}')";
                    command = new OleDbCommand(query, Form5.myConnection);

                    command.ExecuteNonQuery();
                }
                if(oper == Operation.insert)
                    idPhone = DBmanager.GetId("Phons", "idPhone");
                

                idProcInPhone = idCors;
                hDis = comboBox10.Text;
                mDis = comboBox11.Text.Replace(",",".");
                List<int> idFlethMam = new List<int>();

                texScren = comboBox12.Text;

                soatnoshStor = comboBox3.Text;
               

                //inStock =int.Parse(textBox4.Text);

                image = pictureBox1.Image;
               

                if (oper == Operation.insert)
                    query = $"INSERT INTO Phons (idPhone, model, data, os, ram, sizeSc, fsim,Csim, hDesign, mDesign, scrin, sizeBat," +
                        $" componi, idProc, texScren, soatnoshStor, cam) VALUES ({idPhone}, '{model}', {data}, '{os}', {ram}, {textBox8.Text.Replace(",",".")}," +
                        $" '{fSim}', {cSim}, '{hDis}', '{mDis}', '{razScrin}', {batari}, '{compani}', {idCors}, '{texScren}', '{soatnoshStor}',  {textBox1.Text})";
                else
                    query = $"UPDATE Phons SET idPhone={idPhone}, model='{model}', data={data}, os='{os}', ram={ram}, sizeSc={textBox8.Text.Replace(",", ".")}, fsim='{fSim}',Csim={cSim}, hDesign='{hDis}', mDesign='{mDis}', scrin='{razScrin}', sizeBat={batari}," +
                        $" componi='{compani}', idProc={idCors}, texScren='{texScren}', soatnoshStor='{soatnoshStor}',  cam={textBox1.Text} WHERE idPhone={idPhone}";
                //else
                //    query = $"UPDATE Phons SET idPhone={idPhone}, model={model}, data={data}, os={os}, ram={ram}, sizeSc={textBox8.Text}, fsim={fSim},Csim={cSim}, hDesign={hDis}, mDesign={mDis}, scrin={razScrin}, sizeBat={batari}, componi={compani}, idProc={idCors}, texScren={texScren}, soatnoshStor={soatnoshStor}, minPrise={strMinPrise}";
                command = new OleDbCommand(query, Form5.myConnection);

                command.ExecuteNonQuery();
                 
                DBmanager.SetImage(image, idPhone);
                int idFleshMam=0;
                int idColor=0;
                List<int> idStocs = new List<int>();
                for (int i = 0; i < MemComboBoxes.Count; i++)
                {
                    while (true)
                    {
                        query = $"SELECT idFleshMam FROM FleshMem WHERE FleshMamari = {MemComboBoxes[i].Text}";
                        command = new OleDbCommand(query, Form5.myConnection);
                        try
                        {
                            idFleshMam = int.Parse(command.ExecuteScalar().ToString());
                            break;
                        }
                        catch (Exception)
                        {
                            query = $"INSERT INTO FleshMem (FleshMamari) VALUES ({MemComboBoxes[i].Text})";
                            command = new OleDbCommand(query, Form5.myConnection);

                            command.ExecuteNonQuery();
                            
                        }
                    }
                    while (true)
                    {
                        query = $"SELECT idColor FROM Colors WHERE color = '{ColorComboBoxes[i].Text}'";
                        command = new OleDbCommand(query, Form5.myConnection);
                        try
                        {
                            idColor = int.Parse(command.ExecuteScalar().ToString());
                            break;
                        }
                        catch (Exception)
                        {
                            
                            //query = $"INSERT INTO FleshMem (FleshMamari) VALUES ({MemComboBoxes[i].Text})";
                            query = $"INSERT INTO Colors (color) VALUES ('{ColorComboBoxes[i].Text}')";
                            command = new OleDbCommand(query, Form5.myConnection);

                            command.ExecuteNonQuery();

                        }
                    }
                    if (oper == Operation.insert)
                    {
                        int idStoock = DBmanager.GetId("Stock","idStock");
                        query = $"INSERT INTO Stock (idStock,idPhone,idFleshMem,idColor,colech,prise) VALUES ({idStoock},{idPhone},{idFleshMam},{idColor},{NumericUpDowns[i].Value},{TextBoxes[i].Text.Replace(",",".")})";
                        command = new OleDbCommand(query, Form5.myConnection);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                       
                        try
                        {
                            query = $"UPDATE Stock SET idPhone = {idPhone},idFleshMem = {idFleshMam},idColor = {idColor},colech = {NumericUpDowns[i].Value}, prise = {TextBoxes[i].Text.Replace(",", ".")} WHERE idStock = {Form6.idStocks[i]}";
                            command = new OleDbCommand(query, Form5.myConnection);
                            command.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            int idStock=-1;
                            idStock = DBmanager.GetId("Stock", "idStock");
                            query = $"INSERT INTO Stock (idStock,idPhone,idFleshMem,idColor,colech,prise) VALUES ({idStock},{idPhone},{idFleshMam},{idColor},{NumericUpDowns[i].Value},{TextBoxes[i].Text.Replace(",", ".")})";
                            command = new OleDbCommand(query, Form5.myConnection);
                            command.ExecuteNonQuery();
                            //idStocs.Add(idStock);
                        }

                    }
                    for (int t = 0; t < Form6.idDeleteStocks.Count; t++)
                    {
                        query = $"UPDATE Stock SET chatged = TRUE WHERE idStock = {Form6.idDeleteStocks[t]}";
                        command = new OleDbCommand(query, Form5.myConnection);
                        command.ExecuteNonQuery();
                    }

                }
                
                if (oper == Operation.update)
                {
                    Messege mes = new Messege(Messege.messegeType.done, $"Характеристики {textBox2.Text} успешно изменены.");
                    mes.ShowDialog();

                    if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenPhones();
                    }
                }
                else
                {
                    Messege mes = new Messege(Messege.messegeType.done, $"Телефон {textBox2.Text} успешно добавлен.");
                    mes.ShowDialog();

                    if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenPhones();
                    }
                }
                
            }

            
        }
        public bool CheckModel(string From,string where,string equls= "model")
        {
            string query = $"SELECT {equls} FROM {From} WHERE model='{where}'";
            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            command.ExecuteScalar();
            
            if (command.ExecuteScalar() == null)
            {
                return true;
                
            }
            return false;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isTrue = false;
            if (comboBox2.Text == "Новый")
            {
                isTrue = true;
                button4.Enabled = false;
                textBox12.Enabled = isTrue;
                textBox11.Enabled = isTrue;
                comboBox8.Enabled = isTrue;
                comboBox9.Enabled = isTrue;
                textBox16.Enabled = isTrue;
                comboBox15.Enabled = isTrue;
               
                comboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox9.DropDownStyle = ComboBoxStyle.DropDownList;

                textBox12.Text = "";
                textBox11.Text = "";
                comboBox9.SelectedItem = null;
                comboBox8.SelectedItem = null;
                textBox16.Text = "";
                comboBox15.Text = "";

            }
            else
            {
                button4.Enabled = true;
                isTrue = false;
                textBox12.Enabled = isTrue;
                textBox11.Enabled = isTrue;
                comboBox8.Enabled = isTrue;
                comboBox9.Enabled = isTrue;
                textBox16.Enabled = isTrue;
                comboBox15.Enabled = isTrue;

                comboBox8.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox9.DropDownStyle = ComboBoxStyle.DropDown;

                idCors = idProc[comboBox2.SelectedIndex - 1];
                //MessageBox.Show(idCors.ToString());

                string query = $"SELECT model,speed,nCors,razProc,sGPU,GAccelModl FROM Cors WHERE idProc={idCors}";

                OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                textBox12.Text = reader[0].ToString();
                textBox11.Text = reader[1].ToString();
                comboBox8.Text = reader[2].ToString();
                comboBox9.Text = reader[3].ToString();
                textBox16.Text = reader[4].ToString();
                comboBox15.Text = reader[5].ToString();

                reader.Close(); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //LoadData();

            //comboBox4.SelectedIndex= CombSetValue(comboBox4,"8");
            //form6 = new Form6(222);
            //form6.ShowDialog();

            DBmanager.SetImage(textBox20.Text, 504);
        }
       
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (Operation.insert == oper)
                OpenFlesMemForm();
            else
                form6.ShowDialog();

        }
        Form6 form6;
        int countOpen;
        

        private void OpenFlesMemForm()
        {
            if (countOpen == 0)
                form6 = new Form6();
            form6.ShowDialog();
            //MessageBox.Show(MemComboBoxes.Count.ToString());
            if (MemComboBoxes.Count != 0)
                textBox5.Text = "Добавленно";
            countOpen++;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (error)
            {
                DestroiErPoints(textBox2);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = comboBox2.SelectedIndex;
            Form7 form7;
            if(idProcInPhone == idCors)
                form7 = new Form7(idCors,idPhone);
            else
                form7 = new Form7(idCors);
            LoadDataCors();
            form7.ShowDialog();
           if(!form7.isDalate)
                comboBox2.SelectedIndex= index;
           else
                comboBox2.SelectedIndex = 0;

        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
public enum Operation
{
    insert,update
}
