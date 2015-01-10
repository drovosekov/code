using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CSVImportParser.Properties;

namespace CSVImportParser
{
    public sealed class ImportFieldsList : IDisposable
    {
        private ICollection<Fields> _importFields = new Collection<Fields>();

        internal ICollection<Fields> ImportFields
        {
            get
            {
                return _importFields;
            }
            set
            {
                _importFields = value;
            }
        }

        public string NotUsedColumnHeaderText { get; set; }
        
        /// <summary>
        /// добавление в список импортируемых полей нового поля
        /// </summary>
        /// <param name="textHeader">текст отображаемый в заголовке столбца таблицы и в меню выбора используемых полей</param>
        /// <param name="classFieldName">название поля в классе для серилизации</param>
        /// <param name="requiredField">признак обязательности поля для импорта (в меню пункт будет помечен звездочкой)</param>
        public void Add(string textHeader, string classFieldName, bool requiredField)
        {
            _importFields.Add(new Fields()
            {
                HeaderText = textHeader,
                FieldNameOfClassForSerelization = classFieldName,
                Require = requiredField
            });
        }

        public bool Contains(string name)
        {
            return _importFields.Contains(new Fields
              {
                  HeaderText = name
              });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                ImportFields = null;
            }
        }
    }
}
