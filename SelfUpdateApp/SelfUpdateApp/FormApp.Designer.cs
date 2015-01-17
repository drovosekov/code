namespace SelfUpdateApp
{
    partial class FormApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApp));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdCheck = new System.Windows.Forms.ToolStripButton();
            this.cmdUpdate = new System.Windows.Forms.ToolStripButton();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.tmrCheckUpdate = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCheckUpdatesInterval = new System.Windows.Forms.Label();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.lblTicks = new System.Windows.Forms.Label();
            this.lblCheckUpdates = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdCheck,
            this.cmdUpdate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(278, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdCheck
            // 
            this.cmdCheck.Image = ((System.Drawing.Image)(resources.GetObject("cmdCheck.Image")));
            this.cmdCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCheck.Name = "cmdCheck";
            this.cmdCheck.Size = new System.Drawing.Size(145, 22);
            this.cmdCheck.Text = "Проверить обновления";
            this.cmdCheck.Click += new System.EventHandler(this.TMR_CheckUpdate_Tick);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Enabled = false;
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(77, 22);
            this.cmdUpdate.Text = "Обновить";
            this.cmdUpdate.Click += new System.EventHandler(this.CMD_Update_Click);
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Location = new System.Drawing.Point(12, 35);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(97, 13);
            this.lblCurrentVersion.TabIndex = 1;
            this.lblCurrentVersion.Text = "Текущая версия: ";
            // 
            // tmrCheckUpdate
            // 
            this.tmrCheckUpdate.Interval = 10000;
            this.tmrCheckUpdate.Tick += new System.EventHandler(this.TMR_CheckUpdate_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 136);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(278, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusText
            // 
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(43, 17);
            this.lblStatusText.Text = "Готово";
            // 
            // lblCheckUpdatesInterval
            // 
            this.lblCheckUpdatesInterval.AutoSize = true;
            this.lblCheckUpdatesInterval.Location = new System.Drawing.Point(12, 79);
            this.lblCheckUpdatesInterval.Name = "lblCheckUpdatesInterval";
            this.lblCheckUpdatesInterval.Size = new System.Drawing.Size(166, 13);
            this.lblCheckUpdatesInterval.TabIndex = 4;
            this.lblCheckUpdatesInterval.Text = "Интервал обновления, секунд: ";
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 10;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // lblTicks
            // 
            this.lblTicks.AutoSize = true;
            this.lblTicks.Location = new System.Drawing.Point(12, 104);
            this.lblTicks.Name = "lblTicks";
            this.lblTicks.Size = new System.Drawing.Size(43, 13);
            this.lblTicks.TabIndex = 5;
            this.lblTicks.Text = "Время:";
            // 
            // lblCheckUpdates
            // 
            this.lblCheckUpdates.AutoSize = true;
            this.lblCheckUpdates.Location = new System.Drawing.Point(12, 57);
            this.lblCheckUpdates.Name = "lblCheckUpdates";
            this.lblCheckUpdates.Size = new System.Drawing.Size(89, 13);
            this.lblCheckUpdates.TabIndex = 6;
            this.lblCheckUpdates.Text = "Нет обновлений";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 158);
            this.Controls.Add(this.lblCheckUpdates);
            this.Controls.Add(this.lblTicks);
            this.Controls.Add(this.lblCheckUpdatesInterval);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Программа";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdCheck;
        private System.Windows.Forms.ToolStripButton cmdUpdate;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Timer tmrCheckUpdate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label lblCheckUpdatesInterval;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Label lblTicks;
        private System.Windows.Forms.Label lblCheckUpdates;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusText;
    }
}

