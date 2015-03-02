using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using CSVImportParser.Properties;


[assembly: CLSCompliant(true)]
namespace CSVImportParser
{
    public partial class frmCSVImport : Form
    {
        #region delegates
        /// <summary>
        /// вызывается для предварительного форматирования элемента строки таблицы с импортируемыми данными
        /// чтоб подготовить данные к переводу в значения элементов класса
        /// </summary>
        public delegate void PreFormatingRow(ref object datavalue, string columnName);

        /// <summary>
        /// вызывается после нажатия ОК. В случае возврата из ф-ии false - будет показано сообщение
        /// о наличии ошибок в данных таблицы и предложение их исправить
        /// ошибки перечисляются в таблице DGV_ErrorTable (на форме в закладке "Анализ таблицы")
        /// </summary>
        public delegate bool CheckErrorsFunc(ref DataGridView dgvSourceDataTable, ref  DataGridView dgvErrorTable);
        #endregion

        #region private section
        private string _importTipsText;
        private string _importFilter;
        private string _importDialogTitle;
        private CheckErrorsFunc _checkErrorsFunction;
        private PreFormatingRow _preFormatFunction;
        private CSV _parser;
        private frmProgress _progressFunction;
        #endregion

        #region public section
        public string ImportTipsText
        {
            set
            {
                _importTipsText = value;
                ImportTips.Text = value;
            }
        }
        public string TemplateDefaultPath { private get; set; }
        public string ImportFilter
        {
            set
            {
                var v = value;
                if (!v.Contains("*.*"))
                {
                    _importFilter += Resources.ВсеФайлы;
                }
                _importFilter += v;
            }
        }
        public string ImportDialogTitle
        {
            set
            {
                Text = String.Format(Resources.ТекстЗаголовкаОкнаИмпорта, value);
                _importDialogTitle = value;
            }
        }
        public ImportFieldsList ImportFields { get; set; }
        public DataTable Data
        {
            get
            {
                using (var preData = new DataTable() { TableName = "ImportedData" })
                {
                    #region формируем таблицу
                    for (int j = 0; j < DGV_ImportData.Columns.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(DGV_ImportData.Columns[j].DataPropertyName))
                        {
                            preData.Columns.Add(DGV_ImportData.Columns[j].DataPropertyName);
                        }
                    }

                    //выбираем только не игнорируемые строки 
                    IEnumerable<string[]> rows = from DataGridViewRow row in DGV_ImportData.Rows.Cast<DataGridViewRow>()
                                                 where !row.IsNewRow && (row.Cells[0].Value == null || Convert.ToBoolean(row.Cells[0].Value) == false)//не игнор. строки
                                                 select Array.ConvertAll(row.Cells.Cast<DataGridViewCell>()
                                                          .Where(x => !string.IsNullOrEmpty(DGV_ImportData.Columns[x.ColumnIndex].DataPropertyName)) //только колонки с выбранным назначением
                                                          .ToArray(), c => ((c.Value != null) ? c.Value.ToString() : string.Empty));

                    foreach (object[] row in rows)
                    {
                        preData.Rows.Add(row);
                    }
                    #endregion
                    return preData;
                }

            }
        }
        public IEnumerable<T> EnumeratedData<T>() where T : class, new()
        {
            BeginProgress(Resources.Обработка, DGV_ImportData.Rows.Count);

            Collection<T> preList = new Collection<T>();
            bool skip = false;

            ColumnInfo[] settedcolumns = DGV_ImportData.Columns
                .Cast<DataGridViewColumn>()
                .Where(x => !string.IsNullOrEmpty(x.DataPropertyName))
                .Select(x => new ColumnInfo()
                {
                    DataPropertyName = x.DataPropertyName,
                    Index = x.Index
                })
                .ToArray();

            foreach (DataGridViewRow dgvRow in DGV_ImportData.Rows)
            {
                if (Convert.ToBoolean(dgvRow.Cells[0].Value)) continue;

                var obj = new T();

                foreach (ColumnInfo dc in settedcolumns)
                {
                    try
                    {
                        PropertyInfo propertyInfo = obj.GetType().GetProperty(dc.DataPropertyName);
                        if (propertyInfo == null) continue;

                        object v = dgvRow.Cells[dc.Index].Value;
                        if (_preFormatFunction != null)
                        {
                            _preFormatFunction(ref v, dc.DataPropertyName);
                            if (v == null)
                            {
                                skip = true;
                                break;
                            }
                        }
                        if (v == null) continue;

                        if (v.IsFloatNumeric())
                        {
                            v = v.GetDecimal();
                        }

                        //serelize csv cell data to class 
                        propertyInfo.SetValue(obj, TargetType(v, propertyInfo), null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (!skip)
                {
                    preList.Add(obj);
                }
                else
                {
                    skip = false;
                }

                if (dgvRow.Index % 10 == 0)
                    OnProgressChanged(dgvRow.Index);
            }

            return preList;
        }

        public void SetCheck(CheckErrorsFunc check)
        {
            _checkErrorsFunction = check;
        }
        public void PreFormat(PreFormatingRow preFormat)
        {
            _preFormatFunction = preFormat;
        }
        #endregion

        public frmCSVImport()
        {
            InitializeComponent();

            this.LoadFormPositionAndSize();

            object dist = Registry.GetValue(FormExt.GetRegistryPathForFormSettings(Name), "SplitterDistance", null);
            SplitContainer1.SplitterDistance = (dist == null) ? SplitContainer1.SplitterDistance : Convert.ToInt32(dist);
        }
        private void frmCSVImport_Load(object sender, EventArgs e)
        {
            if (_checkErrorsFunction == null) { TabPages.TabPages.RemoveAt(2); }
            if (string.IsNullOrEmpty(_importTipsText)) { TabPages.TabPages.RemoveAt(0); }

            if (ImportFields != null && string.IsNullOrEmpty(ImportFields.NotUsedColumnHeaderText))
            {
                ImportFields.NotUsedColumnHeaderText = Resources.НеИспольз;
            }

            #region init default parser template

            if (File.Exists(TemplateDefaultPath))
            {
                using (var xmlDoc = new XmlTemplateFile(TemplateDefaultPath))
                {
                    txtDelimeter.Text = xmlDoc.GetByTag("Separator");
                    txtStartLineImport.Text = xmlDoc.GetByTag("StartPosition");
                }
            }

            #endregion


            #region init CSV parser

            _parser = new CSV()
            {
                ColumnsDelimeter = Convert.ToChar(txtDelimeter.Text),
                StartLine = Convert.ToInt32(txtStartLineImport.Text)
            };
            _parser.OnDataChanged += OnDataChanged;
            _parser.OnProgressChanged += OnProgressChanged;

            #endregion

        }
        private void frmCSVImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.SaveFormPositionAndSize();

            Registry.SetValue(FormExt.GetRegistryPathForFormSettings(Name), "SplitterDistance", SplitContainer1.SplitterDistance);
        }

        #region отрисовка прогресса
        private void BeginProgress(string status, int max)
        {
            _progressFunction = new frmProgress
            {
                Text = status,
                progressBar = { Maximum = max, Value = 0 }
            };
            _progressFunction.Show();
            Application.DoEvents();
        }
        private void OnProgressChanged(int progressValue)
        {
            _progressFunction.progressBar.Value = progressValue;
            Application.DoEvents();
        }
        private void EndProgress()
        {
            _progressFunction.Dispose();
        }
        #endregion

        private void OnDataChanged(bool rebuild)
        {
            try
            {
                SuspendDGV();
                BeginProgress(Resources.Обработка, _parser.RowsCount);

                txtSourceDataFile.Text = _parser.PreviewData;

                //если изменения данных влияют на структуру колонок - перезагружаем их
                if (rebuild)
                {
                    DGV_ImportData.Columns.Clear();
                    DGV_ImportData.Columns.AddRange(_parser.Columns(ImportFields.NotUsedColumnHeaderText));
                }
                //в противном случае (смена стартовой строки) только удаляем столбец Игнор. 
                //чтоб не удалять все столбцы и не сбить уже выбранные сопоставления колонок при загрузке данных в таблицу
                //т.к. кол-во столбцов не меняется, но данные в таблице надо перезагрузить
                else
                {
                    DGV_ImportData.Columns.RemoveAt(0);
                }

                DGV_ImportData.Rows.Clear();
                _parser.FilleRowsTable(DGV_ImportData);

                DGV_ImportData.Columns.Insert(0, new DataGridViewCheckBoxColumn()
                {
                    Name = "ignor",
                    HeaderText = Resources.Игнор
                });

                if (rebuild)
                    LoadImportTemplate(TemplateDefaultPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Ошибка, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                EndProgress();
                ResumeDGV();
            }
        }

        /// <summary>
        /// присваиваем значение v соотв. свойству класса
        /// </summary>
        /// <param name="sourceValue">какое-то значение (строка, число, дробное число)</param>
        /// <param name="propertyInfo">информация о свойстве класса</param>
        /// <returns>сконвертированный тип</returns>
        private static object TargetType(object sourceValue, PropertyInfo propertyInfo)
        {
            Type targetType = propertyInfo.PropertyType;
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                targetType = Nullable.GetUnderlyingType(targetType);
            }

            return Convert.ChangeType(sourceValue, targetType);
        }

        /// <summary>
        /// проверяем что колонка таблицы уже выбрана из меню и используется
        /// </summary>
        /// <param name="headerText">текст заголовка</param> 
        /// <returns>true, если текст в HeaderText есть в заголовке одной из колонок таблицы</returns>
        private bool ColumnUsed(string headerText)
        {
            return DGV_ImportData.Columns
                                    .Cast<DataGridViewColumn>()
                                    .Count(x => x.HeaderText == headerText) != 0;
        }
        private void HeaderMenuClick(object sender, EventArgs e)
        {
            var fld = (Fields)((ToolStripMenuItem)sender).Tag;
            DataGridViewColumn dgvc = DGV_ImportData.Columns[fld.ColumnIndex];
            var hText = fld.HeaderText.Replace("* ", string.Empty);
            if (hText != ImportFields.NotUsedColumnHeaderText)
            {
                foreach (DataGridViewColumn col in DGV_ImportData.Columns
                                                        .Cast<DataGridViewColumn>()
                                                        .Where(col => col.HeaderText == hText))
                {
                    col.HeaderText = ImportFields.NotUsedColumnHeaderText;
                    col.DataPropertyName = null;
                    return;
                }
            }
            dgvc.HeaderText = hText;
            dgvc.Name = fld.FieldNameOfClassForSerelization;
            dgvc.DataPropertyName = fld.FieldNameOfClassForSerelization;
        }
        private void GenerateContextMenu(int columnIndex)
        {
            HeaderContextMenu.Items.Clear();
            HeaderContextMenu.Items.Add(ImportFields.NotUsedColumnHeaderText, null, HeaderMenuClick)
                .Tag = new Fields()
            {
                ColumnIndex = columnIndex,
                HeaderText = ImportFields.NotUsedColumnHeaderText
            };

            foreach (Fields ifl in ImportFields.ImportFields)
            {
                var menuHederText = ifl.HeaderText;
                if (ColumnUsed(menuHederText)) continue;

                if (ifl.Require) menuHederText = String.Format("* {0}", menuHederText);
                ifl.ColumnIndex = columnIndex;

                HeaderContextMenu.Items.Add(menuHederText, null, HeaderMenuClick)
                    //сохраняем инфу о элементе для последующего использования
                    .Tag = ifl;
            }
        }

        private void DGV_ImportData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //контекстное меню только для заголовка колонок таблицы
            if (e == null || e.Button != MouseButtons.Right) return;

            GenerateContextMenu(e.ColumnIndex);
            HeaderContextMenu.Show(Cursor.Position);
        }

        private void SuspendDGV()
        {
            txtSourceDataFile.Enabled = false;
            SCV_ToolStripMain.Enabled = false;
            DGV_ImportData.Freeze();
        }
        private void ResumeDGV()
        {
            txtSourceDataFile.Enabled = true;
            SCV_ToolStripMain.Enabled = true;
            DGV_ImportData.UnFreeze();
        }
        private void tblDeleteSelectedRows_Click(object sender, EventArgs e)
        {
            SuspendDGV();
            foreach (DataGridViewRow row in DGV_ImportData.SelectedRows
                                                .Cast<DataGridViewRow>()
                                                .Where(row => !row.IsNewRow))
            {
                DGV_ImportData.Rows.RemoveAt(row.Index);
            }
            ResumeDGV();
        }
        private void tblIgnorSelectedRows_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DGV_ImportData.SelectedRows)
            {
                row.Cells[0].Value = !(Convert.ToBoolean(row.Cells[0].Value));
            }
        }
        private void tbExportTemplate_Click(object sender, EventArgs e)
        {
            if (DGV_ImportData.Columns.GetColumnCount(DataGridViewElementStates.Visible) == 0) { return; }

            using (var csvSaveFileDialog = new SaveFileDialog()
                {
                    Title = Resources.ВыберитеНазваниеДляШаблонаИмпорта,
                    Filter = Resources.ФайлыШаблоновИмпортаImportxImportx,
                    FilterIndex = 2,
                    InitialDirectory = string.Empty,
                    FileName = String.Format("{1} {0}", _importDialogTitle.ToLower(), Resources.Шаблон)
                })
            {
                if (csvSaveFileDialog.ShowDialog() == DialogResult.Cancel) { return; }

                using (var xmlDoc = new XmlTemplateFile(string.Empty))
                {
                    xmlDoc.AddNode("CSVImportTemplate");
                    xmlDoc.AddNode("Separator", txtDelimeter.Text);
                    xmlDoc.AddNode("StartPosition", txtStartLineImport.Text);
                    xmlDoc.AddNode("ColumnsList", null, true);
                    for (var c = 1; c < DGV_ImportData.Columns.GetColumnCount(DataGridViewElementStates.Visible); c++)
                    {
                        xmlDoc.AddNode("Column");
                        if (DGV_ImportData.Columns[c].HeaderText == ImportFields.NotUsedColumnHeaderText) continue;

                        xmlDoc.AddAttribute("RealName", DGV_ImportData.Columns[c].DataPropertyName);
                        xmlDoc.AddAttribute("HeaderText", DGV_ImportData.Columns[c].HeaderText);
                    }
                    xmlDoc.Save(csvSaveFileDialog.FileName);
                }
            }
        }
        private void tbImportTemplate_Click(object sender, EventArgs e)
        {
            using (var templateOpenFileDialog = new OpenFileDialog()
                {
                    Title = Resources.ВыберитеШаблонИмпорта,
                    Filter = Resources.ФайлыШаблоновИмпортаImportxImportx,
                    FilterIndex = 2,
                    InitialDirectory = string.Empty,
                    FileName = string.Empty
                })
            {
                if (templateOpenFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
                LoadImportTemplate(templateOpenFileDialog.FileName);
            }
        }
        /// <summary>
        /// экспортируем таблицу с загруженными данными в файл CSV без учета строк отмеченных "Игнор."
        /// </summary> 
        private void tbExport_Click(object sender, EventArgs e)
        {
            if (DGV_ImportData.Columns.GetColumnCount(DataGridViewElementStates.Visible) == 0) { return; }
            try
            {
                DGV_ImportData.EndEdit();
                ToolStripStatusLabel1.Text = Resources.ЭкспортДанныхИзТаблицы;

                using (var csvSaveFileDialog = new SaveFileDialog()
                    {
                        Title = Resources.ЭкспортДанныхИзТаблицы,
                        Filter = Resources.ФильтрФайловCSV,
                        FilterIndex = 2,
                        InitialDirectory = string.Empty,
                        FileName = string.Empty
                    })
                {
                    if (csvSaveFileDialog.ShowDialog(this) == DialogResult.Cancel) { return; }

                    using (var sw = new StreamWriter(csvSaveFileDialog.FileName, false, Encoding.Default))
                    {
                        var delim = _parser.ColumnsDelimeter.ToString();
                        sw.WriteLine(string.Join(delim, (DGV_ImportData.Columns
                                                            .Cast<DataGridViewColumn>()
                                                            .Select(clm => clm.HeaderText))
                                                            .ToArray())
                                              .Replace(Resources.Игнор, string.Empty)
                                              .Substring(1));

                        //выбираем только не игнорируемые строки без первого столбца "Игнор"
                        IEnumerable<string[]> rows =
                            DGV_ImportData.Rows
                                .Cast<DataGridViewRow>()
                                .Where(
                                    row =>
                                        !row.IsNewRow &&
                                        (row.Cells[0].Value == null || Convert.ToBoolean(row.Cells[0].Value) == false))
                                .Select(
                                    row =>
                                        Array.ConvertAll(row.Cells
                                                                .Cast<DataGridViewCell>()
                                                                .Where(c => c.ColumnIndex > 0)
                                                                .ToArray(),
                                            c => ((c.Value != null) ? c.Value.ToString() : string.Empty)));

                        foreach (string[] r in rows)
                        {
                            //строки без первой колонки "игнор", которая в нашем случае будет всегда = False
                            sw.WriteLine(string.Join(delim, r));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ОшибкаЭкспортаТаблицы, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                ToolStripStatusLabel1.Text = Resources.Готово;
            }
        }
        private void tbImportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripStatusLabel1.Text = Resources.ЗагрузкаДанных;
                using (var csvOpenFileDialog = new OpenFileDialog()
                {
                    Title = _importDialogTitle,
                    Filter = _importFilter,
                    FilterIndex = 2,
                    InitialDirectory = string.Empty,
                    FileName = string.Empty
                })
                {
                    if (csvOpenFileDialog.ShowDialog() == DialogResult.Cancel) { return; }

                    using (var fileStream = new FileStream(csvOpenFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var sr = new StreamReader(fileStream, Encoding.Default))
                        {
                            _parser.Data = sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Ошибка, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                ToolStripStatusLabel1.Text = Resources.Готово;
            }
        }

        private void tblCopyToClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGV_ImportData != null) Clipboard.SetDataObject(DGV_ImportData.GetClipboardContent());
            }
            catch (ExternalException ex)
            {
                MessageBox.Show(ex.Message, Resources.Ошибка, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void tblPasteFromClipboard_Click(object sender, EventArgs e)
        {
            IDataObject dataInClipboard = Clipboard.GetDataObject();
            if (dataInClipboard != null && dataInClipboard.GetDataPresent(DataFormats.Text))
            {
                _parser.Data = dataInClipboard.GetData(DataFormats.UnicodeText, true) as string;
            }
        }



        private void LoadImportTemplate(string templatePath)
        {
            if (!File.Exists(templatePath)) { return; }

            SuspendDGV();
            try
            {
                using (var xmlDoc = new XmlTemplateFile(templatePath))
                {
                    ColumnInfo[] columns = xmlDoc.GetNodesListToArrayByTag("Column");
                    if (columns.Length != DGV_ImportData.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1)
                    {
                        throw new NotSupportedException(Resources.ВыбранныйШаблонИмпортаНеПодходитКЗагруженн);
                    }
                    for (var i = 0; i < columns.Length; i++)
                    {
                        if (ImportFields.Contains(columns[i].HeaderText))
                        {
                            DGV_ImportData.Columns[i + 1].HeaderText = columns[i].HeaderText;
                            DGV_ImportData.Columns[i + 1].DataPropertyName = columns[i].DataPropertyName;
                        }
                        else if (!string.IsNullOrEmpty(columns[i].HeaderText))
                        {
                            throw new NotSupportedException(Resources.ВыбранныйШаблонИмпортаНеПодходитКЗагруженн);
                        }
                    }
                    txtDelimeter.Text = xmlDoc.GetByTag("Separator");
                    txtStartLineImport.Text = xmlDoc.GetByTag("StartPosition");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ОшибкаЗагрузкиШаблона, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            ResumeDGV();
        }

        private void txtStartLineImport_TextChanged(object sender, EventArgs e)
        {
            _parser.StartLine = Convert.ToInt32(txtStartLineImport.Text);
        }
        private void txtDelimeter_TextChanged(object sender, EventArgs e)
        {
            _parser.ColumnsDelimeter = Convert.ToChar(txtDelimeter.Text.Replace(@"\s", " ").Replace(@"\t", "\t"));
        }
        private void menuSep_Click(object sender, EventArgs e)
        {
            txtDelimeter.Text = ((ToolStripMenuItem)sender).Tag.ToString();
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            GenerateContextMenu(-1);

            if (HeaderContextMenu.Items.Cast<ToolStripItem>().Count(x => x.Text.StartsWith("* ")) != 0)
            {
                MessageBox.Show(Resources.НеВсеОбязательныеДляИмпортаПоляЗадействова,
                                Resources.ОшибкаИмпорта, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DGV_ImportData.EndEdit();

            if (_checkErrorsFunction != null && _checkErrorsFunction(ref DGV_ImportData, ref DGV_Errors) == false)
            {
                MessageBox.Show(Resources.НеобходимоУстранитьВсеОшибкиВДанныхПередИм,
                                Resources.ОшибкиВИмпортируемыхДанных, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }



    }
}
