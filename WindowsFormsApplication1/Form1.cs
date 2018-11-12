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
using CefSharp.WinForms.Internals;
using CefSharp;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //log4net.ILog log = log4net.LogManager.GetLogger("Form1");

        private ChromiumWebBrowser browser;

        //是否全屏
        bool isFullScreen = Boolean.Parse(System.Configuration.ConfigurationSettings.AppSettings["isFullScreen"]);

        string homePageUrl = System.Configuration.ConfigurationSettings.AppSettings["homeUrl"];

        public Form1()
        {
            //log.Info("form1初始化");
            Log4Helper.Info(this.GetType(), "form1初始化");

            InitializeComponent();

            ///设置
            var setting = new CefSharp.CefSettings();
            setting.Locale = "zh-CN";
            setting.CachePath = "CHBrowser/BrowserCache";//缓存路径
            setting.AcceptLanguageList = "zh-CN,zh;q=0.8";//浏览器引擎的语言
            setting.LocalesDirPath = "CHBrowser/localeDir";//日志
            setting.LogFile = "CHBrowser/LogData";//日志文件
            setting.PersistSessionCookies = true;//
            setting.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";//浏览器内核
            setting.UserDataPath = "CHBrowser/userData";//个人数据
            //CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            ///初始化
            //CefSharp.Cef.Initialize(setting);
            CefSharp.Cef.Initialize();

            //browser = new ChromiumWebBrowser("http://www.baidu.com");
            browser = new ChromiumWebBrowser(homePageUrl);
            browser.Dock = DockStyle.Fill;
            //browser.Load("http://www.baidu.com");]



            browser.LifeSpanHandler = new OpenPageSelf();



            //browser.JavascriptObjectRepository.Register("bound", new KeyBoard(), true);

            browser.JavascriptObjectRepository.Register("boundAsync", new BoundObject(), true);


            browser.CreateControl();
            this.panel1.Controls.Add(browser);


            if (isFullScreen)
            {
                FullScreen();
            }

            //new KeyBoard().ShowKeyBoard();
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



    public class BoundObject
    {
        public void showMessage(string msg)
        {
            switch (msg)
            {
                case "kb":
                    {
                        new KeyBoard().ShowKeyBoard();
                        break;
                    }
                case "py":
                    {
                        new KeyBoard().ShowPy();
                        break;
                    }
                case "sx":
                    {
                        new KeyBoard().ShowSx();
                        break;
                    }
                case "close":
                    {
                        new KeyBoard().Close();
                        break;
                    }
                default:
                    break;
            }
        }
    }

}
