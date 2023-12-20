using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace FileDownloaderApp
{
    public partial class MainForm : Form
    {
        private bool downloadsCompleted = false;
        private bool networkActivity = false;
        private CancellationTokenSource cancellationTokenSource;
        private List<string> downloadUrls = new List<string>();

        public MainForm()
        {
            InitializeComponent();

            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
        }

        private async void btnDownloadFiles_Click(object sender, EventArgs e)
        {
            Console.WriteLine("������ ������ '�������'");

            downloadsCompleted = false;
            cancellationTokenSource = new CancellationTokenSource();

            await StartDownloadsAsync(cancellationTokenSource.Token);

            while (!downloadsCompleted)
            {
                Console.WriteLine("�������� ���������� ��������...");

                await Task.Delay(1000);

                if (!networkActivity)
                {
                    Console.WriteLine("���������� ������� ����������, ���������� ��������");
                    break;
                }
            }

            ShutdownComputer();
        }

        private async Task StartDownloadsAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("������ ��������...");

            foreach (var item in listBoxFiles.Items)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("������ �������� �� ������� ������������");
                    break;
                }

                string url = item.ToString();
                try
                {
                    Console.WriteLine($"�������� �����: {url}");
                    await DownloadFileAsync(url, cancellationToken);
                    Console.WriteLine($"���� {url} ������� ��������");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine($"�������� ����� {url} ��������");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"������ �������� ����� {url}: {ex.Message}");
                }
            }

            downloadsCompleted = !cancellationToken.IsCancellationRequested;
        }

        private async Task DownloadFileAsync(string url, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine($"������ �������� ����� �� ������: {url}");

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url, cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"���� �� ������ {url} ������� ��������");

                        // ����� ������� ����������� ������
                        var contentLength = response.Content.Headers.ContentLength;
                        Console.WriteLine($"������ ����������� ������: {contentLength}");

                        byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                        string folderPath = @"C:\Users\1337w\OneDrive\Desktop\Downloads"; // ������� ���� � ����� ����� �����
                        string fileName = Path.GetFileName(url);
                        string filePath = Path.Combine(folderPath, fileName);

                        // ���������� ������������ ����� �� ���������� ����
                        File.WriteAllBytes(filePath, fileBytes);
                        Console.WriteLine($"���� {url} ������� �������� � �������� �� ����: {filePath}");
                    }
                    else
                    {
                        Console.WriteLine($"������ �������� ����� {url}. ������ ���: {response.StatusCode}");
                        throw new HttpRequestException($"HTTP ������ ���������� � �������: {response.StatusCode}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"�������� ����� {url} �������� �� ������� ������������");
                throw; // �������� � ��������� ����������� ����������
            }
            catch (Exception ex)
            {
                Console.WriteLine($"��������� ������ ��� �������� ����� {url}: {ex.Message}");
                throw; // �������� � ��������� ����������� ����������
            }
        }



        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            Console.WriteLine($"��������� ��������� ����. ����������� ����: {e.IsAvailable}");

            networkActivity = e.IsAvailable;

            if (!networkActivity && cancellationTokenSource != null)
            {
                Console.WriteLine("������� ���������� �������. ������ ��������");
                cancellationTokenSource.Cancel();
            }
        }

        private void ShutdownComputer()
        {
            if (downloadsCompleted)
            {
                MessageBox.Show("����������");
                //System.Diagnostics.Process.Start("shutdown", "/s /t 0");
            }
            else
            {
                Console.WriteLine("�������������� �������� � ������ �����. �������� �� ���������.");
                   //�������������� �������� � ������ �����
                Application.SetSuspendState(PowerState.Hibernate, false, false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string urlToAdd = textBoxURL.Text; // ��������� URL �� ���������� ����
            downloadUrls.Add(urlToAdd); // ���������� URL � ������ downloadUrls
            listBoxFiles.Items.Add(urlToAdd); // �����������: ���������� URL � ListBox ��� ����������� ������������
        }

        private void btnAddToQueue_Click(object sender, EventArgs e)
        {
            btnDownloadFiles.PerformClick();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                NetworkChange.NetworkAvailabilityChanged -= NetworkChange_NetworkAvailabilityChanged;
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
