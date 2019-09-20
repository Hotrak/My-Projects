using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;

namespace BD
{
    public partial class Form11 : Form
    {
        delegate void Operation();
        Operation operation;
        int id;
        public Form11(int id = -1)
        {
            this.id = id;
            InitializeComponent();
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            if (id != -1)
            {
                LoadDataUpdate();
                LoadDataInsert();
                button3.Text = "Изменить";
                Text = "Изменение заказа";
                label1.Text = "Изменение заказа";
                //operation=Update
            }
            else
            {
                operation = Insert;
                numericUpDown1.Value = 5;
                LoadDataInsert();
            }
            
          


        }
        List<int> idStock = new List<int>();
        List<Order> orders = new List<Order>();
        int max;
        private void LoadDataInsert()
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(80, 100);
            listView1.Items.Clear();
            string query;

            ///if (id == -1)         
                max = int.Parse(numericUpDown1.Value.ToString());
            query = $"SELECT a.model, d.color, f.FleshMamari,a.idPhone,c.colech,a.componi,c.idStock FROM Phons a ,Stock c , Colors d, FleshMem f WHERE" +////(SELECT a.FleshMamari FROM FleshMem a1,Stock b1 WHERE b1.idFleshMem = a1.idFleshMam)
               $" a.сharged = FALSE AND c.idPhone = a.idPhone AND c.idColor = d.idColor AND f.idFleshMam = c.idFleshMem AND c.colech <= {max}";//AND a.idPhone=b.idPhone

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            int i = 0;

            idStock.Clear();
            while (reader.Read())
            {
                idStock.Add(int.Parse(reader[6].ToString()));
                ListViewItem listViewItem;
                Order order = orders.Find(item => item.idStoock == int.Parse(reader[6].ToString()));
                if (order == null)
                {
                    orders.Add(new Order(int.Parse(reader[6].ToString()), reader[5].ToString() + " " + reader[0].ToString() + " " + reader[2].ToString(), reader[1].ToString()));
                    listViewItem = new ListViewItem(new string[] { $"{i + 1}", reader[5].ToString() + " " + reader[0].ToString() + " " + reader[2].ToString() + " ГБ", reader[1].ToString(), reader[4].ToString(), "0" });
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(29, 31, 35);
                    listViewItem.UseItemStyleForSubItems = false;
                    listViewItem.SubItems[4].BackColor = color;

                    
                }
                else
                {
                    listViewItem = new ListViewItem(new string[] { $"{i + 1}", reader[5].ToString() + " " + reader[0].ToString() + " " + reader[2].ToString() + " ГБ", reader[1].ToString(), reader[4].ToString(), order.count.ToString() });
                    if (order.count!=0)
                    {
                        System.Drawing.Color color = System.Drawing.Color.FromArgb(54, 54, 54);
                   
                        listViewItem.UseItemStyleForSubItems = false;
                        listViewItem.SubItems[4].BackColor = color;
                    }
                    
                }

                listView1.Items.Add(listViewItem);
                imageList.Images.Add(DBmanager.GetImage(int.Parse(reader[3].ToString())));
                listView1.SmallImageList = imageList;
                listViewItem.ImageIndex = i;
                i++;
            }
        }

        private void LoadDataUpdate()
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(80, 100);
            listView1.Items.Clear();

            string query = $"SELECT a.model, d.color, f.FleshMamari,a.idPhone,c.colech,a.componi,c.idStock,i.colech FROM Phons a ,Stock c , Colors d, FleshMem f,OrderBuy i WHERE" +////(SELECT a.FleshMamari FROM FleshMem a1,Stock b1 WHERE b1.idFleshMem = a1.idFleshMam)
               $" a.сharged = FALSE AND c.idPhone = a.idPhone AND c.idColor = d.idColor AND f.idFleshMam = c.idFleshMem AND c.idStock = i.idStock AND i.idOrderBuy = {id}";//AND a.idPhone=b.idPhone

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            int i = 0;

