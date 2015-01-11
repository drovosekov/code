using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CSVImportParser
{

    public static class FormExt
    {
        private const string RegistryAppPath = @"HKEY_CURRENT_USER\SOFTWARE\drovosekov.net\CSVImportParser\";

        /// <summary>
        /// восстанавливает из реестра положение и размер формы
        /// </summary>
        public static void LoadFormPositionAndSize(this Form frm)
        {
            //читаем настройки реестра
            try
            {
                var path = GetRegistryPathForFormSettings(frm.Name);
                if (Convert.ToInt32(Registry.GetValue(path, "WindowState", 0)) == 2)
                {
                    frm.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    var sw = Convert.ToInt32(Registry.GetValue(path, "FormWidth", 0));
                    var sh = Convert.ToInt32(Registry.GetValue(path, "FormHeight", 0));
                    if (sw != 0 && sh != 0)
                    {
                        frm.Size = new System.Drawing.Size(sw, sh);
                    }
                    frm.Left = Convert.ToInt32(Registry.GetValue(path, "FormLeft", 0));
                    frm.Top = Convert.ToInt32(Registry.GetValue(path, "FormTop", 0));
                }
            }
            catch { return; }
        }

        /// <summary>
        /// запоминаем в реестре положение формы и ее размер
        /// </summary>
        public static void SaveFormPositionAndSize(this Form frm)
        {
            //сохранение настроек в реестре
            var path = GetRegistryPathForFormSettings(frm.Name);
            switch (frm.WindowState)
            {
                case FormWindowState.Normal:
                    if (frm.FormBorderStyle == (FormBorderStyle.Sizable & FormBorderStyle.SizableToolWindow))
                    {
                        Registry.SetValue(path, "FormWidth", frm.Size.Width);
                        Registry.SetValue(path, "FormHeight", frm.Size.Height);
                        Registry.SetValue(path, "WindowState", 0);
                    }
                    Registry.SetValue(path, "FormLeft", frm.Left);
                    Registry.SetValue(path, "FormTop", frm.Top);
                    break;
                case FormWindowState.Maximized:
                    Registry.SetValue(path, "WindowState", (int)frm.WindowState);
                    break;
            }
        }


        /// <summary>
        /// получаем путь в реестре Windows, по которому храняться настройки для формы
        /// </summary>
        /// <param name="frmName">имя формы</param>
        /// <returns></returns>
        public static string GetRegistryPathForFormSettings(string frmName)
        {
            return String.Format("{0}{1}", RegistryAppPath, frmName);
        }
    }
}
