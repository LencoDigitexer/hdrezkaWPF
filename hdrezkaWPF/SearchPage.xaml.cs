using HtmlAgilityPack;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hdrezkaWPF
{
    /// <summary>
    /// Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {

        // Добавьте следующую строку, определяя коллекцию Tasks
        public ObservableCollection<Task> Tasks { get; set; } = new ObservableCollection<Task>();

        public SearchPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текст из TextBox
            string searchText = InputTextBox.Text;

            // ОТПРАВКА ЗАПРОСА И ПАРСНГ
            // ########################################

            // Установка параметров соединения
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 1000;
            ServicePointManager.UseNagleAlgorithm = false;

            var request = (HttpWebRequest)WebRequest.Create("https://hdrezka390bb2.org/engine/ajax/search.php");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            request.Headers.Add("X-App-Hdrezka-App", "1");
            request.Headers.Add("Cookie", "PHPSESSID=emantjccrvv7ss0l1luut6a4cd");

            // Добавляем данные
            var data = "q=" + searchText;
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(new GZipStream(responseStream, CompressionMode.Decompress)))
                {
                    string responseFromServer = reader.ReadToEnd();

                    // Парсинг HTML-страницы с использованием HtmlAgilityPack
                    var doc = new HtmlDocument();
                    doc.LoadHtml(responseFromServer);

                    // Создание массива данных
                    var dataArray = new List<Dictionary<string, string>>();
                    var itemList = doc.DocumentNode.SelectNodes("//ul[@class='b-search__section_list']/li/a");

                    foreach (var item in itemList)
                    {
                        var name = item.SelectSingleNode("span[@class='enty']").InnerText.Trim();
                        var url = item.GetAttributeValue("href", "").Trim();

                        var dataItem = new Dictionary<string, string>
                    {
                        { "name", name },
                        { "url", url }
                    };

                        dataArray.Add(dataItem);
                    }

                    // Вывод массива данных
                    foreach (var dataItem in dataArray)
                    {
                        Console.WriteLine($"Name: {dataItem["name"]}, URL: {dataItem["url"]}");
                        // Создаем новую задачу (замените этот блок кода на вашу логику создания задачи)
                        TaskType taskType = new TaskType { TypeName = dataItem["name"] };
                        User user = new User { Name = dataItem["url"] };
                        Status status = new Status { StatusName = dataItem["url"] };
                        Priority priority = new Priority { ProrityName = "Высокий" };
                        DateTime deadline = DateTime.Now.AddDays(7);

                        // Добавляем новую задачу в коллекцию Tasks
                        Tasks.Add(new Task(taskType, user, "Название задачи", status, priority, deadline));

                        // Обновляем привязку данных
                        tasksGrid.ItemsSource = Tasks;
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            // ########################################

            // Выводим текст в консоль (замените на свою логику)
            Console.WriteLine("Текст из TextBox: " + searchText);

            
        }


        private void filetr_TextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ListTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ViewButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
