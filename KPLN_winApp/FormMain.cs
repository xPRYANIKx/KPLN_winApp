using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace KPLN_winApp
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        // Инициализация компонентов Chromium 
        private void chromiumWebBrowser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
        }


        // Функция проверки работоспособности сайта (наличие интернета)
        public static bool CheckWebsiteAvailability(string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    string response = client.DownloadString(url);
                    if (response.Contains("Строчка вшитая в HTML, еслии страница не может прогрузиться"))
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Получение имени пользователя, авторизованного в системе
            string currentUser = Environment.UserName;


            // Проверка наличия "user.ini" (в случае отсутствия - создать)
            string filePathU = "user.ini";
            if (!File.Exists(filePathU))
            {
                string fileContentAdd = $"{currentUser},0";
                File.WriteAllText(filePathU, fileContentAdd);
            }


            // (!)
            // Получение данных из общего файла "userDB.ini" и обновление файла "user.ini"


            // Загрузка содержимого файла "user.ini" и проверка его строк
            string fileContent = File.ReadAllText(filePathU);
            string[] parts = fileContent.Split(',');
            if (parts.Length == 2)
            {
                string username = parts[0].Trim();
                string authorized = parts[1].Trim();

                if (username == currentUser && authorized == "1")
                {
                    Application.Exit();
                }
            }


            // Проверка доступа на сайт и загрузка страницы
            string url = "https://kpln-employees.ru/";
            bool webResponse = CheckWebsiteAvailability(url);
            if (!webResponse)
            {
                Application.Exit();
            }
            else { chromiumWebBrowser.LoadUrl(url); }


            // (!)
            // Функция обработчик событий: данные заполнены - в HTML добавляется строчка: отлавливаем;
            // В "user.ini" '0' меняется на '1'; обновление данных в "userDB.ini";
            // Application.Exit();


            // Отладка
            Console.WriteLine("Пользователь Windows: " + currentUser);
            Console.WriteLine("Доступ к внешнему ресурсу: " + webResponse);
        }
    }
}
