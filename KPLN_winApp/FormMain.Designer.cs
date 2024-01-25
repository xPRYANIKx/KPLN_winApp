using System.Windows.Forms;

namespace KPLN_winApp
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.chromiumWebBrowser = new CefSharp.WinForms.ChromiumWebBrowser();
            this.SuspendLayout();
            // 
            // chromiumWebBrowser
            // 
            this.chromiumWebBrowser.ActivateBrowserOnCreation = false;
            resources.ApplyResources(this.chromiumWebBrowser, "chromiumWebBrowser");
            this.chromiumWebBrowser.Name = "chromiumWebBrowser";
            this.chromiumWebBrowser.TabStop = false;
            this.chromiumWebBrowser.LoadingStateChanged += new System.EventHandler<CefSharp.LoadingStateChangedEventArgs>(this.chromiumWebBrowser_LoadingStateChanged);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.chromiumWebBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
 
        }

        #endregion

        public CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser;
    }
}

