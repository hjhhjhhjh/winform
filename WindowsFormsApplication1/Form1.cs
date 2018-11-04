using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using Update.Controls;
using Update.Net;
using CefSharp.WinForms.Internals;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private ChromiumWebBrowser browser;

        //是否全屏
        bool isFullScreen = Boolean.Parse(System.Configuration.ConfigurationSettings.AppSettings["isFullScreen"]);

        string homePageUrl = System.Configuration.ConfigurationSettings.AppSettings["homeUrl"];

        public Form1()
        {

            InitializeComponent();
            CefSharp.Cef.Initialize();

            browser = new ChromiumWebBrowser("http://www.baidu.com");
            browser.Dock = DockStyle.Fill;
            //browser.Load("http://www.baidu.com");

            browser.CreateControl();

            browser.LifeSpanHandler = new OpenPageSelf();

            this.panel1.Controls.Add(browser);

            browser.JavascriptObjectRepository.Register("showKeyBoard", new KeyBoard(),true, new CefSharp.BindingOptions { CamelCaseJavascriptNames = false });

            if (isFullScreen)
            {
                FullScreen();
            }
        }

        private void ShowKeyBoard()
        {

        }

        private void FullScreen()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Load(homePageUrl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CefSharp.WebBrowserExtensions.Back(browser);
            //browser.Back();
        }
    }
}
