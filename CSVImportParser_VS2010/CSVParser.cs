using System;
using System.Linq;
using System.Windows.Forms;

namespace CSVImportParser
{
    public class CSV : IDisposable
    {
        private string[] _textReader;
        private readonly char[] _rowDelimeters = { '\r', '\n' };
        private char _columnDelimeter = ';';
        private const int MinColumnWidth = 110;
        private const int MaxPreviewLines = 50;
        private int _startLine;

        public delegate void DataChanged(bool rebuildTable);
        public event DataChanged OnDataChanged;
        public delegate void ProgressChanged(int progressValue);
        public event ProgressChanged OnProgressChanged;

        public string Data
        {
            get
            {
                return string.Join(Environment.NewLine, _textReader);
            }
            set
            {
                _textReader = value.Split(_rowDelimeters, StringSplitOptions.RemoveEmptyEntries);
                if (OnDataChanged != null) { OnDataChanged(true); }
            }
        }

        public string PreviewData
        {
            get
            {
                int len = (_textReader.Length > MaxPreviewLines) ? MaxPreviewLines : _textReader.Length;
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
                if (OnDataChanged != null) { OnDataChanged(false); }
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
            for (int i = StartLine - 1; i < _textReader.Length; i++)
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
