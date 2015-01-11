using System.Windows.Forms;
namespace CSVImportParser
{

    partial class frmCSVImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCSVImport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.SCV_ToolStripMain = new System.Windows.Forms.ToolStrip();
            this.tbImportCSV = new System.Windows.Forms.ToolStripButton();
            this.tblExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tblPasteFromClipboard = new System.Windows.Forms.ToolStripButton();
            this.tblCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tblDeleteSelectedRows = new System.Windows.Forms.ToolStripButton();
            this.tblIgnorSelectedRows = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtDelimeter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuSepTab = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSepSpace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSepDotComa = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSepComa = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtStartLineImport = new System.Windows.Forms.ToolStripTextBox();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbImportTemplate = new System.Windows.Forms.ToolStripButton();
            this.tbExportTemplate = new System.Windows.Forms.ToolStripButton();
            this.cmdCancel = new System.Windows.Forms.ToolStripButton();
            this.cmdOK = new System.Windows.Forms.ToolStripButton();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DGV_ImportData = new System.Windows.Forms.DataGridView();
            this.TabPages = new System.Windows.Forms.TabControl();
            this.TabPage_Tips = new System.Windows.Forms.TabPage();
            this.ImportTips = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtSourceDataFile = new System.Windows.Forms.TextBox();
            this.TabPage_CheckDataTable = new System.Windows.Forms.TabPage();
            this.DGV_Errors = new System.Windows.Forms.DataGridView();
            this.ErrorNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmnRowNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSV_StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.HeaderContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SCV_ToolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_ImportData)).BeginInit();
            this.TabPages.SuspendLayout();
            this.TabPage_Tips.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.TabPage_CheckDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Errors)).BeginInit();
            this.CSV_StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SCV_ToolStripMain
            // 
            this.SCV_ToolStripMain.AutoSize = false;
            this.SCV_ToolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.SCV_ToolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbImportCSV,
            this.tblExport,
            this.toolStripSeparator3,
            this.tblPasteFromClipboard,
            this.tblCopyToClipboard,
            this.toolStripSeparator1,
            this.tblDeleteSelectedRows,
            this.tblIgnorSelectedRows,
            this.ToolStripSeparator4,
            this.toolStripLabel1,
            this.txtDelimeter,
            this.toolStripDropDownButton1,
            this.toolStripLabel2,
            this.txtStartLineImport,
            this.ToolStripSeparator2,
            this.tbImportTemplate,
            this.tbExportTemplate,
            this.cmdCancel,
            this.cmdOK});
            this.SCV_ToolStripMain.Location = new System.Drawing.Point(0, 0);
            this.SCV_ToolStripMain.Name = "SCV_ToolStripMain";
            this.SCV_ToolStripMain.Size = new System.Drawing.Size(849, 32);
            this.SCV_ToolStripMain.TabIndex = 1;
            // 
            // tbImportCSV
            // 
            this.tbImportCSV.Image = ((System.Drawing.Image)(resources.GetObject("tbImportCSV.Image")));
            this.tbImportCSV.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbImportCSV.ImageTransparentColor = System.Drawing.Color.White;
            this.tbImportCSV.Name = "tbImportCSV";
            this.tbImportCSV.Size = new System.Drawing.Size(72, 29);
            this.tbImportCSV.Text = "Импорт";
            this.tbImportCSV.ToolTipText = "Импорт данных из файла CSV";
            this.tbImportCSV.Click += new System.EventHandler(this.tbImportCSV_Click);
            // 
            // tblExport
            // 
            this.tblExport.Image = ((System.Drawing.Image)(resources.GetObject("tblExport.Image")));
            this.tblExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tblExport.ImageTransparentColor = System.Drawing.Color.White;
            this.tblExport.Name = "tblExport";
            this.tblExport.Size = new System.Drawing.Size(77, 29);
            this.tblExport.Text = "Экспорт";
            this.tblExport.ToolTipText = "Экспорт данных из таблицы в файл CSV";
            this.tblExport.Click += new System.EventHandler(this.tbExport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // tblPasteFromClipboard
            // 
            this.tblPasteFromClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblPasteFromClipboard.Image = ((System.Drawing.Image)(resources.GetObject("tblPasteFromClipboard.Image")));
            this.tblPasteFromClipboard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tblPasteFromClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblPasteFromClipboard.Name = "tblPasteFromClipboard";
            this.tblPasteFromClipboard.Size = new System.Drawing.Size(28, 29);
            this.tblPasteFromClipboard.Text = "Вставить данные из буфера омбена в таблицу";
            this.tblPasteFromClipboard.Click += new System.EventHandler(this.tblPasteFromClipboard_Click);
            // 
            // tblCopyToClipboard
            // 
            this.tblCopyToClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("tblCopyToClipboard.Image")));
            this.tblCopyToClipboard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tblCopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblCopyToClipboard.Name = "tblCopyToClipboard";
            this.tblCopyToClipboard.Size = new System.Drawing.Size(28, 29);
            this.tblCopyToClipboard.Text = "Скопировать в буфер обмена выбранные строки";
            this.tblCopyToClipboard.Click += new System.EventHandler(this.tblCopyToClipboard_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tblDeleteSelectedRows
            // 
            this.tblDeleteSelectedRows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblDeleteSelectedRows.Image = ((System.Drawing.Image)(resources.GetObject("tblDeleteSelectedRows.Image")));
            this.tblDeleteSelectedRows.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tblDeleteSelectedRows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblDeleteSelectedRows.Name = "tblDeleteSelectedRows";
            this.tblDeleteSelectedRows.Size = new System.Drawing.Size(28, 29);
            this.tblDeleteSelectedRows.Text = "Удалить выбранные строки";
            this.tblDeleteSelectedRows.Click += new System.EventHandler(this.tblDeleteSelectedRows_Click);
            // 
            // tblIgnorSelectedRows
            // 
            this.tblIgnorSelectedRows.AutoSize = false;
            this.tblIgnorSelectedRows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblIgnorSelectedRows.Image = ((System.Drawing.Image)(resources.GetObject("tblIgnorSelectedRows.Image")));
            this.tblIgnorSelectedRows.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tblIgnorSelectedRows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblIgnorSelectedRows.Name = "tblIgnorSelectedRows";
            this.tblIgnorSelectedRows.Size = new System.Drawing.Size(29, 29);
            this.tblIgnorSelectedRows.Text = "Игнорировать выбранные записи";
            this.tblIgnorSelectedRows.Click += new System.EventHandler(this.tblIgnorSelectedRows_Click);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(77, 29);
            this.toolStripLabel1.Text = "Разделитель:";
            // 
            // txtDelimeter
            // 
            this.txtDelimeter.MaxLength = 1;
            this.txtDelimeter.Name = "txtDelimeter";
            this.txtDelimeter.Size = new System.Drawing.Size(20, 32);
            this.txtDelimeter.Text = ";";
            this.txtDelimeter.TextChanged += new System.EventHandler(this.txtDelimeter_TextChanged);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSepTab,
            this.menuSepSpace,
            this.menuSepDotComa,
            this.menuSepComa});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 29);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // menuSepTab
            // 
            this.menuSepTab.Name = "menuSepTab";
            this.menuSepTab.Size = new System.Drawing.Size(156, 22);
            this.menuSepTab.Tag = "\\t";
            this.menuSepTab.Text = "Знак табуляции";
            this.menuSepTab.Click += new System.EventHandler(this.menuSep_Click);
            // 
            // menuSepSpace
            // 
            this.menuSepSpace.Name = "menuSepSpace";
            this.menuSepSpace.Size = new System.Drawing.Size(156, 22);
            this.menuSepSpace.Tag = "\\s";
            this.menuSepSpace.Text = "Пробел";
            this.menuSepSpace.Click += new System.EventHandler(this.menuSep_Click);
            // 
            // menuSepDotComa
            // 
            this.menuSepDotComa.Name = "menuSepDotComa";
            this.menuSepDotComa.Size = new System.Drawing.Size(156, 22);
            this.menuSepDotComa.Tag = ";";
            this.menuSepDotComa.Text = "Точка с запятой";
            this.menuSepDotComa.Click += new System.EventHandler(this.menuSep_Click);
            // 
            // menuSepComa
            // 
            this.menuSepComa.Name = "menuSepComa";
            this.menuSepComa.Size = new System.Drawing.Size(156, 22);
            this.menuSepComa.Tag = ",";
            this.menuSepComa.Text = "Запятая";
            this.menuSepComa.Click += new System.EventHandler(this.menuSep_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(100, 29);
            this.toolStripLabel2.Text = "Импорт со строки:";
            // 
            // txtStartLineImport
            // 
            this.txtStartLineImport.Name = "txtStartLineImport";
            this.txtStartLineImport.Size = new System.Drawing.Size(50, 32);
            this.txtStartLineImport.Text = "2";
            this.txtStartLineImport.ToolTipText = "Импорт начиная с указанной строки по счету";
            this.txtStartLineImport.TextChanged += new System.EventHandler(this.txtStartLineImport_TextChanged);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // tbImportTemplate
            // 
            this.tbImportTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbImportTemplate.Image = ((System.Drawing.Image)(resources.GetObject("tbImportTemplate.Image")));
            this.tbImportTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbImportTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbImportTemplate.Name = "tbImportTemplate";
            this.tbImportTemplate.Size = new System.Drawing.Size(28, 29);
            this.tbImportTemplate.Text = "Загрузить шаблон импорта";
            this.tbImportTemplate.ToolTipText = "Загрузить шаблон импорта";
            this.tbImportTemplate.Click += new System.EventHandler(this.tbImportTemplate_Click);
            // 
            // tbExportTemplate
            // 
            this.tbExportTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbExportTemplate.Image = ((System.Drawing.Image)(resources.GetObject("tbExportTemplate.Image")));
            this.tbExportTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbExportTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbExportTemplate.Name = "tbExportTemplate";
            this.tbExportTemplate.Size = new System.Drawing.Size(28, 29);
            this.tbExportTemplate.Text = "Сохранить шаблон импорта";
            this.tbExportTemplate.Click += new System.EventHandler(this.tbExportTemplate_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.cmdCancel.Size = new System.Drawing.Size(73, 29);
            this.cmdCancel.Text = "Отмена";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmdOK.Image = ((System.Drawing.Image)(resources.GetObject("cmdOK.Image")));
            this.cmdOK.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.cmdOK.Size = new System.Drawing.Size(61, 29);
            this.cmdOK.Text = "OK...";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 32);
            this.SplitContainer1.Name = "SplitContainer1";
            this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.DGV_ImportData);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.TabPages);
            this.SplitContainer1.Panel2.Controls.Add(this.CSV_StatusStrip);
            this.SplitContainer1.Size = new System.Drawing.Size(849, 480);
            this.SplitContainer1.SplitterDistance = 266;
            this.SplitContainer1.TabIndex = 6;
            // 
            // DGV_ImportData
            // 
            this.DGV_ImportData.AllowUserToResizeRows = false;
            this.DGV_ImportData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DGV_ImportData.BackgroundColor = System.Drawing.Color.White;
            this.DGV_ImportData.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.DGV_ImportData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_ImportData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_ImportData.Location = new System.Drawing.Point(0, 0);
            this.DGV_ImportData.Name = "DGV_ImportData";
            this.DGV_ImportData.RowHeadersVisible = false;
            this.DGV_ImportData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_ImportData.ShowCellErrors = false;
            this.DGV_ImportData.ShowRowErrors = false;
            this.DGV_ImportData.Size = new System.Drawing.Size(849, 266);
            this.DGV_ImportData.TabIndex = 0;
            this.DGV_ImportData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGV_ImportData_ColumnHeaderMouseClick);
            // 
            // TabPages
            // 
            this.TabPages.Controls.Add(this.TabPage_Tips);
            this.TabPages.Controls.Add(this.tabPage2);
            this.TabPages.Controls.Add(this.TabPage_CheckDataTable);
            this.TabPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabPages.Location = new System.Drawing.Point(0, 0);
            this.TabPages.Name = "TabPages";
            this.TabPages.SelectedIndex = 0;
            this.TabPages.Size = new System.Drawing.Size(849, 188);
            this.TabPages.TabIndex = 5;
            // 
            // TabPage_Tips
            // 
            this.TabPage_Tips.Controls.Add(this.ImportTips);
            this.TabPage_Tips.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Tips.Name = "TabPage_Tips";
            this.TabPage_Tips.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Tips.Size = new System.Drawing.Size(841, 162);
            this.TabPage_Tips.TabIndex = 0;
            this.TabPage_Tips.Text = "Подсказки";
            this.TabPage_Tips.UseVisualStyleBackColor = true;
            // 
            // ImportTips
            // 
            this.ImportTips.BackColor = System.Drawing.SystemColors.Info;
            this.ImportTips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImportTips.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ImportTips.Location = new System.Drawing.Point(3, 3);
            this.ImportTips.Multiline = true;
            this.ImportTips.Name = "ImportTips";
            this.ImportTips.ReadOnly = true;
            this.ImportTips.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ImportTips.Size = new System.Drawing.Size(835, 156);
            this.ImportTips.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtSourceDataFile);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(841, 162);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Исходный файл";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtSourceDataFile
            // 
            this.txtSourceDataFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSourceDataFile.Location = new System.Drawing.Point(3, 3);
            this.txtSourceDataFile.Multiline = true;
            this.txtSourceDataFile.Name = "txtSourceDataFile";
            this.txtSourceDataFile.ReadOnly = true;
            this.txtSourceDataFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSourceDataFile.Size = new System.Drawing.Size(835, 156);
            this.txtSourceDataFile.TabIndex = 0;
            // 
            // TabPage_CheckDataTable
            // 
            this.TabPage_CheckDataTable.Controls.Add(this.DGV_Errors);
            this.TabPage_CheckDataTable.Location = new System.Drawing.Point(4, 22);
            this.TabPage_CheckDataTable.Name = "TabPage_CheckDataTable";
            this.TabPage_CheckDataTable.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_CheckDataTable.Size = new System.Drawing.Size(841, 162);
            this.TabPage_CheckDataTable.TabIndex = 2;
            this.TabPage_CheckDataTable.Text = "Анализ данных";
            this.TabPage_CheckDataTable.UseVisualStyleBackColor = true;
            // 
            // DGV_Errors
            // 
            this.DGV_Errors.AllowUserToAddRows = false;
            this.DGV_Errors.AllowUserToDeleteRows = false;
            this.DGV_Errors.AllowUserToResizeRows = false;
            this.DGV_Errors.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DGV_Errors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Errors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ErrorNumber,
            this.ClmnRowNum,
            this.ErrorText});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_Errors.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Errors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_Errors.Location = new System.Drawing.Point(3, 3);
            this.DGV_Errors.Name = "DGV_Errors";
            this.DGV_Errors.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.DGV_Errors.RowHeadersVisible = false;
            this.DGV_Errors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_Errors.Size = new System.Drawing.Size(835, 156);
            this.DGV_Errors.TabIndex = 4;
            // 
            // ErrorNumber
            // 
            this.ErrorNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ErrorNumber.HeaderText = "#";
            this.ErrorNumber.Name = "ErrorNumber";
            this.ErrorNumber.ReadOnly = true;
            this.ErrorNumber.Width = 39;
            // 
            // ClmnRowNum
            // 
            this.ClmnRowNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ClmnRowNum.HeaderText = "Строка";
            this.ClmnRowNum.Name = "ClmnRowNum";
            this.ClmnRowNum.ReadOnly = true;
            this.ClmnRowNum.Width = 68;
            // 
            // ErrorText
            // 
            this.ErrorText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ErrorText.HeaderText = "Текст ошибки";
            this.ErrorText.Name = "ErrorText";
            this.ErrorText.ReadOnly = true;
            // 
            // CSV_StatusStrip
            // 
            this.CSV_StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel1});
            this.CSV_StatusStrip.Location = new System.Drawing.Point(0, 188);
            this.CSV_StatusStrip.Name = "CSV_StatusStrip";
            this.CSV_StatusStrip.Size = new System.Drawing.Size(849, 22);
            this.CSV_StatusStrip.TabIndex = 3;
            this.CSV_StatusStrip.Text = "StatusStrip1";
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(43, 17);
            this.ToolStripStatusLabel1.Text = "Готово";
            // 
            // HeaderContextMenu
            // 
            this.HeaderContextMenu.Name = "HeaderContextMenu";
            this.HeaderContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // frmCSVImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 512);
            this.Controls.Add(this.SplitContainer1);
            this.Controls.Add(this.SCV_ToolStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCSVImport";
            this.Text = "Импорт данных из CSV";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCSVImport_FormClosed);
            this.Load += new System.EventHandler(this.frmCSVImport_Load);
            this.SCV_ToolStripMain.ResumeLayout(false);
            this.SCV_ToolStripMain.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_ImportData)).EndInit();
            this.TabPages.ResumeLayout(false);
            this.TabPage_Tips.ResumeLayout(false);
            this.TabPage_Tips.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.TabPage_CheckDataTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Errors)).EndInit();
            this.CSV_StatusStrip.ResumeLayout(false);
            this.CSV_StatusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ToolStrip SCV_ToolStripMain;
        internal System.Windows.Forms.ToolStripButton tbImportCSV;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton tblDeleteSelectedRows;
        internal System.Windows.Forms.ToolStripButton tblIgnorSelectedRows;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tbImportTemplate;
        internal System.Windows.Forms.ToolStripButton tbExportTemplate;
        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal DataGridView DGV_ImportData;
        internal System.Windows.Forms.StatusStrip CSV_StatusStrip;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtDelimeter;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem menuSepTab;
        private System.Windows.Forms.ToolStripMenuItem menuSepDotComa;
        private System.Windows.Forms.ToolStripMenuItem menuSepSpace;
        private System.Windows.Forms.ToolStripMenuItem menuSepComa;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox txtStartLineImport;
        private System.Windows.Forms.ContextMenuStrip HeaderContextMenu;
        internal System.Windows.Forms.ToolStripButton tblExport;
        internal System.Windows.Forms.ToolStripButton cmdCancel;
        internal System.Windows.Forms.ToolStripButton cmdOK;
        internal System.Windows.Forms.ToolStripButton tblPasteFromClipboard;
        internal System.Windows.Forms.ToolStripButton tblCopyToClipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private TabControl TabPages;
        private TabPage TabPage_Tips;
        private TextBox ImportTips;
        private TabPage tabPage2;
        private TextBox txtSourceDataFile;
        private TabPage TabPage_CheckDataTable;
        internal DataGridView DGV_Errors;
        private DataGridViewTextBoxColumn ErrorNumber;
        private DataGridViewTextBoxColumn ClmnRowNum;
        private DataGridViewTextBoxColumn ErrorText;
    }
}