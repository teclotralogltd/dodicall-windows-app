   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  7 clr-namespace:dodicall.Converterdodicall.Converter  )    #dodicall.View.ViewChatMessageDetail   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     6	converter clr-namespace:dodicall.Converter     -     >�Name$&   ViewChatMessageDetailUserControl��.��� -   $��GridMain�� � RowDefinitions �� $��58 ��� $
��Auto ��� $
��Auto ��� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -   $��GridHeaderChat�� � ColumnDefinitions �� $
��Auto ��� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��$��	7,-2,10,0q�$��36��$��36��.+��� ���� *CurrentModelChat.Avatar+�� $��Right=�$��Bottom=�$��15��$��15��$6��0/dodicall;component/Resources/IconTray_small.png��� $��1�� �� $��* ��� $��* �.+��� -   $��StackPanelTitle��$��0�� �� Orientation$ 
Horizontal=�.+��� -   $��ChatTitleTextBlock��$
��Auto��$��4,11,5,0q�$
��Bold"�$��14(�$��CharacterEllipsis=����� *CurrentModelChat.CheckedTitle+�� -   $��ChatTitleEditButton��$��30��$��30��   ButtonStyleTransparent#��[  $��0,0,0,0q�$��Stretch=�$��Stretch=�$y�Hidden=�.���� $
��Left=�$��10��$��10��$��0,0,0,0q�$,��&/dodicall;component/Resources/edit.png���� -   $��StackPanelEditTitle��$��0��$ 
Horizontal=�$��Stretch=�$y�	Collapsed=�.+��� -   $��ChatTitleTextBox��$��0,5,15,0q�$
��Bold"�$��14(� �� MaxLines$ 1������ *CurrentModelChat.Title+ �� UpdateSourceTrigger$ Explicit=��� -	   $ ��ChatTitleEditConfirmButton��$��16��$��16��$��Right=�$��0,8,15,0q�$/��)/dodicall;component/Resources/confirm.png���� -
   $��ChatTitleEditAbortButton��$��16��$��16��$��Right=�$��0,8,15,0q�$-��'/dodicall;component/Resources/abort.png��� -   $��GridModelContactStatus��$��1�� �� $
��Auto ��� .+��� $ 
Horizontal=�$	��Top=�.+�A� -   $��EllipseStatus��$��3,3,5,0q�$��6��$��6������ *DBCurrentModelChat.ModelContactChat.ModelEnumUserBaseStatusObj.Color+�� $��5,3,0,0q�
���   ����� *CACurrentModelChat.ModelContactChat.ModelEnumUserBaseStatusObj.Name+�� $��1��$��0,3,5,5q�
���   �$��CharacterEllipsis=����� *CACurrentModelChat.ModelContactChat.UserExtendedStatusForFullStatus+�� -   $!��StackPanelCountModelContact��$��1��$��0,0,0,0q�$ 
Horizontal=�$y�Visible=�.+��� -   $��HideOrOpenUserContactList��$��10��$��0,0,3,3q� � Cursor$
 Hand��$��10�� J� RenderTransformOrigin$ .5,.5&�$��/Resources/updown.png��z���  �� Angle$ 90Q��� -   $ ��TextBlockCountModelContact��$��0,5,0,5q�
