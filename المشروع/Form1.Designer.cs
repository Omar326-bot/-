namespace المشروع
{
    partial class Form1
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpOperations = new System.Windows.Forms.GroupBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnDriveInfo = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCopyMove = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCreateFolder = new System.Windows.Forms.Button();
            this.btnCreateFile = new System.Windows.Forms.Button();
            this.grpFileDetails = new System.Windows.Forms.GroupBox();
            this.lblModified = new System.Windows.Forms.Label();
            this.lblCreated = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblExtension = new System.Windows.Forms.Label();
            this.lblFullPath = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.grpOperationsLog = new System.Windows.Forms.GroupBox();
            this.lblOperationCount = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.progressBarOperation = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlGraphics = new System.Windows.Forms.Panel();
            this.grpOperations.SuspendLayout();
            this.grpFileDetails.SuspendLayout();
            this.grpOperationsLog.SuspendLayout();
            this.pnlGraphics.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(316, 31);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(363, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "FILE & FOLDER MANAGER";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.BackColor = System.Drawing.Color.White;
            this.lblPath.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblPath.Location = new System.Drawing.Point(12, 104);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(37, 16);
            this.lblPath.TabIndex = 1;
            this.lblPath.Text = "Path:";
            this.lblPath.Click += new System.EventHandler(this.lblPath_Click);
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtPath.Location = new System.Drawing.Point(15, 101);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(500, 22);
            this.txtPath.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(510, 98);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(119, 25);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lvFiles
            // 
            this.lvFiles.AllowColumnReorder = true;
            this.lvFiles.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvFiles.ForeColor = System.Drawing.Color.White;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Location = new System.Drawing.Point(15, 150);
            this.lvFiles.MultiSelect = false;
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(650, 400);
            this.lvFiles.TabIndex = 4;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.SelectedIndexChanged += new System.EventHandler(this.lvFiles_SelectedIndexChanged);
            this.lvFiles.DoubleClick += new System.EventHandler(this.lvFiles_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 280;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Modified Date";
            this.columnHeader4.Width = 200;
            // 
            // grpOperations
            // 
            this.grpOperations.Controls.Add(this.btnWrite);
            this.grpOperations.Controls.Add(this.btnOpen);
            this.grpOperations.Controls.Add(this.btnDriveInfo);
            this.grpOperations.Controls.Add(this.btnSearch);
            this.grpOperations.Controls.Add(this.btnCopyMove);
            this.grpOperations.Controls.Add(this.btnRead);
            this.grpOperations.Controls.Add(this.btnDelete);
            this.grpOperations.Controls.Add(this.btnCreateFolder);
            this.grpOperations.Controls.Add(this.btnCreateFile);
            this.grpOperations.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOperations.Location = new System.Drawing.Point(681, 150);
            this.grpOperations.Name = "grpOperations";
            this.grpOperations.Size = new System.Drawing.Size(337, 400);
            this.grpOperations.TabIndex = 5;
            this.grpOperations.TabStop = false;
            this.grpOperations.Text = "Operations";
            // 
            // btnWrite
            // 
            this.btnWrite.BackColor = System.Drawing.Color.LightBlue;
            this.btnWrite.Location = new System.Drawing.Point(160, 191);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(177, 75);
            this.btnWrite.TabIndex = 4;
            this.btnWrite.Text = "Write File";
            this.btnWrite.UseVisualStyleBackColor = false;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.LightYellow;
            this.btnOpen.Location = new System.Drawing.Point(160, 262);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(177, 66);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnDriveInfo
            // 
            this.btnDriveInfo.BackColor = System.Drawing.Color.LightGray;
            this.btnDriveInfo.Location = new System.Drawing.Point(6, 323);
            this.btnDriveInfo.Name = "btnDriveInfo";
            this.btnDriveInfo.Size = new System.Drawing.Size(331, 71);
            this.btnDriveInfo.TabIndex = 8;
            this.btnDriveInfo.Text = "Drive Info";
            this.btnDriveInfo.UseVisualStyleBackColor = false;
            this.btnDriveInfo.Click += new System.EventHandler(this.btnDriveInfo_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Plum;
            this.btnSearch.Location = new System.Drawing.Point(6, 262);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(145, 66);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search Files";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCopyMove
            // 
            this.btnCopyMove.BackColor = System.Drawing.Color.LightCyan;
            this.btnCopyMove.Location = new System.Drawing.Point(6, 191);
            this.btnCopyMove.Name = "btnCopyMove";
            this.btnCopyMove.Size = new System.Drawing.Size(145, 75);
            this.btnCopyMove.TabIndex = 5;
            this.btnCopyMove.Text = "Copy / Move";
            this.btnCopyMove.UseVisualStyleBackColor = false;
            this.btnCopyMove.Click += new System.EventHandler(this.btnCopyMove_Click);
            // 
            // btnRead
            // 
            this.btnRead.BackColor = System.Drawing.Color.LightBlue;
            this.btnRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRead.Location = new System.Drawing.Point(6, 121);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(145, 75);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "Read File";
            this.btnRead.UseVisualStyleBackColor = false;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(160, 121);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(177, 75);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCreateFolder
            // 
            this.btnCreateFolder.BackColor = System.Drawing.Color.LightGreen;
            this.btnCreateFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateFolder.Location = new System.Drawing.Point(160, 48);
            this.btnCreateFolder.Name = "btnCreateFolder";
            this.btnCreateFolder.Size = new System.Drawing.Size(177, 75);
            this.btnCreateFolder.TabIndex = 1;
            this.btnCreateFolder.Text = "Create Folder";
            this.btnCreateFolder.UseVisualStyleBackColor = false;
            this.btnCreateFolder.Click += new System.EventHandler(this.btnCreateFolder_Click);
            // 
            // btnCreateFile
            // 
            this.btnCreateFile.BackColor = System.Drawing.Color.LightGreen;
            this.btnCreateFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateFile.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateFile.ForeColor = System.Drawing.Color.Black;
            this.btnCreateFile.Location = new System.Drawing.Point(6, 48);
            this.btnCreateFile.Name = "btnCreateFile";
            this.btnCreateFile.Size = new System.Drawing.Size(145, 71);
            this.btnCreateFile.TabIndex = 0;
            this.btnCreateFile.Text = "Create File";
            this.btnCreateFile.UseVisualStyleBackColor = false;
            this.btnCreateFile.Click += new System.EventHandler(this.btnCreateFile_Click);
            // 
            // grpFileDetails
            // 
            this.grpFileDetails.Controls.Add(this.lblModified);
            this.grpFileDetails.Controls.Add(this.lblCreated);
            this.grpFileDetails.Controls.Add(this.lblSize);
            this.grpFileDetails.Controls.Add(this.lblExtension);
            this.grpFileDetails.Controls.Add(this.lblFullPath);
            this.grpFileDetails.Controls.Add(this.lblName);
            this.grpFileDetails.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFileDetails.Location = new System.Drawing.Point(15, 588);
            this.grpFileDetails.Name = "grpFileDetails";
            this.grpFileDetails.Size = new System.Drawing.Size(573, 184);
            this.grpFileDetails.TabIndex = 6;
            this.grpFileDetails.TabStop = false;
            this.grpFileDetails.Text = "File Details";
            this.grpFileDetails.Enter += new System.EventHandler(this.grpFileDetails_Enter);
            // 
            // lblModified
            // 
            this.lblModified.AutoSize = true;
            this.lblModified.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModified.Location = new System.Drawing.Point(312, 120);
            this.lblModified.Name = "lblModified";
            this.lblModified.Size = new System.Drawing.Size(86, 20);
            this.lblModified.TabIndex = 5;
            this.lblModified.Text = "Modified: -";
            // 
            // lblCreated
            // 
            this.lblCreated.AutoSize = true;
            this.lblCreated.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreated.Location = new System.Drawing.Point(312, 75);
            this.lblCreated.Name = "lblCreated";
            this.lblCreated.Size = new System.Drawing.Size(77, 20);
            this.lblCreated.TabIndex = 4;
            this.lblCreated.Text = "Created: -";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(312, 36);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(50, 20);
            this.lblSize.TabIndex = 3;
            this.lblSize.Text = "Size: -";
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point(17, 117);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(103, 23);
            this.lblExtension.TabIndex = 2;
            this.lblExtension.Text = "Extension: -";
            // 
            // lblFullPath
            // 
            this.lblFullPath.AutoSize = true;
            this.lblFullPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullPath.Location = new System.Drawing.Point(17, 75);
            this.lblFullPath.Name = "lblFullPath";
            this.lblFullPath.Size = new System.Drawing.Size(84, 20);
            this.lblFullPath.TabIndex = 1;
            this.lblFullPath.Text = "Full Path: -";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(17, 36);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(167, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name: No file selected";
            // 
            // grpOperationsLog
            // 
            this.grpOperationsLog.Controls.Add(this.lblOperationCount);
            this.grpOperationsLog.Controls.Add(this.btnBack);
            this.grpOperationsLog.Controls.Add(this.btnClearLog);
            this.grpOperationsLog.Controls.Add(this.lstLog);
            this.grpOperationsLog.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOperationsLog.Location = new System.Drawing.Point(594, 588);
            this.grpOperationsLog.Name = "grpOperationsLog";
            this.grpOperationsLog.Size = new System.Drawing.Size(402, 184);
            this.grpOperationsLog.TabIndex = 8;
            this.grpOperationsLog.TabStop = false;
            this.grpOperationsLog.Text = "Operations Log";
            this.grpOperationsLog.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lblOperationCount
            // 
            this.lblOperationCount.AutoSize = true;
            this.lblOperationCount.Location = new System.Drawing.Point(2, 156);
            this.lblOperationCount.Name = "lblOperationCount";
            this.lblOperationCount.Size = new System.Drawing.Size(171, 23);
            this.lblOperationCount.TabIndex = 11;
            this.lblOperationCount.Text = "Operations Count: 0";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(312, 101);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(84, 55);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.BackColor = System.Drawing.Color.LightCyan;
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLog.Location = new System.Drawing.Point(312, 29);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(84, 66);
            this.btnClearLog.TabIndex = 1;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = false;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // lstLog
            // 
            this.lstLog.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.ItemHeight = 15;
            this.lstLog.Location = new System.Drawing.Point(6, 29);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(300, 124);
            this.lstLog.TabIndex = 0;
            // 
            // progressBarOperation
            // 
            this.progressBarOperation.Location = new System.Drawing.Point(15, 563);
            this.progressBarOperation.Name = "progressBarOperation";
            this.progressBarOperation.Size = new System.Drawing.Size(650, 16);
            this.progressBarOperation.TabIndex = 9;
            this.progressBarOperation.Click += new System.EventHandler(this.progressBarOperation_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(671, 563);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 16);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Ready";
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // pnlGraphics
            // 
            this.pnlGraphics.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlGraphics.Controls.Add(this.lblTitle);
            this.pnlGraphics.Location = new System.Drawing.Point(8, 4);
            this.pnlGraphics.Name = "pnlGraphics";
            this.pnlGraphics.Size = new System.Drawing.Size(1099, 91);
            this.pnlGraphics.TabIndex = 11;
            this.pnlGraphics.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGraphics_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1119, 784);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.progressBarOperation);
            this.Controls.Add(this.grpOperationsLog);
            this.Controls.Add(this.grpFileDetails);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.pnlGraphics);
            this.Controls.Add(this.grpOperations);
            this.ForeColor = System.Drawing.Color.Black;
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File & Folder Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpOperations.ResumeLayout(false);
            this.grpFileDetails.ResumeLayout(false);
            this.grpFileDetails.PerformLayout();
            this.grpOperationsLog.ResumeLayout(false);
            this.grpOperationsLog.PerformLayout();
            this.pnlGraphics.ResumeLayout(false);
            this.pnlGraphics.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox grpOperations;
        private System.Windows.Forms.Button btnCreateFile;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCreateFolder;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnCopyMove;
        private System.Windows.Forms.Button btnDriveInfo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox grpFileDetails;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblModified;
        private System.Windows.Forms.Label lblCreated;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.Label lblFullPath;
        private System.Windows.Forms.GroupBox grpOperationsLog;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ProgressBar progressBarOperation;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblOperationCount;
        private System.Windows.Forms.Panel pnlGraphics;
    }
}

