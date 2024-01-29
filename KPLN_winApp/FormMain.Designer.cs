using System.Windows.Forms;

namespace KPLN_winApp
{
    partial class FormMain
    {
        // Обязательная переменная конструктора.
        private System.ComponentModel.IContainer components = null;

        // Освободить все используемые ресурсы.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region

        // Требуемый метод для поддержки конструктора — не изменяйте содержимое этого метода с помощью редактора кода.
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chromiumWebBrowser = new CefSharp.WinForms.ChromiumWebBrowser();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.SuspendLayout();

            // FormMain
            this.Name = "FormMain";
            this.ControlBox = false;
            this.Controls.Add(this.chromiumWebBrowser);
            this.KeyPreview = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.Gainsboro;       
            this.Load += new System.EventHandler(this.FormMain_Load);
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);

            // chromiumWebBrowser
            resources.ApplyResources(this.chromiumWebBrowser, "chromiumWebBrowser");
            this.chromiumWebBrowser.Name = "chromiumWebBrowser";
            this.chromiumWebBrowser.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser.TabStop = false;
        }

        #endregion
    }
}

