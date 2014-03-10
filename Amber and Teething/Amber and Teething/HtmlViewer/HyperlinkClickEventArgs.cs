﻿using System;

namespace Amber_and_Teething.HtmlViewer
{
    public class HyperlinkClickEventArgs : EventArgs
    {
        public HyperlinkClickEventArgs(Uri uri)
        {
            this.NavigationUri = uri;
        }

        public Uri NavigationUri { get; set; }
    }
}
