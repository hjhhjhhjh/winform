using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TouchSystem
{
    public class Round : Button
    {
        protected override void OnCreateControl()
        {
            using (var path = new GraphicsPath())
            {
                int b = this.Width / 15;
                //int w = this.Width / 5;
                int w = 2 * b;
                path.AddEllipse(new Rectangle(b, b, this.Width - w, this.Height - w));
                this.Region = new Region(path);
            }
            base.OnCreateControl();
        }
    }


    //class Round : Button
    //{
    //    protected override void OnCreateControl()
    //    {
    //        using (var path = new GraphicsPath())
    //        {
    //            path.AddEllipse(new Rectangle(2, 2, this.Width - 5, this.Height - 5));
    //            this.Region = new Region(path);
    //        }
    //        base.OnCreateControl();
    //    }
    //}
}
