using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WebGrappler.Handler;
using WebGrappler.Models;
using WebGrappler.Services;

namespace WebGrappler.ViewModels
{
    public class ChrominViewModel:NotifyBase
    {

        private CustomChrominWebBrowser _WebBrowser;

        public CustomChrominWebBrowser WebBrowser
        {
            get { return _WebBrowser; }
            set
            {
                if (value != _WebBrowser)
                {
                    _WebBrowser = value;
                    Notify("WebBrowser");
                }
            }
        }

        private BitmapImage _Icon;

        public BitmapImage Icon
        {
            get {
                return _Icon; }
            set
            {
                if (value != _Icon)
                {
                    _Icon = value;
                    Notify("Icon");
                }
            }
        }

        private bool _IsSelected;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (value != _IsSelected)
                {
                    _IsSelected = value;
                    Notify("IsSelected");
                }
            }
        }

        private bool _IsPlaying;

        public bool IsPlaying
        {
            get { return _IsPlaying; }
            set
            {
                if (value != _IsPlaying)
                {
                    _IsPlaying = value;
                    Notify("IsPlaying");
                }
            }
        }



        public ChrominViewModel(CustomChrominWebBrowser browser) {
            _WebBrowser = browser;

        }

 }
}
