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
    public partial class UserControl6 : UserControl
    {
        public UserControl6()
        {
            InitializeComponent();
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        }
        public void LoadData(string query = "")
        {
            listView1.Items.Clear();
            
            if (query == "")
                query = $"SELECT a.idOrder,a.data,a.idClient,b.fam,a.skidka FROM Orders a, Clients b WHERE a.idClient = b.idClient ORDER BY data";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();


            int i = 0; 
            while (reader.Read())
            {
                query = $"SELECT a.colech ,a.price FROM Histori a, Stock b WHERE a.idOrder = {reader[0].ToString()} AND a.idStock = b.idStock";

                OleDbCommand command2 = new OleDbCommand(query, Form5.myConnection);

                OleDbDataReader reader1 = command2.ExecuteReader();
                double summ = 0d;
                while (reader1.Read())
                {
                    summ += int.Parse(reader1[0].ToString()) * double.Parse(reader1[1].ToString());
                }
                summ = Math.Round(summ - (summ * (double.Parse(reader[4].ToString()) / 100)),2);
                ListViewItem listViewItem = new ListViewItem(new string[] { reader[0].ToString(), reader[2].ToString(), reader[3].ToString(), reader[1].ToString().Remove(reader[1].ToString().Length - 8), summ.ToString() });
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

            query = $"SELECT Min(data) FROM Orders";

            command = new OleDbCommand(query, Form5.myConnection);
            try
            {
                string[] dataMin = command.ExecuteScalar().ToString().Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
                dateTimePicker1.MaxDate = DateTime.Now;
                dateTimePicker1.MinDate = new DateTime(int.Parse(dataMin[2]), int.Parse(dataMin[1]), int.Parse(dataMin[0]));

            }
            catch (Exception)
            {
                
            }
            
            
        }
        public void ClerAllTexBooxes()
        {
            foreach (var item in Controls)
            {
                if (item is TextBox)
                    (item as TextBox).Text = "";
                
            }
        }

        int activOrder = 0;
        double activPrice = 0;
        string data="";
        int ii = 0;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                {
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        listView1.SelectedItems.Clear();
                        activOrder =int.Parse(lv.SubItems[0].Text);
                        activPrice = double.Parse(lv.SubItems[4].Text);
                        data = lv.SubItems[3].Text;
                        if (ii % 2 ==0)
                        {
                            Form9 form9 = new Form9(activOrder, data, activPrice);
                            form9.LoadData();
                            form9.ShowDialog();
                        }
                       
                        ii++;
                    }
                }
                    
            }
           
        }
        string fam="";
        string name="";
        string fam2="";
        string idOrder="";
        string dataF="";
        private void Select()
        {
            string query = $"SELECT a.idOrder,a.data,a.idClient,b.fam,a.skidka FROM Orders a, Clients b WHERE a.idClient = b.idClient {fam} {name} {fam2} {idOrder} {dataF} ORDER BY data DESC";
            LoadData(query);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)//fam
        {
            if ((sender as TextBox).Text.Trim() != "")
            {
                fam = $"AND b.fam LIKE '%{(sender as TextBox).Text}%'";
            }
            else
                fam = "";
            Select();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() != "")
            {
                name = $"AND b.name_ LIKE '%{(sender as TextBox).Text}%'";
            }
            else
                name = "";
            Select();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() != "")
            {
                fam2 = $"AND b.fam2 LIKE '%{(sender as TextBox).Text}%'";
            }
            else
                fam2 = "";
            Select();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Trim() != "")
            {
                idOrder = $"AND a.idOrder = {(sender as TextBox).Text}";
            }
            else
                idOrder = "";
            Select();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox5.Text = dateTimePicker1.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Trim() != "")
            {
                dataF = $"AND a.data LIKE '%{textBox5.Text}%'";
            }
            else
                dataF = "";
            Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
          
        }
    }
}
