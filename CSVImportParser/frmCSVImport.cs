using System;
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
        public delegate void PreFormatingRow(ref object datavalue, string datacolumns);

        /// <summary>
        /// вызывается после нажатия ОК. В случае возврата из ф-ии false - будет показано сообщение
        /// о наличии ошибок в данных таблицы и предложение их исправить
        /// ошибки перечисляются в таблице DGV_ErrorTable (на форме в закладке "Анализ таблицы")
        /// </summary>
        public delegate bool CheckErrorsFunc(ref DGV DGV_SourceDataTable, ref  DataGridView DGV_ErrorTable);
        #endregion

        #region private section
        private string _ImportTipsText;
        private string _ImportFilter;
        private string _ImportDialogTitle;
        private CheckErrorsFunc _check;
        private PreFormatingRow _preFormat;
        private CSVParser Parser;
        #endregion

        #region public section
        public string ImportTipsText
        {
            get
            {
                return _ImportTipsText;
            }
            set
            {
                _ImportTipsText = value;
                ImportTips.Text = value;
            }
        }
        public string TemplateDefaultPath { get; set; }
        public string ImportFilter
        {
            get
            {
                return _ImportFilter;
            }
            set
            {
                string v = value;
                if (!v.Contains("*.*"))
                {
                    _ImportFilter += Properties.Resources.ВсеФайлы;
                }
                _ImportFilter += v;
            }
        }
        public string ImportDialogTitle
        {
            get
            {
                return _ImportDialogTitle;
            }
            set
            {
                Text = String.Format(Properties.Resources.ТекстЗаголовкаОкнаИмпорта, value);
                _ImportDialogTitle = value;
            }
        }
        public ImportFieldsList ImportFields { get; set; }
        public DataTable Data
        {
            get
            {
                using (DataTable preData = new DataTable() { TableName = "ImportedData" })
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
                    var rows = from DataGridViewRow row in DGV_ImportData.Rows.Cast<DataGridViewRow>()
                               where !row.IsNewRow && (row.Cells[0].Value == null || Convert.ToBoolean(row.Cells[0].Value) == false)//не игнор. строки
                               select Array.ConvertAll(row.Cells.Cast<DataGridViewCell>()
                                        .Where(x => !string.IsNullOrEmpty(DGV_ImportData.Columns[x.ColumnIndex].DataPropertyName)) //только колонки с выбранным назначением
                                        .ToArray(), c => ((c.Value != null) ? c.Value.ToString() : ""));

                    foreach (string[] row in rows)
                    {
                        preData.Rows.Add(row);
                    }
                    #endregion
                    return preData;
                }

            }
        }
        public Collection<T> ListData<T>() where T : class, new()
        {
            Collection<T> preList = new Collection<T>();
            bool skip = false;
            object v;

            var settedcolumns = DGV_ImportData.Columns.Cast<DataGridViewColumn>()
                .Where(x => !string.IsNullOrEmpty(x.DataPropertyName))
                .Select(x => new ColumnInfo()
                {
                    DataPropertyName = x.DataPropertyName,
                    Index = x.Index
                })
                .ToArray();

            foreach (DataGridViewRow dgv_row in DGV_ImportData.Rows)
            {
                if (Convert.ToBoolean(dgv_row.Cells[0].Value) == false)//только не игнор. строки
                {
                    T obj = new T();

                    foreach (ColumnInfo dc in settedcolumns)
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(dc.DataPropertyName);
                            if (propertyInfo != null)
                            {
                                v = dgv_row.Cells[dc.Index].Value;
                                if (_preFormat != null)
                                {
                                    _preFormat(ref v, dc.DataPropertyName);
                                    if (v == null)
                                    {
                                        skip = true;
                                        break;
                                    }
                                }
                                if (v != null)
                                {
                                    if (CommonFuncs.IsFloatNumeric(v.ToString()))
                                    {
                                        v = CommonFuncs.GetDecimal(v.ToString());
                                    }

                                    //серелизуем значение ячейки в соотв. свойства класса 
                                    propertyInfo.SetValue(obj, targetType(v, propertyInfo), null);
                                }
                            }
                        }
                        catch //(Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                            continue;
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
                }
            }

            return preList;
        }

        public void SetCheck(CheckErrorsFunc check)
        {
            _check = check;
        }
        public void preFormat(PreFormatingRow preFormat)
        {
            _preFormat = preFormat;
        }
        #endregion

        public frmCSVImport()
        {
            InitializeComponent();
        }
        private void frmCSVImport_Load(object sender, EventArgs e)
        {
            CommonFuncs.LoadFormPositionAndSize(this);

            if (_check == null) { TabPages.TabPages.RemoveAt(2); }
            if (string.IsNullOrEmpty(ImportTipsText)) { TabPages.TabPages.RemoveAt(0); }

            Parser = new CSVParser()
            {
                ColumnsDelimeter = Convert.ToChar(txtDelimeter.Text),
                StartLine = Convert.ToInt32(txtStartLineImport.Text)
            };
            Parser.onDataChanged += onDataChanged;
            Parser.onProgressChanged += onProgressChanged;
            SplitContainer1.SplitterDistance = Convert.ToInt32(Registry.GetValue(CommonFuncs.GetRegistryPathForFormSettings(Name), "SplitterDistance", 266));
        }
        private void frmCSVImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            CommonFuncs.SaveFormPositionAndSize(this);

            Registry.SetValue(CommonFuncs.GetRegistryPathForFormSettings(Name), "SplitterDistance", SplitContainer1.SplitterDistance);
        }

        private void onDataChanged(bool Rebuild)
        {
            SuspendDGV();
            try
            {
                txtSourceDataFile.Text = Parser.PreviewData;
                toolStripProgressBar1.Maximum = Parser.RowsCount;
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Visible = true;

                //если изменения данных влияют на структуру колонок - перезагружаем их
                if (Rebuild)
                {
                    DGV_ImportData.Columns.Clear();
                    DGV_ImportData.Columns.AddRange(Parser.Columns(ImportFields.NotUsedColumnHeaderText));
                }
                //в противном случае (смена стартовой строки) только удаляем столбец Игнор. 
                //чтоб не удалять все столбцы и не сбить уже выбранные сопоставления колонок при загрузке данных в таблицу
                //т.к. кол-во столбцов не меняется, но данные в таблице надо перезагрузить
                else
                {
                    DGV_ImportData.Columns.RemoveAt(0);
                }

                DGV_ImportData.Rows.Clear();
                Parser.FilleRowsTable(DGV_ImportData);

                DGV_ImportData.Columns.Insert(0, new DataGridViewCheckBoxColumn()
                {
                    Name = "ignor",
                    HeaderText = Resources.Игнор
                });

                if (Rebuild)
                {
                    LoadImportTemplate(TemplateDefaultPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Ошибка, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                toolStripProgressBar1.Visible = false;
                ResumeDGV();
            }
        }

        private void onProgressChanged(int ProgressValue)
        {
            toolStripProgressBar1.Value = ProgressValue;
            Application.DoEvents();
        }

        /// <summary>
        /// засовыываем значение v в соотв. свойство класса
        /// </summary>
        /// <param name="v">какое-то значение (строка, число, дробное число)</param>
        /// <param name="propertyInfo">информация о свойстве класса</param>
        /// <returns>сконвертированный тип</returns>
        private static object targetType(object v, PropertyInfo propertyInfo)
        {
            Type targetType = propertyInfo.PropertyType;
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                targetType = Nullable.GetUnderlyingType(targetType);
            }

            return Convert.ChangeType(v, targetType);
        }

        /// <summary>
        /// проверяем что колонка таблицы уже выбрана из меню и используется
        /// </summary>
        /// <param name="HeaderText">текст заголовка</param> 
        /// <returns>true, если текст в HeaderText есть в заголовке одной из колонок таблицы</returns>
        private bool ColumnUsed(string HeaderText)
        {
            return DGV_ImportData.Columns.Cast<DataGridViewColumn>()
                                          .Where(x => x.HeaderText == HeaderText)
                                          .Count() != 0;
        }
        private void HeaderMenuClick(object sender, EventArgs e)
        {
            Fields fld = (Fields)((ToolStripMenuItem)sender).Tag;
            DataGridViewColumn dgvc = DGV_ImportData.Columns[fld.ColumnIndex];
            string hText = fld.HeaderText.Replace("* ", "");
            foreach (DataGridViewColumn col in DGV_ImportData.Columns)
            {
                if (col.HeaderText == hText)
                {
                    col.HeaderText = ImportFields.NotUsedColumnHeaderText;
                    col.DataPropertyName = null;
                    return;
                }
            }
            dgvc.HeaderText = hText;
            dgvc.Name = fld.RealDBName;
            dgvc.DataPropertyName = fld.RealDBName;
        }
        private void GenerateContextMenu(int ColumnIndex)
        {
            HeaderContextMenu.Items.Clear();
            foreach (Fields ifl in ImportFields.ImportFields)
            {
                string MenuHederText = ifl.HeaderText;
                if (MenuHederText == ImportFields.NotUsedColumnHeaderText || !ColumnUsed(MenuHederText))
                {
                    if (ifl.Require)
                    {
                        MenuHederText = String.Format("* {0}", MenuHederText);
                    }
                    ToolStripItem itm = HeaderContextMenu.Items.Add(MenuHederText, null, HeaderMenuClick);
                    ifl.ColumnIndex = ColumnIndex;
                    //сохраняем инфу о элементе для последующего использования
                    itm.Tag = ifl;
                }
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
            DGV_ImportData.SuspendLayout();
        }
        private void ResumeDGV()
        {
            txtSourceDataFile.Enabled = true;
            SCV_ToolStripMain.Enabled = true;
            DGV_ImportData.ResumeLayout();
        }
        private void tblDeleteSelectedRows_Click(object sender, EventArgs e)
        {
            SuspendDGV();
            foreach (DataGridViewRow row in DGV_ImportData.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    DGV_ImportData.Rows.RemoveAt(row.Index);
                }
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

            using (SaveFileDialog CSV_SaveFileDialog = new SaveFileDialog()
                {
                    Title = Resources.ВыберитеНазваниеДляШаблонаИмпорта,
                    Filter = Resources.ФайлыШаблоновИмпортаImportxImportx,
                    FilterIndex = 2,
                    InitialDirectory = string.Empty,
                    FileName = String.Format("{1} {0}", ImportDialogTitle.ToLower(), Resources.Шаблон)
                })
            {
                if (CSV_SaveFileDialog.ShowDialog() == DialogResult.Cancel) { return; }

                using (xmlTemplateFile xmlDoc = new xmlTemplateFile(""))
                {
                    xmlDoc.AddNode("CSVImportTemplate");
                    xmlDoc.AddNode("Separator", txtDelimeter.Text);
                    xmlDoc.AddNode("StartPosition", txtStartLineImport.Text);
                    xmlDoc.AddNode("ColumnsList", null, true);
                    for (int c = 1; c < DGV_ImportData.Columns.GetColumnCount(DataGridViewElementStates.Visible); c++)
                    {
                        xmlDoc.AddNode("Column");
                        if (DGV_ImportData.Columns[c].HeaderText != ImportFields.NotUsedColumnHeaderText)
                        {
                            xmlDoc.AddAttribute("RealName", DGV_ImportData.Columns[c].DataPropertyName);
                            xmlDoc.AddAttribute("HeaderText", DGV_ImportData.Columns[c].HeaderText);
                        }
                    }
                    xmlDoc.Save(CSV_SaveFileDialog.FileName);
                }
            }
        }
        private void tbImportTemplate_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog Template_OpenFileDialog = new OpenFileDialog()
                {
                    Title = Resources.ВыберитеШаблонИмпорта,
                    Filter = Resources.ФайлыШаблоновИмпортаImportxImportx,
                    FilterIndex = 2,
                    InitialDirectory = string.Empty,
                    FileName = ""
                })
            {
                if (Template_OpenFileDialog.ShowDialog() == DialogResult.Cancel) { return; }
                LoadImportTemplate(Template_OpenFileDialog.FileName);
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

                using (SaveFileDialog CSV_SaveFileDialog = new SaveFileDialog()
                    {
                        Title = Resources.ЭкспортДанныхИзТаблицы,
                        Filter = Resources.ФайлыCSVCsv,
                        FilterIndex = 2,
                        InitialDirectory = string.Empty,
                        FileName = ""
                    })
                {
                    if (CSV_SaveFileDialog.ShowDialog(this) == DialogResult.Cancel) { return; }

                    using (StreamWriter sw = new StreamWriter(CSV_SaveFileDialog.FileName, false, Encoding.Default))
                    {
                        string delim = Parser.ColumnsDelimeter.ToString();
                        sw.WriteLine(string.Join(delim, (from DataGridViewColumn clm in DGV_ImportData.Columns.Cast<DataGridViewColumn>()
                                                         select clm.HeaderText).ToArray()).Replace(Resources.Игнор, "").Substring(1));

                        //выбираем только не игнорируемые строки 
                        var rows = from DataGridViewRow row in DGV_ImportData.Rows.Cast<DataGridViewRow>()
                                   where !row.IsNewRow && (row.Cells[0].Value == null || Convert.ToBoolean(row.Cells[0].Value) == false)
                                   select Array.ConvertAll(row.Cells.Cast<DataGridViewCell>().ToArray(), (c) => ((c.Value != null) ? c.Value.ToString() : ""));

                        foreach (var r in rows)
                        {
                            sw.WriteLine(string.Join(delim, r).Replace("False", "").Substring(1));
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
                using (OpenFileDialog CSV_OpenFileDialog = new OpenFileDialog()
                {
                    Title = _ImportDialogTitle,
                    Filter = _ImportFilter,
                    FilterIndex = 2,
                    InitialDirectory = string.Empty,
                    FileName = ""
                })
                {
                    if (CSV_OpenFileDialog.ShowDialog() == DialogResult.Cancel) { return; }

                    using (FileStream fileStream = new FileStream(CSV_OpenFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader sr = new StreamReader(fileStream, Encoding.Default))
                        {
                            Parser.Data = sr.ReadToEnd();
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
                Clipboard.SetDataObject(DGV_ImportData.GetClipboardContent());
            }
            catch (ExternalException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void tblPasteFromClipboard_Click(object sender, EventArgs e)
        {
            IDataObject dataInClipboard = Clipboard.GetDataObject();
            if (dataInClipboard.GetDataPresent(DataFormats.Text))
            {
                Parser.Data = dataInClipboard.GetData(DataFormats.UnicodeText, true) as string;
            }
        }



        private void LoadImportTemplate(string TemplatePath)
        {
            if (!File.Exists(TemplatePath)) { return; }

            SuspendDGV();
            try
            {
                using (xmlTemplateFile xmlDoc = new xmlTemplateFile(TemplatePath))
                {
                    ColumnInfo[] Columns = xmlDoc.GetNodesListToArrayByTag("Column");
                    if (Columns.Length != DGV_ImportData.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1)
                    {
                        throw new NotSupportedException(Resources.ВыбранныйШаблонИмпортаНеПодходитКЗагруженн);
                    }
                    for (int i = 0; i < Columns.Length; i++)
                    {
                        if (ImportFields.Contains(Columns[i].HeaderText))
                        {
                            DGV_ImportData.Columns[i + 1].HeaderText = Columns[i].HeaderText;
                            DGV_ImportData.Columns[i + 1].DataPropertyName = Columns[i].DataPropertyName;
                        }
                        else if (!string.IsNullOrEmpty(Columns[i].HeaderText))
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
            Parser.StartLine = Convert.ToInt32(txtStartLineImport.Text);
        }
        private void txtDelimeter_TextChanged(object sender, EventArgs e)
        {
            Parser.ColumnsDelimeter = Convert.ToChar(txtDelimeter.Text.Replace("\\s", " ").Replace("\\t", "\t"));
        }
        private void menuSep_Click(object sender, EventArgs e)
        {
            txtDelimeter.Text = (sender as ToolStripMenuItem).Tag.ToString();
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            GenerateContextMenu(-1);

            if (HeaderContextMenu.Items.Cast<ToolStripItem>().Where(x => x.Text.StartsWith("* ")).Count() != 0)
            {
                MessageBox.Show(Resources.НеВсеОбязательныеДляИмпортаПоляЗадействова,
                                Resources.ОшибкаИмпорта, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DGV_ImportData.EndEdit();

            if (_check != null && _check(ref DGV_ImportData,ref DGV_Errors) == false)
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
