using System.Windows.Forms;

namespace CSVImportParser
{
    public static class DataGridViewExt
    {

        public static void Freeze(this DataGridView dgv)
        {
            dgv.SuspendLayout();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }
        public static void UnFreeze(this DataGridView dgv)
        {
            dgv.ResumeLayout();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
    }
}