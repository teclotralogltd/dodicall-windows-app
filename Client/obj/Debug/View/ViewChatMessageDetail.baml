   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  5      7 clr-namespace:dodicall.Converterdodicall.Converter  5      )    #dodicall.View.ViewChatMessageDetail   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:dodicall.View     5      6	converter clr-namespace:dodicall.Converter     5      -   5          >�Name$&   ViewChatMessageDetailUserControl��5	   i   .��5
      � -   5        $��GridMain��5
       � RowDefinitions 5   
   �� 5      $��58 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � -   5        $��GridHeaderChat��5       � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      � $��0��6   $��	7,-2,10,0q�6#   $��36��66   $��36��6B   .+�5       �� ��6   �� *CurrentModelChat.Avatar+�� 5!      $��Right=�6   $��Bottom=�64   $��15��6O   $��15��6[   $6��0/dodicall;component/Resources/IconTray_small.png��6f   5"      � 5#      $��1��6    5$      �� 5%      $��* �6$   �� 5&      $��* �6%   .+�5(      �� -   5        $��StackPanelTitle��5(   C   $��0��6    �� Orientation$ 
Horizontal=�6*   .+�5)      �� -   5        $��ChatTitleTextBlock��5)       $
��Auto��6:   $��4,11,5,0q�6G   $
��Bold"�6Y   $��14(�6k   $��CharacterEllipsis=�6�   ��6y   �� *CurrentModelChat.CheckedTitle+6�   �� 5*      -   5        $��ChatTitleEditButton��5*      $��30��68   $��30��6C      ButtonStyleTransparent#��[  6O   $��0,0,0,0q�6   $��Stretch=�6�   $��Stretch=�6�   $y�Hidden=�6�   .��5+      �� $
��Left=�6    $��10��6;   $��10��6G   $��0,0,0,0q�6R   $,��&/dodicall;component/Resources/edit.png��6c   5,      5-      �� 5.      -   5        $��StackPanelEditTitle��5.   y   $��0��6   $ 
Horizontal=�6*   $��Stretch=�6C   $y�	Collapsed=�6a   .+�5/      �� -   5        $��ChatTitleTextBox��5/      $��0,5,15,0q�66   $
��Bold"�6H   $��14(�6Z    �� MaxLines$ 1��6h   ��6u   �� *CurrentModelChat.Title+ �� UpdateSourceTrigger$ Explicit=�6�   �� 50      -	   5        $ ��ChatTitleEditConfirmButton��50      $��16��6>   $��16��6I   $��Right=�6U   $��0,8,15,0q�6q   $/��)/dodicall;component/Resources/confirm.png��6�   51      �� 52      -
   5        $��ChatTitleEditAbortButton��52      $��16��6<   $��16��6G   $��Right=�6S   $��0,8,15,0q�6o   $-��'/dodicall;component/Resources/abort.png��6�   6�   53      � 54      -   5        $��GridModelContactStatus��54   $   $��1��6    55      �� 56      $
��Auto �6+   �� 57      .+�59      �� $ 
Horizontal=�6!   $	��Top=�6:   .+�5:      A� -   5        $��EllipseStatus��5:   I   $��3,3,5,0q�6"   $��6��64   $��6��6?   ��6^   �� *DBCurrentModelChat.ModelContactChat.ModelEnumUserBaseStatusObj.Color+�� 5;      $��5,3,0,0q�6$   
���   ���6H   �� *CACurrentModelChat.ModelContactChat.ModelEnumUserBaseStatusObj.Name+5<      �� 5=      $��1��6    $��0,3,5,5q�60   
���   �$��CharacterEllipsis=�6�   ��6T   �� *CACurrentModelChat.ModelContactChat.UserExtendedStatusForFullStatus+6�   5>      �� 5?      -   5        $!��StackPanelCountModelContact��5?   *   $��1��6   $��0,0,0,0q�6M   $ 
Horizontal=�6^   $y�Visible=�6w   .+�5@      �� -   5        $��HideOrOpenUserContactList��5@      $��10��6=   $��0,0,3,3q�6H    � Cursor$
 Hand��6Y   $��10��6g    J� RenderTransformOrigin$ .5,.5&�6s   $��/Resources/updown.png��6�   z�5A      �� 5B       �� Angle$ 90Q�6.   5D      �� 5E      -   5        $ ��TextBlockCountModelContact��5E   1   $��0,5,0,5q�6    
