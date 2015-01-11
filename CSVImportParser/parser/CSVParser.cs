using System;
using System.Linq;
using System.Windows.Forms;

namespace CSVImportParser
{
    public class CSV : IDisposable
    {
        private string[] _textReader;
        private char _columnDelimeter = ';';
        private const int MinColumnWidth = 110;
        private const int MaxPreviewLines = 50;
        private int _startLine;
         
        public event Action<bool> OnDataChanged;
        public event Action<int> OnProgressChanged;

        public string Data
        {
            get
            {
                return string.Join(Environment.NewLine, _textReader);
            }
            set
            {
                _textReader = value.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (OnDataChanged != null) { OnDataChanged(true); }
            }
        }

        public string PreviewData
        {
            get
            {
                var len = (_textReader.Length > MaxPreviewLines) ? MaxPreviewLines : _textReader.Length;
                return string.Join(Environment.NewLine, _textReader, 0, len);
            }
        }

        public int RowsCount
        {
            get
            {
                return _textReader.Length;
            }
        }

        public char ColumnsDelimeter
        {
            get
            {
                return _columnDelimeter;
            }
            set
            {
                _columnDelimeter = value;
                if (OnDataChanged != null) { OnDataChanged(true); }
            }
        }

        public int StartLine
        {
            get
            {
                return _startLine;
            }
            set
            {
                _startLine = value;
                //только при изменении первой строки мы не перезагружаем данные в таблицу
                if (OnDataChanged != null) OnDataChanged(false);
            }
        }

        public DataGridViewTextBoxColumn[] Columns(string defaultHeaderValueText)
        {
            return _textReader[0].Split(_columnDelimeter)
                    .Select(x => new DataGridViewTextBoxColumn()
                    {
                        HeaderText = defaultHeaderValueText,
                        MinimumWidth = MinColumnWidth
                    })
                    .ToArray();
        }

        public void FilleRowsTable(DataGridView dgv)
        {
            for (var i = _startLine - 1; i < _textReader.Length; i++)
            {
                dgv.Rows.Add(_textReader[i].Split(_columnDelimeter));
                if (i % 10 != 0) continue;
                if (OnProgressChanged != null) OnProgressChanged(i);
            }
        }

        public void Dispose()
        {
            OnDataChanged = null;
            OnProgressChanged = null;
            _textReader = null;
        }
    }
}
