   M S B A M L   `   `   `  ���� F  BWebGrappler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WebGrapplerWebGrappler  ?$clr-namespace:WebGrappler.ViewModelsWebGrappler.ViewModels  D @ExToolKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:ExToolKit;assembly=ExToolKit	ExToolKit H2clr-namespace:ExToolKit.Helpers;assembly=ExToolKitExToolKit.Helpers V RCefSharp.Wpf, Version=75.1.143.0, Culture=neutral, PublicKeyToken=40c4b6fc221f4138A0clr-namespace:CefSharp.Wpf;assembly=CefSharp.WpfCefSharp.Wpf     WebGrappler.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:WebGrappler     3cw$clr-namespace:WebGrappler.ViewModels     9ex*clr-namespace:ExToolKit;assembly=ExToolKit     Bexh2clr-namespace:ExToolKit.Helpers;assembly=ExToolKit     @web0clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf     -   $��_Main_Window��
�������   ActiveBackground#���     � Icon$  /Sources/Window.ico��$��1q� � Title$ WebGrappler��$	��600��$	��800��  ExToolKit.ExWindow   FunctionBar ��  �� Orientation$ 
Horizontal=�.+�  ExToolKit.ExPopBar  $	��600��  Used_Task_List_DataStyle#��[   Task_List_ContainerStyle#��[ ���� *DataContext.TaskViewList+ �� RelativeSource ��  �� AncestorType$ WindowL�%  ExToolKit.Helpers.AdornerHelper   DataReference �� *DataContext.TaskViewList+ �� $ WindowL�   AdornerElement ��  J� IsHitTestVisible .    ActiveHighLight#���  $��0,2,12,0q�$��Right=�$	��Top=�
	 � Tag	 �� 
 �� Path$
 DataContext.Count� ��  �� Mode$
 Self=�y��� *Text+ �� $
 Self=�  CountVisiableConvert �� 	Converter# [ ��`�   ActiveCountConvert `� 	Converter# [  `� ConverterParameter$
 Task��.1��� $
 DataContext� �� $
 Self=��� $	
 Tag� �� $
 Self=�����  � ToolTip$ 解析列表��  IconTextBlock#��[   Task#��[   $	��600��  Download_Item_DataStyle#��[  	 Download_ContainerStyle#��[	 ���� *DownloadList+ �� *DataContext.DownloadList+ �� $ WindowL� ��  .  #���  $��0,2,12,0q�$��Right=�$	��Top=�	 �� $
 DataContext.Count� �� $
 Self=�y��� *Text+ �� $
 Self=�# [ ��`� # [ $ Download��.1��� $
 DataContext� �� $
 Self=��� $	
 Tag� �� $
 Self=����� $ 下载列表��#��[  
 Download#��[
 �� $	��0,1q�
������ $��2�����	#4Fe3e3e3  $ 设置��$��38������ #��[   Setting#��[ .@�  ExToolKit.ExPopBarItem     CommandParameter$ 
_OpenRules��$
��Left=�$��Stretch=�   Icon$       ��   Command �� *DataContext.MenuControl+ �� $ WindowL�.���� $�� 编辑解析规则��  $ _OpenDownload��$
��Left=�$��Stretch=�$       �� �� *DataContext.MenuControl+ �� $ WindowL�.���� $�� 打开存储目录��  $ _OpenLog��$
��Left=�$��Stretch=�$       �� �� *DataContext.MenuControl+ �� $ WindowL�.���� $�� 打开日志��  $ 
_OpenAbout��$
��Left=�$��Stretch=�$       �� �� *DataContext.MenuControl+ �� $ WindowL�.���� $�� 关于��.��� .+�� $��0q�.+��� ���� *	PreLoad+�� .���� ���� *
TaskList+ �� UpdateSourceTrigger$ PropertyChanged=�����  �� DataType-   'WebGrappler.ViewModels.ChrominViewModel"  .R��� $
��1080��$	��400��$	��400��._��� ���� *
WebBrowser+����  �� 
TargetType" ��.R��� $
��Auto=�
�������.���� �� -   $��	Main_Grid�� J� Opacity$ 1Q� �� SelectedIndex$ 0�� � DataContext �� $
 SelectedItem� �� $
 Self=����� *DataContext.PageList+ �� $ WindowL�$ PropertyChanged=