�������$
 Hand��6e   ��6s   �� **(CurrentModelChat.CountModelContactString+5F      5G      �� 5H      $��2��6   $��5,0,5,0q�6)   $ 
Horizontal=�6:   .+�5I      �� -   5        $��ButtonPhone��5I      $��50��6,   $��48��67   #��[  6C   $��Center=�6s   $��Center=�6�   .��5J      �� -   5        $��
ImagePhone��5J      $��19��6.   $��21��6:   5K      �� 5L      -   5        $��ButtonVideo��5L      $��48��6,   #��[  68   $��Center=�6h   $��Center=�6�   ��6�   �� *CommandComingSoon+.��5M      �� -   5        $��
ImageVideo��5M      $��20��6.   $��31��6:   5N      5O      �� 5P      $��3��6   $��5q�6(   $��1��63   
�������6=   �� 5Q      -   5        $��4��5Q      $��21��6$   $��30��60   $��/Resources/3dotMenu.png��6;   	 � ContextMenu	 5R      �� 5S      .@�5T      m� -   5         $  ViewSelectionContact_ChatMembers#���  5T   #   {�. 6p   �� 5U      -   5        $
��Sep1��5U   $   m� 5V       * &ViewChatDetail_ContextMenuChatSettings#���  6#   {�.  6e   �� 5W      -   5        $��LeaveChatSeparator��5W   $   m� 5X      -   5        $��LeaceChatContextMenuItem��5X   #    ' #ViewChatDetail_ContextMenuLeaveChat#���  6D   {�.  y�6�   �� 
 �� Path$
 CurrentModelChat.IsP2P� �� 	Converter D   >dodicall.Converter.ConverterBoolToVisibilityCollapsedInversion  m� 5Y      -   5        $��MultipleChoiceMenuItem��5Y   #    , (ViewChatDetail_ContextMenuMultipleChoice#���  6B   6�   5Z      5\      5]      �� 5^   
   $��1��6   .+�5_      �� $��1��6   
�������6#   �� 5`      -   5        $��ScrollChatUserList��5`   &   $��0��6   $��Disabled=�6@   $
��Auto=�6i   .��5a      �� -   5        $��ItemsControlChatUserList��5a      $
��Left=�6?   ��6[   �� $,
 &CurrentModelChat.ListModelContactNotMe���5b      �� 5c      .R�5d      � $��50��6(   $
��Left=�64   5e      5f      ��5h      �� 5i      .R�5j      � $��ChatMemberDetail��6@   $��20��6#   $��2,2,2,2q�6/   .+�5k   "   �� 
������� �� RadiusX$ 10��6>    �� RadiusY$ 10��6K   A� 5l   "   $��EllipseStatus��6?   $��6��6*   $��6��65   $��6,6,0,6q�6T   $
��Left=�6e   ��6�   �� *" ModelEnumUserBaseStatusObj.Color+�� 5m   "   $��TextBlockFullName��6>   $��12,0,6,0q�6,   $
��Left=�6W   ��6r   �� *
FullName+	 5n   "   �� 5o   &   $
��Left=�62   $
��Left=�6T   .@�5p   *   m� {�.  ��6E   �� *
FullName+�� 5q   *   m� 5r   *   $��SendRequest_MenuItem��63   {�.   * &ViewContactDirectoryDetail_SendRequest#���  6a   �� 5s   *   $��SendRequest_Separator��64   m� 5t   *   $��SendMessage_MenuItem��63   {�.   ) %ViewChatDetail_ContextMenuSendMessage#���  6a   m� 5u   *     WindowMain_MenuCall#���  63   {�.   m� Command 6t   �� *CommandComingSoon+�� 5v   *   m� 5w   *    & "ViewContact_ContextMenuOpenProfile#���  63   {�.  6q   �� 5x   *   m� 5y   *   $��BlockContact_MenuItem��63    *	 &ViewChatDetail_ContextMenuBlockContact#��� 	 6P   {�.  6�   m� 5z   *   $��RemoveContact_MenuItem��63    3
 /ViewChatDetail_ContextMenuRemoveFromContactList#��� 
 6Q   {�.  6�   �� 5{   *   $"��BlockRemoveContact_Separator��64   m� 5|   *   $��RemoveMemberFromChat��63   -   5         ' #WindowMain_MenuChats_RemoveFromChat#���  5|   O   {�. 6�   5}   '   5       �� Triggers 5�      �� 5�   "    �� Value$ false��6Q    �� Binding 6.   �� *IsAccessStatus+.a�5�   &   ��  �� 
TargetName$ EllipseStatus��6-    �� Property � y� �� Value$ 	Collapsed=�6^   �� 5�   &   $ TextBlockFullName��6-    � ��$ 6,0,6,0q�6^   �� 5�   &   $" BlockRemoveContact_Separator��6-    � y�$ 	Collapsed=�6m   �� 5�   &   $ RemoveContact_MenuItem��6-    � y�$ 	Collapsed=�6g   �� 5�   &   $ BlockContact_MenuItem��6-    � y�$ 	Collapsed=�6f   �� 5�   &   $ SendMessage_MenuItem��6-    � {� .  6d   5�   #   �� 5�   "   $
 true��6Q    6.   �� *IsAccessStatus+.a�5�   &   �� $ SendRequest_MenuItem��6-    � y�$ 	Collapsed=�6e   �� 5�   &   $ SendRequest_Separator��6-    � y�$ 	Collapsed=�6f   5�   #   5�      5�      5�      �� 5�      $��1��6   
�������6#   5�      � 5�   
   -   5        $��TopMenuMultipleChoice��5�      $��2��6,   $y�	Collapsed=�69   .+�5�      �� $ 
Horizontal=�6   $��10q�62   .+�5�      �� -   5        $��CheckBoxSelectAll��5�      $��Center=�64   $
��Left=�6P   6k   �� 5�        ViewChatDetail_SelectAll#���  6   5�      �� 5�      -   5        $ ��ButtonCancelMultipleChoice��5�      $��2��67   $��Right=�6D   $��25��6a   $��25��6m   $��10q�6x   #��[  6�   ��5�      �� 5�      $��12��6   $��12��6(   $��Center=�63   $��Center=�6P   $-��'/dodicall;component/Resources/abort.png��6k   5�      5�      � 5�   
   -    5        $��GridDetailListMessage��5�      $��3��6,   .+�5�      �� -!   5        $��ScrollViewerListMessage��5�   (   $��10q�6   .��5�      � .+�5�      �� -"   5        $��GridListMessage��5�   !   $��Bottom=�68   5�      5�      � 5�      -#   5        $��GridTopDateMessage��5�      $	��Top=�6-   $��50��6E   $y�Hidden=�6S   .+�5�      �� $	��Top=�6   
�������$��50��6C    J� Opacity$	 0.9Q�6Q   �� 5�      -$   5        $��RectangleDateMessage��5�      $	��Top=�68   
�������$ 14��6b   $ 14��6o   $��28��6|   $	��120��6�   $��	0,15,30,0q�6�   �� 5�      -%   5        $��TextBlockTopDateMessage��5�      $��Center=�6;   $
��Wrap=�6X    �� TextAlignment$ Center=�6l   $��	0,20,30,0q�6�   ��6�   �� *
FontSize+6�   5�      5�      �� 5�   
   -&   5        $��RectangleQuotedMessage��5�   !   $��4��6   $��1��6?   
�������$��	10,0,10,0q�6V   $y�	Collapsed=�6i   � 5�   
   $��5��6    5�      �� 5�      �� 5�      $
��auto �6#   .+�5�      �� $
��Auto=�6   $	��200��6?   .��5�      � -'   5        $��GridQuotedMessage��5�      $y�	Collapsed=�60   5�      �� 5�      -(   5        $��ButtonRemoveQuotedMessage��5�   %   $��1��6   $��Right=�6F   $��25��6c   $��25��6o   $��10q�6z   #��[  6�   $y�	Collapsed=�6�   ��5�      �� 5�      $��12��6   $��12��6(   $��Center=�63   $��Center=�6P   $-��'/dodicall;component/Resources/abort.png��6k   5�      5�      �� 5�   
   -)   5        $��RectangleMessage��5�   !   $��6��6   $��1��69   
���  ��$��	10,0,10,0q�6O   � 5�   
   -*   5        $��GridSetMessage��5�      $��7��6%    5�      �� 5�      �� 5�      $
��Auto �6#   .+�5�      �� -+   5        $��TextBoxWriteMessage��5�   &   $��0��6   $��Right=�6A    �� TextWrapping$
 Wrap=�6{    �� AcceptsReturn . " �� VerticalScrollBarVisibility$ Hidden=�6�   $��0q�6�   $ 10��6�   ��6]   �� *
FontSize+��5�      �� *'%CurrentModelChatMessage.StringContent+$ PropertyChanged=�5�   >   �� 5�      -,   5        $��TextBlockWriteMessage��5�      $��16,0,0,0q�65   $	��Top=�6G   $
��Left=�6_   
�������  ViewChatDetail_EnterMessage#���  6�   ��6�   �� *
FontSize+6�   �� 5�      $��1��6   $��5,5,10,5q�6)   $ 
Horizontal=�6;   .+�5�      �� --   5         �� 	IsEnabled .  $��40��5�   >   #��[  6J   $��Center=�6z   $��Center=�6�   .��5�      �� $��20��6   $��22��6(   $��/Resources/clip.png��63   	 5�      �� 5�      .@�5�      m� -.   5          ViewChatDetail_SendContact#���  5�   '   6]   5�      5�      � 5�      $��10,0,0,0q�6   $��29��6)   $��29��65   .+�5�      �� -/   5        $��ImageSendMessage��5�      $!��/Resources/send_message.png��64     ImageStyleButton#��[ 6Y   6�   �� 5�      -0   5        $��RectangleSendMessage��5�       $	 0.5Q�6<   
�������6J   5�      5�      5�      � 5�   
   -1   5        $��GridWriteMessageDisable��5�      $��7��6   
�������$	 0.7Q�6N   $y�Hidden=�6\   � 5�   
   -2   5        $��BottomMenuMultipleChoice��5�      $��8��6/   $y�	Collapsed=�6=    5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      .+�5�      �� -3   5        $"��ButtonMultipleForwardMessage��5�      $��0��69   $��Center=�6I   $��Center=�6f   #��[  6�   ��5�      �� 5�      $��	0,15,0,15q�6!   .+�5�      �� $��15��6    $��15��6,   $��Center=�67   $��Center=�6T   $/��)/dodicall;component/Resources/forward.png��6o   �� 5�        ViewChatDetail_Forward#���  6    5�      5�      �� 5�      -4   5        $��ButtonMultipleCopyMessage��5�      $��1��66   $��Center=�6F   $��Center=�6c   #��[  6   ��5�      �� 5�      $��	0,15,0,15q�6!   .+�5�      �� $��15��6    $��15��6,   $��Center=�67   $��Center=�6T   $,��&/dodicall;component/Resources/copy.png��6o   �� 5�        ViewChatDetail_Copy#���  6    5�      5�      �� 5�      -5   5        $��ButtonMultipleCiteMessage��5�      $��2��66   $��Center=�6F   $��Center=�6c   #��[  6   ��5�      �� 5�      $��	0,15,0,15q�6!   .+�5�      �� $��15��6    $��15��6,   $��Center=�67   $��Center=�6T   $+��%/dodicall;component/Resources/cit.png��6o   �� 5�        ViewChatDetail_Cite#���  6    5�      5�      �� 5�      -6   5        $!��ButtonMultipleExportMessage��5�      $��3��68   $��Center=�6H   $��Center=�6e   #��[  6�   ��5�      �� 5�      $��	0,15,0,15q�6!   .+�5�      �� $��15��6    $��15��6,   $��Center=�67   $��Center=�6T   $.��(/dodicall;component/Resources/export.png��6o   �� 5�        ViewChatDetail_Export#���  6    5�      5�      �� 5�      -7   5        $#��ButtonMultipleFavoriteMessage��5�      $��4��6:   $��Center=�6J   $��Center=�6g   #��[  6�   ��5�      �� 5      $��	0,15,0,15q�6!   .+�5     �� $��15��6    $��15��6,   $��Center=�67   $��Center=�6T   $/��)/dodicall;component/Resources/favGray.png��6o   �� 5       ViewChatDetail_ToFavorites#���  6    5     5     �� 5     -8   5        $!��ButtonMultipleDeleteMessage��5     $��5��68   $��Center=�6H   $��Center=�6e   #��[  6�   ��5     �� 5     $��	0,15,0,15q�6!   .+�5	     �� $��15��6    $��15��6,   $��Center=�67   $��Center=�6T   $.��(/dodicall;component/Resources/delete.png��6o   �� 5
       ViewChatDetail_Delete#���  6    5     5     5     5     5     