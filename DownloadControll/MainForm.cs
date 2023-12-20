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
            Console.WriteLine("Нажата кнопка 'Скачать'");

            downloadsCompleted = false;
            cancellationTokenSource = new CancellationTokenSource();

            await StartDownloadsAsync(cancellationTokenSource.Token);

            while (!downloadsCompleted)
            {
                Console.WriteLine("Ожидание завершения загрузки...");

                await Task.Delay(1000);

                if (!networkActivity)
                {
                    Console.WriteLine("Отсутствие сетевой активности, прерывание загрузки");
                    break;
                }
            }

            ShutdownComputer();
        }

        private async Task StartDownloadsAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Начало загрузки...");

            foreach (var item in listBoxFiles.Items)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Отмена загрузки по запросу пользователя");
                    break;
                }

                string url = item.ToString();
                try
                {
                    Console.WriteLine($"Загрузка файла: {url}");
                    await DownloadFileAsync(url, cancellationToken);
                    Console.WriteLine($"Файл {url} успешно загружен");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine($"Загрузка файла {url} отменена");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки файла {url}: {ex.Message}");
                }
            }

            downloadsCompleted = !cancellationToken.IsCancellationRequested;
        }

        private async Task DownloadFileAsync(string url, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine($"Начало загрузки файла по адресу: {url}");

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url, cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Файл по адресу {url} успешно загружен");

                        // Вывод размера загруженных данных
                        var contentLength = response.Content.Headers.ContentLength;
                        Console.WriteLine($"Размер загруженных данных: {contentLength}");

                        byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                        string folderPath = @"C:\Users\1337w\OneDrive\Desktop\Downloads"; // Укажите путь к вашей папке здесь
                        string fileName = Path.GetFileName(url);
                        string filePath = Path.Combine(folderPath, fileName);

                        // Сохранение загруженного файла по указанному пути
                        File.WriteAllBytes(filePath, fileBytes);
                        Console.WriteLine($"Файл {url} успешно загружен и сохранен по пути: {filePath}");
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка загрузки файла {url}. Статус код: {response.StatusCode}");
                        throw new HttpRequestException($"HTTP запрос завершился с ошибкой: {response.StatusCode}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Загрузка файла {url} отменена по запросу пользователя");
                throw; // Перехват и повторное возбуждение исключения
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при загрузке файла {url}: {ex.Message}");
                throw; // Перехват и повторное возбуждение исключения
            }
        }



        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            Console.WriteLine($"Изменение состояния сети. Доступность сети: {e.IsAvailable}");

            networkActivity = e.IsAvailable;

            if (!networkActivity && cancellationTokenSource != null)
            {
                Console.WriteLine("Сетевая активность пропала. Отмена загрузки");
                cancellationTokenSource.Cancel();
            }
        }

        private void ShutdownComputer()
        {
            if (downloadsCompleted)
            {
                MessageBox.Show("Выключение");
                //System.Diagnostics.Process.Start("shutdown", "/s /t 0");
            }
            else
            {
                Console.WriteLine("Предотвращение перехода в спящий режим. Загрузка не завершена.");
                   //предотвращение перехода в спящий режим
                Application.SetSuspendState(PowerState.Hibernate, false, false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string urlToAdd = textBoxURL.Text; // Получение URL из текстового поля
            downloadUrls.Add(urlToAdd); // Добавление URL в список downloadUrls
            listBoxFiles.Items.Add(urlToAdd); // Опционально: добавление URL в ListBox для отображения пользователю
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
