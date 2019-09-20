using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelObj = Microsoft.Office.Interop.Excel;

namespace BD
{
    class Exele
    {
        string path = "";
        Application excel = new ExcelObj.Application();
        Workbook wb;
        Worksheet ws;
        public Exele()
        {

        }
        public Exele(string path,int Sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
        }
        public string ReadCell(int i,int j)
        {
            i++;
            j++;
            if (ws.Cells[i,j].Value2 != null)
            {
                return ws.Cells[i, j].Value2;
            }
            return "";
        }

        public void StatistickExportModals(string data, List<List<string>> list)
        {

            ws.Cells[1, 1].Value2 = "Отчёт за";
            ws.Cells[1, 2].Value2 = data;
            if (list[0].Count == 5)
            {
                ws.Cells[2, 1].Value2 = "Модель";
                ws.Cells[2, 2].Value2 = "Цвет";
                ws.Cells[2, 3].Value2 = "Дата";
                ws.Cells[2, 4].Value2 = "Шт";
                ws.Cells[2, 5].Value2 = "Сумма(BYN)";
            }
            if (list[0].Count == 4)
            {
                ws.Cells[2, 1].Value2 = "Компания";
                ws.Cells[2, 2].Value2 = "Дата";
                ws.Cells[2, 3].Value2 = "Шт";
                ws.Cells[2, 4].Value2 = "Сумма(BYN)";
            }
            if (list[0].Count == 3)
            {
                ws.Cells[2, 1].Value2 = "Дата";
                ws.Cells[2, 2].Value2 = "Шт";
                ws.Cells[2, 3].Value2 = "Сумма(BYN)";
            }
            ws.get_Range("A2", $"E2").Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ws.get_Range("A3", $"E{100}").Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //ws.get_Range("B1", $"B{count}").Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            int count = 2;
            double sum = 0;
            ws.Columns[1].ColumnWidth = 25;
            ws.Columns[2].ColumnWidth = 13;
            ws.Columns[3].ColumnWidth = 13;
            ws.Columns[4].ColumnWidth = 16;
            ws.Columns[5].ColumnWidth = 16;

            (ws.Cells[2, 1] as ExcelObj.Range).Font.Size = 14;
            (ws.Cells[2, 2] as ExcelObj.Range).Font.Size = 14;
            (ws.Cells[2, 3] as ExcelObj.Range).Font.Size = 14;
            (ws.Cells[2, 4] as ExcelObj.Range).Font.Size = 14;
            (ws.Cells[2, 5] as ExcelObj.Range).Font.Size = 14;

            (ws.Cells[2, 1] as ExcelObj.Range).Font.Bold = true;
            (ws.Cells[2, 2] as ExcelObj.Range).Font.Bold = true;
            (ws.Cells[2, 3] as ExcelObj.Range).Font.Bold = true;
            (ws.Cells[2, 4] as ExcelObj.Range).Font.Bold = true;
            (ws.Cells[2, 5] as ExcelObj.Range).Font.Bold = true;

            foreach (List<string> item in list)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    ws.Cells[count + 1, i + 1] = item[i];
                }
                sum += double.Parse(item[item.Count - 1]);
                count++;
            }
            //ws.get_Range($"B2", $"C2").Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;

            
            //ws.get_Range("D1", $"D{count}").Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ws.Cells[count + 1, 1] = "ИТОГО";
            ws.Cells[count + 1, list[0].Count] = sum.ToString();
            (ws.Cells[count + 1, 1] as ExcelObj.Range).Font.Size = 14;