�������$
 Hand������ **(CurrentModelChat.CountModelContactString+�� $��2��$��5,0,5,0q�$ 
Horizontal=�.+��� -   $��ButtonPhone��$��50��$��48��#��[  $��Center=�$��Center=�.���� -   $��
ImagePhone��$��19��$��21���� -   $��ButtonVideo��$��48��#��[  $��Center=�$��Center=����� *CommandComingSoon+.���� -   $��
ImageVideo��$��20��$��31���� $��3��$��5q�$��1��
��������� -   $��4��$��21��$��30��$��/Resources/3dotMenu.png��	 � ContextMenu	 �� .@�m� -    $  ViewSelectionContact_ChatMembers#���  {�. �� -   $
��Sep1��m�  * &ViewChatDetail_ContextMenuChatSettings#���  {�.  �� -   $��LeaveChatSeparator��m� -   $��LeaceChatContextMenuItem�� ' #ViewChatDetail_ContextMenuLeaveChat#���  {�.  y��� 
 �� Path$
 CurrentModelChat.IsP2P� �� 	Converter D   >dodicall.Converter.ConverterBoolToVisibilityCollapsedInversion  m� -   $��MultipleChoiceMenuItem�� , (ViewChatDetail_ContextMenuMultipleChoice#���  �� $��1��.+��� $��1��
��������� -   $��ScrollChatUserList��$��0��$��Disabled=�$
��Auto=�.���� -   $��ItemsControlChatUserList��$
��Left=����� $,
 &CurrentModelChat.ListModelContactNotMe����� .R�� $��50��$
��Left=����� .R�� $��ChatMemberDetail��$��20��$��2,2,2,2q�.+��� 
������� �� RadiusX$ 10�� �� RadiusY$ 10��A� $��EllipseStatus��$��6��$��6��$��6,6,0,6q�$
��Left=����� *" ModelEnumUserBaseStatusObj.Color+�� $��TextBlockFullName��$��12,0,6,0q�$
��Left=����� *
FullName+	 �� $
��Left=�$
��Left=�.@�m� {�.  ���� *
FullName+�� m� $��SendRequest_MenuItem��{�.   * &ViewContactDirectoryDetail_SendRequest#���  �� $��SendRequest_Separator��m� $��SendMessage_MenuItem��{�.   ) %ViewChatDetail_ContextMenuSendMessage#���  m�   WindowMain_MenuCall#���  {�.   m� Command �� *CommandComingSoon+�� m�  & "ViewContact_ContextMenuOpenProfile#���  {�.  �� m� $��BlockContact_MenuItem�� *	 &ViewChatDetail_ContextMenuBlockContact#��� 	 {�.  m� $��RemoveContact_MenuItem�� 3
 /ViewChatDetail_ContextMenuRemoveFromContactList#��� 
 {�.  �� $"��BlockRemoveContact_Separator��m� $��RemoveMemberFromChat��-    ' #WindowMain_MenuChats_RemoveFromChat#���  {�.  �� Triggers ��  �� Value$ false�� �� Binding �� *IsAccessStatus+.a���  �� 
TargetName$ EllipseStatus�� �� Property � y� �� Value$ 	Collapsed=��� $ TextBlockFullName�� � ��$ 6,0,6,0q��� $" BlockRemoveContact_Separator�� � y�$ 	Collapsed=��� $ RemoveContact_MenuItem�� � y�$ 	Collapsed=��� $ BlockContact_MenuItem�� � y�$ 	Collapsed=��� $ SendMessage_MenuItem�� � {� .  �� $
 true�� �� *IsAccessStatus+.a��� $ SendRequest_MenuItem�� � y�$ 	Collapsed=��� $ SendRequest_Separator�� � y�$ 	Collapsed=��� $��1��
�������� -   $��TopMenuMultipleChoice��$��2��$y�	Collapsed=�.+��� $ 
Horizontal=�$��10q�.+��� -   $��CheckBoxSelectAll��$��Center=�$
��Left=���   ViewChatDetail_SelectAll#���  �� -   $ ��ButtonCancelMultipleChoice��$��2��$��Right=�$��25��$��25��$��10q�#��[  ���� $��12��$��12��$��Center=�$��Center=�$-��'/dodicall;component/Resources/abort.png��� -    $��GridDetailListMessage��$��3��.+��� -!   $��ScrollViewerListMessage��$��10q�.��� .+��� -"   $��GridListMessage��$��Bottom=�� -#   $��GridTopDateMessage��$	��Top=�$��50��$y�Hidden=�.+��� $	��Top=�
�������$��50�� J� Opacity$	 0.9Q��� -$   $��RectangleDateMessage��$	��Top=�
�������$ 14��$ 14��$��28��$	��120��$��	0,15,30,0q��� -%   $��TextBlockTopDateMessage��$��Center=�$
��Wrap=� �� TextAlignment$ Center=�$��	0,20,30,0q����� *
FontSize+�� -&   $��RectangleQuotedMessage��$��4��$��1��
�������$��	10,0,10,0q�$y�	Collapsed=�� $��5�� �� �� $
��auto �.+��� $
��Auto=�$	��200��.��� -'   $��GridQuotedMessage��$y�	Collapsed=��� -(   $��ButtonRemoveQuotedMessage��$��1��$��Right=�$��25��$��25��$��10q�#��[  $y�	Collapsed=����� $��12��$��12��$��Center=�$��Center=�$-��'/dodicall;component/Resources/abort.png���� -)   $��RectangleMessage��$��6��$��1��
���  ��$��	10,0,10,0q�� -*   $��GridSetMessage��$��7�� �� �� $
��Auto �.+��� -+   $��TextBoxWriteMessage��$��0��$��Right=� �� TextWrapping$
 Wrap=� �� AcceptsReturn . " �� VerticalScrollBarVisibility$ Hidden=�$��0q�$ 10������ *
FontSize+���� *'%CurrentModelChatMessage.StringContent+$ PropertyChanged=��� -,   $��TextBlockWriteMessage��$��16,0,0,0q�$	��Top=�$
��Left=�
�������  ViewChatDetail_EnterMessage#���  ���� *
FontSize+�� $��1��$��5,5,10,5q�$ 
Horizontal=�.+��� --    �� 	IsEnabled .  $��40��#��[  $��Center=�$��Center=�.���� $��20��$��22��$��/Resources/clip.png��	 �� .@�m� -.     ViewChatDetail_SendContact#���  � $��10,0,0,0q�$��29��$��29��.+��� -/   $��ImageSendMessage��$!��/Resources/send_message.png��  ImageStyleButton#��[ �� -0   $��RectangleSendMessage��$	 0.5Q�
�������� -1   $��GridWriteMessageDisable��$��7��
�������$	 0.7Q�$y�Hidden=�� -2   $��BottomMenuMultipleChoice��$��8��$y�	Collapsed=� �� �� �� �� �� �� .+��� -3   $"��ButtonMultipleForwardMessage��$��0��$��Center=�$��Center=�#��[  ���� $��	0,15,0,15q�.+��� $��15��$��15��$��Center=�$��Center=�$/��)/dodicall;component/Resources/forward.png����   ViewChatDetail_Forward#���  �� -4   $��ButtonMultipleCopyMessage��$��1��$��Center=�$��Center=�#��[  ���� $��	0,15,0,15q�.+��� $��15��$��15��$��Center=�$��Center=�$,��&/dodicall;component/Resources/copy.png����   ViewChatDetail_Copy#���  �� -5   $��ButtonMultipleCiteMessage��$��2��$��Center=�$��Center=�#��[  ���� $��	0,15,0,15q�.+��� $��15��$��15��$��Center=�$��Center=�$+��%/dodicall;component/Resources/cit.png����   ViewChatDetail_Cite#���  �� -6   $!��ButtonMultipleExportMessage��$��3��$��Center=�$��Center=�#��[  ���� $��	0,15,0,15q�.+��� $��15��$��15��$��Center=�$��Center=�$.��(/dodicall;component/Resources/export.png����   ViewChatDetail_Export#���  �� -7   $#��ButtonMultipleFavoriteMessage��$��4��$��Center=�$��Center=�#��[  ���� $��	0,15,0,15q�.+��� $��15��$��15��$��Center=�$��Center=�$/��)/dodicall;component/Resources/favGray.png����   ViewChatDetail_ToFavorites#���  �� -8   $!��ButtonMultipleDeleteMessage��$��5��$��Center=�$��Center=�#��[  ���� $��	0,15,0,15q�.+��� $��15��$��15��$��Center=�$��Center=�$.��(/dodicall;component/Resources/delete.png����   ViewChatDetail_Delete#���  