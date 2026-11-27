using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace المشروع
{
    public partial class Form1 : Form
    {
        // العامل المسؤول عن البحث في الخلفية
        private BackgroundWorker searchWorker;
        private BackgroundWorker writeWorker;
        private BackgroundWorker readWorker;
        
        // العامل المسؤول عن النسخ والنقل في الخلفية
        private BackgroundWorker copyMoveWorker;
        private string currentPath = "";

        // ده القفل اللي هنستخدمه لحماية البيانات المشتركة
        private readonly object syncLock = new object();

        // عداد العمليات اللي اتسجلت
        private int operationsCount = 0;
        private void ReadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string filePath = (string)e.Argument;

            StringBuilder content = new StringBuilder();

            using (StreamReader reader = new StreamReader(filePath))
            {
                char[] buffer = new char[4096];

                long totalSize = reader.BaseStream.Length;
                long totalRead = 0;

                int bytesRead;

                while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    content.Append(buffer, 0, bytesRead);

                    totalRead = reader.BaseStream.Position;

                    if (totalSize > 0)
                    {
                        int percentage = (int)(totalRead * 100 / totalSize);

                        readWorker.ReportProgress(Math.Min(percentage, 100));
                    }
                }
            }

            e.Result = content.ToString();
        }

        private void ReadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message);
                return;
            }
            progressBarOperation.Value = 100;
            lblStatus.Text = "Completed";

            string content = (string)e.Result;

            if (content == "")
            {
                MessageBox.Show("The file is empty.");
            }
            else
            {
                MessageBox.Show(content, "File Content");
            }

            AddLog("File read successfully.");
        }
        private void ReadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarOperation.Value = e.ProgressPercentage;

            lblStatus.Text = "Reading: " + e.ProgressPercentage + "%";
        }
        // الدالة دي بتنفذ البحث في الخلفية
        private void SearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // نستقبل مسار المجلد وكلمة البحث
            object[] data = (object[])e.Argument;

            string folderPath = (string)data[0];
            string searchText = (string)data[1];

            // نجيب أسماء الملفات الموجودة في المجلد
            string[] files = Directory.GetFiles(folderPath);

            // هنحفظ مسارات الملفات اللي لقيناها
            List<string> results = new List<string>();

            int totalFiles = files.Length;

            // نلف على كل الملفات ونشوف هل الاسم مطابق للبحث
            for (int i = 0; i < totalFiles; i++)
            {
                FileInfo file = new FileInfo(files[i]);

                if (file.Name.IndexOf(
                    searchText,
                    StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(file.FullName);
                }

                // نحسب نسبة تقدم البحث
                int percentage = (int)((i + 1) * 100.0 / totalFiles);

                searchWorker.ReportProgress(percentage);
            }

            // نرجع النتائج للدالة اللي هتشتغل بعد انتهاء البحث
            e.Result = results;
        }


        // الدالة دي بتحدّث شريط التقدم أثناء البحث
        private void SearchWorker_ProgressChanged(
            object sender, ProgressChangedEventArgs e)
        {
            progressBarOperation.Value = e.ProgressPercentage;

            lblStatus.Text = "Searching: " + e.ProgressPercentage + "%";
        }


        // الدالة دي بتشتغل بعد انتهاء البحث
        private void SearchWorker_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // لو حصل خطأ أثناء البحث
            if (e.Error != null)
            {
                lblStatus.Text = "Failed";

                MessageBox.Show("Error: " + e.Error.Message);

                return;
            }

            // البحث انتهى
            progressBarOperation.Value = 100;
            lblStatus.Text = "Completed";

            // نستقبل النتائج
            List<string> results = (List<string>)e.Result;

            // نمسح النتائج القديمة من القائمة
            lvFiles.Items.Clear();

            // نعرض الملفات اللي لقيناها
            foreach (string filePath in results)
            {
                FileInfo file = new FileInfo(filePath);

                ListViewItem item = new ListViewItem(file.Name);

                item.SubItems.Add("File");
                item.SubItems.Add(FormatFileSize(file.Length));
                item.SubItems.Add(
                    file.LastWriteTime.ToString("dd/MM/yyyy"));

                lvFiles.Items.Add(item);
            }

            // نسجل العملية في سجل العمليات
            AddLog("Search completed. Results found: " + results.Count);

            // نعرض عدد النتائج
            MessageBox.Show(
                "Search completed. Results found: " + results.Count);
        }

            // الدالة دي بتنفذ النسخ أو النقل في الخلفية
        private void CopyMoveWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // نستقبل البيانات اللي جاية من زر Copy / Move
            object[] data = (object[])e.Argument;

            string sourcePath = (string)data[0];
            string destinationPath = (string)data[1];
            bool isMove = (bool)data[2];

            // نعرف حجم الملف
            long totalSize = new FileInfo(sourcePath).Length;

            // بنحدد حجم الجزء اللي هيتنقل كل مرة
            int bufferSize = 4096;

            byte[] buffer = new byte[bufferSize];

            long totalCopied = 0;

            // هنعرف هل بدأنا إنشاء الملف الجديد ولا لأ
            bool destinationCreated = false;

            try
            {
                // فتح الملف الأصلي للقراءة
                using (FileStream source = new FileStream(
                    sourcePath, FileMode.Open, FileAccess.Read))
                {
                    // إنشاء الملف الجديد للكتابة
                    using (FileStream destination = new FileStream(
                        destinationPath, FileMode.CreateNew, FileAccess.Write))
                    {
                        destinationCreated = true;

                        int bytesRead;

                        // نقرأ ونكتب الملف على أجزاء
                        while ((bytesRead = source.Read(
                            buffer, 0, buffer.Length)) > 0)
                        {
                            destination.Write(buffer, 0, bytesRead);

                            totalCopied += bytesRead;

                            // نحسب نسبة التقدم
                            if (totalSize > 0)
                            {
                                int percentage = (int)(
                                    totalCopied * 100.0 / totalSize);

                                copyMoveWorker.ReportProgress(
                                    Math.Min(percentage, 100));
                            }
                        }
                    }
                }

                // لو المستخدم اختار Move نمسح الملف الأصلي بعد النسخ
                if (isMove)
                {
                    File.Delete(sourcePath);
                }

                // نبعت نتيجة العملية بعد الانتهاء
                e.Result = new object[]
                {
            isMove,
            Path.GetFileName(sourcePath)
                };
            }
            catch
            {
                // لو العملية فشلت بعد إنشاء الملف الجديد
                // نحاول نمسح النسخة غير المكتملة
                if (destinationCreated && File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }

                throw;
            }

            // لو الملف فاضي، نعرض التقدم 100%
            if (totalSize == 0)
            {
                copyMoveWorker.ReportProgress(100);
            }
        }


        // الدالة دي بتحدّث شريط التقدم
        private void CopyMoveWorker_ProgressChanged(
            object sender, ProgressChangedEventArgs e)
        {
            progressBarOperation.Value = e.ProgressPercentage;

            lblStatus.Text = "Copy / Move: " +
                e.ProgressPercentage + "%";
        }


        // الدالة دي بتشتغل بعد انتهاء النسخ أو النقل
        private void CopyMoveWorker_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // لو حصل خطأ
            if (e.Error != null)
            {
                lblStatus.Text = "Failed";

                MessageBox.Show("Error: " + e.Error.Message);

                LoadDirectory(currentPath);

                return;
            }

            // العملية انتهت بنجاح
            progressBarOperation.Value = 100;
            lblStatus.Text = "Completed";

            // نستقبل نتيجة العملية
            object[] result = (object[])e.Result;

            bool isMove = (bool)result[0];
            string fileName = (string)result[1];

            // نسجل العملية في السجل
            if (isMove)
            {
                AddLog("File moved: " + fileName);

                MessageBox.Show("File moved successfully.");
            }
            else
            {
                AddLog("File copied: " + fileName);

                MessageBox.Show("File copied successfully.");
            }

            // نحدّث الملفات الموجودة في المجلد الحالي
            LoadDirectory(currentPath);
        }
        

        public Form1()
        {
            InitializeComponent();

            // Read Worker
            readWorker = new BackgroundWorker();

            readWorker.DoWork += ReadWorker_DoWork;
            readWorker.RunWorkerCompleted += ReadWorker_RunWorkerCompleted;
            readWorker.WorkerReportsProgress = true;
            readWorker.ProgressChanged += ReadWorker_ProgressChanged;

            // Write Worker
            writeWorker = new BackgroundWorker();

            writeWorker.WorkerReportsProgress = true;

            writeWorker.DoWork += WriteWorker_DoWork;
            writeWorker.ProgressChanged += WriteWorker_ProgressChanged;
            writeWorker.RunWorkerCompleted += WriteWorker_RunWorkerCompleted;

            // تجهيز العامل المسؤول عن البحث في الخلفية
            searchWorker = new BackgroundWorker();

            searchWorker.WorkerReportsProgress = true;

            searchWorker.DoWork += SearchWorker_DoWork;
            searchWorker.ProgressChanged += SearchWorker_ProgressChanged;
            searchWorker.RunWorkerCompleted += SearchWorker_RunWorkerCompleted;

            // تجهيز العامل المسؤول عن النسخ والنقل
            copyMoveWorker = new BackgroundWorker();

            copyMoveWorker.WorkerReportsProgress = true;

            copyMoveWorker.DoWork += CopyMoveWorker_DoWork;
            copyMoveWorker.ProgressChanged += CopyMoveWorker_ProgressChanged;
            copyMoveWorker.RunWorkerCompleted += CopyMoveWorker_RunWorkerCompleted;
        }
        private void WriteWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] data = (object[])e.Argument;

            string filePath = (string)data[0];
            string content = (string)data[1];

            byte[] bytes = Encoding.UTF8.GetBytes(content);

            using (FileStream stream = new FileStream(
                filePath, FileMode.Create, FileAccess.Write))
            {
                int totalBytes = bytes.Length;
                int totalWritten = 0;
                int chunkSize = 4096;

                while (totalWritten < totalBytes)
                {
                    int bytesToWrite = Math.Min(
                        chunkSize, totalBytes - totalWritten);

                    stream.Write(bytes, totalWritten, bytesToWrite);

                    totalWritten += bytesToWrite;

                    int percentage = (int)(
                        totalWritten * 100.0 / totalBytes);

                    writeWorker.ReportProgress(percentage);
                }
            }

            e.Result = filePath;
        }

        // تحديث شريط التقدم وحالة الكتابة أثناء تنفيذ العملية
        private void WriteWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarOperation.Value = e.ProgressPercentage;
            lblStatus.Text = "Writing: " + e.ProgressPercentage + "%";
        }

        // الدالة دي بتشتغل بعد انتهاء الكتابة، سواء نجحت أو حصل خطأ
        private void WriteWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lblStatus.Text = "Failed";
                MessageBox.Show("Error: " + e.Error.Message);
                return;
            }

            progressBarOperation.Value = 100;
            lblStatus.Text = "Completed";

            AddLog("File written successfully.");
            LoadDirectory(currentPath);

            MessageBox.Show("File written successfully.");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblPath_Click(object sender, EventArgs e)
        {

        }
        private void btnWrite_Click(object sender, EventArgs e)
        {
            // نتأكد إن المستخدم اختار ملفًا
            if (lvFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a file first.");
                return;
            }

            // نأخذ اسم الملف المحدد ونحدد مساره الكامل
            string selectedName = lvFiles.SelectedItems[0].Text;
            string selectedPath = Path.Combine(currentPath, selectedName);

            // نتأكد إن العنصر المحدد ملف موجود فعلًا
            if (!File.Exists(selectedPath))
            {
                MessageBox.Show("Please select a valid file.");
                return;
            }

            // نطلب المحتوى الجديد من المستخدم
            string content = ShowInputDialog(
                "Write File",
                "Enter the new file content:");

            // لو المستخدم ألغى أو ساب الخانة فاضية نخرج
            if (string.IsNullOrEmpty(content))
                return;

            // نطلب تأكيد استبدال المحتوى القديم
            DialogResult result = MessageBox.Show(
                "This will replace the old file content. Continue?",
                "Confirm Write",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            // نتأكد إن عامل الكتابة مش شغال بالفعل
            if (writeWorker.IsBusy)
            {
                MessageBox.Show("A write operation is already running.");
                return;
            }

            // نبدأ شريط التقدم ونوضح إن الكتابة بدأت
            progressBarOperation.Value = 0;
            lblStatus.Text = "Writing file...";

            // نبعت مسار الملف والمحتوى للعامل عشان يكتب في الخلفية
            writeWorker.RunWorkerAsync(new object[] { selectedPath, content });
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void grpFileDetails_Enter(object sender, EventArgs e)
        {

        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    currentPath = dialog.SelectedPath;
                    //حفظ المسار في متغير
                    txtPath.Text = dialog.SelectedPath;

                    LoadDirectory(dialog.SelectedPath);
                }
            }
        }
        // Function to load the directory contents into the ListView
        private void LoadDirectory(string path)
        {
            lvFiles.Items.Clear();

            try
            {
                DirectoryInfo directory = new DirectoryInfo(path);

                // Display folders
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(folder.Name);

                    item.SubItems.Add("File Folder");
                    item.SubItems.Add("-");
                    item.SubItems.Add(folder.LastWriteTime.ToString("dd/MM/yyyy HH:mm"));

                    lvFiles.Items.Add(item);
                }

                // Display files
                foreach (FileInfo file in directory.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name);

                    item.SubItems.Add(file.Extension);
                    item.SubItems.Add(FormatFileSize(file.Length));
                    item.SubItems.Add(file.LastWriteTime.ToString("dd/MM/yyyy HH:mm"));

                    lvFiles.Items.Add(item);
                }

                AddLog("Opened folder: " + path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        // Function to format file size in a human-readable format
        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024)
                return bytes + " B";

            if (bytes < 1024 * 1024)
                return (bytes / 1024.0).ToString("0.00") + " KB";

            if (bytes < 1024L * 1024 * 1024)
                return (bytes / (1024.0 * 1024)).ToString("0.00") + " MB";

            return (bytes / (1024.0 * 1024 * 1024)).ToString("0.00") + " GB";
        }
        // الدالة دي بتضيف رسالة للسجل وبتحدّث عداد العمليات
        private void AddLog(string message)
        {
            // نتأكد إننا بنحدّث عناصر الواجهة من Thread الواجهة
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => AddLog(message)));
                return;
            }

            // نحمي العداد والسجل باستخدام lock
            lock (syncLock)
            {
                // نزود عدد العمليات بواحد
                operationsCount++;

                // نجهز الرسالة ومعاها الوقت
                string logMessage =
                    "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + message;

                // نضيف الرسالة في سجل العمليات
                lstLog.Items.Add(logMessage);

                // نحدّث عدد العمليات المعروض
                lblOperationCount.Text =
                    "Operations Count: " + operationsCount;

                // نخلي آخر رسالة ظاهرة
                lstLog.TopIndex = lstLog.Items.Count - 1;
            }
        }
        private void lvFiles_DoubleClick(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 0)
                return;
            // بخرج من الدله لو مخترتش اي عنصر 
            string selectedName = lvFiles.SelectedItems[0].Text;
            // بجيب اسم العنصر اللي عاوزه 

            string selectedPath = Path.Combine(currentPath, selectedName);
            // دمج المسار الجديد مع المسار اللي موجود 
            if (Directory.Exists(selectedPath))
            // لو الاستاذ موجود فعلا 
            {
                currentPath = selectedPath;

                txtPath.Text = currentPath;

                LoadDirectory(currentPath);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(currentPath))
                return;

            DirectoryInfo parent = Directory.GetParent(currentPath);

            if (parent != null)
            {
                currentPath = parent.FullName;

                txtPath.Text = currentPath;

                LoadDirectory(currentPath);
            }
        }

        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 0)
                return;

            string selectedName = lvFiles.SelectedItems[0].Text;

            string selectedPath = Path.Combine(currentPath, selectedName);

            if (File.Exists(selectedPath))
            {
                FileInfo file = new FileInfo(selectedPath);

                lblName.Text = "Name: " + file.Name;

                lblFullPath.Text = "Full Path: " + file.FullName;

                lblExtension.Text = "Extension: " + file.Extension;

                lblSize.Text = "Size: " + FormatFileSize(file.Length);

                lblCreated.Text = "Created: " + file.CreationTime.ToString("dd/MM/yyyy");

                lblModified.Text = "Modified: " + file.LastWriteTime.ToString("dd/MM/yyyy");
            }
            else if (Directory.Exists(selectedPath))
            {
                DirectoryInfo folder = new DirectoryInfo(selectedPath);

                lblName.Text = "Name: " + folder.Name;

                lblFullPath.Text = "Full Path: " + folder.FullName;

                lblExtension.Text = "Extension: Folder";

                lblSize.Text = "Size: -";

                lblCreated.Text = "Created: " + folder.CreationTime.ToString("dd/MM/yyyy");

                lblModified.Text = "Modified: " + folder.LastWriteTime.ToString("dd/MM/yyyy");
            }
        }
        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            // نتأكد إن فيه مجلد مفتوح
            if (string.IsNullOrEmpty(currentPath) || !Directory.Exists(currentPath))
            {
                MessageBox.Show("Please open a folder first.");
                return;
            }

            // نطلب اسم الملف من المستخدم
            string fileName = ShowInputDialog(
                "Create File", "Enter file name with extension, e.g. Notes.txt");

            // لو المستخدم لم يكتب اسمًا
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            // نمنع كتابة مسار بدل اسم الملف
            if (Path.GetFileName(fileName) != fileName)
            {
                MessageBox.Show("Enter a file name only.");
                return;
            }

            // نحدد المسار الكامل للملف
            string filePath = Path.Combine(currentPath, fileName);

            try
            {
                // نتأكد إن الملف مش موجود بالفعل
                if (File.Exists(filePath))
                {
                    MessageBox.Show("This file already exists.");
                    return;
                }

                // ننشئ ملفًا فارغًا
                File.WriteAllText(filePath, "");

                // نسجل العملية
                AddLog("File created: " + fileName);

                // نحدّث قائمة الملفات
                LoadDirectory(currentPath);

                MessageBox.Show("File created successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            // نتأكد إن فيه مجلد مفتوح
            if (string.IsNullOrEmpty(currentPath) || !Directory.Exists(currentPath))
            {
                MessageBox.Show("Please open a folder first.");
                return;
            }

            // نطلب اسم المجلد
            string folderName = ShowInputDialog(
               "Create Folder", "Enter folder name:");

            // لو الاسم فارغ نخرج
            if (string.IsNullOrWhiteSpace(folderName))
                return;

            // نمنع كتابة مسار بدل اسم المجلد
            if (Path.GetFileName(folderName) != folderName)
            {
                MessageBox.Show("Enter a folder name only.");
                return;
            }

            // نحدد المسار الكامل للمجلد
            string folderPath = Path.Combine(currentPath, folderName);

            try
            {
                // نتأكد إن المجلد مش موجود بالفعل
                if (Directory.Exists(folderPath))
                {
                    MessageBox.Show("This folder already exists.");
                    return;
                }

                // ننشئ المجلد
                Directory.CreateDirectory(folderPath);

                // نسجل العملية
                AddLog("Folder created: " + folderName);

                // نحدّث قائمة الملفات
                LoadDirectory(currentPath);

                MessageBox.Show("Folder created successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private string ShowInputDialog(string title, string message)
        {
            using (Form prompt = new Form())
            {
                prompt.Text = title;
                prompt.Width = 400;
                prompt.Height = 170;
                prompt.StartPosition = FormStartPosition.CenterParent;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.MaximizeBox = false;
                prompt.MinimizeBox = false;

                Label lblMessage = new Label();
                lblMessage.Text = message;
                lblMessage.Left = 10;
                lblMessage.Top = 10;
                lblMessage.Width = 360;

                TextBox txtInput = new TextBox();
                txtInput.Left = 10;
                txtInput.Top = 40;
                txtInput.Width = 350;

                Button btnOK = new Button();
                btnOK.Text = "OK";
                btnOK.Left = 200;
                btnOK.Top = 80;
                btnOK.DialogResult = DialogResult.OK;

                Button btnCancel = new Button();
                btnCancel.Text = "Cancel";
                btnCancel.Left = 285;
                btnCancel.Top = 80;
                btnCancel.DialogResult = DialogResult.Cancel;

                prompt.Controls.Add(lblMessage);
                prompt.Controls.Add(txtInput);
                prompt.Controls.Add(btnOK);
                prompt.Controls.Add(btnCancel);

                prompt.AcceptButton = btnOK;
                prompt.CancelButton = btnCancel;

                if (prompt.ShowDialog(this) == DialogResult.OK)
                    return txtInput.Text;

                return "";

            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // نتأكد إن فيه عنصر محدد
            if (lvFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a file or folder first.");
                return;
            }

            // نأخذ اسم العنصر المحدد
            string selectedName = lvFiles.SelectedItems[0].Text;

            // نحدد المسار الكامل للعنصر
            string selectedPath = Path.Combine(currentPath, selectedName);

            try
            {
                // لو العنصر ملف
                if (File.Exists(selectedPath))
                {
                    DialogResult result = MessageBox.Show(
                        "Are you sure you want to delete this file?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        File.Delete(selectedPath);

                        AddLog("File deleted: " + selectedName);

                        MessageBox.Show("File deleted successfully.");
                    }
                    else
                    {
                        return;
                    }
                }
                // لو العنصر مجلد
                else if (Directory.Exists(selectedPath))
                {
                    DialogResult result = MessageBox.Show(
                        "This will delete the folder and ALL its contents. Are you sure?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        Directory.Delete(selectedPath, true);

                        AddLog("Folder deleted: " + selectedName);

                        MessageBox.Show("Folder deleted successfully.");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("The selected item no longer exists.");
                    return;
                }

                // تحديث قائمة الملفات
                LoadDirectory(currentPath);

                // مسح تفاصيل العنصر السابق
                lblName.Text = "Name:";
                lblFullPath.Text = "Full Path:";
                lblExtension.Text = "Extension:";
                lblSize.Text = "Size:";
                lblCreated.Text = "Created:";
                lblModified.Text = "Modified:";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a file.");
                return;
            }

            string selectedName = lvFiles.SelectedItems[0].Text;
            string selectedPath = Path.Combine(currentPath, selectedName);

            if (!File.Exists(selectedPath))
            {
                MessageBox.Show("Please select a valid file.");
                return;
            }

            if (readWorker.IsBusy)
            {
                MessageBox.Show("A read operation is already running.");
                return;
            }

            progressBarOperation.Value = 0;
            lblStatus.Text = "Reading file...";

            readWorker.RunWorkerAsync(selectedPath);
        }

        private void btnCopyMove_Click(object sender, EventArgs e)
        {
            // نتأكد إن المستخدم اختار ملفًا
            if (lvFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a file first.");
                return;
            }

            // نحدد اسم الملف ومساره
            string selectedName = lvFiles.SelectedItems[0].Text;

            string sourcePath = Path.Combine(currentPath, selectedName);

            // نتأكد إن العنصر ملف وليس مجلدًا
            if (!File.Exists(sourcePath))
            {
                MessageBox.Show("Please select a valid file.");
                return;
            }

            // نتأكد إن العامل مشغولش بعملية تانية
            if (copyMoveWorker.IsBusy)
            {
                MessageBox.Show("A Copy / Move operation is already running.");
                return;
            }

            // نحدد هل المستخدم يريد Copy أم Move
            DialogResult choice = MessageBox.Show(
                "Choose Yes to Copy or No to Move.",
                "Copy or Move",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (choice == DialogResult.Cancel)
                return;

            // نحدد هل العملية نسخ أم نقل
            bool isMove = (choice == DialogResult.No);

            // نختار المجلد المستهدف
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select destination folder";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                // نحدد المسار الجديد للملف
                string destinationPath = Path.Combine(
                    dialog.SelectedPath, selectedName);

                // نمنع الكتابة فوق ملف موجود
                if (File.Exists(destinationPath))
                {
                    MessageBox.Show(
                        "A file with the same name already exists in the destination folder.");

                    return;
                }

                // نبدأ شريط التقدم
                progressBarOperation.Value = 0;

                // نحدّث حالة البرنامج
                lblStatus.Text = isMove
                    ? "Moving file..."
                    : "Copying file...";

                // نبدأ العملية في الخلفية
                copyMoveWorker.RunWorkerAsync(
                    new object[]
                    {
                sourcePath,
                destinationPath,
                isMove
                    });
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // نتأكد إن المستخدم اختار عنصرًا
            if (lvFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a file or folder first.");
                return;
            }

            // نأخذ اسم العنصر المحدد
            string selectedName = lvFiles.SelectedItems[0].Text;

            // نحدد المسار الكامل
            string selectedPath = Path.Combine(currentPath, selectedName);

            try
            {
                // نتأكد إن الملف أو المجلد موجود
                if (!File.Exists(selectedPath) &&
                    !Directory.Exists(selectedPath))
                {
                    MessageBox.Show("The selected item does not exist.");
                    return;
                }

                // فتح الملف أو المجلد باستخدام Windows
                Process.Start(new ProcessStartInfo
                {
                    FileName = selectedPath,
                    UseShellExecute = true
                });

                // تسجيل العملية
                AddLog("Opened: " + selectedName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // الزر ده بيبدأ البحث عن الملفات في الخلفية
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // نتأكد إن فيه مجلد مفتوح
            if (string.IsNullOrEmpty(currentPath) ||
                !Directory.Exists(currentPath))
            {
                MessageBox.Show("Please open a folder first.");
                return;
            }

            // نطلب اسم الملف أو جزءًا منه
            string searchText = ShowInputDialog(
                "Search Files",
                "Enter file name or part of the name:");

            // لو المستخدم مدخلش كلمة بحث نخرج
            if (string.IsNullOrWhiteSpace(searchText))
                return;

            // نتأكد إن عملية بحث تانية مش شغالة
            if (searchWorker.IsBusy)
            {
                MessageBox.Show("A search operation is already running.");
                return;
            }

            // نبدأ شريط التقدم
            progressBarOperation.Value = 0;

            // نحدّث حالة البرنامج
            lblStatus.Text = "Searching...";

            // نشغّل البحث في الخلفية ونبعت له البيانات
            searchWorker.RunWorkerAsync(
                new object[] { currentPath, searchText });
        }

        private void btnDriveInfo_Click(object sender, EventArgs e)
        {
            try
            {
                // الحصول على جميع الأقراص
                DriveInfo[] drives = DriveInfo.GetDrives();

                // متغير لتجميع المعلومات
                string information = "";

                foreach (DriveInfo drive in drives)
                {
                    information += "Drive: " + drive.Name + "\n";
                    information += "Type: " + drive.DriveType + "\n";

                    // نتأكد إن القرص جاهز للاستخدام
                    if (drive.IsReady)
                    {
                        information += "Format: " + drive.DriveFormat + "\n";

                        information += "Total Size: " +
                            FormatFileSize(drive.TotalSize) + "\n";

                        information += "Free Space: " +
                            FormatFileSize(drive.AvailableFreeSpace) + "\n";
                    }
                    else
                    {
                        information += "Drive is not ready.\n";
                    }

                    information += "----------------------\n";
                }

                // عرض المعلومات
                MessageBox.Show(
                    information,
                    "Drive Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // تسجيل العملية
                AddLog("Drive information displayed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            // نتأكد إن السجل يحتوي على رسائل
            if (lstLog.Items.Count == 0)
            {
                MessageBox.Show("The log is already empty.");
                return;
            }

            // نطلب تأكيد المستخدم
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear all log messages?",
                "Clear Log",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // مسح جميع الرسائل
                lstLog.Items.Clear();

                MessageBox.Show("Log cleared successfully.");
            }
        }

        private void progressBarOperation_Click(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void pnlGraphics_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // لون خلفية مكان الرسم
            g.Clear(Color.WhiteSmoke);

            // فرشاة لرسم جسم الفولدر
            using (SolidBrush folderBrush = new SolidBrush(Color.Goldenrod))
            using (Pen folderPen = new Pen(Color.DarkGoldenrod, 2))
            {
                // رسم جسم الفولدر
                g.FillRectangle(folderBrush, 20, 45, 130, 60);

                // رسم الجزء العلوي للفولدر
                g.FillRectangle(folderBrush, 20, 30, 50, 20);

                // رسم حدود الفولدر
                g.DrawRectangle(folderPen, 20, 45, 130, 60);
                g.DrawRectangle(folderPen, 20, 30, 50, 20);
            }

            // كتابة اسم البرنامج
            using (Font titleFont = new Font("Segoe UI", 11, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(Color.DarkSlateGray))
            {
                g.DrawString(
                    "OS File Manager",
                    titleFont,
                    textBrush,
                    20,
                    115
                );
            }
        }
    }
}