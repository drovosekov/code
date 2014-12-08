using System;
using System.Linq;
using System.Windows.Forms;

namespace CSVImportParser
{
    public class CSVParser
    {
        private string[] _textReader;
        private readonly char[] _rowDelimeters = new char[] { '\r', '\n' };
        private char _ColumnDelimeter = ';';
        private const int _minColumnWidth = 110;
        private const int _maxPreviewLines = 50;

        public delegate void DataChanged(bool RebuildTable);
        public event DataChanged onDataChanged;
        public delegate void ProgressChanged(int ProgressValue);
        public event ProgressChanged onProgressChanged;


        public string Data
        {
            get
            {
                return string.Join(Environment.NewLine, _textReader);
            }
            set
            {
                _textReader = value.Split(_rowDelimeters, StringSplitOptions.RemoveEmptyEntries);
                if (onDataChanged != null) { onDataChanged(true); }
            }
        }

        public string PreviewData
        {
            get
            {
                int len = (_textReader.Length > _maxPreviewLines) ? _maxPreviewLines : _textReader.Length;
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

        public DataGridViewTextBoxColumn[] Columns(string DefaultHeaderValueText)
        {
            return _textReader[0].Split(_ColumnDelimeter)
                    .Select(x => new DataGridViewTextBoxColumn()
                    {
                        HeaderText = DefaultHeaderValueText,
                        MinimumWidth = _minColumnWidth
                    })
                    .ToArray();
        }

        public char ColumnsDelimeter
        {
            get
            {
                return _ColumnDelimeter;
            }
            set
            {
                _ColumnDelimeter = value;
                if (onDataChanged != null) { onDataChanged(true); }
            }
        }

        private int _StartLine;
        public int StartLine
        {
            get
            {
                return _StartLine;
            }
            set
            {
                _StartLine = value;
                //только при изменении первой строки мы не перезагружаем данные в таблицу
                if (onDataChanged != null) { onDataChanged(false); }
            }
        }

        public void FilleRowsTable(DataGridView DGV)
        {
            for (int i = StartLine - 1; i < _textReader.Length; i++)
            {
                DGV.Rows.Add(_textReader[i].Split(_ColumnDelimeter));
                if (i % 10 == 0)
                {
                    onProgressChanged(i);
                }
            }
        }
    }
}
