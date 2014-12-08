using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CSVImportParser.Properties;

namespace CSVImportParser
{
    public class ImportFieldsList : IDisposable
    {
        private ICollection<Fields> _ImportFields = new Collection<Fields>();

        internal ICollection<Fields> ImportFields
        {
            get
            {
                return _ImportFields;
            }
            set
            {
                _ImportFields = value;
            }
        }

        private string _NotUsedColumnHeaderText = Resources.НеИспольз;
        public string NotUsedColumnHeaderText
        {
            get
            {
                return _NotUsedColumnHeaderText;
            }
            set
            {
                _NotUsedColumnHeaderText = value;
            }
        }
        public ImportFieldsList()
        {
            _ImportFields.Add(new Fields()
            {
                HeaderText = _NotUsedColumnHeaderText,
                RealDBName = null
            });
        }
        /// <summary>
        /// добавление в список импортируемых полей нового поля
        /// </summary>
        /// <param name="Text">текст отображаемый в заголовке столбца таблицы и в меню выбора используемых полей</param>
        /// <param name="Mult">множитель для перевода в размерность БД</param>
        /// <param name="Req">признак обязательности поля для импорта (в меню пункт будет помечен звездочкой)</param>
        public void Add(string Text, string DBName, bool Req)
        {
            _ImportFields.Add(new Fields()
            {
                HeaderText = Text,
                RealDBName = DBName,
                Require = Req
            });
        }

        public bool Contains(string Name)
        {
            return _ImportFields.Contains(new Fields
              {
                  HeaderText = Name
              });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ImportFields = null;
            }
        }
    }
}
