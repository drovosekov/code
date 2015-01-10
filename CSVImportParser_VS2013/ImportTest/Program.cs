using System;
using System.Windows.Forms;
using CSVImportParser;
using System.Collections.Generic;

namespace ImportTest
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var importFields = new ImportFieldsList())
            {
                importFields.Add("Номер", "Number", true);
                importFields.Add("Материал", "TypeObject", true); 
                importFields.Add("Габарит", "Gabarit", true);
                importFields.Add("Высота", "Height", false);

                using (var frm = new frmCSVImport()
                {
                    ImportDialogTitle = "Импорт элементов",
                    ImportFields = importFields,
                    ImportFilter = "CSV файлы|*.csv",
                    TemplateDefaultPath = String.Format(@"{0}..\..\dox\template.tpl", App.GetAppPath)
                })
                {
                    if (frm.ShowDialog() != DialogResult.OK) return;

                    frm.PreFormat(DataImportFormatting.PreImportFormat);

                    IEnumerable<TestImportClass> formattedCollection = frm.EnumeratedData<TestImportClass>();

                    DataImportFormatting.PostImportFormat(formattedCollection);
                }

            }
        }
    }
}
