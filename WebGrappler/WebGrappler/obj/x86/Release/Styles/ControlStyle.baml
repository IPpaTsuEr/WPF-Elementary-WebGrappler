   M S B A M L   `   `   `  ���� F  BWebGrappler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:WebGrappler.ViewModelsWebGrappler.ViewModels  7 clr-namespace:WebGrappler.ModelsWebGrappler.Models  D @ExToolKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:ExToolKit;assembly=ExToolKit	ExToolKit H2clr-namespace:ExToolKit.Helpers;assembly=ExToolKitExToolKit.Helpers 9!clr-namespace:WebGrappler.HelpersWebGrappler.Helpers  7 clr-namespace:WebGrappler.StylesWebGrappler.Styles  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers ]� 
Conditions �� Property �� Value P� Property	 P� Value 	  Hover  
Foreground  SystemButton  Task_Button   ExToolKit.ArcProgress
    Center    Radius    EndAngle    StrokeThickness-  'ExToolKit.Helpers.RotateTransformHelper   EnableAutoLoop J� RenderTransformOrigin   	Animation W� From	 W� To g� Duration g� RepeatBehavior �� 
TargetName  ActiveHighLight  Download_Item_DataStyle �� DataType2   ,WebGrappler.ViewModels.DownloadItemViewModel � ColumnDefinitions
 � Tag � ToolTip �� RelativeSource �� AncestorType  PathIconConvert �� 	Converter  DoNothingMultiConverter `� 	Converter �� Path �� Mode  IconTextBlock 		 Pause  J� IsHitTestVisible! J� Effect3  -System.Windows.Media.Effects.DropShadowEffect"   Color#   
BlurRadius$   ShadowDepth%   	Direction& � RowDefinitions 
 Copy' �� Source( �� TextAlignment 
 Remove  Drop) � Maximum* � Value  DownloadTimeConvert+ �� Orientation  BytesReadableConvert, �� StringFormat  DownloadDateConvert  
OpenFloder- �� Triggers. P� 
SourceName/ ^� 
Conditions0 �� Binding1 �� Value2 �� Binding 	 Fresh 
 Resume  Download_ContainerStyle  ExToolKit.ExPopBarItem  Used_Task_List_DataStyle*   $WebGrappler.ViewModels.TaskViewModel  TaskProgressConvert  Task_List_ContainerStyle  TabItemDataStyle-   'WebGrappler.ViewModels.ChrominViewModel(  "WebGrappler.Helpers.LoadingManager3   	IsLoading4 J� OpacityMask5 �� EndPoint6 �� 
StartPoint7 � Color8 � Offset 	 Close  ActiveTabBackground9 �� Mode  TabScroll_RepeatButton: �� Triggers  TabScroll_ScrollViewer; �� LineLeftCommand  ScrollButtonVisiable< �� Angle= �� CanContentScroll> �� CanHorizontallyScroll? �� CanVerticallyScroll@ �� LineRightCommand  ActiveBackground  AddFileA ��NameB J� SnapsToDevicePixelsC � UseLayoutRoundingD K� IsItemsHost  
Background   Goto ! InputBackgroundE �� 
CaretBrushF J� InputBindings
G �� KeyH �� CommandI �� CommandParameterJ �� ElementName 
" Lovely # HomeK �� UpdateSourceTrigger 
$ Search % TaskL ��NameM �� SelectedContentTemplateN �� SelectedContent"O �� SelectedContentStringFormatP �� ContentStringFormat�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     +vm$clr-namespace:WebGrappler.ViewModels  &m clr-namespace:WebGrappler.Models  1ap*clr-namespace:ExToolKit;assembly=ExToolKit  :aph2clr-namespace:ExToolKit.Helpers;assembly=ExToolKit  (ld!clr-namespace:WebGrappler.Helpers  *local clr-namespace:WebGrappler.Styles  %�D  '��       &	     &	 "    &	 �    7  7  7  7 	 7  7 
 7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  &	 r    7  &	 p    7  7  7  7  7 	 7  7  7 
 7  7  7  7  7  7  7  7  7  7  7  7  7  &	 �*    &	 *+    7  7  7  '�� 0    7  &	 N2    &	 �3    7  7  7  7  '�� V6    7  7  7  7  7  7  7   7  7  7  7  7  7  7  7   7  7  7  7  7   7  7  7 " 7  7  7  7 # 7  7  7 $ 7  7  7  7 % 7  �� "  ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� #��z��#��z��#��z��#��z��#��z��#��z��._��� #��z��#��z��#��z�� ]�  ��  �@J�IsMouseOver . ��  � {� . ./���  �@�Cursor$
 Hand��P�  �@J�IsMouseOver	 . . ��� # �    � ���� # �   � ��P�  � {�	 .  . ���  � ��
 �������  � ��
 ���� �� "  ��.���  � ��$ 32����  � ��# �  ��  � ��
 ���� ��  � ��
 ���� ��  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� #��z��#��z��#��z��#��z��#��z��#��z��._��� #��z��#��z��#��z�� ]�  ��  �@J�IsMouseOver . ��  � {� . ./���  �@�Cursor$
 Hand��P�  �@J�IsMouseOver	 . . ��� # �    � ��P�  � {�	 .  . ���  � ��
 �������  � ��
 ���� �� "  ��.���  � �� �� " ��.R�� .+�   $��MainTask_Prog_Bar��$
 16,16&�$ 14Q�$	 292Q�$ 3Q�
