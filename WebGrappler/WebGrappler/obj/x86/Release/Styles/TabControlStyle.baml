   M S B A M L   `   `   `  ���� D  @WPF爬虫, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WPF爬虫.ViewModelsWPF爬虫.ViewModels  3clr-namespace:WPF爬虫.ModelsWPF爬虫.Models  D @ExToolKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:ExToolKit;assembly=ExToolKit	ExToolKit 5clr-namespace:WPF爬虫.HelpersWPF爬虫.Helpers  3clr-namespace:WPF爬虫.StylesWPF爬虫.Styles  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType �� Property �� Value   
Foreground �� 
TargetType �� Triggers ]� 
Conditions �� Property �� Value P� Property	 P� Value 	 Hover  Task_List_DataStyle
 �� DataType+    %WPF爬虫.ViewModels.ChrominViewModel �� Orientation �� TextAlignment � ToolTip  Download_DataStyle0   *WPF爬虫.ViewModels.DownloadItemViewModel � ColumnDefinitions �� RelativeSource �� AncestorType �� StringFormat  BytesReadableConvert �� 	Converter �Name � Minimum � Maximum � Value �� Triggers �� Value �� Binding �� 
TargetName � RowDefinitions  
Background  ExToolKit.ExFunctionBar  ExToolKit.ArcProgress   Center   Radius   EndAngle   StrokeThickness  ActiveHighLight    Content! ��Name" J� SnapsToDevicePixels# � UseLayoutRounding$ K� IsItemsHost  ActiveBackground  DoNothingMultiConverter% `� 	Converter& �� Source' �� 
CaretBrush 	 UnActive( �� Mode) �� Path* �� ElementName+ ��Name, �� SelectedContentTemplate- �� SelectedContent". �� SelectedContentStringFormat/ �� ContentStringFormat0 P� 
SourceName 
 TabItemDataStyle1 J� RenderTransformOrigin&   WPF爬虫.Helpers.LoadingManager2   	IsLoading3 J� OpacityMask4 �� EndPoint5 �� 
StartPoint6 � Color7 � Offset�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     )vm"clr-namespace:WPF爬虫.ViewModels  $mclr-namespace:WPF爬虫.Models  1ap*clr-namespace:ExToolKit;assembly=ExToolKit  &ldclr-namespace:WPF爬虫.Helpers  (localclr-namespace:WPF爬虫.Styles  %�  '��       &	     &	 �    7  7  7  7  '��     7  7  7  7  7  7  &	
 *    7  '�� �    7 
 �� "  ��.���  � ��$ 32����  � ��
 ���� ��  � ��
 ���� ��  � ��# �   ��  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� #��z��#��z��#��z��#��z��#��z��#��z��._��� #��z��#��z��#��z�� ]�  ��  �@J�IsMouseOver . ��  � {� . ./���  �@�Cursor$
 Hand��P�  �@J�IsMouseOver	 . . ��� # �   � ��P�  � {�	 .  . ���  � ��
 �������  � ��
 ���� �� "
   .R��� $ 
Horizontal=�.+��� ���� *Icon+�� $
 Left=�$��Center=����� *WebBrowser.Title+ �� *WebBrowser.Address+�� "
  .R��  �� $
