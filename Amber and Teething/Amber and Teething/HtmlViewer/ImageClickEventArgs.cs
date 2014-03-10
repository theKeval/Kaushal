using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Amber_and_Teething.HtmlViewer
{
    public class ImageClickEventArgs : EventArgs
    {
        public ImageClickEventArgs(ImageSource source)
        {
            this.ImageSource = source;
        }

        public ImageSource ImageSource { get; set; }
    }
}
