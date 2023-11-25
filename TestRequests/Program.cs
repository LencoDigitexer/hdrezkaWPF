using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net;
using System.Security.Policy;
using System.IO;
using System.IO.Compression;
using HtmlAgilityPack;

namespace TestRequests
{
    internal class Program
    {
        static void Main()
        {
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
            var data = "q=drive";
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
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
