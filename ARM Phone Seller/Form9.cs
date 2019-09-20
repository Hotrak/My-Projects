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
    public partial class Form9 : Form
    {
        double itogSkid;
        public Form9(int idOrder,string data, double itogSkid = 0)
        {
            this.itogSkid = itogSkid;
            InitializeComponent();
            id = idOrder;
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.label2.Text = $"Заказ № {id}  от {data}";
        }
        int id;
        private string query;

        public void LoadData()
        {
            listView1.Items.Clear();
           
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(120, 150);
            //if (query == "")
                query = $"SELECT a.idPhone, a.model, c.price ,d.FleshMamari , c.colech FROM Phons a,Stock b,Histori c, FleshMem d WHERE" +
                    $" c.idOrder = {id} AND c.idStock = b.idStock AND a.idPhone = b.idPhone AND d.idFleshMam = b.idFleshMem";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            int i = 0;
            double sum = 0;
            while (reader.Read())
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { "", reader[1].ToString()+" "+ reader[3].ToString()+" ГБ", reader[4].ToString(), reader[2].ToString() });
                sum += double.Parse(reader[2].ToString()) * double.Parse(reader[4].ToString());
                listView1.Items.Add(listViewItem);
                imageList.Images.Add(DBmanager.GetImage(int.Parse(reader[0].ToString())));
                listView1.SmallImageList = imageList;
                listViewItem.ImageIndex = i;
                i++;
            }
            
            double result = Math.Round(sum - itogSkid, 2);
           
            double skidka =Math.Round(result / sum*100);
            label5.Text = $"ИТОГО (BYN) = {sum}";
            label6.Text = $"СКИДКА ({skidka}%) = {result}";
            label7.Text = $"ИТОГО СО СКИДКОЙ (BYN) = {sum - result}";
            reader.Close();
        }
        int activOrder;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form9_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
    }
}
