   M S B A M L   `   `   `  ����F  BWebGrappler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WebGrapplerWebGrappler  5   	   ?$clr-namespace:WebGrappler.ViewModelsWebGrappler.ViewModels  5   	   D @ExToolKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:ExToolKit;assembly=ExToolKit	ExToolKit 5   	   H2clr-namespace:ExToolKit.Helpers;assembly=ExToolKitExToolKit.Helpers 5	   	   V RCefSharp.Wpf, Version=75.1.143.0, Culture=neutral, PublicKeyToken=40c4b6fc221f4138A0clr-namespace:CefSharp.Wpf;assembly=CefSharp.WpfCefSharp.Wpf 5
   	       WebGrappler.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WebGrappler     5   	   3cw$clr-namespace:WebGrappler.ViewModels     5   	   9ex*clr-namespace:ExToolKit;assembly=ExToolKit     5   	   Bexh2clr-namespace:ExToolKit.Helpers;assembly=ExToolKit     5	   	   @web0clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf     5
   	   -   5        $��_Main_Window��5   -   
�������  � WindowStartupLocation$  CenterScreen=�6A      ActiveBackground#���   5   	    � Icon$ /Sources/Window.ico��5      $��1q�6)    � Title$ WebGrappler��5   	   $	��600��6   $	��800��6*     ExToolKit.ExWindow   FunctionBar 5      �� 5   
    �� Orientation$ 
Horizontal=�6   .+�5        ExToolKit.ExPopBar  $	��600��6     Used_Task_List_DataStyle#��[ 5        Task_List_ContainerStyle#��[ 5      ��5      �� *DataContext.TaskViewList+ �� RelativeSource ��  �� AncestorType$ WindowL�%  ExToolKit.Helpers.AdornerHelper   DataReference 5      �� *DataContext.TaskViewList+ �� $ WindowL�   AdornerElement 5      �� 5      	 J� IsHitTestVisible	 .    ActiveHighLight#���  5       $��0,2,12,0q�6P   $��Right=�5       $	��Top=�6<   

 � Tag
 5       ��  �� Path$ DataContext.Count� ��  �� Mode$
 Self=�y�5       �� *Text+ �� $
 Self=�  CountVisiableConvert �� 	Converter# [ ��5       `� 5!        ActiveCountConvert `� 	Converter# [ 6+    `� ConverterParameter$
 Task��6[   .1�5"   "   �� $ DataContext�6*    6=   �� $
 Self=��� 5#   "   $	 Tag�6*    65   �� $
 Self=�5$      5&      ��5*      �� 5+       � ToolTip$ 解析列表��6      IconTextBlock#��[ 6/     Task#��[ 6V   5-        5/      $	��600��6     Download_Item_DataStyle#��[ 50       	 Download_ContainerStyle#��[	 52      ��51      �� *DownloadList+ 53      �� *DataContext.DownloadList+ �� $ WindowL� 55      �� 56      	 .  #���  5:       $��0,2,12,0q�6P   $��Right=�5;       $	��Top=�6<   
 57       �� $ DataContext.Count� �� $
 Self=�y�59       �� *Text+ �� $
 Self=�# [ ��5<      `� 5=      # [ 6+   $ Download��6[   .1�5>   "   �� $ DataContext�6*    6=   �� $
 Self=��� 5?   "   $	 Tag�6*    65   �� $
 Self=�5@      5C      ��5F      �� 5G      $ 下载列表��6    #��[ 6/    
 Download#��[
 6V   5J      �� 5L      $	��0,1q�6   
������ $��2��6E   ���	#4Fe3e3e36O     5N      $ 设置��6   $��38��6'   ��5P      �� 5Q      #��[ 6      Setting#��[ 6G   .@�5T        ExToolKit.ExPopBarItem     CommandParameter$ 
_OpenRules��6�   $
��Left=�5U   "   $��Stretch=�6D      Icon$       ��6b      Command 5T   "   �� *DataContext.MenuControl+ �� $ WindowL�.��5V      �� $�� 编辑解析规则��6    5W        5X      $ _OpenDownload��6�   $
��Left=�5Y   "   $��Stretch=�6D   $       ��6b    5X   "   �� *DataContext.MenuControl+ �� $ WindowL�.��5Z      �� $�� 打开存储目录��6    5[        5\      $ _OpenLog��6�   $
��Left=�5]      $��Stretch=�67   $       ��6U    5\   #   �� *DataContext.MenuControl+ �� $ WindowL�.��5^      �� $�� 打开日志��6    5_        5`      $ 
_OpenAbout��6�   $
��Left=�5a      $��Stretch=�67   $       ��6U    5`   #   �� *DataContext.MenuControl+ �� $ WindowL�.��5b      �� $�� 关于��6    5c      5d      5e      .��5g      � .+�5h   
   � $��0q�6   .+�5i      �� ��6   �� *	PreLoad+�� 5j      .��5k      �� ��6   �� *
TaskList+ �� UpdateSourceTrigger$ PropertyChanged=���5l      �� 5m       �� DataType-   'WebGrappler.ViewModels.ChrominViewModel"  6'   .R�5n      �� $
��1080��6%   $	��400��63   $	��400��6B   ._�5o   "   �� ��63   �� *
WebBrowser+6T   5p      5q      ��5s      �� 5t       �� 
TargetType" ��6*   .R�5u      �� $
��Auto=�6+   
�������.��5v   "   �� 5w      5x      5{      5|      5}      �� 5   
   -   5        $��	Main_Grid��5       �� SelectedIndex$ 0��5�       � DataContext 5�      �� $ SelectedItem� �� $
 Self=���5�      �� *DataContext.PageList+ �� $ WindowL�$ PropertyChanged=�5�      5�      5�      