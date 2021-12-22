using System;
using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;

namespace BasicTermS
{
    public static class DataFromCsv
    {

        public static Type GetColumnType(string type)
        {
            var result = typeof(string);

            switch (type)
            {
                case "int":
                    result = typeof(int);
                    break;
                case "long":
                    result = typeof(long);
                    break;
                case "float":
                    result = typeof(float);
                    break;
                case "double":
                    result = typeof(double);
                    break;
                case "decimal":
                    result = typeof(decimal);
                    break;
                default:
                    break;
            }
            return result;
        }

        //
        // Params :
        //
        // Dictionary<string, string> paramColumnDictionnary = {"ColumnName1":"ColumnType1",
        //                                                      "ColumnName2":"ColumnType2",
        //                                                      "ColumnName3":"ColumnType3",
        //                                                      ...}
        //
        public static DataTable AddColumnWithType(DataTable paramDataTable, Dictionary<string, string> paramColumnDictionnary)
        {
            foreach(var item in paramColumnDictionnary)
            {
                paramDataTable.Columns.Add(item.Key, GetColumnType(item.Value));
            }
            return paramDataTable;
        }

    }
}