��Auto ��� �� $
��Auto �.+��� $��Open_Button��$��打开��$y�Hidden=����� *DataContext.OpenControl+ �� $ WindowL����� *
FullPath+�� $ Vertical=�$��1��.+��� $ Vertical=�$
��Left=�.+��� $ 
Horizontal=�.+��� $	��200��$��CharacterEllipsis=����� *
FileName+ �� *
FullPath+�� $��Finish_Text��$��10(�$y�Hidden=����� *	EndTime+$ 完成于{}���� $��10(�$��5,0,2,0q�$��Bottom=�
��� $� �� �� ��� *ReceivedBytes+8    �� $�/�� �� ��� *
TotalBytes+8   �� $� �� �� $ 	Speed_Run����� *CurrentSpeed+8   �� $�/S��� $��Download_Pro��
������ $��1q�$��4��$��Stretch=����	#FF2983EE$ 0Q� �� *
TotalBytes+ �� *ReceivedBytes+�� $��2��$	��url������ *Url+ �� $ False�� �� *	IsValid+.a��� $ Download_Pro�� � ��
 �  ���� $
 True�� �� *
IsComplete+.a��� $ Download_Pro�� � y�$ Hidden=��� $ Open_Button�� � y�$ Visible=��� $ 	Speed_Run�� �� *AverageSpeed+8   � ��� $ Finish_Text�� � y�$ Visible=��� $
 true�� �� *IsCancelled+.a��� $ Download_Pro�� � y�$ 	Collapsed=��� $
 true�� �� *
IsPaused+.a��� $ Download_Pro�� � ��
 � ����� "  ��.���  � �� �� " ��.R��� ._�� 
������� �� $��32 ��� $
��Auto ��� .+�� #���   �� $��32 ��� .+�  $��32��$��32��8��  ���� *DataContext.TaskList+ �� $ WindowL���  $ 16,16&�$ 14Q�$	 292Q�$ 3Q�
������ #���  .  �� T�� $! HeaderPanel��$��1��" . # . $ 
Horizontal=�$ . #���  � $��1��#���  $��32�� �� $
��Auto ��� �� $
��Auto �.+��� $ 
Horizontal=�.+��� $	��《������ *DataContext.WebControl+ �� $ WindowL���`� 8%  .1��� $& _Back���� �� $��@������ *DataContext.WebControl+ �� $ WindowL���`� 8%  .1��� $& _Fresh���� �� $	��》������ *DataContext.WebControl+ �� $ WindowL���`� 8%  .1��� $& _Next���� �� $��Url_Text��$��1��$��14(�$��1q�$��Stretch=�$��Center=�
������ 
�������$��1q�#' �   #��� 	 ���� *WebBrowser.Address+$( OneWay=�.���� $ 
Horizontal=�$��2��.+��� $��Go������ *DataContext.WebControl+ �� $ WindowL���`� 8%  .1��� $& _OpenUrl���� �� $
) Text�$* Url_Text���� $
��Task������ *DataContext.WebControl+ �� $ WindowL���`� 8%  .1��� $& _NewTask���� �� $
) Text�$* Url_Text���� $+ PART_SelectedContentHost��$��2��$��SelectedContent��" . # . #��z, #��z- #/ z. $��Stretch=�$��Stretch=� P� $0 Url_Text�� �@J�	IsFocused	 . . ��� # �  $ Url_Text�� � ���� # �   $ Url_Text�� � ���� "
   .R��� $��1,0,1,0q����	#0F232323._�� $��
_BackPanel��#���  $	��200��#��z��#��z�� �� $
��Auto ��� �� $
��Auto �.+�  $��Load_Ani��$��20��$��20��$ 10,10&�$ 8Q�$ 2Q�$	 320Q�#���  $y�Visible=�$1 0.5,0.5&�2 �� *WebBrowser.IsLoading+�� $��Web_Icon��$��20��$��20��$��UniformToFill=�$��5,0,10,0q�$��Center=�$��Center=�$y�Hidden=����� *Icon+�� $��1��$��	0,0,-20,0q�$��CharacterEllipsis=�#���   
������ $��Stretch=�$��Center=�$
 Left=� �� *WebBrowser.Title+���� *WebBrowser.Title+3 �� $4 1,0.5&�$5 0,0.5&�.��� $6 Black��$7 0Q�� $6 Black��$
7 0.75Q�� $	7 0.9Q��� $ 
Horizontal=�$��2��$��32��$��Center=�$��Right=�.+��� $y�	Collapsed=�.��>>�� $��X������ *DataContext.WebControl+ �� $ WindowL���`� 8%   .1��� $& _Close����  �� $
 True�� �� *
IsSelected+.a��� # �  $ 
_BackPanel�� � ���� $ False�� �� *WebBrowser.IsLoading+.a��� $ Load_Ani�� � y�$ Hidden=��� $ Web_Icon�� � y�$ Visible=��� "  ��.���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  � ��
 ���� ��  � ��$ 0q���  � ��8   ��  � �� �� *
WebBrowser+��  �@��
IsSelected �� *
IsSelected+$( TwoWay=���  � �� �� " ��.R��� ._�� #���   �� $��2 ��� .+��� $��
Active_Bar��#��� 	 $��0q��� $��2��$��Header�� P�  �@��
IsSelected	 . . ��� # �  $ 
Active_Bar�� � ��