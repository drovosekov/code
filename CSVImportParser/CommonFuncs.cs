using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CSVImportParser
{

    public static class CommonFuncs
    {
        private readonly static char _decimalSepparator = 0.1f.ToString()[1];
        public const string RegistryAppPath = @"HKEY_CURRENT_USER\SOFTWARE\drovosekov.net\CSVImportParser\";

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
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            return Regex.IsMatch(s, @"^\d+$");
        }

        /// <summary>
        /// Проверка строки - является ли она валидным числом c дробной частью
        /// </summary>
        /// <param name="s">входная строка для проверки</param>
        /// <returns>true/false</returns>
        public static bool IsFloatNumeric(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            return Regex.IsMatch(s, @"^[-]?\d+[.|,]\d+$");
        }

        /// <summary>
        /// определяем относиться ли указанный тип к Nullable (int?, bool? и т.п.)
        /// </summary>
        /// <param name="type">определяемый тип</param>
        /// <returns>true|false</returns>
        public static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        /// <summary>
        /// преобразует число из строки с любым разделителем дробной части (точкой или запятой) в тип double
        /// </summary>
        /// <returns>число типа double</returns>
        /// <remarks></remarks>
        public static double GetDouble(string value)
        {
            return double.Parse(value.Replace(".", _decimalSepparator.ToString()).Replace(",", _decimalSepparator.ToString()));
        }
        /// <summary>
        /// преобразует число из строки с любым разделителем дробной части (точкой или запятой) в тип decimal
        /// </summary>
        /// <returns>число типа decimal</returns>
        /// <remarks></remarks>
        public static decimal GetDecimal(string value)
        {
            return decimal.Parse(value.Replace(".", _decimalSepparator.ToString()).Replace(",", _decimalSepparator.ToString()));
        }

        /// <summary>
        /// преобразует число в строке в тип float с любым разделителем дробной части: точкой или запятой
        /// </summary>
        /// <param name="value">число которое надо сохранить как float</param>
        /// <returns>число типа float</returns>
        /// <remarks></remarks>
        public static float GetFloat(string value)
        {
            //Try parsing in the current culture
            return float.Parse(value.Replace(".", _decimalSepparator.ToString()).Replace(",", _decimalSepparator.ToString()));
        }

        /// <summary>
        /// возвращает значение поля таблицы dgv типа Double
        /// </summary>
        /// <param name="dgv">таблица из которой получаем значение поля для текущей строки</param>
        /// <param name="field">название поля (столбца) для которого нужно получить значение из текущей строки таблицы</param>
        /// <returns>значение поля типа Double</returns>
        public static double GetDoubleValueByField(ref DataGridView dgv, string field)
        {
            if (dgv.CurrentRow == null || dgv.CurrentRow.Cells[field].Value == null)
            {
                return -1;
            }
            return Convert.ToDouble(dgv.CurrentRow.Cells[field].Value);
        }
        /// <summary>
        /// возвращает значение поля таблицы dgv типа Double
        /// </summary>
        /// <param name="dgv">строка таблицы из которой получаем значение нужного поля</param>
        /// <param name="field">название поля (столбца) для которого нужно получить значение из текущей строки таблицы</param>
        /// <returns>значение поля типа Double</returns>
        public static double GetDoubleValueByField(ref DataGridViewRow dgv, string field)
        {
            if (dgv == null || dgv.Cells[field].Value == null)
            {
                return -1;
            }
            return Convert.ToDouble(dgv.Cells[field].Value);
        }
        /// <summary>
        /// возвращает значение поля таблицы dgv типа Int
        /// </summary>
        /// <param name="dgv">таблица из которой получаем значение поля для текущей строки</param>
        /// <param name="field">название поля (столбца) для которого нужно получить значение из текущей строки таблицы</param>
        /// <returns>значение поля типа Int</returns>
        public static int GetIntValueByField(ref DataGridView dgv, string field)
        {
            if (dgv.CurrentRow == null || dgv.CurrentRow.Cells[field].Value == null)
            {
                return -1;
            }
            return Convert.ToInt32(dgv.CurrentRow.Cells[field].Value);
        }
        /// <summary>
        /// возвращает значение поля таблицы dgv типа Int
        /// </summary>
        /// <param name="dgv">строка таблицы из которой получаем значение нужного поля</param>
        /// <param name="field">название поля (столбца) для которого нужно получить значение из текущей строки таблицы</param>
        /// <returns>значение поля типа Int</returns>
        public static int GetIntValueByField(DataGridViewRow dgv, string field)
        {
            if (dgv == null || dgv.Cells[field].Value == null)
            {
                return -1;
            }
            return Convert.ToInt32(dgv.Cells[field].Value);
        }

        public static bool ValueInInterval(decimal Value, decimal CompareValue, decimal toleranceUpAndDown)
        {
            return (Value <= CompareValue + toleranceUpAndDown && Value > CompareValue - toleranceUpAndDown);
        }
        public static bool ValueInInterval(decimal Value, decimal CompareValue, decimal toleranceDown, decimal toleranceUp)
        {
            return (Value <= CompareValue + toleranceUp && Value > CompareValue - toleranceDown);
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
                int sw, sh;
                string path = GetRegistryPathForFormSettings(frm.Name);
                if (Convert.ToInt32(Registry.GetValue(path, "WindowState", 0)) == 2)
                {
                    frm.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    sw = Convert.ToInt32(Registry.GetValue(path, "FormWidth", 0));
                    sh = Convert.ToInt32(Registry.GetValue(path, "FormHeight", 0));
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
            string path = GetRegistryPathForFormSettings(frm.Name);
            if (frm.WindowState == FormWindowState.Normal)
            {
                if (frm.FormBorderStyle == (FormBorderStyle.Sizable & FormBorderStyle.SizableToolWindow))
                {
                    Registry.SetValue(path, "FormWidth", frm.Size.Width);
                    Registry.SetValue(path, "FormHeight", frm.Size.Height);
                    Registry.SetValue(path, "WindowState", 0);
                }
                Registry.SetValue(path, "FormLeft", frm.Left);
                Registry.SetValue(path, "FormTop", frm.Top);
            }
            else
                if (frm.WindowState == FormWindowState.Maximized)
                {
                    Registry.SetValue(path, "WindowState", (int)frm.WindowState);
                }
        }


        /// <summary>
        /// получаем путь в реестре Windows, по которому храняться настройки для формы
        /// </summary>
        /// <param name="frm">имя формы</param>
        /// <returns></returns>
        public static string GetRegistryPathForFormSettings(string frmName)
        {
            return String.Format("{0}{1}", RegistryAppPath, frmName);
        } 
    }
}
