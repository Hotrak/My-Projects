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
    public partial class Form1 : Form
    {
        int id;
        public Form1(int id)
        {
            InitializeComponent();
            this.id = id;
            LoadData();
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        List<int> idPhone = new List<int>();
        List<int> idStock = new List<int>();
        List<int> coleches = new List<int>();
        List<int> prices = new List<int>();
        List<string> colors = new List<string>();
        List<int> idFlehs = new List<int>();
        
        private void LoadData()
        {
            textBox1.Enabled = false;
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(80, 100);
            listView1.Items.Clear();

            string query = $"SELECT a.model, d.color, f.FleshMamari,a.idPhone,c.colech,a.componi,c.idStock,i.colech,c.prise,c.idFleshMem,c.idColor  FROM Phons a ,Stock c , Colors d, FleshMem f,OrderBuy i WHERE" +////(SELECT a.FleshMamari FROM FleshMem a1,Stock b1 WHERE b1.idFleshMem = a1.idFleshMam)
               $" a.сharged = FALSE AND c.idPhone = a.idPhone AND c.idColor = d.idColor AND f.idFleshMam = c.idFleshMem AND c.idStock = i.idStock AND i.idOrderBuy = {id}";//AND a.idPhone=b.idPhone

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            int i = 0;

            idPhone.Clear();
            coleches.Clear();
            prices.Clear();
            colors.Clear();
            idFlehs.Clear();
            idStock.Clear();
            while (reader.Read())
            {
                idPhone.Add(int.Parse(reader[3].ToString()));
                idStock.Add(int.Parse(reader[6].ToString()));
                prices.Add(int.Parse(reader[8].ToString()));
                idFlehs.Add(int.Parse(reader[9].ToString()));
                colors.Add(reader[9].ToString());
                ListViewItem listViewItem;
                listViewItem = new ListViewItem(new string[] { $"{i + 1}", reader[5].ToString() + " " + reader[0].ToString() + " " + reader[2].ToString() + " ГБ", reader[1].ToString(), reader[7].ToString(), $"0" });

                
                listView1.Items.Add(listViewItem);
                coleches.Add(int.Parse(reader[4].ToString()));
                imageList.Images.Add(DBmanager.GetImage(int.Parse(reader[3].ToString())));
                listView1.SmallImageList = imageList;
                listViewItem.ImageIndex = i;
                i++;
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        
                        try
                        {
                            if (double.Parse(textBox1.Text) != 0)
                            {
                                lv.UseItemStyleForSubItems = false;
                                System.Drawing.Color color = System.Drawing.Color.FromArgb(54, 54, 54);
                                lv.SubItems[4].BackColor = color;
                            }
                            else
                            {
                                lv.UseItemStyleForSubItems = false;
                                System.Drawing.Color color = System.Drawing.Color.FromArgb(29, 31, 35);
                                lv.SubItems[4].BackColor = color;
                            }
                            lv.SubItems[4].Text = textBox1.Text;
                        }
                        catch (Exception)
                        {
                            lv.UseItemStyleForSubItems = false;
                            System.Drawing.Color color = System.Drawing.Color.FromArgb(29, 31, 35);
                            lv.SubItems[4].BackColor = color;
                            lv.SubItems[4].Text = "0";
                        }

                    }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        textBox1.Text = lv.SubItems[4].Text;
                    }
                
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query;
            OleDbCommand command;
            for (int i = 0; i< idStock.Count;i++)
            {
                query = $"UPDATE Stock SET colech = {(coleches[i] + int.Parse(listView1.Items[i].SubItems[3].Text))} WHERE idStock = { idStock[i]}";
                command = new OleDbCommand(query, Form5.myConnection);
                command.ExecuteNonQuery();
            }
            
            for (int i = 0; i < idPhone.Count;i++)
            {
                double max;
                if (int.Parse(listView1.Items[i].SubItems[4].Text) > prices[i])
                    max = int.Parse(listView1.Items[i].SubItems[4].Text);
                else max =  prices[i];
                query = $"UPDATE Stock SET prise = {max.ToString().Replace(",",".")} WHERE idPhone = { idPhone[i]} AND idFleshMem = {idFlehs[i]}";
                command = new OleDbCommand(query, Form5.myConnection);
                command.ExecuteNonQuery();
            }

            query = $"DELETE FROM OrderData WHERE idOrderBuy = {id}";
            command = new OleDbCommand(query, Form5.myConnection);
            command.ExecuteNonQuery();
            Close();

        }
    }
}
