using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
    static class DBmanager
    {
        static public void SetImage(string path,int id)
        {
                     
            OleDbCommand oleDbCommand = new OleDbCommand($"UPDATE `Phons` SET img = ( ? ) WHERE idPhone = {id}", Form5.myConnection);         
                    
            OleDbParameter oleDbParameter = new OleDbParameter("image", OleDbType.VarBinary);                                     
                              
            Image image = Image.FromFile(path);                                                                              
            MemoryStream memoryStream = new MemoryStream();
            
            
            //image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);                                                        
            image.Save(memoryStream, image.RawFormat);                                                     
            oleDbParameter.Value = memoryStream.ToArray();                                                                        
            oleDbCommand.Parameters.Add(oleDbParameter);                                                                          
                                                                                                                                  
            oleDbCommand.ExecuteNonQuery();                                                                                       
                                                                                                                                  
            memoryStream.Dispose();
        }
        static public void SetImage(Image img, int id,string table= "`Phons`",string idRec= "idPhone")
        {
            Bitmap bitmap = new Bitmap(img);

            OleDbCommand oleDbCommand = new OleDbCommand($"UPDATE {table} SET img = ( ? ) WHERE {idRec} = {id}", Form5.myConnection);

            OleDbParameter oleDbParameter = new OleDbParameter("image", OleDbType.VarBinary);

            Image image = bitmap;
            MemoryStream memoryStream = new MemoryStream();


            //image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);                                                        
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
            oleDbParameter.Value = memoryStream.ToArray();
            oleDbCommand.Parameters.Add(oleDbParameter);

            oleDbCommand.ExecuteNonQuery();

            memoryStream.Dispose();
        }
        static public Image GetImage(int id)
        {

            OleDbCommand oleDbCommand = new OleDbCommand($"SELECT img FROM Phons WHERE idPhone={id}", Form5.myConnection);                 
                                                                                                                              
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();                                                      

            if (oleDbDataReader.HasRows)                                                                                        
            {
                MemoryStream memoryStream = new MemoryStream();

                foreach (DbDataRecord record in oleDbDataReader)                                                                 
                    memoryStream.Write((byte[])record["img"], 0, ((byte[])record["img"]).Length);

                Image image = Image.FromStream(memoryStream);
               
                //image.Save(@"C:\alien.BMP");                                                                                     
                memoryStream.Dispose();
                return image;
            }
            else
                return null;                                                                      

        }
        static Random rand = new Random();
        static public int GetId(string tabl, string idTa)
        {

            string query = $"SELECT {idTa} FROM {tabl}";

            OleDbCommand command = new OleDbCommand(query, Form5.myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            int[] idMess = new int[200];
            //idMess[0] = 100;
            int count = 0;
            while (reader.Read())
            {
                idMess[count] = int.Parse(reader[0].ToString());
                count++;
            }
            reader.Close();
            bool Rep = false;
            int id = idMess.Max() + 1;

            return id;
        }
        //static public int GetId(string tabl,string idTa)
        //{
        //    string query = $"SELECT {idTa} FROM {tabl}";

        //    OleDbCommand command = new OleDbCommand(query, Form5.myConnection);
        //    OleDbDataReader reader = command.ExecuteReader();
        //    int[] idMess = new int[200];
        //    idMess[0] = 100;
        //    int count = 1;
        //    while (reader.Read())
        //    {
        //        idMess[count] =int.Parse(reader[0].ToString());
        //        count++;
        //    }
        //    reader.Close();
        //    bool Rep = false;
        //    int id = 0;
        //    while (!Rep)
        //    {
        //        id = rand.Next(0,2000);
        //        for (int i =0;i<count;i++)
        //        {
        //            if (idMess[i] != id)
        //            {
        //                Rep = true;
        //            } 
        //        }
        //    }
            
        //    return id;
        //}
    }
}
