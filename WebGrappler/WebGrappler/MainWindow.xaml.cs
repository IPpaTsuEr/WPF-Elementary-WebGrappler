
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebGrappler.Handler;
using WebGrappler.Services;
using WebGrappler.Models;
using System.ComponentModel;
using System.Threading;
using WebGrappler.Commands;
using WebGrappler.ViewModels;
using System.Collections.ObjectModel;
using ExToolKit;

namespace WebGrappler
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// Install-Package CefSharp.Wpf -Version 75.1.143
    public partial class MainWindow : ExWindow
    {
        WindowView _DataContext =  new WindowView();
        public MainWindow()
        {
            this.DataContext = _DataContext;
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
            Closed += MainWindow_Closed;
            InitializeComponent();
        }
       

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            _DataContext.OnClosing();
            
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        
        //private  void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        //{
        //    Console.WriteLine("Load State Changed " + e.IsLoading + " ----- ");
            
        //    if (e.IsLoading == false)
        //    {

        //        var w = sender as CustomChrominWebBrowser;
        //        if (w.Info == null) return;

        //        var b = e.Browser;
        //        if (w.GetBrowser() == b) Console.WriteLine("Compare True ---------------------------");

        //        //Get Rules When Page Loaded End
        //        var infoTask = w.Info;
        //        string url = this.Dispatcher.Invoke((Func<string>)(() => { return w.Address; }));
        //        var Rules = CmdHelper.GetRule(url);


        //    }
        //    if (false)
        //    {
        //        //在此处理 URL Text 类型Task


        //        var browser = new ChromiumWebBrowser();
                
        //        JSHelper.InjuctJQ(browser.GetBrowser().MainFrame);
        //        Console.WriteLine("================AA");
        //        //JSHelper.ResetFunction(browser.GetBrowser().MainFrame);
                
        //        JSHelper.EnableJQ(browser.GetBrowser().MainFrame);
        //        //string t = await JSHelper.GetText(browser.GetBrowser().MainFrame, "#intro_l div h1");

        //        //TaskHelper.Excute(browser.GetBrowser().MainFrame, CurrentTask);
        //        //Console.WriteLine(JSHelper.JQ);

        //        //
        //        for (int i=0;i<100;i++)
        //        {
        //        //JSHelper.KeyDown(browser.GetBrowser().MainFrame, "#comicContain",34 );
        //        }
        //        JSHelper.RollDownWithKey(browser.GetBrowser().MainFrame, "#comicContain",200,50, 34);
        //        //TaskHelper.AddTask(null);
        //        //var b = sender as ChromiumWebBrowser;
        //        ////b.GetBrowser().MainFrame.EvaluateScriptAsync("alert(\"alert\");");
        //        //var t  = b.GetBrowser().MainFrame.EvaluateScriptAsync(@"(function(){
        //        //        var list = []; 
        //        //        $('#play_0 ul li a ').each(function(){
        //        //            list.push($(this).attr('href'));
        //        //        });
        //        //        return list;}());");
        //        //Console.WriteLine("Watie");
        //        //t.ContinueWith(Jresult => {
        //        //    var s = Jresult.Result.Result;
        //        //    Console.WriteLine(s.GetType());
        //        //    foreach (var item in s as List<object>)
        //        //    {
        //        //        Console.WriteLine(item as string);
        //        //    }
        //        //});


        //    }
        //}


    }
}
