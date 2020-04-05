namespace Lab_2
{
    partial class MainForm
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
            this.cmbClasses = new System.Windows.Forms.ComboBox();
            this.btAddObject = new System.Windows.Forms.Button();
            this.btUpdateObject = new System.Windows.Forms.Button();
            this.btReadObject = new System.Windows.Forms.Button();
            this.btDeleteObject = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.miReference = new System.Windows.Forms.ToolStripMenuItem();
            this.miAboutProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.miAboutDeveloper = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbObjects = new System.Windows.Forms.ComboBox();
            this.miTask = new System.Windows.Forms.ToolStripMenuItem();
            this.miAdditionalTask = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbClasses
            // 
            this.cmbClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClasses.FormattingEnabled = true;
            this.cmbClasses.Location = new System.Drawing.Point(25, 50);
            this.cmbClasses.Name = "cmbClasses";
            this.cmbClasses.Size = new System.Drawing.Size(121, 21);
            this.cmbClasses.Sorted = true;
            this.cmbClasses.TabIndex = 0;
            this.cmbClasses.SelectedIndexChanged += new System.EventHandler(this.cmbClasses_SelectedIndexChanged);
            // 
            // btAddObject
            // 
            this.btAddObject.Location = new System.Drawing.Point(175, 50);
            this.btAddObject.Name = "btAddObject";
            this.btAddObject.Size = new System.Drawing.Size(100, 23);
            this.btAddObject.TabIndex = 1;
            this.btAddObject.Text = "Добавить";
            this.btAddObject.UseVisualStyleBackColor = true;
            this.btAddObject.Click += new System.EventHandler(this.btAddObject_Click);
            // 
            // btUpdateObject
            // 
            this.btUpdateObject.Location = new System.Drawing.Point(600, 50);
            this.btUpdateObject.Name = "btUpdateObject";
            this.btUpdateObject.Size = new System.Drawing.Size(100, 23);
            this.btUpdateObject.TabIndex = 2;
            this.btUpdateObject.Text = "Редактировать";
            this.btUpdateObject.UseVisualStyleBackColor = true;
            this.btUpdateObject.Click += new System.EventHandler(this.btUpdateObject_Click);
            // 
            // btReadObject
            // 
            this.btReadObject.Location = new System.Drawing.Point(600, 80);
            this.btReadObject.Name = "btReadObject";
            this.btReadObject.Size = new System.Drawing.Size(100, 23);
            this.btReadObject.TabIndex = 3;
            this.btReadObject.Text = "Просмотреть";
            this.btReadObject.UseVisualStyleBackColor = true;
            this.btReadObject.Click += new System.EventHandler(this.btReadObject_Click);
            // 
            // btDeleteObject
            // 
            this.btDeleteObject.Location = new System.Drawing.Point(600, 110);
            this.btDeleteObject.Name = "btDeleteObject";
            this.btDeleteObject.Size = new System.Drawing.Size(100, 23);
            this.btDeleteObject.TabIndex = 4;
            this.btDeleteObject.Text = "Удалить";
            this.btDeleteObject.UseVisualStyleBackColor = true;
            this.btDeleteObject.Click += new System.EventHandler(this.btDeleteObject_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miReference});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(730, 24);
            this.menuStrip.TabIndex = 5;
            // 
            // miReference
            // 
            this.miReference.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAboutProgram,
            this.miAboutDeveloper,
            this.miTask,
            this.miAdditionalTask});
            this.miReference.Name = "miReference";
            this.miReference.Size = new System.Drawing.Size(65, 20);
            this.miReference.Text = "Справка";
            // 
            // miAboutProgram
            // 
            this.miAboutProgram.Name = "miAboutProgram";
            this.miAboutProgram.Size = new System.Drawing.Size(214, 22);
            this.miAboutProgram.Text = "О программе";
            this.miAboutProgram.Click += new System.EventHandler(this.miAboutProgram_Click);
            // 
            // miAboutDeveloper
            // 
            this.miAboutDeveloper.Name = "miAboutDeveloper";
            this.miAboutDeveloper.Size = new System.Drawing.Size(214, 22);
            this.miAboutDeveloper.Text = "О разработчике";
            this.miAboutDeveloper.Click += new System.EventHandler(this.miAboutDeveloper_Click);
            // 
            // cmbObjects
            // 
            this.cmbObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjects.FormattingEnabled = true;
            this.cmbObjects.Location = new System.Drawing.Point(350, 50);
            this.cmbObjects.Name = "cmbObjects";
            this.cmbObjects.Size = new System.Drawing.Size(221, 21);
            this.cmbObjects.Sorted = true;
            this.cmbObjects.TabIndex = 7;
            // 
            // miTask
            // 
            this.miTask.Name = "miTask";
            this.miTask.Size = new System.Drawing.Size(214, 22);
            this.miTask.Text = "Задание";
            this.miTask.Click += new System.EventHandler(this.miTask_Click);
            // 
            // miAdditionalTask
            // 
            this.miAdditionalTask.Name = "miAdditionalTask";
            this.miAdditionalTask.Size = new System.Drawing.Size(214, 22);
            this.miAdditionalTask.Text = "Дополнительное задание";
            this.miAdditionalTask.Click += new System.EventHandler(this.miAdditionalTask_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 358);
            this.Controls.Add(this.cmbObjects);
            this.Controls.Add(this.btDeleteObject);
            this.Controls.Add(this.btReadObject);
            this.Controls.Add(this.btUpdateObject);
            this.Controls.Add(this.btAddObject);
            this.Controls.Add(this.cmbClasses);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ильиной Александры, группа 851001. ООТПиСП, Лабораторная работа №2.";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbClasses;
        private System.Windows.Forms.Button btAddObject;
        private System.Windows.Forms.Button btUpdateObject;
        private System.Windows.Forms.Button btReadObject;
        private System.Windows.Forms.Button btDeleteObject;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem miReference;
        private System.Windows.Forms.ToolStripMenuItem miAboutProgram;
        private System.Windows.Forms.ToolStripMenuItem miAboutDeveloper;
        private System.Windows.Forms.ComboBox cmbObjects;
        private System.Windows.Forms.ToolStripMenuItem miTask;
        private System.Windows.Forms.ToolStripMenuItem miAdditionalTask;
    }
}

