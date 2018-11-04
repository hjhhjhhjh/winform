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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Load("http://zwfw.sd.gov.cn");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CefSharp.WebBrowserExtensions.Back(browser);
            //browser.Back();
        }
    }
}
