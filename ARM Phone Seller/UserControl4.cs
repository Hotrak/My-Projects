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
    public partial class UserControl4 : UserControl
    {
        public UserControl4()
        {
            InitializeComponent();
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (Form5.famClient != "")
            {
                int idOdrer = DBmanager.GetId("Orders", "idOrder");
                string data = DateTime.Now.ToString();
                string query = $"INSERT INTO Orders (idClient,data,idOrder,skidka) VALUES ({Form5.idClient},'{DateTime.Now}',{idOdrer},{Form5.skidka})";
                OleDbCommand command = new OleDbCommand(query, Form5.myConnection);
                command.ExecuteNonQuery();


                for (int i = 0; i < Form5.bascet.Count; i++)
                {
                    query = $"SELECT idColor FROM Colors WHERE color = '{Form5.bascet[i].color}'";
                    command = new OleDbCommand(query, Form5.myConnection);
                    int idColor = int.Parse(command.ExecuteScalar().ToString());

                    query = $"SELECT idStock FROM Stock WHERE idPhone = {Form5.bascet[i].idPhone} AND idFleshMem = {Form5.bascet[i].idFleshMem} AND idColor = {idColor}";
                    command = new OleDbCommand(query, Form5.myConnection);
                    int idStock = int.Parse(command.ExecuteScalar().ToString());
                    //int idStock =
                    query = $"INSERT INTO Histori (idOrder,idStock,colech,price) VALUES ({idOdrer},{idStock},{Form5.bascet[i].count},{summa})";
                    command = new OleDbCommand(query, Form5.myConnection);
                    command.ExecuteNonQuery();

                }

                if (checkBox1.Checked)
                {
                    Export(idOdrer, data);
                    Messege mes = new Messege(Messege.messegeType.done, "Готово");
                    mes.ShowDialog();
                    DeleteBas();
                    if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenPhones();
                    }
                    Form5.famClient = null;
                    Form5.skidka = 0;
                   
                }
                else
                {
                    DeleteBas();
                    if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                    {
                        (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenPhones();
                    }
                    Form5.famClient = null;
                    
                    Form5.skidka = 0;
                    Messege mes = new Messege(Messege.messegeType.done, "Готово");
                    mes.ShowDialog();
                }



            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, "Для оформления заказа необходимо выбрать покупателя");
                mes.ShowDialog();
            }
        }
        private void Export(int idOdrer,string data)
        {
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
            ex.WriteToCell(idOdrer, data, Form5.skidka);
            ex.SaveAs(filename);
            ex.Close();
            System.Diagnostics.Process.Start(filename);
           
        }
        public void DeleteBas()
        {
            Form5.bascet = new List<Bascet>();
        }
        double summa = 0;
        public void LoadData(List<Bascet> bascet)
        {
            listView1.Items.Clear();
            
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(170, 200);
            
            int i = 0;
            summa=0;
            foreach (var item in bascet)
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { (i+1).ToString(), item.model, item.fleshMem.ToString(), item.color,item.count.ToString(),item.prise.ToString() });
                summa += item.prise* item.count;
                imageList.Images.Add(item.img);

                listView1.Items.Add(listViewItem);
                listView1.SmallImageList = imageList;
                listViewItem.ImageIndex = i;
                i++;
            }
            if (Form5.famClient != null)
            {
                label7.Text = "Покупатель: " + Form5.famClient;
                label8.Text = "Скидка: " + Form5.skidka.ToString() + "%";
                double price = summa - summa * (double.Parse(Form5.skidka.ToString()) / 100);
                label6.Text = "К оплате: " + price + " (BYN)";
            }
            else
            {
                label7.Text = "Покупатель: не выбран" + Form5.famClient;
                label8.Text = "";
                
                label6.Text = "К оплате: " + summa + " (BYN)";
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenPhones();
            }
        }
        int id =-1;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        id = int.Parse(lv.SubItems[0].Text);
                    }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id != -1)
            {
                Messege mes = new Messege(Messege.messegeType.warning, $"Вы хотите удалинть из корзины {Form5.bascet[id - 1].model} ?");
                mes.ShowDialog();
                if (mes.isAssept)
                {
                    //string query = $"UPDATE Colors SET colech = {Form5.bascet[id - 1].Maxcount} WHERE " +
                    //$"idFleshMem = {Form5.bascet[id - 1].idFleshMem} AND color = '{Form5.bascet[id - 1].color}'";

                    string query = $"SELECT idColor FROM Colors WHERE color = '{Form5.bascet[id - 1].color}'";
                    OleDbCommand command = new OleDbCommand(query, Form5.myConnection);
                    string idColor = command.ExecuteScalar().ToString();

                    query = $"UPDATE Stock SET colech = {Form5.bascet[id - 1].Maxcount} WHERE idPhone = {Form5.bascet[id - 1].idPhone} AND idFleshMem = {Form5.bascet[id - 1].idFleshMem} AND idColor = {idColor}";
                    command = new OleDbCommand(query, Form5.myConnection);
                    command.ExecuteNonQuery();

                    Form5.bascet.RemoveAt(id - 1);
                    if (Form5.bascet.Count==0)
                    {
                        if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                        {
                            (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenPhones();
                        }
                    }
                    LoadData(Form5.bascet);
                    id = -1;

                }
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Вы не выбрали телефон который хотите удалить из корзины!");
                mes.ShowDialog();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenClients();
            }
        }
    }
}
