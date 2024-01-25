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


namespace KPLN_winApp
{
    public partial class FormMain : System.Windows.Forms.Form
    {

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Загрузка содержимого файла "users.ini"
            string fileContent = File.ReadAllText("users.ini");
            // Получение имени пользователя, авторизованного в системе
            string currentUser = Environment.UserName;
            // Проверка строк файла "users.ini"
            string[] lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    string username = parts[0].Trim();
                    bool authorized = bool.Parse(parts[1].Trim());
                    // Принудительное закрытие программы
                    if (username == currentUser && authorized)
                    {
                        Application.Exit();
                        return; // Скорее всего не понадобится
                    }
                }
            }
            // Загрузка страницы
            chromiumWebBrowser.LoadUrl("https://kpln-employees.ru/");
        }

        private void chromiumWebBrowser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {

        }

    }
}
