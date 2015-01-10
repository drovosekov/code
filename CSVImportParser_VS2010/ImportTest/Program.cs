using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using CSVImportParser;

namespace ImportTest
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (ImportFieldsList ImportFields = new ImportFieldsList())
            {
                ImportFields.Add("Номер", "Number", true);
                ImportFields.Add("Материал", "TypeObject", true); 
                ImportFields.Add("Габарит", "Gabarit", true);
                ImportFields.Add("Высота", "Height", false);

                using (frmCSVImport frm = new frmCSVImport()
                {
                    ImportDialogTitle = "Импорт элементов",
                    ImportFields = ImportFields,
                    ImportFilter = "CSV файлы|*.csv",
                    TemplateDefaultPath = String.Format(@"..\..\dox\template.tpl", App.GetAppPath)
                })
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                        frm.preFormat(DataImportFormatting.preImportFormat);

                        Collection<TestImportClass> FormattedCollection = frm.ListData<TestImportClass>();
                    }
                }

            }
        }
    }
}
