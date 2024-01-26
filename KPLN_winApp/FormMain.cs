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
        // Функция проверки доступа на сайт
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
        // 
        //
        //
        //
        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Проверка доступа на сайт и загрузка страницы
            string url = "https://kpln-employees.ru/";
            bool webResponse = CheckWebsiteAvailability(url);
            if (!webResponse)
            {
                Application.Exit();
            } else { chromiumWebBrowser.LoadUrl(url); }
            // Получение имени пользователя, авторизованного в системе
            string currentUser = Environment.UserName;
            // Загрузка содержимого файла "users.ini" и проверка егом строк
            string fileContent = File.ReadAllText("users.ini");
            string[] lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    string username = parts[0].Trim();
                    string authorized = parts[1].Trim();

                    if (username == currentUser && authorized == "1")
                    {
                        Application.Exit();
                    }
                }
            }
            // Отладка
            Console.WriteLine("Пользователь Windows: " + currentUser);
            Console.WriteLine("Доступ к внешнему ресурсу: " + webResponse);
        }
    }
}
