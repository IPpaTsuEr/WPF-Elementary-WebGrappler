using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WebGrappler.Commands;
using WebGrappler.Models;

namespace WebGrappler.Handler
{
    class CustomMenuHandler :IContextMenuHandler
    {
        public enum CustomMenuId
        {
            ViewResource = 28500,
            OpenDeV = 28499,
            CopyLink = 28498,
            OpenLink = 28497,
            Copy = 28496,
            Past = 28405,
            Cut = 28494,
            SelectAll = 28493,
            Print = 28492,
            Reload = 28491,
            Fresh = 28490,
            GoBack = 28489,
            GoForward = 28488,
            Save = 28487
        }

        public CustomCommand MenuCommand { get; set; }

        public CustomMenuHandler()
        {
            MenuCommand = new CustomCommand(Judger,Excuter);
        }

        private bool Judger(object pra)
        {
            var cmds = pra as Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>;
            switch (cmds.Item4)
            {
                case CustomMenuId.ViewResource:
                    return !cmds.Item2.IsLoading;
                case CustomMenuId.OpenDeV:
                    return true;
                case CustomMenuId.CopyLink:
                    
                case CustomMenuId.OpenLink:
                    return cmds.Item3.LinkUrl != "";
                case CustomMenuId.Copy:
                    return cmds.Item3.SelectionText!="";
                case CustomMenuId.Past:
                    return Clipboard.ContainsText();
                case CustomMenuId.Cut:
                    return cmds.Item3.SelectionText!="";
                case CustomMenuId.SelectAll:
                    return !cmds.Item2.IsLoading;
                case CustomMenuId.Print:
                    return !cmds.Item2.IsLoading;
                case CustomMenuId.Reload:
                    return true;
                case CustomMenuId.Fresh:
                    return true;
                case CustomMenuId.GoBack:
                    return cmds.Item2.CanGoBack;
                case CustomMenuId.GoForward:
                    return cmds.Item2.CanGoForward;
                case CustomMenuId.Save:
                    return cmds.Item3.SourceUrl!="";
                default:
                    break;
            }

            return true;
        }

        private void Excuter(object pra)
        {
            var cmds = pra as Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>;
            var chromiumWebBrowser = cmds.Item1;
            var browser = cmds.Item2;
            var parameters = cmds.Item3;
            var commandId = cmds.Item4;

            switch (commandId)
            {
                case CustomMenuId.ViewResource:
                    browser.FocusedFrame.ViewSource();
                    break;
                case CustomMenuId.OpenDeV:
                    browser.ShowDevTools();
                    break;
                case CustomMenuId.OpenLink:
                    var ccm = chromiumWebBrowser as CustomChrominWebBrowser;
                    (ccm.LifeSpanHandler as CustomLifeSpanHandler).RiaseEvent(parameters.LinkUrl);
                    break;
                case CustomMenuId.CopyLink:
                    Clipboard.SetText(parameters.LinkUrl);
                    break;
                case CustomMenuId.Copy:
                    browser.FocusedFrame.Copy();
                    break;
                case CustomMenuId.Cut:
                    browser.FocusedFrame.Cut();
                    break;
                case CustomMenuId.Past:
                    browser.FocusedFrame.Paste();
                    Clipboard.Clear();
                    break;
                case CustomMenuId.SelectAll:
                    browser.FocusedFrame.SelectAll();
                    break;
                case CustomMenuId.Print:
                    browser.GetHost().Print();
                    break;
                case CustomMenuId.Fresh:
                    browser.Reload();
                    break;
                case CustomMenuId.Reload:
                    browser.Reload(true);
                    break;
                case CustomMenuId.Save:
                    browser.GetHost().StartDownload(parameters.SourceUrl);
                    break;
                case CustomMenuId.GoBack:
                    browser.GoBack();
                    break;
                case CustomMenuId.GoForward:
                    browser.GoForward();
                    break;

                default:
                    break;
            }
        }


        public void OnBeforeContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {

            model.Clear();

            //MultiBinding Mtb = new MultiBinding();
            var para = new CustomMenuParams(parameters);

            //Mtb.Converter = (IMultiValueConverter)System.Windows.Application.Current.Resources["DoNothingMultiConverter"];
            //Mtb.Bindings.Add(new Binding() { Source = chromiumWebBrowser});
            //Mtb.Bindings.Add(new Binding() { Source = browser });
            //Mtb.Bindings.Add(new Binding() { Source = para});
            //Mtb.Bindings.Add(new Binding() { Source = 28498 });

            App.Current.Dispatcher.Invoke(new Action<CustomMenuParams>((obj)=>{

                var m = new ContextMenu() { IsOpen = true };
                m.Items.Add(new MenuItem()
                {
                    Header = "打开链接",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser,IBrowser,CustomMenuParams, CustomMenuId>(chromiumWebBrowser,browser,para, CustomMenuId.OpenLink)
                });
                m.Items.Add(new MenuItem()
                {
                    Header = "复制链接",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.CopyLink)
                });
                m.Items.Add(new MenuItem()
                {
                    Header = "保存为",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.Save)
                });
                m.Items.Add(new Separator());
                m.Items.Add(new MenuItem()
                {
                    Header = "复制",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.Copy)
                });
                m.Items.Add(new MenuItem()
                {
                    Header = "剪切",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.Cut)
                });
                m.Items.Add(new MenuItem()
                {
                    Header = "粘贴",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.Past)
                });
                m.Items.Add(new MenuItem()
                {
                    Header = "打印",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.Print)
                });
                m.Items.Add(new Separator());
                m.Items.Add(new MenuItem()
                {
                    Header = "后退",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.GoBack)
                });
                m.Items.Add(new MenuItem()
                {
                    Header = "刷新",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.Fresh)
                });
                m.Items.Add(new MenuItem()
                {
                    Header = "前进",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.GoForward)
                });
                m.Items.Add(new MenuItem()
                {
                    Header = "重载入",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.Reload)
                });
                m.Items.Add(new Separator());
                m.Items.Add(new MenuItem()
                {
                    Header = "查看源文件",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.ViewResource)
                });
                m.Items.Add(new MenuItem()
                {
                    Header = "检查元素",
                    Command = MenuCommand,
                    CommandParameter = new Tuple<IWebBrowser, IBrowser, CustomMenuParams, CustomMenuId>(chromiumWebBrowser, browser, para, CustomMenuId.OpenDeV)
                });

                ((ChromiumWebBrowser)chromiumWebBrowser).ContextMenu = m;
            }), para);



        }

        public bool OnContextMenuCommand(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            //switch ((int)commandId)
            //{
            //    case 28498:browser.ShowDevTools();
            //        break;
            //    case 28499:
            //        App.Current.Dispatcher.Invoke(() =>
            //        {
            //            ((ChromiumWebBrowser)chromiumWebBrowser).ContextMenu.IsOpen = false;
            //        });
            //        break;
            //    default:
            //        break;
            //}  
            return true;
        }

        public void OnContextMenuDismissed(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
        {

        }

        public bool RunContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            //不使用默认菜单
            return true;
        }
    }
}
