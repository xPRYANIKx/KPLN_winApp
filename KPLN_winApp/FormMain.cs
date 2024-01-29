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
using CefSharp;
using CefSharp.WinForms;


namespace KPLN_winApp
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        public FormMain()
        {
            InitializeComponent();
        }


        // Инициализация компонентов Chromium
        public ChromiumWebBrowser chromiumWebBrowser;


        // Функция проверки работоспособности сайта (наличие интернета)
        public static bool CheckWebsiteAvailability(string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    string response = client.DownloadString(url);
                    if (response.Contains("<span jsselect=\"heading\" jsvalues=\".innerHTML:msg\" jstcache=\"9\">Не удается получить доступ к сайту</span>") || 
                        response.Contains("Вшитая в код строчка")) {
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


        private void FormMain_Load(object sender, EventArgs e)
        {
            // Получение имени пользователя, авторизованного в системе
            string currentWinUser = Environment.UserName;

            // Проверка наличия "user.ini" (в случае отсутствия - создать)
            string internalUserFileUrl = "user.ini";
            if (!File.Exists(internalUserFileUrl))
            {
                string fileUserContentAdd = $"{currentWinUser},0";
                File.WriteAllText(internalUserFileUrl, fileUserContentAdd);
            }

            // Загрузка содержимого файла "user.ini" и проверка его строк
            string userFileContent = File.ReadAllText(internalUserFileUrl);
            string[] userFileParts = userFileContent.Split(',');
            if (userFileParts.Length == 2)
            {
                string userFileUsername = userFileParts[0].Trim();
                string userFileAuthorized = userFileParts[1].Trim();

                // Получение данных из общего файла "users.ini"
                string mainUserFileUrl = "Z:\\Методист\\users.ini"; 
                string[] usersFileContent = File.ReadAllLines(mainUserFileUrl);

                // Обновление файла "user.ini"
                for (int i = 0; i < usersFileContent.Length; i++)
                {
                    string[] usersFileParts = usersFileContent[i].Split(',');
                    if (usersFileParts[0] == userFileUsername)
                    {
                        userFileAuthorized = usersFileParts[1];
                        string tempInternaArr = userFileUsername + ',' + userFileAuthorized;
                        File.WriteAllText(internalUserFileUrl, tempInternaArr);
                        break;
                    }
                }

                // Проверка выполнения условия и выключение
                if (userFileUsername == currentWinUser && userFileAuthorized == "1")
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
            else {;
                chromiumWebBrowser.LoadUrl(url);
            }


            // Функция обработчик событий: данные заполнены - в HTML добавляется строчка: отлавливаем;
            // В "user.ini" '0' меняется на '1'; обновление данных в "userDB.ini";
            // Application.Exit();


            // Отладка
            Console.WriteLine("Пользователь Windows: " + currentWinUser);
            Console.WriteLine("Доступ к внешнему ресурсу: " + webResponse);
        }
    }
}
