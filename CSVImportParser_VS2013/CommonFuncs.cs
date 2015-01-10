using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CSVImportParser
{

    public static class CommonFuncs
    {
        private readonly static char DecimalSepparator = 0.1f.ToString()[1];
        private const string RegistryAppPath = @"HKEY_CURRENT_USER\SOFTWARE\drovosekov.net\CSVImportParser\";

        /// <summary>
        /// округляет заданное число в большую сторону
        /// </summary>
        /// <param name="value">округляемое число</param>
        /// <param name="rvalueto">кратность округления</param>
        /// <returns>округленное значение числа</returns>
        public static int RoundedUpTo(int value, int rvalueto)
        {
            //округляем value до rvalueto в большую сторону
            if (value % rvalueto != 0)
            {
                return (value - (value % rvalueto) + rvalueto);
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Проверка строки - является ли она валидным числом
        /// </summary>
        /// <param name="s">входная строка для проверки</param>
        /// <returns>true/false</returns>
        public static bool IsNumeric(string s)
        {
            return !string.IsNullOrEmpty(s) && Regex.IsMatch(s, @"^\d+$");
        }

        /// <summary>
        /// Проверка строки - является ли она валидным числом c дробной частью
        /// </summary>
        /// <param name="s">входная строка для проверки</param>
        /// <returns>true/false</returns>
        public static bool IsFloatNumeric(string s)
        {
            return !string.IsNullOrEmpty(s) && Regex.IsMatch(s, @"^[-]?\d+[.|,]\d+$");
        }

        /// <summary>
        /// преобразует число из строки с любым разделителем дробной части (точкой или запятой) в тип decimal
        /// </summary>
        /// <returns>число типа decimal</returns>
        /// <remarks></remarks>
        public static decimal GetDecimal(string value)
        {
            return decimal.Parse(value.Replace(".", DecimalSepparator.ToString()).Replace(",", DecimalSepparator.ToString()));
        }
        
        /// <summary>
        /// восстанавливает из реестра положение и размер формы
        /// </summary>
        /// <param name="frm">WinForm</param>
        public static void LoadFormPositionAndSize(Form frm)
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
        /// <param name="frm">WinForm</param>
        public static void SaveFormPositionAndSize(Form frm)
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
