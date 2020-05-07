namespace Lab_3
{
    partial class FormSerialization
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
            this.cmbSerialization = new System.Windows.Forms.ComboBox();
            this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btChooseFile = new System.Windows.Forms.Button();
            this.lblSerializationName = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSerialization
            // 
            this.cmbSerialization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialization.FormattingEnabled = true;
            this.cmbSerialization.Location = new System.Drawing.Point(135, 51);
            this.cmbSerialization.Name = "cmbSerialization";
            this.cmbSerialization.Size = new System.Drawing.Size(250, 21);
            this.cmbSerialization.TabIndex = 0;
            this.cmbSerialization.SelectedIndexChanged += new System.EventHandler(this.cbSerialization_SelectedIndexChanged);
            // 
            // dlgFileOpen
            // 
            this.dlgFileOpen.Title = "Открыть";
            // 
            // dlgFileSave
            // 
            this.dlgFileSave.CreatePrompt = true;
            this.dlgFileSave.Title = "Сохранить...";
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(136, 25);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(250, 20);
            this.tbFileName.TabIndex = 1;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(25, 28);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(67, 13);
            this.lblFileName.TabIndex = 2;
            this.lblFileName.Text = "Имя файла:";
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(286, 142);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(100, 23);
            this.btOk.TabIndex = 3;
            this.btOk.Text = "Ок";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(392, 142);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(100, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btChooseFile
            // 
            this.btChooseFile.Location = new System.Drawing.Point(392, 25);
            this.btChooseFile.Name = "btChooseFile";
            this.btChooseFile.Size = new System.Drawing.Size(100, 23);
            this.btChooseFile.TabIndex = 5;
            this.btChooseFile.Text = "Выбрать файл";
            this.btChooseFile.UseVisualStyleBackColor = true;
            this.btChooseFile.Click += new System.EventHandler(this.btChooseFile_Click);
            // 
            // lblSerializationName
            // 
            this.lblSerializationName.AutoSize = true;
            this.lblSerializationName.Location = new System.Drawing.Point(25, 54);
            this.lblSerializationName.Name = "lblSerializationName";
            this.lblSerializationName.Size = new System.Drawing.Size(104, 13);
            this.lblSerializationName.TabIndex = 6;
            this.lblSerializationName.Text = "Тип сериализации:";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 190);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(534, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // FormSerialization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 212);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lblSerializationName);
            this.Controls.Add(this.btChooseFile);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.cmbSerialization);
            this.MaximizeBox = false;
            this.Name = "FormSerialization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSerialization";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSerialization_FormClosing);
            this.Load += new System.EventHandler(this.FormSerialization_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSerialization;
        private System.Windows.Forms.OpenFileDialog dlgFileOpen;
        private System.Windows.Forms.SaveFileDialog dlgFileSave;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btChooseFile;
        private System.Windows.Forms.Label lblSerializationName;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}