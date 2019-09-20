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
    public partial class UserControl5 : UserControl
    {
        public UserControl5()
        {
            InitializeComponent();
            
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        }
        public void LoadData(string query = "")
        {
            listView1.Items.Clear();
            fam = "";
            name = "";
            fam2 = "";

            if (query == "")
                query = $"SELECT idClient,fam,name_,fam2,tel,gorod,ylica,dom FROM Clients ORDER BY idClient";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                ListViewItem listViewItem = new ListViewItem(new string[] {reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString() });//, reader[4].ToString(), reader[5].ToString(), reader[6].ToString()

                listView1.Items.Add(listViewItem);
                if (i % 2 == 0)
                {
                    Color color = Color.FromArgb(54, 54, 54);
                    listView1.Items[i].UseItemStyleForSubItems = false;
                    for (int j = 0; j < 5; j++)
                    {
                        listView1.Items[i].SubItems[j].BackColor = color;
                    }
                }
                i++;
            }
            reader.Close();

            if (Form5.bascet.Count > 0)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
            button4.Enabled = false;
            button3.Visible = false;
            if (Form5.bascet.Count != 0)
            {
                button3.Visible = true;
                button3.Enabled = false;
            }

            button2.Enabled = false;
            panel1.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.ShowDialog();
            Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fam != "")
            {
                Messege mes = new Messege(Messege.messegeType.warning, $"Вы действительно хотите удалить клиента? Вся информация связанная с ним будет удалена!");
                mes.ShowDialog();

                if (mes.isAssept)
                {
                    string query = $"DELETE FROM Clients WHERE idClient = {idClient}";
                    OleDbCommand command = new OleDbCommand(query, Form5.myConnection);
                    command.ExecuteNonQuery();
                    LoadData();
                }
                
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Вы не выделили покупателя которого хотите удалить");
                mes.ShowDialog();
            }
            
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (fam != "")
            {
                Form5.idClient = idClient;
                Form5.famClient = fam;
                string skidka;
                if (label16.Text.Length == 3)
                    skidka = label16.Text[0] + label16.Text[1].ToString();
                else
                    skidka = label16.Text[0].ToString();

                Form5.skidka = int.Parse(skidka);
                if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenBascket();
                }
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Вы не выделили покупателя");
                mes.ShowDialog();
            }
        }
        string fam = "";
        string name = "";
        string fam2 = "";
        int idClient;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadAdish()
        {
            string query = $"SELECT a.gorod,a.ylica,a.dom FROM Clients a WHERE a.idClient = {idClient}";

            //query = $"SELECT a.gorod,a.ylica,a.dom, SUM(d.prise * c.colech) FROM Clients a, Orders b,Histori c, Stock d WHERE a.idClient = {idClient} AND b.idClient = a.idClient AND c.idOrder = b.idOrder AND d.idStock = c.idStock GROUP BY gorod,ylica,dom";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                label13.Text = reader[0].ToString();
                label14.Text = reader[1].ToString();
                label15.Text = reader[2].ToString();
                query = $"SELECT SUM(d.prise * c.colech) FROM Clients a, Orders b,Histori c, Stock d WHERE a.idClient = {idClient} AND b.idClient = a.idClient AND c.idOrder = b.idOrder AND d.idStock = c.idStock GROUP BY gorod,ylica,dom";

                command = new OleDbCommand(query, Form5.myConnection);

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if(int.Parse(reader[0].ToString()) >1000)
                        label16.Text = "5%";
                    if (int.Parse(reader[0].ToString()) > 1500)
                        label16.Text = "10%";
                    if (int.Parse(reader[0].ToString()) > 2000)
                        label16.Text = "15%";
                    else
                    {
                        label16.Text = "15%";
                        

                    }
                    label18.Text = reader[0].ToString() +" руб.";

                }
                else
                {
                    
                    label16.Text = "0%";
                }


            }
            
                
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (fam != "")
            {
                Form8 form = new Form8(idClient);
                form.ShowDialog();
                LoadData();
            }

                
        }
        public void StartTimer()
        {
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                
                button4.Enabled = false;
               
                button2.Enabled = false;
                panel1.Visible = false;
                if (Form5.bascet.Count != 0)
                    button3.Enabled = false;
                else
                    button3.Visible = false;
                
            }
            else
            {
                
                button4.Enabled = true;               
                button2.Enabled = true;
                panel1.Visible = true;
                if (Form5.bascet.Count != 0)
                    button3.Enabled = true;
                else
                    button3.Visible = false;
            }
            
                if ((System.Windows.Forms.Application.OpenForms["Form5"] as Form5).GetZindex(this) != 0)
                {
                    timer1.Enabled = false;
                }
        }
        string tel="";
        string sFam="";
        string sName="";
        string sFam2="";
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            //+375(29)3956378 +375 (  ) 
            if (maskedTextBox1.Text.Replace(" ", "").Trim() != "+375()")
            {
                tel = $"AND tel LIKE '%{maskedTextBox1.Text}%'";
                
            }
            else
                tel = "";
            Select();


        }
        private void Select()
        {
            //string query = $"SELECT fam,name_,fam2,tel,gorod,ylica,dom FROM Clients WHERE tel LIKE '%+%' {tel} {sFam} {sName} {sFam2}";
            string query = $"SELECT idClient,fam,name_,fam2,tel,gorod,ylica,dom FROM Clients WHERE tel LIKE '%+%' {tel} {sFam} {sName} {sFam2}";
            LoadData(query);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() !="")
            {
                sFam = $"AND fam LIKE '%{(sender as TextBox).Text.Trim()}%'";

            }
            else
                sFam = "";
            Select();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() != "")
            {
                sName = $"AND name_ LIKE '%{(sender as TextBox).Text.Trim()}%'";

            }
            else
                sName = "";
            Select();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() != "")
            {
                sFam2 = $"AND fam2 LIKE '%{(sender as TextBox).Text.Trim()}%'";

            }
            else
                sFam2 = "";
            Select();
        }

        private void UserControl5_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        fam = lv.SubItems[1].Text;

                        idClient = int.Parse(lv.SubItems[0].Text);
                        LoadAdish();
                    }
            }
        }
    }
}