������ 
������  . $ 0.5,0.5&� W� $ 0X�$	 360X�$ 0:0:10D�$ Forever���� $��Main_Task_Tip��$��Center=�$��Center=�
�������#��z�� P�  �@J�IsMouseOver	 . . ��� $ MainTask_Prog_Bar��# �   � ���� "  .R�� $	��460�� �� $
��Auto ��� �� $
��Auto �.+��� $��DControl_Button��$ _Pause��$ 暂停������ *DataContext.DownloadControl+ �� $ WindowL�.���� $��32��$��64��$��Uniform=����� *
FullPath+8   ��`� 8  .1��� $	 Tag� �� $
 Self=��� �� $��DHandler_Text��8�� 8�� $��Center=�$��Center=�$��18(�
�������  .  $y�Hidden=�!   $" Black��$# 2Q�$$ 1Q�$% 0Q�� $��DTitle_Grid��$��1��$	��4,2q�& �� �� $
��Auto ��� $
��Auto �.+�� $��0,4,0,0q� �� $
��Auto ��� �� $
��Auto ��� $
��Auto �.+��� $��24��$��24��$ 复制链接������ *DataContext.DownloadControl+ �� $ WindowL�.���� 8�� 8�� ��`� 8  .1��� $' _CopyUrl���� �� $��1��$��CharacterEllipsis=�$��16(�$��Stretch=�$��Center=�$
( Left=����� *
FileName+ �� *
FileName+�� $��2��$��24��$��24��$ 移除记录������ *DataContext.DownloadControl+ �� $ WindowL�.���� 8�� 8�� ��`� 8 	 .1��� $' _Remove���� �� $��DRemove_Button��$��3��$$ 移除文件并删除此记录��$��24��$��24������ *DataContext.DownloadControl+ �� $ WindowL�.���� $��DRemove_Button_Text��8��
 8�� ��`� 8  .1��� $' _Drop���� � $��DProgress_Bar��$��1��$	��1,4q�$��4��#���  ) �� *
TotalBytes+* �� *ReceivedBytes+� $��2��$��12(� �� $
��Auto ��� �� $
��Auto ��� $
��Auto �.+��� $��DStatus_Text��$��0��$��	下载中���� $��
DTime_Text��$��1��$��CharacterEllipsis=�$
��Left=� �� *Text+ �� $
 Self=����� *
NeedTime+8  �� $+ 
Horizontal=�$��2��.+��� $��DRecevie_Text������ *ReceivedBytes+8  $
, {0}/���� $��
DSize_Text������ *
TotalBytes+8  �� $��DSpeed_Text������ *CurrentSpeed+8  $, ({0}/s)���� $��	DEnd_Text��$��3��$y�	Collapsed=����� *	EndTime+8  $
,  {0}���� $��2��$��Stretch=�$��64��$ 打开存储路径������ *DataContext.DownloadControl+ �� $ WindowL�.���� 8�� 8�� $��22(���`� 8  .1��� $' _Open���� - P� $. DControl_Button�� �@J�IsMouseOver	 . . ��� $ DHandler_Text�� � y�$ Visible=�P� $. DTitle_Grid�� �@J�IsMouseOver	 . . ��� $ DTitle_Grid�� � �� �	#0196FFF5P� $. DRemove_Button�� �@J�IsMouseOver	 . . ��� $ DRemove_Button_Text�� � ��
 �  ��^� / �� 0 �� *
IsPaused+	 False�� 0 �� *IsInProgress+ True.0��� $ DControl_Button�� �@�ToolTip$ 暂停���� $ DControl_Button�� �@�Tag$ _Pause���� $ DRemove_Button�� �@�ToolTip$- '强制结束任务并删除对应文件���� $1 False��2 �� *	IsValid+.a��� $ DProgress_Bar�� � ��
 �  ���� $ DStatus_Text�� � ��$ 资源无效���� $ DControl_Button�� �@�ToolTip$ 重试���� $ DControl_Button�� �@�Tag$ _Retry���� $ 
DTime_Text�� � y�$ Hidden=��� $ DHandler_Text��8   � ���� $ DHandler_Text�� � ��$ 26(��� $ DHandler_Text�� � ��$ Heavy"��� $
1 True��2 �� *IsCancelled+.a��� $ DProgress_Bar�� � �� �	#FFFF380C�� $ DStatus_Text�� � ��$ 	已取消���� $ DControl_Button�� �@�ToolTip$ 重新下载���� $ DControl_Button�� �@�Tag$ _Retry���� $ 
DTime_Text�� � y�$ Hidden=��� $ DHandler_Text��8   � ���� $ DHandler_Text�� � ��$ 26(��� $ DHandler_Text�� � ��$ Heavy"��� $
1 True��2 �� *
IsPaused+.a��� $ DProgress_Bar�� � �� �	#FFFF6800�� $ DStatus_Text�� � ��$ 	已暂停���� $ DControl_Button�� �@�ToolTip$ 继续���� $ DControl_Button�� �@�Tag$ _Resume���� $ 
DTime_Text�� � y�$ Hidden=��� $ DHandler_Text�� � ��$ 22(��� $ DHandler_Text��8   � ���� $ DRemove_Button�� �@�ToolTip$' !结束任务并删除对应文件���� $
1 True��2 �� *
IsComplete+.a��� $ DProgress_Bar�� � y�$ Hidden=��� $ 
DTime_Text�� � y�$ Hidden=��� $ DRecevie_Text�� � y�$ 	Collapsed=��� $ DSpeed_Text�� � y�$ 	Collapsed=��� $ 	DEnd_Text�� � y�$ Visible=��� $ DStatus_Text�� � ��$ 完成���� $ DControl_Button�� Z�  �@�ToolTip�� $ DControl_Button�� Z�  �@�Tag�� $ DControl_Button�� � {� .  �� $ DHandler_Text�� � y�$ Hidden=��� "   .���  � ��$
 Left=���  � ��
 �������  �@ Command �� *DataContext.DownloadControl+ �� $ WindowL���  �@ CommandParameter `� 8   .1��� $' _Excute���� $ DataContext� �� $
 Self=��� "  .R�� $	��400�� �� $
��auto ��� �� $
��auto �.+��� $��Ring_Buttong��$y�Visible=�$ _Pause��$ 暂停������ *DataContext.TaskControl+ �� $ WindowL���`� 8   .1��� $	 Tag� �� $
 Self=��� .��   $��Ring_Progress��$ 0.5,0.5&�$��48��$��48��8�� $
 24,24&�$ 20Q�$ 4Q� .  W� $ 0X�$	 360X�$ Forever��$ 0:0:6D� `� 8  .1��� $ SuccessCount��� $ 
ErrorCount��� $ 
TotalCount��� $��THandl_Text��8�� 8�� $��Center=�$��Center=�  .  !   $" Black��$# 2Q�$% 0Q�$$ 1Q�� $��1��& �� �� .+��  �� $
��Auto ��� �� $
��Auto ��� $
��Auto �.+��� $��24��$��24������ *DataContext.TaskControl+ �� $ WindowL���`� 8  .1��� $' _CopyUrl���� .���� 8�� 8�� $ 	复制URL���� $��1��$��16(�$��Center=�$��CharacterEllipsis=����� *Title+ �� *Title+�� $��2��$��24��$��24������ *DataContext.TaskControl+ �� $ WindowL���`� 8  .1��� $' _Remove���� .���� 8��	 8��
 $ 移除记录���� $��TDrop_Button��$��3��$��24��$��24��$ 删除对应文件������ *DataContext.TaskControl+ �� $ WindowL���`� 8  .1��� $' _Drop���� .���� $��TDrop_Button_Text��8�� 8�� � $��1��$��Center=�$��2q� �� $
��Auto ��� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��TStatus_Text��$��	解析中���� $��2������ *SuccessCount+$, 已完成{0}���� $��3������ *
TotalCount+$, /共{0}���� $��4������ *
ErrorCount+$, (出错{0})���� $��2��$ 打开输出目录��$��48��$��2,0,0,0q����� *DataContext.TaskControl+ �� $ WindowL���`� 8  .1��� $' _Open���� .���� 8�� 8�� $��22(�- P� $. TDrop_Button�� �@J�IsMouseOver	 . . ��� $ TDrop_Button_Text�� � ��
 �  ��P� $. Ring_Buttong�� �@J�IsMouseOver	 . . ��� $ THandl_Text�� � y�$ Visible=�P� $. Ring_Buttong�� �@J�IsMouseOver	 .  . ��� $ THandl_Text�� � y�$ 	Collapsed=��� $1 false��2 �� *
IsPaused+.a��� $ TStatus_Text�� � ��$ 	解析中���� $ Ring_Buttong�� �@�Tag$ _Pause���� $ Ring_Buttong�� �@�ToolTip$ 暂停���� $ Ring_Progress��8   � ���� $ TDrop_Button�� �@�ToolTip$- '强制结束任务并删除对应文件���� $
1 True��2 �� *IsCompleted+.a��� $ Ring_Buttong�� Z�  �@�Tag�� $ Ring_Buttong�� � {� .  �� $ Ring_Progress�� � ��
 � � ��� $ THandl_Text�� � y�$ Hidden=��� $ TStatus_Text�� � ��$ 	已完成���� $ Ring_Progress�� �@ EnableAutoLoop .  �� $
1 True��2 �� *	IsErrored+.a��� $ Ring_Buttong�� �@�Tag$ _Retry���� $ Ring_Buttong�� �@�ToolTip$ 重试���� $ Ring_Progress�� � ��
 �  ���� $ TStatus_Text�� � ��$ 因错误而终止���� $ THandl_Text��8   � ���� $ THandl_Text�� � ��$ Heavy"��� $ THandl_Text�� � ��$ 24(��� $ Ring_Progress�� �@ EnableAutoLoop .  �� $
1 True��2 �� *
IsPaused+.a��� $ Ring_Buttong�� �@�Tag$ _Resume���� $ Ring_Buttong�� �@�ToolTip$ 继续���� $ Ring_Progress�� � ��
 � ����� $ TStatus_Text�� � ��$ 	已暂停���� $ THandl_Text��8   � ���� $ THandl_Text�� � ��$ 18(��� $ Ring_Progress�� �@ EnableAutoLoop .  �� $ TDrop_Button�� �@�ToolTip$' !结束任务并删除对应文件���� $
1 True��2 �� *	IsStopped+.a��� $ Ring_Buttong�� �@�ToolTip$ 重新解析���� $ Ring_Buttong�� �@�Tag$ _Retry���� $ Ring_Progress�� � ��
 � E���� $ TStatus_Text�� � ��$ 	已停止���� $ THandl_Text��8   � ���� $ THandl_Text�� � ��$ Heavy"��� $ THandl_Text�� � ��$ 24(��� $ Ring_Progress�� �@ EnableAutoLoop .  �� $
1 true��2 �� *
IsPauseing+.a��� $ TStatus_Text�� � ��$ 请稍后，正在暂停���� $ Ring_Buttong�� Z�  �@�Tag�� $ Ring_Buttong�� �@�ToolTip$ 正在暂停���� $
1 true��2 �� *
IsStopping+.a��� $ TStatus_Text�� � ��$ 请稍后，正在停止���� $ Ring_Buttong�� Z�  �@�Tag�� $ Ring_Buttong�� �@�ToolTip$ 正在停止���� $
1 true��2 �� *
IsResuming+.a��� $ TStatus_Text�� � ��$ 请稍后，正在恢复���� $ Ring_Buttong�� Z�  �@�Tag�� $ Ring_Buttong�� �@�ToolTip$ 正在恢复���� "   .���  � ��
 �������  � ��
 ���� ��  � ��$ 0q���  � ��$
 Left=��� "  .R��� $��0q�
������ $��0q�._�� $��
_BackPanel��
������ $	��200��$��80��$	��200��#��z��#��z�� �� $
��Auto ��� �� $
��Auto �.+�   $��Load_Ani��$��20��$��20��$
 10,10&�$ 8Q�$ 2Q�$	 320Q�#���  $y�Visible=�$ 0.5,0.5&�3 �� *WebBrowser.IsLoading+�� $��Web_Icon��$��20��$��20��$��UniformToFill=�$��5,0,10,0q�$��Center=�$��Center=�$y�Hidden=����� *Icon+�� $��1��$��	0,0,-20,0q�$��CharacterEllipsis=�#���  
������ $��Stretch=�$��Center=�$
( Left=� �� *WebBrowser.Title+���� *WebBrowser.Title+4 �� $5 1,0.5&�$6 0,0.5&�.��� $7 Black��$8 0Q�� $7 Black��$
8 0.75Q�� $	8 0.9Q��� $+ 
Horizontal=�$��2��$��32��$��Center=�$��0q�$��Right=�.+��� $y�	Collapsed=��� $��32������ *DataContext.WebControl+ �� $ WindowL���`� 8   .1��� $' _Close���� .���� $��	Tab_Close��8�� 8�� 
�������- �� $
1 True��2 �� *
IsSelected+.a��� # �  $ 
_BackPanel�� � ���� $1 False��2 �� *WebBrowser.IsLoading+.a��� $ Load_Ani�� � y�$ Hidden=��� $ Web_Icon�� � y�$ Visible=�P�  �@J�IsMouseOver	 . . ��� $ 	Tab_Close��# �   � ���� "  ��.���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Stretch=���  � ��
 ���� ��  � ��$ 1,0,1,0q���  � �� �	#af3f3f3f��  � ��8   ��  � �� �� *
WebBrowser+��  �@��
IsSelected �� *
IsSelected+$9 TwoWay=���  � �� �� " ��.R��� #��z��#��z��._�� 
������ & �� $��3��$��3 ��� .+��� $��
Active_Bar�����	#3fe6e6e6$��0q��� $��2��$��Header�� P�  �@��
IsSelected	 . . ��� # �  $ 
Active_Bar�� � ���� "  ��.���  � ��
 ���� ��  �@�Cursor$
 Hand����  � ��$ 32����  � ��
 ���� ��  � ��$ 1q���  � ��# �  ��  � �� �� " ��.R��� #��z��#��z��#��z��#��z��#��z��._��� #��z��#��z��: P�  �@J�IsMouseOver	 . . ���  � ��
 �������  � �� �	#2FE3E3E3�� "  ��.���  � �� . ��  � ��
 �������  � ��
 �������  � ��$ 0q���  � �� �� " ��.R��� #��z��#��z��#��z��._��  �� $
��auto ��� �� $
��auto �.+��� $��PART_SubRepeatButton��#��Z; 8��  y��� *ScrollableWidth+ �� $ TemplatedParent=�8  .��F� =����        �@KL @KL �    ��� �    @T� � 	= @KL �    @B 	$ 0.5,0.5&�#��z��#��z��z��� $	< 180Q��� #= z��> .  ? .  $��1��#��z��#��z��#��z���� $��PART_AddRepeatButton��$��2��#��Z@ 8�� y��� *ScrollableWidth+ �� $ TemplatedParent=�8  .��F� =����        �@KL @KL �    ��� �    @T� � 	= @KL �    @B 	#��z��#��z���� "  ��.���  � �� �� " ��.R��� $��0q�._�� #���  & �� $��34 ��� $
��Auto ��� .+�� #���   �� $��32 ��� .+��� $��32��$��32��$ 此网页可解析��$y�Hidden=�._��� 8��  $( Center=�$��Center=�$��Center=�8�� �� 8�� $��Disabled=�$
��Auto=���. $��1��.���� $A HeaderPanel��$��1��B . C . ��.  $��Disabled=�$+ 
Horizontal=�D . #���  � $��1��#���  $��36�� �� $
��Auto ��� $��8* ��� $
��Auto ��� $��2* �$	��200���� $
��Auto �.+��� $+ 
Horizontal=�$	��1,3q�.+��� 8�� $ 后退������ *DataContext.WebControl+ �� $ WindowL���`� 8  .1��� $' _Back���� .���� 8�� 8�� $ 0.5,0.5&�z��� $	< 180Q��� 8�� $ 刷新������ *DataContext.WebControl+ �� $ WindowL���`� 8  .1��� $' _Fresh���� .���� 8��	 8��
 $��18(��� 8�� $ 前进������ *DataContext.WebControl+ �� $ WindowL���`� 8  .1��� $' _Next���� .���� 8�� 8�� �� $��
Url_Border��$��1��#��� ! 
������ $��1q�$	��1,3q�._��  �� �� $
��Auto ��� $
��Auto �.+��� $��Url_Text��$��14(�$	��4,1q�
�������$��Stretch=�$��Center=�
������ $��0q�#E �  ���� *WebBrowser.Address+$9 OneWay=�.��F �� $G Enter��H �� *DataContext.WebControl+ �� $ WindowL�I `� 8  .1��� $' _OpenUrl���� �� $
 Text�$J Url_Text��.���� $��1��$	��2,0q�8�� $ 转到������ *DataContext.WebControl+ �� $ WindowL���`� 8  .1��� $' _OpenUrl���� �� $
 Text�$J Url_Text��.���� 8�� 8�� �� $��2��8�� .���� 8�� 8�� �� $��2��$+ 
Horizontal=�$	��1,3q�.+��� 8�� $ 主页������ *DataContext.WebControl+ �� $ WindowL���`� 8  .1��� $' _OpenUrl���� �� $' http://www.baidu.com/s?��.���� 8�� 8�� �� $��
Search_Bar��$��3��$	��1,3q�$��1q�
������ #��� ! ._��  �� �� $
��Auto �.+��� $��Search_Text��$��0q�$��14(�$	��4,1q�$��Stretch=�$��Center=�
������ 
�������#E �  .��F �� $G Enter��I �� *Text+$J Search_Text��$K PropertyChanged=�H �� *DataContext.SearchControl+ �� $ WindowL�.���� $��1��$ 搜索��8�� ���� *DataContext.SearchControl+ �� $ WindowL����� $J Search_Text��$
 Text�.���� 8�� 8�� �� $+ 
Horizontal=�$��4��$	��1,3q�.+��� $��_Task_Button��8�� #���  $ 解析爬取������ *DataContext.WebControl+ �� $ WindowL�$K PropertyChanged=���`� 8  .1��� $' _NewTask���� �� $
 Text�$J Url_Text��.���� 8��  8��! ���� *
Foreground+ �� $ ButtonL��� $L PART_SelectedContentHost��$��2��$��SelectedContent��B . C . #��zM #��zN #P zO $��Stretch=�$��Stretch=� P� $. Url_Text�� �@J�	IsFocused	 . . ��� # �  $ 
Url_Border�� � ���� # �  $ Url_Text�� � ��P� $. 
Search_Bar�� �@J�IsMouseOver	 . . ��� $ 
Search_Bar��8 "  � ��P� $. Search_Text�� �@J�	IsFocused	 . . ��� $ Search_Text�� � ��
 �����P� $. _Task_Button�� � {�	 .  . ��� $ _Task_Button�� � ��
 �����