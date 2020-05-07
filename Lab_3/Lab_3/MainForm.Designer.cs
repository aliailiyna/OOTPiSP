namespace Lab_3
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
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miSeparatorOne = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miReference = new System.Windows.Forms.ToolStripMenuItem();
            this.miAboutProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.miSeparatorTwo = new System.Windows.Forms.ToolStripSeparator();
            this.miAboutDeveloper = new System.Windows.Forms.ToolStripMenuItem();
            this.miTask = new System.Windows.Forms.ToolStripMenuItem();
            this.miTaskTwo = new System.Windows.Forms.ToolStripMenuItem();
            this.miAdditionalTaskTwo = new System.Windows.Forms.ToolStripMenuItem();
            this.miSeparatorThree = new System.Windows.Forms.ToolStripSeparator();
            this.miTaskThree = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbObjects = new System.Windows.Forms.ComboBox();
            this.btClear = new System.Windows.Forms.Button();
            this.lblFullObjectsList = new System.Windows.Forms.Label();
            this.lbFullObjectsList = new System.Windows.Forms.ListBox();
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
            this.miFile,
            this.miReference,
            this.miTask});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(730, 24);
            this.menuStrip.TabIndex = 5;
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFileOpen,
            this.miFileSave,
            this.miSeparatorOne,
            this.miExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(48, 20);
            this.miFile.Text = "Файл";
            // 
            // miFileOpen
            // 
            this.miFileOpen.Name = "miFileOpen";
            this.miFileOpen.Size = new System.Drawing.Size(141, 22);
            this.miFileOpen.Text = "Открыть...";
            this.miFileOpen.Click += new System.EventHandler(this.miFileOpen_Click);
            // 
            // miFileSave
            // 
            this.miFileSave.Name = "miFileSave";
            this.miFileSave.Size = new System.Drawing.Size(141, 22);
            this.miFileSave.Text = "Сохранить...";
            this.miFileSave.Click += new System.EventHandler(this.miFileSave_Click);
            // 
            // miSeparatorOne
            // 
            this.miSeparatorOne.Name = "miSeparatorOne";
            this.miSeparatorOne.Size = new System.Drawing.Size(138, 6);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(141, 22);
            this.miExit.Text = "Выход";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miReference
            // 
            this.miReference.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAboutProgram,
            this.miSeparatorTwo,
            this.miAboutDeveloper});
            this.miReference.Name = "miReference";
            this.miReference.Size = new System.Drawing.Size(65, 20);
            this.miReference.Text = "Справка";
            // 
            // miAboutProgram
            // 
            this.miAboutProgram.Name = "miAboutProgram";
            this.miAboutProgram.Size = new System.Drawing.Size(162, 22);
            this.miAboutProgram.Text = "О программе";
            this.miAboutProgram.Click += new System.EventHandler(this.miAboutProgram_Click);
            // 
            // miSeparatorTwo
            // 
            this.miSeparatorTwo.Name = "miSeparatorTwo";
            this.miSeparatorTwo.Size = new System.Drawing.Size(159, 6);
            // 
            // miAboutDeveloper
            // 
            this.miAboutDeveloper.Name = "miAboutDeveloper";
            this.miAboutDeveloper.Size = new System.Drawing.Size(162, 22);
            this.miAboutDeveloper.Text = "О разработчике";
            this.miAboutDeveloper.Click += new System.EventHandler(this.miAboutDeveloper_Click);
            // 
            // miTask
            // 
            this.miTask.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTaskTwo,
            this.miAdditionalTaskTwo,
            this.miSeparatorThree,
            this.miTaskThree});
            this.miTask.Name = "miTask";
            this.miTask.Size = new System.Drawing.Size(64, 20);
            this.miTask.Text = "Задания";
            // 
            // miTaskTwo
            // 
            this.miTaskTwo.Name = "miTaskTwo";
            this.miTaskTwo.Size = new System.Drawing.Size(223, 22);
            this.miTaskTwo.Text = "Задание 2";
            this.miTaskTwo.Click += new System.EventHandler(this.miTaskTwo_Click);
            // 
            // miAdditionalTaskTwo
            // 
            this.miAdditionalTaskTwo.Name = "miAdditionalTaskTwo";
            this.miAdditionalTaskTwo.Size = new System.Drawing.Size(223, 22);
            this.miAdditionalTaskTwo.Text = "Дополнительное задание 2";
            this.miAdditionalTaskTwo.Click += new System.EventHandler(this.miAdditionalTaskTwo_Click);
            // 
            // miSeparatorThree
            // 
            this.miSeparatorThree.Name = "miSeparatorThree";
            this.miSeparatorThree.Size = new System.Drawing.Size(220, 6);
            // 
            // miTaskThree
            // 
            this.miTaskThree.Name = "miTaskThree";
            this.miTaskThree.Size = new System.Drawing.Size(223, 22);
            this.miTaskThree.Text = "Задание 3";
            this.miTaskThree.Click += new System.EventHandler(this.miTaskThree_Click);
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
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(600, 139);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(100, 23);
            this.btClear.TabIndex = 10;
            this.btClear.Text = "Очистить";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // lblFullObjectsList
            // 
            this.lblFullObjectsList.Location = new System.Drawing.Point(22, 215);
            this.lblFullObjectsList.Name = "lblFullObjectsList";
            this.lblFullObjectsList.Size = new System.Drawing.Size(675, 23);
            this.lblFullObjectsList.TabIndex = 12;
            this.lblFullObjectsList.Text = "Полный список объектов";
            this.lblFullObjectsList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFullObjectsList
            // 
            this.lbFullObjectsList.FormattingEnabled = true;
            this.lbFullObjectsList.Location = new System.Drawing.Point(25, 241);
            this.lbFullObjectsList.Name = "lbFullObjectsList";
            this.lbFullObjectsList.Size = new System.Drawing.Size(675, 264);
            this.lbFullObjectsList.Sorted = true;
            this.lbFullObjectsList.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 558);
            this.Controls.Add(this.lbFullObjectsList);
            this.Controls.Add(this.lblFullObjectsList);
            this.Controls.Add(this.btClear);
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
            this.Text = "Ильиной Александры, группа 851001. ООТПиСП, Лабораторная работа №3.";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
        private System.Windows.Forms.ToolStripMenuItem miTaskTwo;
        private System.Windows.Forms.ToolStripMenuItem miAdditionalTaskTwo;
        private System.Windows.Forms.ToolStripMenuItem miTaskThree;
        private System.Windows.Forms.ToolStripSeparator miSeparatorTwo;
        private System.Windows.Forms.ToolStripSeparator miSeparatorThree;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Label lblFullObjectsList;
        private System.Windows.Forms.ListBox lbFullObjectsList;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miFileOpen;
        private System.Windows.Forms.ToolStripMenuItem miFileSave;
        private System.Windows.Forms.ToolStripSeparator miSeparatorOne;
        private System.Windows.Forms.ToolStripMenuItem miExit;
    }
}

