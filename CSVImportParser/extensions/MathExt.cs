using System.Text.RegularExpressions;

namespace CSVImportParser
{
    public static class MathExt
    {
        private readonly static string DecimalSepparator = 0.1f.ToString()[1].ToString();
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
            return value;
        }

        /// <summary>
        /// Проверка строки - является ли она валидным числом
        /// </summary>
        /// <param name="s">входная строка для проверки</param>
        /// <returns>true/false</returns>
        public static bool IsNumeric(this object s)
        {
            return !string.IsNullOrEmpty(s.ToString()) && Regex.IsMatch(s.ToString(), @"^\d+$");
        }

        /// <summary>
        /// Проверка строки - является ли она валидным числом c дробной частью
        /// </summary>
        /// <param name="s">входная строка для проверки</param>
        /// <returns>true/false</returns>
        public static bool IsFloatNumeric(this object s)
        {
            return !string.IsNullOrEmpty(s.ToString()) && Regex.IsMatch(s.ToString(), @"^[-]?\d+[.|,]\d+$");
        }

        /// <summary>
        /// преобразует число из строки с любым разделителем дробной части (точкой или запятой) в тип decimal
        /// </summary>
        /// <returns>число типа decimal</returns>
        /// <remarks></remarks>
        public static decimal GetDecimal(this object value)
        {
            return decimal.Parse(value.ToString().Replace(".", DecimalSepparator).Replace(",", DecimalSepparator));
        }
    }
}