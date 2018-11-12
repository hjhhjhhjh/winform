using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class KeyBoard
    {

        [DllImport("controllerApi.dll", EntryPoint = "IMESetPath", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern void IMESetPath(StringBuilder fullPath);

        [DllImport("controllerApi.dll", EntryPoint = "IMEShow", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern void IMEShow();

        [DllImport("controllerApi.dll", EntryPoint = "IMESetMode", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern void IMESetMode(StringBuilder mode);

        [DllImport("controllerApi.dll", EntryPoint = "IMEClose", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern void IMEClose();

        string path = System.Configuration.ConfigurationSettings.AppSettings["srfPath"];

        public void ShowKeyBoard()
        {
            IMESetPath(new StringBuilder(path));
            IMEShow();
        }

        public void ShowPy()
        {
            IMESetMode(new StringBuilder("py26"));
            IMEShow();
        }

        public void ShowSx()
        {
            IMESetMode(new StringBuilder("hw26"));
            IMEShow();
        }

        public void Close() {
            IMEClose();
        }

    }
}
