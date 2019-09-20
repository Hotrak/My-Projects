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
    public partial class PhonsPanal : UserControl
    {


        public PhonsPanal()
        {
            InitializeComponent();
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            textBox3.KeyPress += textBox2_KeyPress;
           

        }

        List<int> idPhone = new List<int>();
        List<string> modals = new List<string>();
        public bool FindID(List<int> list, int id)
        {
            foreach (var item in list)
            {
                if (item == id)
                {
                    return true;
                }
            }
            return false;
        }
        
        public void SelectData(string query = "")
        {
            
            listView1.Items.Clear();
            modals.Clear();
            idPhone.Clear();
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(120, 150);
            if (query == "")
                query = "SELECT a.model,Min(b.prise),a.idPhone,a.сharged,a.componi FROM Phons a, Stock b WHERE сharged = FALSE AND a.idPhone = b.idPhone GROUP BY a.model,a.idPhone,a.сharged,a.componi,b.prise";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            int i = 0;
            //idPhone.Add(0);

            
            while (reader.Read())
            {
                if (i == 0)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { "","        "+ reader[4].ToString()+" " + reader[0].ToString(), reader[1].ToString() + "    ", 3+"",4+"",5+"" });

                    //MessageBox.Show(reader[2].ToString());
                    idPhone.Add(int.Parse(reader[2].ToString()));
                    modals.Add(reader[4].ToString() + " " + reader[0].ToString());

                    listView1.Items.Add(listViewItem);
                    imageList.Images.Add(DBmanager.GetImage(int.Parse(reader[2].ToString())));
                    
                    listView1.SmallImageList = imageList;
                    listViewItem.ImageIndex = i;
                    i++;

                } else
                if (!FindID(idPhone, int.Parse(reader[2].ToString())))
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { "", "        " + reader[4].ToString() + " " +  reader[0].ToString(), reader[1].ToString()+"    ", 3 + "", 4 + "", 5 + "" });

                    //MessageBox.Show(reader[2].ToString());
                    idPhone.Add(int.Parse(reader[2].ToString()));
                    modals.Add(reader[4].ToString() + " " + reader[0].ToString());

                    listView1.Items.Add(listViewItem);
                    
                  
                    imageList.Images.Add(DBmanager.GetImage(int.Parse(reader[2].ToString())));
                    listView1.SmallImageList = imageList;
                    listViewItem.ImageIndex = i;
                    i++;
                }

            }

            reader.Close();
            //ClearFiltrs();

        }
       
        public void LoadData(string query = "")
        {
            
            textBox2.Text = "От";//подсказка
            textBox2.ForeColor = Color.Gray;

            textBox3.Text = "До";//подсказка
            textBox3.ForeColor = Color.Gray;

            ActivModel = "";
            companys.Clear();
            SelectData(query);
            comboBox2.Items.Clear();
            int year = DateTime.Now.Year;
            year -= 2000;
            comboBox2.Items.Add("");
            //MessageBox.Show(year.ToString());
            for (int j = 16; j <= year; j++)
            {
                comboBox2.Items.Add(2000 + j);
            }

            query = "SELECT componi FROM Phons WHERE сharged = FALSE GROUP BY componi ORDER BY componi DESC";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();
            comboBox1.Items.Add(" ");
            companys.Add(" ");
   
            while (reader.Read())
            {
                if (!isHere(companys, reader[0].ToString()))
                {
                    comboBox1.Items.Add(reader[0].ToString());
                    companys.Add(reader[0].ToString());
                }
            }


            query = "SELECT FleshMamari FROM FleshMem ORDER BY FleshMamari";

            command = new OleDbCommand(query, Form5.myConnection);

            reader = command.ExecuteReader();
            comboBox8.Items.Clear();
            comboBox8.Items.Add(" ");
            
            while (reader.Read())
            {
                comboBox8.Items.Add(reader[0].ToString());
            }
        }

        internal void TimerStart()
        {
            timer1.Enabled = true;
        }

        List<string> companys = new List<string>();
        private bool isHere(List<string> list, string str)
        {
            foreach (var item in list)
            {
                if (item==str)
                {
                    return true;
                }
            }
            return false;
        }
        public string ActivModel;

        private void LoadOptions()
        {

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        ActivModel = lv.SubItems[1].Text;

                        if (!(System.Windows.Forms.Application.OpenForms["Form5"] as Form5).idPhonesForCompere.Contains(GetIdPhone()))
                            button5.Enabled = true;
                        else
                        {

                            button5.Enabled = false;
                            //listView1.SelectedIndices = 0;
                            
                        }
                    }
            }
        }
        public int GetIdPhone()
        {
            //String[] words = ActivModel.Replace("        ", "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            //ActivModel = ActivModel.Replace(words[0], "");
            for (int i = 0; i < idPhone.Count; i++)
            {
               
                if (modals[i].Trim() == ActivModel.Trim())
                {
                    return idPhone[i];
                }
                //else
                    //return -1;
            }
            return -1;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (GetIdPhone() != -1)
            {
                if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                {
                    //(System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenCheng();
                    (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenCheng(GetIdPhone());
                }
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Вы не выбрали телефон который хотите изменить!");
                mes.ShowDialog();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (GetIdPhone() != -1)
            {
                if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenAbaut(GetIdPhone());
                }
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Вы не выбрали телефон!");
                mes.ShowDialog();
            }
                
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            if (GetIdPhone() != -1)
            {
                Messege mes = new Messege(Messege.messegeType.warning, $"Вы действительно хотите удалить {ActivModel}?");
                mes.ShowDialog();
                if (mes.isAssept)
                {
                    //string query = $"DELETE FROM Phons WHERE idPhone={GetIdPhone()}";
                    string query = $"UPDATE Phons SET сharged = TRUE WHERE idPhone={GetIdPhone()}";

                    OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

                    command.ExecuteNonQuery();
                    
                    LoadData();
                   
                }
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Вы не выбрали телефон который хотите удалить!");
                mes.ShowDialog();
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenInsert();
            }
        }
        int idProcInPhone;
        string prise1;
        string prise2;
        string data;
        string model;
        string os;
        string scrinSize;
        string razScrin;
        string ram;
        string fleshPam;
        string cam;

        string fSim;
        string cSim;
        string batari;
        string compani;

        int idCors;
        string proc;
        int speed;
        int countCors;
        int razProс;
        int sGPU;
        string GAccelModl;

        string hDis;
        string mDis;

        string texScren;
        string soatnoshStor;

        string queryForSelect;
        private void SelectData()
        {
            if (!Clear)
            {
                queryForSelect = $"SELECT a.model,Min(b.prise),a.idPhone,a.сharged,a.componi FROM Phons a, Stock b WHERE сharged = FALSE AND a.idPhone = b.idPhone {compani} {data} {os} {soatnoshStor} {texScren} {ram} {cSim} {fleshPam} {batari} {prise1} {prise2} {scrinSize} {cam} {razScrin} {model} {status} GROUP BY a.model,a.idPhone,a.сharged,a.componi,b.prise";
                //MessageBox.Show(queryForSelect.Length.ToString());
                //queryForSelect = $"SELECT a.model,Min(b.prise),a.idPhone, b.idFleshMam,a.componi FROM Phons a,FleshMem b,Stock c WHERE" +
                //    $" a.сharged = FALSE AND c.idFleshMem = b.idFleshMam AND c.idPhone = a.idPhone {compani} {data} {os} {soatnoshStor} {texScren} {ram} {cSim} {fleshPam} {batari} {prise1} {prise2} {scrinSize} {cam} {razScrin} {model} {status}";//AND a.idPhone=b.idPhone
                SelectData(queryForSelect);
                button6.Enabled = queryForSelect.Length != 198;
            }
            

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//company
        {
            if (comboBox1.Text.Trim() != "")
                compani = $"AND a.componi LIKE '{comboBox1.Text}'";
            else
                compani = "";
            SelectData();
        }
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)//Data
        {
            if (comboBox2.Text.Trim() != "")
                data = $"AND a.data = {comboBox2.Text}";
            else
                data = "";
            SelectData();
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)//OS
        {
            if (comboBox6.Text.Trim() != "")
                os = $"AND a.os LIKE '{comboBox6.Text}'";
            else
                os = "";
            SelectData();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)//soatnoshStor
        {
            if (comboBox5.Text.Trim() != "")
                soatnoshStor = $"AND a.soatnoshStor LIKE '{comboBox5.Text.Trim()}'";
            else
                soatnoshStor = "";
            SelectData();
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)//texScren
        {
            if ((sender as ComboBox).Text.Trim() != "")
                texScren = $"AND a.texScren LIKE '{(sender as ComboBox).Text.Trim()}'";
            else
                texScren = "";
            SelectData();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text.Trim() != "")
                ram = $"AND a.ram = {(sender as ComboBox).Text.Trim()}";
            else
                ram = "";
            SelectData();
        }

        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text.Trim() != "")
                cSim = $"AND a.cSim = {(sender as ComboBox).Text.Trim()}";
            else
                cSim = "";
            SelectData();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

            if ((sender as ComboBox).Text.Trim() != "")
            {
                string query = $"SELECT idFleshMam FROM FleshMem WHERE FleshMamari = {(sender as ComboBox).Text.Trim()}";
                OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

                string idfleshMem =  command.ExecuteScalar().ToString();
                fleshPam = $"AND b.idFleshMem = {idfleshMem}";
            }   
            else
                fleshPam = "";
            SelectData();
        }
        //1500 - 2000 мАч
        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text.Trim() != "")
            {
                string value = (sender as ComboBox).Text.Replace("менее ","");
                value = value.Replace(" мАч", "");
                value = value.Replace("- ", "");
                value = value.Replace(" и более", "");
                string[] values = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (values.Length == 2)
                {
                    batari = $"AND a.sizeBat >= {values[0]} AND a.sizeBat <= {values[1]}";
                }
                else
                if ((sender as ComboBox).SelectedIndex==1)
                {
                    batari = $"AND a.sizeBat <= {values[0]}";
                }
                else
                    batari = $"AND a.sizeBat >= {values[0]}";
            }
            else
                batari = "";
            SelectData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (textBox2.Text=="")
            //{
            //    textBox2.Font
            //}
            if (listView1.SelectedItems.Count == 0)
            {
                button5.Enabled = false;
                button4.Enabled = false;
                button3.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
                button4.Enabled = true;
                button3.Enabled = true;
                button2.Enabled = true;
                if (!(System.Windows.Forms.Application.OpenForms["Form5"] as Form5).idPhonesForCompere.Contains(GetIdPhone()))
                    button5.Enabled = true;
                else
                {

                    button5.Enabled = false;
                    //listView1.SelectedIndices = 0;

                }
            }
           
            if ((System.Windows.Forms.Application.OpenForms["Form5"] as Form5).GetZindex(this) != 0)
               {
                    timer1.Enabled = false;
               }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() != ""&& (sender as TextBox).Text.Trim() != "От" && (sender as TextBox).Text[0] !='.')//От
                prise1 = $"AND b.prise >= {(sender as TextBox).Text.Trim()}";
            else
                prise1 = "";
            SelectData();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() != "" && (sender as TextBox).Text.Trim() != "До" && (sender as TextBox).Text[0] != '.')
                prise2 = $"AND b.prise <= {(sender as TextBox).Text.Trim()}";
            else
                prise2 = "";
            SelectData();
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text.Trim() != "")
            {
                string value = (sender as ComboBox).Text.Replace("менее ", "");
                value = value.Replace("- ", "");
                value = value.Replace(" и более", "");
                string[] values = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (values.Length == 2)
                {
                    scrinSize = $"AND a.sizeSc >= {values[0]} AND a.sizeSc <= {values[1]}";
                }
                else
                if ((sender as ComboBox).SelectedIndex == 1)
                {
                    scrinSize = $"AND a.sizeSc <= {values[0]}";
                }
                else
                    scrinSize = $"AND a.sizeSc >= {values[0]}";
            }
            else
                scrinSize = "";
            SelectData();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)//cam
        {
            if ((sender as ComboBox).Text.Trim() != "")
            {
                string value = (sender as ComboBox).Text.Replace("до ", "");
                value = value.Replace(" Мп", "");
                value = value.Replace("- ", "");
                value = value.Replace(" и более", "");
                string[] values = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (values.Length == 2)
                {
                    cam = $"AND a.cam >= {values[0]} AND a.cam <= {values[1]}";
                }
                else
                if ((sender as ComboBox).SelectedIndex == 1)
                {
                    cam = $"AND a.cam <= {values[0]}";
                }
                else
                    cam = $"AND a.cam >= {values[0]}";
            }
            else
                cam = "";
            SelectData();
        }

        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text.Trim() != "")
                razScrin = $"AND a.scrin = '{(sender as ComboBox).Text.Trim()}'";
            else
                razScrin = "";
            SelectData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() != "")
                model = $"AND a.model LIKE '%{(sender as TextBox).Text.Trim()}%'";
            else
                model = "";
            SelectData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (GetIdPhone() != -1)
            {
                if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                {
                    //myList.Contains(myString)
                    if (!(System.Windows.Forms.Application.OpenForms["Form5"] as Form5).idPhonesForCompere.Contains(GetIdPhone()))
                    {
                        (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).idPhonesForCompere.Add(GetIdPhone());
                        button5.Enabled = false;
                    }    
                    else
                    {
                        Messege mes = new Messege(Messege.messegeType.error, $" !");
                        mes.ShowDialog();
                    }
                }
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Вы не выбрали телефон который хотите изменить!");
                mes.ShowDialog();
            }
        }
        string status="";
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text.Trim() != "")
            {
                if((sender as ComboBox).SelectedIndex == 0)
                    status = $"AND c.colech > 0";
                if ((sender as ComboBox).SelectedIndex == 1)
                    status = $"AND c.colech = 0";
            }
            else
                status = "";
            SelectData();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() == "" || (sender as TextBox).Text.Trim() == "От")
            {
                textBox2.Text = null;
                textBox2.ForeColor = Color.Black;
            }
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
            {
                textBox2.Text = "От";//подсказка
                textBox2.ForeColor = Color.Gray;
            }
            
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() == "" || (sender as TextBox).Text.Trim() == "До")
            {
                textBox3.Text = null;
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() == "")
            {
                textBox3.Text = "До";//подсказка
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 46) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }

        private void PhonsPanal_Load(object sender, EventArgs e)
        {

        }
        bool Clear;
        public void ClearFiltrs()
        {
            Clear = true;
            foreach (var item in Controls)
            {
                if (item is TextBox)
                    if ((item as TextBox).Text != "")
                        (item as TextBox).Text = "";
                if (item is ComboBox)
                    if ((item as ComboBox).Text != "")
                        (item as ComboBox).SelectedItem = null;
            }
            Clear = false;
            LoadData();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ClearFiltrs();
        }
    }
}
