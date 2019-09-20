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
    public partial class Form5 : Form
    {
        public static Dictionary<string, bool> STATE = new Dictionary<string, bool>();
        string[] str = {"stat","srav","" };
        public static List<Bascet> bascet = new List<Bascet>();
        public static int idClient;
        public static string famClient;
        public static int skidka;
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=ARMtel2.mdb";
        public static OleDbConnection myConnection;
        public Form5()
        {
            InitializeComponent();
            Opacity = 0;
            Timer timer = new Timer();
            timer.Tick += new EventHandler((sender, e) =>
            {
                if ((Opacity += 0.5d) == 1) timer.Stop();
            });
            timer.Interval = 100;
            timer.Start();

            int zIndex = Controls.GetChildIndex(userControl31);
            Form5.famClient = "";


        }
        public int GetZindex(UserControl control)
        {
            return Controls.GetChildIndex(control);
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(41, 44, 51);
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            panel3.BringToFront();
            
            OpenPhones();
            userControl51.LoadData();//Клиенты
            try
            {
                string query = $"INSERT INTO Cors (idProc) VALUES (1)";

                OleDbCommand command = new OleDbCommand(query, Form5.myConnection);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //MessageBox.Show("Ошибка");
            }

        }
        public static void ChengState(string inex)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenStatistick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenPhones(false);
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bascet.Count > 0)
            {
                
                OpenBascket();
            }
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Ваша корзина пуста. Добавте телефон чтобы оформить заказ");
                mes.ShowDialog();
            }
            

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        private void Form5_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        public void OpenAbaut(int id)
        {
            //panel3.Height = button4.Height;
            //panel3.Top = button4.Top;
            userControl11.BringToFront();
            userControl11.LoadData(id);
        }
        public void OpenCheng(int id)
        {
            panel3.Height = button3.Height;
            panel3.Top = button3.Top;
            userControl31.oper = Operation.update;
            userControl31.LoadData(id);
            userControl31.BringToFront();
            
        }
        public void OpenBascket()
        {
            userControl41.LoadData(bascet);
            userControl41.BringToFront();
            
            panel3.Height = button4.Height;
            panel3.Top = button4.Top;

        }
        public void OpenPhones(bool load = true)
        {
            
            if(load) phonsPanal1.LoadData();
                phonsPanal1.BringToFront();

            phonsPanal1.TimerStart();
            phonsPanal1.Focus();
            panel3.Height = button3.Height;
            panel3.Top = button3.Top;
        }
        public void OpenInsert()
        {
            panel3.Height = button3.Height;
            panel3.Top = button3.Top;
            userControl31.ResetForm();
            userControl31.oper = Operation.insert;
            userControl31.LoadData();
            userControl31.Visible= false;
            userControl31.BringToFront();
            userControl31.Visible = true;

        }
        public void OpenClients()
        {
            
            userControl51.StartTimer();
            userControl51.LoadData(); 
            userControl51.BringToFront();
            panel3.Height = button7.Height;
            panel3.Top = button7.Top;
        }
        public void OpenHistori()
        {
            if (Controls.GetChildIndex(userControl61) != 0)
            {
                userControl61.LoadData();
                userControl61.ClerAllTexBooxes();
                userControl61.BringToFront();
            }
                
           
            panel3.Height = button9.Height;
            panel3.Top = button9.Top;
            
        }
        public void OpenStatistick()
        {
            if (Controls.GetChildIndex(userControl71) != 0)
            {
                userControl71.loadData();
                userControl71.BringToFront();
            }
                
            panel3.Height = button1.Height;
            panel3.Top = button1.Top;

        }
        public List<int> idPhonesForCompere = new List<int>();
        public void OpenCompere()
        {
            if (Controls.GetChildIndex(userControl81) != 0)
            {
                userControl81.LoadData(idPhonesForCompere);
                userControl81.BringToFront();
            }
            
           
            panel3.Height = button8.Height;
            panel3.Top = button8.Top;

        }
        private void button7_Click(object sender, EventArgs e)
        {
            userControl31.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            userControl31.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button4.Text = "Корзина("+Form5.bascet.Count+")";
            button8.Text = "Сравнение("+idPhonesForCompere.Count+")";
        }

        private void userControl41_Load(object sender, EventArgs e)
        {

        }

        

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            //userControl31.BringToFront();
            //panel6.Height = button7.Height;
            //panel6.Top = button7.Top;
        }
        
        private void button7_Click_2(object sender, EventArgs e)
        {
            OpenClients();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            OpenHistori();

            //Form9 form9 = new Form9(1218);
            //form9.LoadData();
            //form9.Show();
            
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if(idPhonesForCompere.Count!=0)
                OpenCompere();
            else
            {
                Messege mes = new Messege(Messege.messegeType.error, $"Сравнивать нечего. Добавте телефоны чтобы начать сравнение");
                mes.ShowDialog();
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form10 form11 = new Form10();
            OpenPhones();
            form11.ShowDialog();
        }
    }
}
