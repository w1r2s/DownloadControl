namespace FileDownloaderApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.labelCurrentFile = new System.Windows.Forms.Label();
            this.labelDownloadSpeed = new System.Windows.Forms.Label();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.textBoxDownloadSpeed = new System.Windows.Forms.TextBox();
            this.textBoxCurrentFile = new System.Windows.Forms.TextBox();
            this.labelEnterURL = new System.Windows.Forms.Label();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnDownloadFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelCurrentFile
            // 
            this.labelCurrentFile.AutoSize = true;
            this.labelCurrentFile.Location = new System.Drawing.Point(14, 27);
            this.labelCurrentFile.Name = "labelCurrentFile";
            this.labelCurrentFile.Size = new System.Drawing.Size(111, 20);
            this.labelCurrentFile.TabIndex = 0;
            this.labelCurrentFile.Text = "Текущий файл:";
            // 
            // labelDownloadSpeed
            // 
            this.labelDownloadSpeed.AutoSize = true;
            this.labelDownloadSpeed.Location = new System.Drawing.Point(14, 73);
            this.labelDownloadSpeed.Name = "labelDownloadSpeed";
            this.labelDownloadSpeed.Size = new System.Drawing.Size(221, 20);
            this.labelDownloadSpeed.TabIndex = 1;
            this.labelDownloadSpeed.Text = "Текущая скорость скачивания:";
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.ItemHeight = 20;
            this.listBoxFiles.Location = new System.Drawing.Point(17, 133);
            this.listBoxFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(477, 204);
            this.listBoxFiles.TabIndex = 2;
            // 
            // textBoxDownloadSpeed
            // 
            this.textBoxDownloadSpeed.Location = new System.Drawing.Point(241, 70);
            this.textBoxDownloadSpeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxDownloadSpeed.Name = "textBoxDownloadSpeed";
            this.textBoxDownloadSpeed.ReadOnly = true;
            this.textBoxDownloadSpeed.Size = new System.Drawing.Size(108, 27);
            this.textBoxDownloadSpeed.TabIndex = 3;
            // 
            // textBoxCurrentFile
            // 
            this.textBoxCurrentFile.Location = new System.Drawing.Point(131, 24);
            this.textBoxCurrentFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCurrentFile.Name = "textBoxCurrentFile";
            this.textBoxCurrentFile.ReadOnly = true;
            this.textBoxCurrentFile.Size = new System.Drawing.Size(181, 27);
            this.textBoxCurrentFile.TabIndex = 4;
            // 
            // labelEnterURL
            // 
            this.labelEnterURL.AutoSize = true;
            this.labelEnterURL.Location = new System.Drawing.Point(14, 104);
            this.labelEnterURL.Name = "labelEnterURL";
            this.labelEnterURL.Size = new System.Drawing.Size(76, 20);
            this.labelEnterURL.TabIndex = 5;
            this.labelEnterURL.Text = "Enter URL:";
            // 
            // textBoxURL
            // 
            this.textBoxURL.Location = new System.Drawing.Point(131, 101);
            this.textBoxURL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(300, 27);
            this.textBoxURL.TabIndex = 6;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(437, 99);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(194, 31);
            this.btnDownload.TabIndex = 7;
            this.btnDownload.Text = "Добавить в очередь";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnDownloadFiles
            // 
            this.btnDownloadFiles.Location = new System.Drawing.Point(525, 306);
            this.btnDownloadFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDownloadFiles.Name = "btnDownloadFiles";
            this.btnDownloadFiles.Size = new System.Drawing.Size(106, 31);
            this.btnDownloadFiles.TabIndex = 8;
            this.btnDownloadFiles.Text = "Скачать файлы";
            this.btnDownloadFiles.UseVisualStyleBackColor = true;
            this.btnDownloadFiles.Click += new System.EventHandler(this.btnDownloadFiles_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(700, 400); // Установка размера формы
            this.Controls.Add(this.btnDownloadFiles);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.labelEnterURL);
            this.Controls.Add(this.textBoxCurrentFile);
            this.Controls.Add(this.textBoxDownloadSpeed);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.labelDownloadSpeed);
            this.Controls.Add(this.labelCurrentFile);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "File Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Запрещает изменение размеров формы пользователем
        }

        private Label labelCurrentFile;
        private Label labelDownloadSpeed;
        private ListBox listBoxFiles;
        private TextBox textBoxDownloadSpeed;
        private TextBox textBoxCurrentFile;
        private Label labelEnterURL;
        private TextBox textBoxURL;
        private Button btnDownload;
        private Button btnDownloadFiles;
    }
}