            idStock.Clear();
            List<int> coleches = new List<int>();
            while (reader.Read())
            {
                idStock.Add(int.Parse(reader[6].ToString()));
                ListViewItem listViewItem;
                Order order = orders.Find(item => item.idStoock == int.Parse(reader[6].ToString()));
                if (order == null)
                {
                    orders.Add(new Order(int.Parse(reader[6].ToString()), reader[5].ToString() + " " + reader[0].ToString() + " " + reader[2].ToString(), reader[1].ToString()));
                    orders[orders.Count - 1].count = int.Parse(reader[7].ToString());
                    
                    listViewItem = new ListViewItem(new string[] { $"{i + 1}", reader[5].ToString() + " " + reader[0].ToString() + " " + reader[2].ToString() + " ГБ", reader[1].ToString(), reader[4].ToString(), $"{orders[orders.Count - 1].count}" });
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(29, 31, 35);
                    listViewItem.UseItemStyleForSubItems = false;
                    listViewItem.SubItems[4].BackColor = color;
                 
                }
                else
                {
                    listViewItem = new ListViewItem(new string[] { $"{i + 1}", reader[5].ToString() + " " + reader[0].ToString() + " " + reader[2].ToString() + " ГБ", reader[1].ToString(), reader[4].ToString(), order.count.ToString() });
                    if (order.count != 0)
                    {
                        System.Drawing.Color color = System.Drawing.Color.FromArgb(54, 54, 54);

                        listViewItem.UseItemStyleForSubItems = false;
                        listViewItem.SubItems[4].BackColor = color;
                    }

                }
                coleches.Add(int.Parse(reader[4].ToString()));
                listView1.Items.Add(listViewItem);
                imageList.Images.Add(DBmanager.GetImage(int.Parse(reader[3].ToString())));
                listView1.SmallImageList = imageList;
                listViewItem.ImageIndex = i;
                i++;
                
                
            }
            max = coleches.Max();
            numericUpDown1.Value = max;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            LoadDataInsert();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Order order = orders.Find(item => item.count>0);
            if (order != null)
            {
                Insert();
                Messege mes = new Messege(Messege.messegeType.done, "Готово");
                mes.ShowDialog();
                Close();
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, "Для оформления заказа необходимо заказать хотя бы один телефонт.");
                mes.ShowDialog();
            }
                
        }
        private void Insert()
        {
            if (id != -1)
            {
                string queryz = $"DELETE FROM OrderData WHERE idOrderBuy = {id}";
                OleDbCommand commandz = new OleDbCommand(queryz, Form5.myConnection);
                commandz.ExecuteNonQuery();
            }
            int idOrderBuy = DBmanager.GetId("OrderBuy", "idOrderBuy");

            string query = $"INSERT INTO OrderData (idOrderBuy,data) VALUES ({idOrderBuy},'{DateTime.Now}') ";
            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);
            command.ExecuteNonQuery();

            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].count != 0)
                {
                    query = $"INSERT INTO OrderBuy (idOrderBuy,idStock,colech) VALUES ({idOrderBuy},{orders[i].idStoock},{orders[i].count}) ";

                    command = new OleDbCommand(query, Form5.myConnection);

                    command.ExecuteNonQuery();
                }

            }
            
            Export();
        }
        private void Export()
        {
            List<List<string>> list = new List<List<string>>();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (int.Parse(listView1.Items[i].SubItems[4].Text) != 0)
                {
                    list.Add(new List<string>());
                    for (int j = 1; j < listView1.Items[i].SubItems.Count; j++)
                    {
                        if (j != 3)
                            list[list.Count-1].Add(listView1.Items[i].SubItems[j].Text);
                    }
                }
                
                
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                DefaultExt = "xlsx",
                AddExtension = true
            };
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = saveFileDialog1.FileName;

            Exele ex = new Exele();
            ex.CreateNewFile();
            
            ex.ExportOstatki(list);
            ex.SaveAs(filename);
            ex.Close();
            System.Diagnostics.Process.Start(filename);
        }
        private void Form11_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

       

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            LoadDataInsert();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                numericUpDown2.Enabled = true;
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        numericUpDown2.Value = int.Parse(lv.SubItems[4].Text);
                    }
            }
            else
            {
                numericUpDown2.Enabled = false;
            }
        }

        private void numericUpDown2_ValueChanged_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        lv.SubItems[4].Text = numericUpDown2.Value.ToString();
                        Order order = orders.Find(item => item.idStoock == idStock[int.Parse(lv.SubItems[0].Text)-1]);
                        if (order != null)
                        {
                            orders.Remove(order);
                            order.count = int.Parse(numericUpDown2.Value.ToString());
                            orders.Add(order);

                            if (int.Parse(numericUpDown2.Value.ToString()) != 0)
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
                            
                        }
                       
                    }
            }
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
public class Order
{
    public int count;
    public int idStoock;
    public string name;
    public string color;

    public Order(int idStoock, string name, string color)
    {
        this.idStoock = idStoock;
        this.name = name;
        this.color = color;
    }
}
public enum Oper
{
    insert,update,procces
}