            (ws.Cells[count + 1, 1] as ExcelObj.Range).Font.Bold = true;

        }
        public void CreateNewFile()
        {
            this.wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            ws = wb.Worksheets[1];
        }

        public void WriteToCell(int idOrder,string data, int skidka)
        {
           
            ws.Cells[1, 1].Value2 = "№ Заказа";
            ws.Cells[1, 2] = idOrder;

            ExcelObj.Range _excelCells2 = (ExcelObj.Range)ws.get_Range("B2", "C2").Cells;
            _excelCells2.Merge(Type.Missing);

            ws.Cells[2, 1].Value2 = "№ Дата";
            ws.Cells[2, 2] = data;

            ws.Cells[3, 1].Value2 = "Модель";
            ws.Cells[3, 2].Value2 = "Шт";
            ws.Cells[3, 3].Value2 = "Цена";

            ws.Rows[3].RowHeight = 24;

            ws.Columns[1].ColumnWidth = 25;
            ws.Columns[2].ColumnWidth = 7.5f;
            ws.Columns[3].ColumnWidth = 17;

            (ws.Cells[3, 1] as ExcelObj.Range).Font.Size = 14;
            (ws.Cells[3, 2] as ExcelObj.Range).Font.Size = 14;
            (ws.Cells[3, 3] as ExcelObj.Range).Font.Size = 14;

            (ws.Cells[3, 1] as ExcelObj.Range).Font.Bold = true;
            (ws.Cells[3, 2] as ExcelObj.Range).Font.Bold = true;
            (ws.Cells[3, 3] as ExcelObj.Range).Font.Bold = true;

            ws.get_Range("A3", "C3").Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ws.get_Range("A3", "C3").Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
            double summ = 0;
            for (int i = 0; i < Form5.bascet.Count;i++)
            {
                int iV = i + iterval;
                ws.Cells[iV, 1].Value2 = Form5.bascet[i].model+" "+ Form5.bascet[i].fleshMem+" ГБ";
                ws.Cells[iV, 2].Value2 = Form5.bascet[i].count;
                ws.Cells[iV, 3].Value2 = Form5.bascet[i].prise* Form5.bascet[i].count+" руб";
                summ +=Form5.bascet[i].prise * Form5.bascet[i].count;
            }

            ws.get_Range("B3", $"C{Form5.bascet.Count+ iterval+3}").Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;


            ExcelObj.Range _excelCells1 = (ExcelObj.Range)ws.get_Range($"A{Form5.bascet.Count + iterval}", $"B{Form5.bascet.Count + iterval}").Cells;
            _excelCells1.Merge(Type.Missing);
            ws.Cells[Form5.bascet.Count + iterval, 1] = "ИТОГО";
            ws.Cells[Form5.bascet.Count + iterval, 3] = summ +" руб";

            ws.get_Range($"A{Form5.bascet.Count + iterval}", $"C{Form5.bascet.Count + iterval}").Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
            (ws.Cells[Form5.bascet.Count + iterval, 1] as ExcelObj.Range).Font.Size = 16;
            (ws.Cells[Form5.bascet.Count + iterval, 3] as ExcelObj.Range).Font.Size = 16;
            (ws.Cells[Form5.bascet.Count + iterval, 1] as ExcelObj.Range).Font.Bold = true;
            (ws.Cells[Form5.bascet.Count + iterval, 3] as ExcelObj.Range).Font.Bold = true;

            ws.Rows[Form5.bascet.Count + iterval].RowHeight = 40;
            if (skidka != 0)
            {
                ws.Cells[Form5.bascet.Count + iterval + 1, 1] = $"СКИДКА({skidka}%)";
                ws.Cells[Form5.bascet.Count + iterval + 1, 3] = Math.Round(summ * skidka /100) + " руб";

                ws.get_Range($"A{Form5.bascet.Count + iterval + 1}", $"C{Form5.bascet.Count + iterval + 1}").Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
                (ws.Cells[Form5.bascet.Count + iterval + 1, 1] as ExcelObj.Range).Font.Size = 12;
                (ws.Cells[Form5.bascet.Count + iterval + 1, 3] as ExcelObj.Range).Font.Size = 12;
                ws.Rows[Form5.bascet.Count + iterval + 1].RowHeight = 28.5f;

                ws.Cells[Form5.bascet.Count + iterval + 2, 1] = "ИТОГО СО СКИДКОЙ";
                ws.Cells[Form5.bascet.Count + iterval + 2, 3] = Math.Round(summ - summ * skidka/100) + " руб";

                ws.get_Range($"A{Form5.bascet.Count + iterval + 2}", $"C{Form5.bascet.Count + iterval + 2}").Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
                (ws.Cells[Form5.bascet.Count + iterval + 2, 1] as ExcelObj.Range).Font.Size = 16;
                (ws.Cells[Form5.bascet.Count + iterval + 2, 3] as ExcelObj.Range).Font.Size = 16;
                (ws.Cells[Form5.bascet.Count + iterval + 2, 1] as ExcelObj.Range).Font.Bold = true;
                (ws.Cells[Form5.bascet.Count + iterval + 2, 3] as ExcelObj.Range).Font.Bold = true;
                ws.Rows[Form5.bascet.Count + iterval + 2].RowHeight = 40;
            }


        }
        public void ExportOstatki(List<List<string>> list)
        {
            ws.Cells[1, 1].Value2 = "Остатки ";
            ws.Cells[1, 2].Value2 = DateTime.Now.Day + "." + DateTime.Now.Month+"."+ DateTime.Now.Year;

            ws.Cells[2, 1].Value2 = "Модель";
            ws.Cells[2, 2].Value2 = "Цвет";
            ws.Cells[2, 3].Value2 = "Шт";
           
            ws.get_Range("A2", $"E2").Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ws.get_Range("A3", $"E{100}").Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //ws.get_Range("B1", $"B{count}").Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            int count = 2;
            double sum = 0;
            ws.Columns[1].ColumnWidth = 25;
            ws.Columns[2].ColumnWidth = 13;
            ws.Columns[3].ColumnWidth = 13;
            

            (ws.Cells[2, 1] as ExcelObj.Range).Font.Size = 14;
            (ws.Cells[2, 2] as ExcelObj.Range).Font.Size = 14;
            (ws.Cells[2, 3] as ExcelObj.Range).Font.Size = 14;
            

            (ws.Cells[2, 1] as ExcelObj.Range).Font.Bold = true;
            (ws.Cells[2, 2] as ExcelObj.Range).Font.Bold = true;
            (ws.Cells[2, 3] as ExcelObj.Range).Font.Bold = true;
          

            foreach (List<string> item in list)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    ws.Cells[count + 1, i + 1] = item[i];
                }
                //sum += double.Parse(item[item.Count - 1]);
                count++;
            }
        }
        int iterval = 4;
        public void SaveAs(string path)
        {
            wb.SaveAs(path);
            
        }
        public void Close()
        {
            wb.Close();
        }
    }
}
