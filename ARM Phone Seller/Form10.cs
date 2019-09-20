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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            LoadData();
            activOrder = "-1";
        }
        List<int> idOrders = new List<int>();
        private void LoadData()
        {
            button2.Enabled = false;
            button5.Enabled = false;
            button4.Enabled = false;
            listView1.Items.Clear();
            string query = "SELECT b.idOrderBuy,SUM(a.colech),b.data FROM OrderBuy a,OrderData b WHERE a.idOrderBuy = b.idOrderBuy GROUP BY b.idOrderBuy,b.data";
            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            int i = 1;
            idOrders.Clear();
            while (reader.Read())
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { $"{reader[0].ToString()}", reader[2].ToString().Remove(reader[2].ToString().Length-8), reader[1].ToString() });

                listView1.Items.Add(listViewItem);
                i++;
                idOrders.Add(int.Parse(reader[0].ToString()));
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form11 form = new Form11();
            form.ShowDialog();
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Messege mes = new Messege(Messege.messegeType.warning, $"Вы действительно хотите удалить заказ №{activOrder}?");
            mes.ShowDialog();
            if (mes.isAssept)
            {
                //string query = $"DELETE FROM Phons WHERE idPhone={GetIdPhone()}";
                string query = $"DELETE FROM OrderData WHERE idOrderBuy = {activOrder}";

                OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

                command.ExecuteNonQuery();

                LoadData();

            }
        }
        string activOrder;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                {
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        activOrder = lv.SubItems[0].Text;

                    }
                }
                button2.Enabled = true;
                button5.Enabled = true;
                button4.Enabled = true;

            }
            else
            {
                button2.Enabled = false;
                button5.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1(int.Parse(activOrder));
            f.ShowDialog();
            LoadData();
        }

        private void Form10_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form11 form = new Form11(int.Parse(activOrder));
            form.ShowDialog();
            LoadData();
        }
    }
}
