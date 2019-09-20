using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
    class DB
    {

        public string data;
        public string model;
        public string os;
        public string Vos;
        public string scrinSize;
        public int ram;
        public string mamori;
        public string fSim;
        public string compani;
        public string prise;

        public string proc;
        public int speed;
        public int countCors;
        public int razProс;
        public int sGPU;
        public string GAccelModl;

        public string hDis;
        public string mDis;
        public string cDis;
        public string cFPanl;

        public OleDbConnection myConnection;

       
        public DB(string data, string model, string os, string Vos, string scrinSize, int ram, string mamori,
            string fSim, string compani, string prise, string proc, int speed, int countCors, int razProс, int sGPU, string GAccelModl,
            string hDis, string mDis, string cDis, string cFPanl)
        {
            //myConnection = new OleDbConnection(Form1.connectString);

            this.data = data;
            this.model = model;
            this.os = os;
            this.Vos= Vos;
            this.scrinSize= scrinSize;
            this.ram= ram;
            this.mamori= mamori;
            this.fSim=fSim;
            this.compani=compani;
            this.prise= prise;
        
            this.proc= proc;
            this.speed= speed;
            this.countCors= countCors;
            this.razProс= razProс;
            this.sGPU= sGPU;
            this.GAccelModl = GAccelModl;

            this.hDis = hDis;
            this.mDis= mDis;
            this.cDis= cDis;
            this.cFPanl= cFPanl;
        }
        public void Dalate(int id)
        {

        }
        public void Insart(int id)
        {

        }
        public void Update()
        {
            //string query = $"UPDATE Fons SET nam='{name}',fam='{fam}',fam2='{Tfam}',data ='{data}',tel='{tel}',loc='{from}',rang='{rang}' WHERE fam = '{this.fam}'";

            //OleDbCommand command = new OleDbCommand(query, myConnection);
            //command.ExecuteNonQuery();
        }

    }
}
