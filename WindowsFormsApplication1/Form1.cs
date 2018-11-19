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
using AutoUpdaterDotNET;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //log4net.ILog log = log4net.LogManager.GetLogger("Form1");

        private ChromiumWebBrowser browser;

        //是否全屏
        bool isFullScreen = Boolean.Parse(System.Configuration.ConfigurationSettings.AppSettings["isFullScreen"]);

        string homePageUrl = System.Configuration.ConfigurationSettings.AppSettings["homeUrl"];

        string[] NoResetSizeUrls = System.Configuration.ConfigurationSettings.AppSettings["NoResetSizeUrls"].Split(',');

        double ResetSize = Double.Parse(System.Configuration.ConfigurationSettings.AppSettings["ResetSize"]);

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
            Cef.EnableHighDPISupport();

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

            //browser.SetZoomLevel(2.0);

            //browser.FrameLoadStart += (o, e) => NowBrowserAddress.Contains("zwfw.sd.gov.cn")?browser.SetZoomLevel(2):return; ;
            browser.FrameLoadStart += Browser_FrameLoadStart;
            browser.AddressChanged += Browser_AddressChanged;

        }

        private void Browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            //如果是属于不需要重置的地址,就直接返回
            for (int i = 0; i < NoResetSizeUrls.Length; i++)
            {
                if (NowBrowserAddress.Contains(NoResetSizeUrls[i]))
                {
                    browser.SetZoomLevel(1);
                    return;
                }
            }

            browser.SetZoomLevel(ResetSize);
        }

        string NowBrowserAddress = "";

        private void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            NowBrowserAddress = e.Address;
        }

        private void SetBtnLocationAndSize(Button btn, int i)
        {
            int sizeW = (this.Width / 30);

            if (i == 1)
            {
                Point p = new Point();
                p.X = this.Width - 3 * sizeW - sizeW / 4;
                p.Y = sizeW / 2;
                btn.Location = p;
            }

            if (i == 2)
            {
                Point p = new Point();
                p.X = this.Width - 2 * sizeW;
                p.Y = sizeW / 2;
                btn.Location = p;
            }



            SetBtnSize(btn);
        }

        private void SetBtnSize(Button btn)
        {
            int sizeW = (this.Width / 30);
            btn.Width = sizeW;
            btn.Height = sizeW;
        }



        private void FullScreen()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //回首页
            browser.Load(homePageUrl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //后退
            CefSharp.WebBrowserExtensions.Back(browser);
            browser.Back();
        }

        private void checkUpdate()
        {
            //AutoUpdater.Mandatory = true;
            //AutoUpdater.Start("https://rbsoft.org/updates/AutoUpdaterTest.xml");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            SetBtnLocationAndSize(button1, 2);
            SetBtnLocationAndSize(button2, 1);
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
