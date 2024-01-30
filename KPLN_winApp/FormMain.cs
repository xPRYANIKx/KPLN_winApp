using System;
using System.IO;
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


            // Локализация интерфейса на русский язык
            CefSettings settings = new CefSettings();
            settings.Locale = "ru";
            Cef.Initialize(settings);
            // Инициализация контекстного меню
            this.Controls.Add(chromiumWebBrowser);
            chromiumWebBrowser.MenuHandler = new CustomMenuHandler();

        }


        // Инициализация компонентов Chromium
        public ChromiumWebBrowser chromiumWebBrowser;
        private void chromiumWebBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e) {

        }

        // Контекстное меню в ChromiumWebBrowser
        public class CustomMenuHandler : IContextMenuHandler
        {
            public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
            {
                if (model.Count != 1 && !parameters.IsEditable)
                {
                    model.AddSeparator();
                    model.AddItem((CefMenuCommand)26505, "Закрыть приложение");
                }
            }
            public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
            {
                if (commandId == (CefMenuCommand)26505)
                {
                    Application.Exit();
                    return true;
                }
                return false;
            }
            public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
            {
            }

            public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
            {
                return false;
            }
        }




        private void FormMain_Load(object sender, EventArgs e)
        {
            // Получение имени пользователя, авторизованного в системе
            string currentWinUser = Environment.UserName;



            // Проверка наличия "currentuser.ini" (в случае отсутствия - создать)
            string internalUserFileUrl = "currentuser.ini";
            if (!File.Exists(internalUserFileUrl))
            {
                string fileUserContentAdd = $"{currentWinUser},0";
                File.WriteAllText(internalUserFileUrl, fileUserContentAdd);
            }



            // Получение данных из общего файла "userlist.ini"
            string mainUserFileUrl = "Z:\\Методист\\userlist.ini";
            try
            {                
                File.ReadAllLines(mainUserFileUrl);
            }
            catch (System.IO.FileNotFoundException)
            {
                Application.Exit();
            }
            catch (System.IO.IOException)
            {
                Application.Exit();
            }
            string[] usersFileContent = File.ReadAllLines(mainUserFileUrl);



            // Загрузка содержимого файла "currentuser.ini" и проверка его строк
            string userFileContent = File.ReadAllText(internalUserFileUrl);
            string[] userFileParts = userFileContent.Split(',');
            if (userFileParts.Length == 2)
            {
                string userFileUsername = userFileParts[0].Trim();
                string userFileAuthorized = userFileParts[1].Trim();


                // Обновление файла "currentuser.ini" из файла "userlist.ini"
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


                // Проверка выполнения условия совпадения строчек в "currentuser.ini" и в "userlist.ini"
                if (userFileUsername == currentWinUser && userFileAuthorized == "1")
                {
                    Application.Exit();
                }
            }



            // Проверка доступа на сайт и загрузка страницы
            bool webResponse;
            string url = "https://kpln-employees.ru/";
            try
            {
                using (var client = new WebClient())
                {
                    string response = client.DownloadString(url);
                    if (response.Contains("<span jsselect=\"heading\" jsvalues=\".innerHTML:msg\" jstcache=\"9\">Не удается получить доступ к сайту</span>") ||
                        response.Contains("Вшитая в код строчка"))
                    {
                        webResponse = false;
                    }
                    webResponse = true;
                }
            }
            catch (Exception)
            {
                webResponse = false;
            }
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
        }  
    }
}
