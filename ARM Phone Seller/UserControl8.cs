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
    public partial class UserControl8 : UserControl
    {
        public UserControl8()
        {
            InitializeComponent();
            
           //listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
          


        }
        List<int> id = new List<int>();
        List<string> modals = new List<string>();

        public void LoadData(List<int> id=null)
        {
            modals.Clear();
            listView1.Items.Clear();
            this.id = id;
            if (id.Count == 0)
                return;
            string where = $"and ( a.idPhone = {id[0]}";
            for (int c = 1; c < id.Count;c++)
            {
                where += $" OR a.idPhone = {id[c]}";
            }
            where += " )";

          string query = $"SELECT  model, data, os, ram, sizeSc, fsim,Csim, hDesign, mDesign, scrin, sizeBat," +
                       $"texScren, soatnoshStor,  cam, Min(b.prise) ,a.idPhone,idProc FROM Phons a,Stock b WHERE a.idPhone = b.idPhone  {where} GROUP BY model, data, os, ram, sizeSc, fsim,Csim, hDesign, mDesign, scrin, sizeBat,texScren, soatnoshStor,  cam ,a.idPhone,idProc";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();


            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(90, 120);
            int i = 0; 
            while (reader.Read())
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { "", reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                    reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(),
                    reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString()});

                modals.Add(reader[0].ToString());
                listView1.Items.Add(listViewItem);
                imageList.Images.Add(DBmanager.GetImage(int.Parse(reader[15].ToString())));
                listView1.SmallImageList = imageList;
                listViewItem.ImageIndex = i;
                listView1.Items[i].UseItemStyleForSubItems = false;
                i++;

            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            foreach (ColumnHeader item in listView1.Columns)
            {
                item.Width += 20;
                //item. += 20;
            }
            Color color = Color.FromArgb(54, 54, 54);
            //listView1.Items[1].SubItems[4].BackColor = color;

            int count=0;
            int indexMax=0;
            int[] massColons = {4, 5 ,7,11,14,15};
               
            while (count < massColons.Length)
            {
                List<int> maxValus = new List<int>();
                for (int ii = 0; ii < i; ii++)
                {
                    if (massColons[count] != 15)
                    {
                        if (double.Parse(listView1.Items[ii].SubItems[massColons[count]].Text) >=
                       double.Parse(listView1.Items[indexMax].SubItems[massColons[count]].Text))
                            indexMax = ii;
                    }
                    else
                    {
                        if (double.Parse(listView1.Items[ii].SubItems[massColons[count]].Text) <=
                       double.Parse(listView1.Items[indexMax].SubItems[massColons[count]].Text))
                            indexMax = ii;
                    }
                   
                    //listView1.Items[ii].SubItems[4].BackColor = Color.Red;
                }
                for (int k = 0; k <i;k++)
                {
                    if (double.Parse(listView1.Items[k].SubItems[massColons[count]].Text) == double.Parse(listView1.Items[indexMax].SubItems[massColons[count]].Text))
                    {
                        listView1.Items[k].SubItems[massColons[count]].BackColor = Color.Green;
                    }
                }
               
                count++;
                button4.Enabled = false;
            }
           
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            //var el = id.First(n => n == modals.IndexOf(ActivModel));//Ищем введённый
            id.RemoveAt(modals.IndexOf(ActivModel));//удаляем

            if (id.Count == 0)
            {
                if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenPhones();
                }
            }
            else
                LoadData(id);
        }
        string ActivModel;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itiem = listView1.SelectedItems[listView1.SelectedItems.Count - 1];
                if (itiem != null)
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        ActivModel = lv.SubItems[1].Text;
                    }
                button4.Enabled = true;
            }else
                button4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            id.Clear();

            if (System.Windows.Forms.Application.OpenForms["Form5"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["Form5"] as Form5).OpenPhones();
            }
            listView1.Items.Clear();
            //LoadData();



        }
    }
}
