using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Data.OleDb;

namespace BD
{
    public partial class UserControl7 : UserControl
    {

        Random ran = new Random();
        public UserControl7()
        {
            InitializeComponent();
            

            //loadData();
        }
        string[] month = { "январь","февраль","март","апрель","май","июнь","июль","август","сентябрь", "октябрь", "ноябрь", "дикабрь"};
        public void loadData()
        {

            LoadModals();

            //chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(41, 44, 51);
            chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(64, 64, 64);

            comboBox2.Items.Clear();
            string query = $"SELECT YEAR(c.data) FROM  Orders c  " +
               $"GROUP BY YEAR(c.data) ORDER BY YEAR(c.data) DESC";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                comboBox2.Items.Add(reader[0].ToString());
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader[0].ToString());
                }
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                button4.Enabled = false;
                button5.Enabled = false;
            }
               
            comboBox3.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox1.SelectedIndex = 1;
            comboBox4.SelectedIndex = 1;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 2;
            comboBox8.SelectedIndex = 0;


            LoadYPhonsPrise("2019");
            LoadPhonsTable();

            foreach (var item in panel1.Controls)
            {
                if (item is CheckBox)
                {
                    if ((item as CheckBox) != checkBox1)
                    {
                        (item as CheckBox).Dispose();
                    }
                }
            }
            intervalTop = 20;
            intervalLeft = 0;
            for (int i = 1; i < comboBox6.Items.Count; i++)
            {
                comboBox6.SelectedIndex = i;
                CreateCheck(comboBox6.Text);
            }
            listView1.Visible = true;
            listView2.Visible = false;

        }
        int horoh = 1;
        private void AddDate(string name, int n)
        {
            for (int i = 1; i <= n; i++)
            {
                bool bo = false ;
                for (int j = 0; j < Month.Count; j++)
                {
                    if (Month[j] == i && Company[j] == name)
                    {
                        chart1.Series[name].Points.Add(Prises[j]);
                        bo = true;
                        //MessageBox.Show(i.ToString() + " " + Month[j]); 
                        break;
                    }
                }
                if (!bo)
                {
                    chart1.Series[name].Points.Add(0);

                }

                horoh++;
            }
        }
        private void AddDateOb(string name, int n)
        {
            for (int i = 1; i <= n; i++)
            {
                bool bo = false;
                for (int j = 0; j < Month.Count; j++)
                {
                    if (Month[j] == i)
                    {
                        chart1.Series[name].Points.Add(Prises[j]);
                        bo = true;
                        break;
                    }
                }
                if (!bo)
                {
                    chart1.Series[name].Points.Add(0);

                }
                horoh++;
            }
        }
        public void LoadYearPhons()
        {
            string query = $"SELECT  SUM(a.prise) , b.componi , YEAR(c.data) FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone{sortCompaniStr} GROUP BY b.componi, YEAR(c.data) ORDER BY YEAR(c.data)";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MessageBox.Show(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());
            }

        }
        List<double> Prises = new List<double>();
        List<int> Month = new List<int>();
        List<string> Company = new List<string>();
        public void LoadModals()
        {
            comboBox6.Items.Clear();
            comboBox6.Items.Add("");
            string query = $"SELECT componi FROM Phons GROUP BY componi";
            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                
                comboBox6.Items.Add(reader[0].ToString());

            }
            
        }
        public void LoadYPhonsPrise(string year)
        {
            //MessageBox.Show("df");
            Prises.Clear();
            Company.Clear();
            Month.Clear();
            chart1.Series.Clear();
            string query;
            // - (c.skidka/100*a.prise *d.colech)) 
            if (comboBox5.SelectedIndex == 0)
                query = $"SELECT SUM(d.price *d.colech), b.componi , MONTH(c.data) FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                    $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND YEAR(c.data)='{year}'{sortCompaniStr} GROUP BY b.componi, MONTH(c.data) ORDER BY MONTH(c.data)";//month(dateField)
            else
                query = $"SELECT  SUM(d.colech) , b.componi , MONTH(c.data) FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                   $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND YEAR(c.data)='{year}'{sortCompaniStr} GROUP BY b.componi, MONTH(c.data) ORDER BY MONTH(c.data)";//month(dateField)

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Prises.Add(double.Parse(reader[0].ToString()));
                Company.Add(reader[1].ToString());
                Month.Add(int.Parse(reader[2].ToString()));

               // MessageBox.Show(reader[0].ToString() +" "+ reader[1].ToString() +" "+ reader[2].ToString());
            }
            var selectedTeams = from t in Company
                                orderby t
                                select t;
            string temp = "";
            
            foreach (var item in selectedTeams)
            {
                if (item != temp)
                {
                    temp = item;
                    
                    addSeries(item,1,12);
                    AddDate(item, 12);
                }     
            }
            string sort = "";
            string desc = "";
            if (comboBox7.SelectedIndex == 0) sort = "SUM(a.prise *d.colech)";
            if (comboBox7.SelectedIndex == 1) sort = "SUM(d.colech)";
            if (comboBox7.SelectedIndex == 2) sort = "MONTH(c.data)";

            if (comboBox8.SelectedIndex == 0) desc = "DESC";
            if (comboBox8.SelectedIndex == 1) desc = "";
            query = $"SELECT  ROUND(SUM(d.price *d.colech),2) , b.componi , MONTH(c.data),SUM(d.colech) FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                    $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND YEAR(c.data)='{year}'{sortCompaniStr} GROUP BY b.componi, MONTH(c.data) ORDER BY {sort} {desc}";
            LoadTable(query);

        }
        public void LoadYPhonsPriseOb(string year)
        {
            Prises.Clear();
            Company.Clear();
            Month.Clear();
            chart1.Series.Clear();
            string query;
            if (comboBox5.SelectedIndex == 0)
                query = $"SELECT SUM(d.price *d.colech)  , MONTH(c.data) FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                    $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND YEAR(c.data)='{year}' GROUP BY MONTH(c.data) ORDER BY MONTH(c.data)";//month(dateField)
            else
                query = $"SELECT SUM(d.colech), MONTH(c.data) FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                    $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND YEAR(c.data)='{year}' GROUP BY MONTH(c.data) ORDER BY MONTH(c.data)";//month(dateField)

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Prises.Add(double.Parse(reader[0].ToString()));
                
                Month.Add(int.Parse(reader[1].ToString()));

                // MessageBox.Show(reader[0].ToString() +" "+ reader[1].ToString() +" "+ reader[2].ToString());
            }
            addSeries("Продажи", 1, 12);
            AddDateOb("Продажи", 12);

            string sort = "";
            string desc = "";
            if (comboBox7.SelectedIndex == 0) sort = "SUM(a.prise *d.colech)";
            if (comboBox7.SelectedIndex == 1) sort = "SUM(d.colech)";
            if (comboBox7.SelectedIndex == 2) sort = "MONTH(c.data)";

            if (comboBox8.SelectedIndex == 0) desc = "DESC";
            if (comboBox8.SelectedIndex == 1) desc = "";
            
            query = $"SELECT ROUND(SUM(d.price *d.colech),2)  , MONTH(c.data),SUM(d.colech) FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                   $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND YEAR(c.data)='{year}' GROUP BY MONTH(c.data) ORDER BY {sort} {desc}";//month(dateField)
            LoadTable(query);
        }
        public void LoadMPhonsPriseOb(string month, string year)
        {
            Prises.Clear();
            Month.Clear();
            chart1.Series.Clear();
            string query = "";
            if (comboBox5.SelectedIndex == 0)
            {
                query = $"SELECT  SUM(d.price *d.colech) , DAY(c.data) FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                       $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND MONTH(c.data)='{month}' AND YEAR(c.data)='{year}' GROUP BY  DAY(c.data) ORDER BY DAY(c.data)";
            }
            else
                query = $"SELECT  SUM(d.colech) ,DAY(c.data) FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                    $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND MONTH(c.data)='{month}' AND YEAR(c.data)='{year}' GROUP BY DAY(c.data) ORDER BY DAY(c.data)";//month(dateField)

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Prises.Add(double.Parse(reader[0].ToString()));
                Month.Add(int.Parse(reader[1].ToString()));
            }
            addSeries("Продажи", 1, 30);
            AddDateOb("Продажи",30);

            string sort = "";
            string desc = "";
            if (comboBox7.SelectedIndex == 0) sort = "SUM(a.prise *d.colech)";
            if (comboBox7.SelectedIndex == 1) sort = "SUM(d.colech)";
            if (comboBox7.SelectedIndex == 2) sort = "DAY(c.data)";

            if (comboBox8.SelectedIndex == 0) desc = "DESC";
            if (comboBox8.SelectedIndex == 1) desc = "";

            query = $"SELECT  ROUND(SUM(d.price *d.colech),2)- c.skidka/100 * ROUND(SUM(d.price *d.colech),2) , DAY(c.data) ,SUM(d.colech)  FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                  $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND MONTH(c.data)='{month}' AND YEAR(c.data)='{year}' GROUP BY DAY(c.data),c.skidka ORDER BY {sort} {desc}";

            LoadTable(query);

        }
        public void LoadMPhonsPrise(string month , string year)
        {
            Prises.Clear();
            Company.Clear();
            Month.Clear();
            chart1.Series.Clear();
            string query="";
           
            if (comboBox5.SelectedIndex == 0)
            {
                //query = $"SELECT  SUM(a.prise *d.colech) - (c.skidka/100 * SUM(a.prise *d.colech))  , b.componi , DAY(c.data) , SUM(d.colech)  FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                //       $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND MONTH(c.data)='{month}' AND YEAR(c.data)='{year}'{sortCompaniStr} GROUP BY b.componi, DAY(c.data) ORDER BY DAY(c.data)";
                query = $"SELECT  SUM(d.price *d.colech)- c.skidka/100 * ROUND(SUM(d.price *d.colech),2)  , b.componi , DAY(c.data) , SUM(d.colech)  FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND MONTH(c.data)='{month}' AND YEAR(c.data)='{year}'{sortCompaniStr} GROUP BY b.componi, DAY(c.data),c.skidka ORDER BY DAY(c.data)";

            }
            else
                query = $"SELECT  SUM(d.colech) , b.componi , DAY(c.data) FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                    $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND MONTH(c.data)='{month}' AND YEAR(c.data)='{year}'{sortCompaniStr} GROUP BY b.componi, DAY(c.data) ORDER BY DAY(c.data)";//month(dateField)

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(120, 150);
            int i = 0;
            while (reader.Read())
            {
                Prises.Add(double.Parse(reader[0].ToString()));
                Company.Add(reader[1].ToString());
                Month.Add(int.Parse(reader[2].ToString()));

               
            }
            var selectedTeams = from t in Company
                                orderby t
                                select t;
            string temp = "";
            foreach (var item in selectedTeams)
            {
                if (item != temp)
                {
                    temp = item;
                    addSeries(item,1,30);
                    AddDate(item, 30);
                }
            }
            string sort = "";
            string desc = "";
            if (comboBox7.SelectedIndex == 0) sort = "SUM(a.prise *d.colech)";
            if (comboBox7.SelectedIndex == 1) sort = "SUM(d.colech)";
            if (comboBox7.SelectedIndex == 2) sort = "DAY(c.data)";

            if (comboBox8.SelectedIndex == 0) desc = "DESC";
            if (comboBox8.SelectedIndex == 1) desc = "";
           
            query = $"SELECT  ROUND(SUM(d.price *d.colech),2)- c.skidka/100 * ROUND(SUM(d.price *d.colech),2) , b.componi , DAY(c.data) ,SUM(d.colech)  FROM Stock a, Phons b, Orders c, Histori d WHERE c.idOrder = d.idOrder " +
                  $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND MONTH(c.data)='{month}' AND YEAR(c.data)='{year}'{sortCompaniStr} GROUP BY b.componi, DAY(c.data),c.skidka ORDER BY {sort} {desc}";

            LoadTable(query);

        }
        private void LoadTable(string query, bool tel = false)
        {
            listView1.Items.Clear();
            
            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(120, 150);
            
            
            
            bool exp = false;

            while (reader.Read())
            {
                ListViewItem listViewItem;
                
                try
                {
                    if(comboBox3.SelectedIndex==0)
                        listViewItem = new ListViewItem(new string[] {  reader[1].ToString(),month[int.Parse(reader[2].ToString())-1] , reader[3].ToString(), reader[0].ToString() });
                    else
                        listViewItem = new ListViewItem(new string[] {  reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[0].ToString() });
                    listView1.Columns[0].Text = "Модель";
                    listView1.Columns[1].Text = "Дата";
                    listView1.Columns[2].Text = "Шт";
                    listView1.Columns[3].Text = "Сумма (BYN)";
                }
                catch (Exception)
                {
                    if (comboBox3.SelectedIndex == 0)
                    {
                        listViewItem = new ListViewItem(new string[] {month[int.Parse(reader[1].ToString())-1], reader[2].ToString(), reader[0].ToString() });
                    }
                        
                    else
                    {
                        listViewItem = new ListViewItem(new string[] { reader[1].ToString() + " " + comboBox3.Text, reader[2].ToString(), reader[0].ToString() });
                    }
                        
                    exp = true;
                }
               
                listView1.Items.Add(listViewItem);
               
            }
            if (exp)
            {
                listView1.Columns[0].Text = "Дата";
                listView1.Columns[1].Text = "Шт";
                listView1.Columns[2].Text = "Сумма (BYN)";
                listView1.Columns[3].Text = ""; 
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            foreach (ColumnHeader item in listView1.Columns)
            {
                
                item.Width += 40;

            }

            if (!exp)
                listView1.Columns[3].Width = 160;
            else
                listView1.Columns[3].Width = 0;
            addColor(ref listView1);
        }
        private void addColor(ref ListView list)
        {
            for (int i = 0; i < list.Items.Count; i++)
                if (i % 2 != 0)
                {
                    Color color = Color.FromArgb(54, 54, 54);
                    list.Items[i].UseItemStyleForSubItems = false;
                    for (int j = 0; j < list.Items[i].SubItems.Count; j++)
                    {
                        list.Items[i].SubItems[j].BackColor = color;
                    }
                }
        }
        private void LoadPhonsTable()
        {
            listView2.Items.Clear();
            string sort = "";
            if (comboBox7.SelectedIndex == 0) { sort = "SUM(a.prise *d.colech)"; }
            if (comboBox7.SelectedIndex == 1) { sort = "SUM(d.colech)"; }

            string desc;
            if(comboBox8.SelectedIndex == 0)
                desc = "desc";
            else
                desc = "";

            string query;
            if (comboBox3.SelectedIndex != 0)
            {
                if (comboBox7.SelectedIndex == 2) { sort = "DAY(c.data)"; }
             
                query = $"SELECT ROUND(SUM(d.price *d.colech),2)- c.skidka/100 * ROUND(SUM(d.price *d.colech),2) ,b.model , DAY(c.data),SUM(d.colech),i.color,f.FleshMamari,b.componi FROM Stock a, Phons b, Orders c, Histori d,Colors i , FleshMem f WHERE c.idOrder = d.idOrder " +
                        $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND a.idColor = i.idColor AND f.idFleshMam = a.idFleshMem AND  MONTH(c.data)='{comboBox3.SelectedIndex}' AND YEAR(c.data)='{comboBox2.Text}'{sortCompaniStr} GROUP BY b.componi, b.model, DAY(c.data) ,i.color,f.FleshMamari,c.skidka ORDER BY {sort} {desc}";//month(dateField)
            }
            else
            {
                if (comboBox7.SelectedIndex == 2) { sort = "MONTH(c.data)"; }
               
                query = $"SELECT  ROUND(SUM(d.price *d.colech),2) - c.skidka/100 * ROUND(SUM(d.price *d.colech),2) , b.model , MONTH(c.data),SUM(d.colech),i.color,f.FleshMamari,b.componi FROM Stock a, Phons b, Orders c, Histori d, Colors i , FleshMem f WHERE c.idOrder = d.idOrder " +
                     $"AND d.idStock = a.idStock AND a.idPhone = b.idPhone AND a.idColor = i.idColor AND f.idFleshMam = a.idFleshMem AND YEAR(c.data)='{comboBox2.Text}'{sortCompaniStr} GROUP BY b.componi , b.model, MONTH(c.data),i.color,f.FleshMamari,c.skidka ORDER BY {sort} {desc}";//month(dateField)

            }

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

            OleDbDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                ListViewItem listViewItem;
                if (comboBox3.SelectedIndex != 0)
                    listViewItem = new ListViewItem(new string[] { reader[6].ToString() + " "+ reader[1].ToString() + " " + reader[5].ToString() + " ГБ", reader[4].ToString(), reader[2].ToString(), reader[3].ToString(), reader[0].ToString() });
                else
                    listViewItem = new ListViewItem(new string[] { reader[6].ToString() + " " + reader[1].ToString() + " " + reader[5].ToString() + " ГБ", reader[4].ToString(), month[int.Parse(reader[2].ToString())-1], reader[3].ToString(), reader[0].ToString() });

                listView2.Items.Add(listViewItem);
            }
            addColor(ref listView2);
        }
            int countLabels=0;
        private void Create(string str)
        {
            Label lb1 = new Label();
            lb1.Name = "Label1";
            lb1.Text = str;
            lb1.Left = 36+countLabels;
            lb1.Top = 355;
            lb1.TextAlign =ContentAlignment.TopCenter;
            lb1.Font = new Font("Arial", 9);
            countLabels += 78;


            this.Controls.Add(lb1);
            lb1.BringToFront();
        }
        private void addSeries(string name, int min, int max)
        {
            var chart = chart1.ChartAreas[0];

            chart.AxisX.IntervalType = DateTimeIntervalType.Number;

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;

            if (comboBox4.SelectedIndex == 0 || comboBox4.SelectedIndex == 2)
            {
                chart.AxisX.Minimum = min;
                chart.AxisX.Maximum = max;
            }
            else
            {
                chart.AxisX.Minimum = min - 1;
                chart.AxisX.Maximum = max + 1;
            }

            if (comboBox5.SelectedIndex == 0)
            {
                if (comboBox1.SelectedIndex == 1)
                    chart.AxisY.Maximum = 80000;
                else
                    chart.AxisY.Maximum = 200000;
                chart.AxisY.Interval = 10000;
            }
            else
            {

                chart.AxisY.Maximum = 50;

                chart.AxisY.Interval = 5;
            }
            chart.AxisY.Minimum = 0;

            chart.AxisX.Interval = 1;

            chart1.Series.Add(name);

            if (comboBox4.SelectedIndex == 0) { chart1.Series[name].ChartType = SeriesChartType.Spline; chart1.Series[name].BorderWidth = 3; }
            if (comboBox4.SelectedIndex == 1) { chart1.Series[name].ChartType = SeriesChartType.Column; }
            if (comboBox4.SelectedIndex == 2) { chart1.Series[name].ChartType = SeriesChartType.Line; chart1.Series[name].BorderWidth = 3; }
            
            
        }
        private void chart1_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(e.Text))
                return;

            Console.WriteLine(e.HitTestResult.ChartElementType);
           
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                case ChartElementType.DataPointLabel:
                case ChartElementType.Gridlines:
                case ChartElementType.Axis:
                case ChartElementType.TickMarks:
                case ChartElementType.PlottingArea:
               
                   
                    var area = chart1.ChartAreas[0];

                    
                    var areaPosition = area.Position;

                    
                    var areaRect = new RectangleF(areaPosition.X * chart1.Width / 100, areaPosition.Y * chart1.Height / 100,
                        areaPosition.Width * chart1.Width / 100, areaPosition.Height * chart1.Height / 100);

                    
                    var innerPlot = area.InnerPlotPosition;

                    double x = area.AxisX.Minimum +
                                (area.AxisX.Maximum - area.AxisX.Minimum) * (e.X - areaRect.Left - innerPlot.X * areaRect.Width / 100) /
                                (innerPlot.Width * areaRect.Width / 100);
                    double y = area.AxisY.Maximum -
                                (area.AxisY.Maximum - area.AxisY.Minimum) * (e.Y - areaRect.Top - innerPlot.Y * areaRect.Height / 100) /
                                (innerPlot.Height * areaRect.Height / 100);
                    if(comboBox5.SelectedIndex == 1)
                        y = Math.Round(y,0);
                    
                    x = Math.Round(x,0);
                    textBox1.Text= $"{x:F2} {y:F2}";

                    //Console.WriteLine("{0:F2} {1:F2}", x, y);

                    if (comboBox3.SelectedIndex != 0)
                        e.Text = String.Format("{0:F2} {1:F2}", x, y);
                    else
                    {
                        if(x>0&&x<13)
                            e.Text = String.Format("{0:F2} {1:F2}", month[int.Parse((x - 1).ToString())], y);
                    }
                        

                    break;
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void UserControl7_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            //textBox1.Text = String.Format("X = {0}, Y = {1}", e.X, e.Y);
            int X = e.X;
            int Y = e.Y;
            textBox1.Visible = false;
            textBox1.Left = X;
            textBox1.Top = Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //LoadYearPhons();
            if (comboBox1.SelectedIndex == 1)
            {
                if (comboBox3.SelectedIndex != 0)
                    LoadMPhonsPrise(comboBox3.SelectedIndex.ToString(), comboBox2.Text);
                else
                    LoadYPhonsPrise(comboBox2.Text);
                listView1.Visible = true;
                listView2.Visible = false;
            }
            else
            if (comboBox1.SelectedIndex == 0)
            {
                if (comboBox3.SelectedIndex != 0)
                    LoadMPhonsPriseOb(comboBox3.SelectedIndex.ToString(), comboBox2.Text);
                else
                    LoadYPhonsPriseOb(comboBox2.Text);
                listView1.Visible = true;
                listView2.Visible = false;
            }
               

            if (comboBox1.SelectedIndex == 2)
            {
                LoadPhonsTable();
                listView2.Visible = true;
                listView1.Visible = false;
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<List<string>> list = new List<List<string>>();

            ListView listView;
            if (listView1.Visible) listView = listView1;
            else listView = listView2;


            for (int i = 0; i < listView.Items.Count; i++)
            {
                list.Add(new List<string>());
                for (int j = 0; j < listView.Items[i].SubItems.Count; j++)
                {
                    list[i].Add(listView.Items[i].SubItems[j].Text);
                }
            }

            //Exele ex = new Exele("d:/ex1.xlsx", 1);

            ////string data 
            //ex.StatistickExportModals(comboBox2.Text +" " + comboBox3.Text, list);

            //ex.SaveAs(@"C:\Users\User\Desktop\легчайшый тест\ex2.xlsx");
            //ex.Close();
            //System.Diagnostics.Process.Start(@"C:/Users/User/Desktop/легчайшый тест/ex2.xlsx");

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
            ex.StatistickExportModals(comboBox2.Text + " " + comboBox3.Text, list);
            ex.SaveAs(filename);
            ex.Close();
            System.Diagnostics.Process.Start(filename);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (!panel1.Visible)
            {
                panel1.Visible = true;
            }
            else
            {
                sortCompaniStr = "";
                textBox2.Text = "";
                foreach (var item in panel1.Controls)
                {
                    if (item is CheckBox)
                    {
                        if ((item as CheckBox).Checked && (item as CheckBox).Text != "Выделить всё")//(item as CheckBox).Text != "Выделить всё" &&
                        {

                            if (sortCompaniStr == "") { sortCompaniStr += $" AND (b.componi = '{(item as CheckBox).Text}'"; textBox2.Text += $"{(item as CheckBox).Text}"; }
                            else { sortCompaniStr += $" OR b.componi = '{(item as CheckBox).Text}'"; textBox2.Text += $", {(item as CheckBox).Text}"; }
                        }
                    }
                }
                
                if (sortCompaniStr!="") sortCompaniStr += ")";

                panel1.Visible = false;
            }
                
        }
        string sortCompaniStr ="";
        int intervalTop = 20;
        int intervalLeft = 0;
        bool isOver=false;
        private void CreateCheck(string text)
        {
            CheckBox check = new CheckBox();
           
            check.Left = checkBox1.Left+ intervalLeft;
            check.Top = checkBox1.Top + intervalTop;
            check.Width = 100;
            check.Text = text;

            check.Font = checkBox1.Font;
            check.ForeColor = checkBox1.ForeColor;

            if (intervalTop < 100 && !isOver)
            {
                intervalTop += 20;
                //intervalLeft += 125;
                //isOver = true;
            }
            else
            {
                intervalTop = 20;
                intervalLeft += 100;
            }
            //intervalTop += 20;
            this.panel1.Controls.Add(check);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool check = false;
            if ((sender as CheckBox).Checked)
            {
                check = true;
            }
            else
                check = false;
            foreach (var item in panel1.Controls)
            {
                if (item is CheckBox)
                {
                    if ((item as CheckBox).Text != "Выделить всё")
                    {
                        (item as CheckBox).Checked = check;
                    }
                }
            }

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 0)
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
            }
            else
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //LoadYearPhons();
            if (comboBox1.SelectedIndex == 1)
            {
                if (comboBox3.SelectedIndex != 0)
                {
                    label9.Visible = false;
                    LoadMPhonsPrise(comboBox3.SelectedIndex.ToString(), comboBox2.Text);
                }
                else
                {
                    LoadYPhonsPrise(comboBox2.Text);
                    if (comboBox4.SelectedIndex == 1)
                        label9.Visible = true;
                    else
                        label9.Visible = false;

                }

                listView1.Visible = true;
                listView2.Visible = false;
            }
            else
            if (comboBox1.SelectedIndex == 0)
            {
                if (comboBox3.SelectedIndex != 0)
                {
                    label9.Visible = false;
                    LoadMPhonsPriseOb(comboBox3.SelectedIndex.ToString(), comboBox2.Text);
                }
                else
                {
                    if(comboBox4.SelectedIndex==1)
                        label9.Visible = true;
                    else
                        label9.Visible = false;
                    LoadYPhonsPriseOb(comboBox2.Text);
                }
                listView1.Visible = true;
                listView2.Visible = false;
            }


            if (comboBox1.SelectedIndex == 2)
            {
                LoadPhonsTable();
                listView2.Visible = true;
                listView1.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<List<string>> list = new List<List<string>>();

            ListView listView;
            if (listView1.Visible) listView = listView1;
            else listView = listView2;


            for (int i = 0; i < listView.Items.Count; i++)
            {
                list.Add(new List<string>());
                for (int j = 0; j < listView.Items[i].SubItems.Count; j++)
                {
                    list[i].Add(listView.Items[i].SubItems[j].Text);
                }
            }

            //Exele ex = new Exele("d:/ex1.xlsx", 1);

            ////string data 
            //ex.StatistickExportModals(comboBox2.Text +" " + comboBox3.Text, list);

            //ex.SaveAs(@"C:\Users\User\Desktop\легчайшый тест\ex2.xlsx");
            //ex.Close();
            //System.Diagnostics.Process.Start(@"C:/Users/User/Desktop/легчайшый тест/ex2.xlsx");

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
            ex.StatistickExportModals(comboBox2.Text + " " + comboBox3.Text, list);
            ex.SaveAs(filename);
            ex.Close();
            System.Diagnostics.Process.Start(filename);
        }
    }
}
