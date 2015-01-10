using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace ImportTest
{
    /// <summary>
    /// в классе описываются функции для подготовки данных к выгрузке в БД
    /// </summary>
    public static class DataImportFormatting
    {
        /// <summary>
        /// форматирование строки таблицы при импорте
        /// </summary>
        /// <param name="datavalue">значение ячейки</param>
        /// <param name="column">название колонки для передаваемого значнения ячейки</param>
        public static void PreImportFormat(ref object datavalue, string column)
        {
            if (datavalue == null || string.IsNullOrEmpty(datavalue.ToString()))
            {
                datavalue = null;
            }
            else if (column == "TypeObject")
            {
                switch (datavalue.ToString().ToLower())
                {
                    case "металл": datavalue = 1; break;
                    case "дерево": datavalue = 2; break;
                    case "жб": datavalue = 3; break;
                    default: datavalue = null; break;
                }
            }
            else if (column == "Number" &&
                     !Regex.IsMatch(datavalue.ToString(), @"^[1-9]\d*[A|А]?$", RegexOptions.IgnoreCase))//номер [+ литера А (англ. или кириллич.)]
            {
                //если номер это не число и даже не число с литерой А (в англ. или рус. раскладке, малая или заглавная)
                //то эту строку надо пропустить из обработки и не передавать в таблицу с данными  
                datavalue = null;
            }
        }

        /// <summary>
        /// пост обработка списка   
        /// </summary>
        /// <param name="dataList">список из модуля импорта</param> 
        public static void PostImportFormat(IEnumerable<TestImportClass> dataList)
        {
            foreach (TestImportClass classElement in dataList)
            {
                classElement.Number += "_test";
                classElement.Gabarit = decimal.Round(classElement.Gabarit, 2);
            }
        }
    }
}
