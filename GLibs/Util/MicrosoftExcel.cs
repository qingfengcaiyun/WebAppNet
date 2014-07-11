using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Glibs.Util
{
    public class MicrosoftExcel
    {
        public static List<Dictionary<String, Object>> Import(String filePath)
        {
            if (File.Exists(filePath))
            {
                IWorkbook workbook = WorkbookFactory.Create(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));//使用接口，自动识别excel2003/2007格式
                IFormulaEvaluator fe = workbook.GetCreationHelper().CreateFormulaEvaluator();

                int sheetCount = workbook.Count;
                int rowCount = 0;
                int colCount = 0;

                List<Dictionary<string, object>> list = null;

                int i, j;

                if (sheetCount > 0)
                {
                    list = new List<Dictionary<string, object>>();

                    Dictionary<string, object> item = null;
                    ISheet sheet = null;
                    IRow firstRow = null;
                    IRow row = null;

                    sheet = workbook.GetSheetAt(0);
                    rowCount = sheet.LastRowNum;

                    if (rowCount > 0)
                    {
                        firstRow = sheet.GetRow(1);
                        colCount = firstRow.LastCellNum;

                        for (i = 2; i <= rowCount; i++)
                        {
                            row = sheet.GetRow(i);

                            if (row == null)
                            {
                                continue;
                            }

                            item = new Dictionary<string, object>();

                            for (j = 1; j < colCount; j++)
                            {
                                if (row.GetCell(j).CellType == CellType.Formula)
                                {
                                    item.Add(firstRow.GetCell(j).StringCellValue, fe.Evaluate(row.GetCell(j)).StringValue);
                                }
                                else
                                {
                                    item.Add(firstRow.GetCell(j).StringCellValue, row.GetCell(j).StringCellValue);
                                }
                            }

                            list.Add(item);
                        }
                    }
                }

                return list;
            }
            else
            {
                return null;
            }
        }

        public static Boolean Export(List<string> fields, List<Dictionary<string, object>> list, string filePath, bool isExcel2007)
        {
            IWorkbook workbook = null;
            if (isExcel2007)
            {
                workbook = new XSSFWorkbook();
            }
            else
            {
                workbook = new HSSFWorkbook();
            }

            if (fields == null || list == null || fields.Count == 0 || list.Count == 0)
            {
                return false;
            }
            else
            {
                ISheet sheet = workbook.CreateSheet("Sheet1");
                IRow row = sheet.CreateRow(0);

                for (int h = 0; h < fields.Count; h++)
                {
                    row.CreateCell(h).SetCellValue(fields[h]);
                }

                for (int i = 1, j = list.Count; i < j; i++)
                {
                    row = sheet.CreateRow(i);
                    for (int h = 0; h < fields.Count; h++)
                    {
                        //row.CreateCell(h).SetCellValue(list[i][(fields[h]).toString()]);
                    }
                }

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }

                if (File.Exists(filePath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
