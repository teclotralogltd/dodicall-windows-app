   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.16.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  7 clr-namespace:dodicall.Converterdodicall.Converter      dodicall.View.ViewContact   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     7
converters clr-namespace:dodicall.Converter     Hi:http://schemas.microsoft.com/expression/2010/interactivity     -   $��ViewContactUserControl��.���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� �� $
��Auto ��� $
��Auto �.+�� $��46�� � ColumnDefinitions �� �� �� �� .+�� -   $��0��$��36��$��41��.+��� -   $��ImageContact��$��29��$��29��$7��1/dodicall;component/Resources/contacts_active.png��� -   $��GridCountInvateUnread��$��Right=�$	��Top=�.+��� -   $$��RectangleGridCountInvateUnread��
���  ��$��16��$��16�� �� RadiusX$ 8�� �� RadiusY$ 8���� $��0,0,0,1q�
�������$��Center=�$��Center=�$��10(����� *CountInvateUnreadString+�� -   $��RectangleContact��$��0��$��Bottom=�
������ $��2��� -   $��1��$��36��$��41��.+��� -   $��ImageHistory��$��29��$��28��$8��2/dodicall;component/Resources/history_inactive.png��� -	   $��GridCountUnreadCall��$��Right=�$	��Top=�.+��� -
   $"��RectangleGridCountUnreadCall��
���  ��$��16��$��16��$ 8��$ 8���� -   $��LabelGridCountUnreadCall��$��0,0,0,1q�
�������$��Center=�$��Center=�$��10(��� -   $��RectangleHistory��$��1��$��Bottom=�
������ $��2��� -   $��2��$��36��$��45��.+��� -   $��	ImageChat��$��29��$��29��$5��//dodicall;component/Resources/chat_inactive.png��� -   $��GridCountUnreadMessage��$��Right=�$	��Top=�.+��� -   $%��RectangleGridCountUnreadMessage��
���  ��$��16��$��16��$ 8��$ 8���� -   $!��LabelGridCountUnreadMessage��$��0,0,0,1q�
�������$��Center=�$��Center=�$��10(��� $��0,0,2,2q�$��12��$��13��$��Right=�$��Bottom=�$5��//dodicall;component/Resources/no_connection.png������  �� 
TargetType" �� �� Triggers ��  �� Binding �� *&$ModelConnectStateObj.ChatStatus.Code+ �� Value 0.a���  �� Property � y�	 �� Value$	 Hidden=���  �� *&$ModelConnectStateObj.ChatStatus.Code+ 1.a���  � y�$	 Visible=���  �� *&$ModelConnectStateObj.ChatStatus.Code+ 2.a���  � y�$	 Hidden=���  �� *&$ModelConnectStateObj.ChatStatus.Code+ 3.a���  � y�$	 Hidden=��� $��0,0,2,2q�$��13��$��13��$��Right=�$��Bottom=�$1��+/dodicall;component/Resources/connected.png������ " �� ��  �� *&$ModelConnectStateObj.ChatStatus.Code+ 0.a���  � y�$	 Hidden=���  �� *&$ModelConnectStateObj.ChatStatus.Code+ 1.a���  � y�$	 Hidden=���  �� *&$ModelConnectStateObj.ChatStatus.Code+ 2.a���  � y�$	 Hidden=���  �� *&$ModelConnectStateObj.ChatStatus.Code+ 3.a���  � y�$	 Visible=��� -   $��RectangleChat��$��2��$��Bottom=�
������ $��2��� -   $��3��$��36��$��45��.+��� -   $��ImageDialpad��$��29��$��29��$8��2/dodicall;component/Resources/dialpad_inactive.png���� $��0,0,2,2q�$��12��$��13��$��Right=�$��Bottom=�$5��//dodicall;component/Resources/no_connection.png������ " �� ��  �� *&$ModelConnectStateObj.VoipStatus.Code+ 0.a���  � y�$	 Hidden=���  �� *&$ModelConnectStateObj.VoipStatus.Code+ 1.a���  � y�$	 Visible=���  �� *&$ModelConnectStateObj.VoipStatus.Code+ 2.a���  � y�$	 Hidden=���  �� *&$ModelConnectStateObj.VoipStatus.Code+ 3.a���  � y�$	 Hidden=��� $��0,0,2,2q�$��13��$��13��$��Right=�$��Bottom=�$1��+/dodicall;component/Resources/connected.png������ " �� ��  �� *&$ModelConnectStateObj.VoipStatus.Code+ 0.a���  � y�$	 Hidden=���  �� *&$ModelConnectStateObj.VoipStatus.Code+ 1.a���  � y�$	 Hidden=���  �� *&$ModelConnectStateObj.VoipStatus.Code+ 2.a���  � y�$	 Hidden=���  �� *&$ModelConnectStateObj.VoipStatus.Code+ 3.a���  � y�$	 Visible=��� -   $��RectangleDialpad��$��3��$��Bottom=�
������ $��2��� -   $��GridContact��$��2��
�������y��� 
 �� Path$
 ContactList� �� 	Converter 8   2dodicall.Converter.ConverterBoolToVisibilityHidden    �� $
��Auto ��� $
��Auto ��� �� $
��Auto �.+�� .+��� -   $��TextBoxSearch��$��8q�
������ ���� *ContactListFilter+ �� UpdateSourceTrigger$ PropertyChanged=��� -   $��TextBlockSearch��$��16,0,0,0q�$��Center=�$
��Left=�
�������$��14(�   ViewContact_Search#���   �� $��12��$��0,0,16,0q�$��Right=�$.��(/dodicall;component/Resources/search.png��� $��1��$��38�� �� �� .+�� 
�������.+��� -   $��Center=�$��Center=� �� Orientation$ 
Horizontal=�.+��� 
�������$��14(�$
��Bold"����� *CurrentApplyFilterName+�� -   $��ImageFilterContact��$��5,5,0,5q�$��6��$/��)/dodicall;component/Resources/up_down.png��$��Bottom=� � ContextMenu �� -   $��ContextMenuFilterContact��.@�m� -     ViewContact_AllContact#���  m� -     ViewContact_DodicallContact#���  m� -     ViewContact_SavedContact#���  m� -     ViewContact_BlockedContact#���  m� -      ViewContact_WhiteContact#���  �� $��1��$��Center=�$��Center=�
�������$��14(�$
��Bold"�  ViewContact_GroupContact#���  �� -!   $��ListBoxContact��$��2��$��0q����� *CurrentListModelContact+ �� SelectedItem �� *CurrentModelContact+���� .R�� -"   $��50��$
��Left=�����  �� ElementName$ ViewContactUserControl��$
 WidthContactItem� �� .@�m� $��ContextMenuCall��-#     ViewContact_ContextMenuCall#���  m� -$    ' #ViewContact_ContextMenuWriteMessage#���  {��� *
IsDodicall+m� $��ContextMenuEdit�� 	 ViewContact_ContextMenuEdit#��� 	 {�.  m� $��ContextMenuAddToWhite��-%    %
 !ViewContact_ContextMenuAddToWhite#��� 
 ���� " m� ��  �� *White+ true.a���  � y�$	 	Collapsed=���  �� *White+	 false.a���  � y�$	 Visible=�m� $ ��ContextMenuDeleteFromWhite��-&    * &ViewContact_ContextMenuDeleteFromWhite#���  ���� " m� ��  �� *White+ true.a���  � y�$	 Visible=���  �� *White+	 false.a���  � y�$	 	Collapsed=�m� $��ContextMenuBlock��-'      ViewContact_ContextMenuBlock#���  ���� " m� ��  �� *	Blocked+ true.a���  � y�$	 	Collapsed=���  �� *	Blocked+	 false.a���  � y�$	 Visible=�m� $��ContextMenuUnblock��-(    " ViewContact_ContextMenuUnblock#���  ���� " m� ��  �� *	Blocked+ true.a���  � y�$	 Visible=���  �� *	Blocked+	 false.a���  � y�$	 	Collapsed=�m� -)    ! ViewContact_ContextMenuDelete#���   �� $��50 ��� .+��� $��2��� $��0��$��0,0,3,0q�$��36��$��36��.+��� ���� *Avatar+�� $��Right=�$��Bottom=�$��15��$��15��$6��0/dodicall;component/Resources/IconTray_small.png��y��� $
 
IsDodicall� ;   5dodicall.Converter.ConverterBoolToVisibilityCollapsed  � $��1��  �� ��  �Name$ RowDefinitionStatus���� $
��Auto �.+��� $��TextBlockFullName��$��0,8,0,0q�$
��Bold"�$��14(�$��CharacterEllipsis=�$��Center=����� *
FullName+� $��
GridStatus��$��1�� �� $
��Auto ��� .+��� $ 
Horizontal=�.+�A� $��EllipseStatus��$��3,7,3,5q�$��6��$��6������ *" ModelEnumUserBaseStatusObj.Color+�� $��5,5,0,5q�$��CharacterEllipsis=�$��12(����� *!ModelEnumUserBaseStatusObj.Name+�� $��1��$��0,5,0,5q�$��CharacterEllipsis=�$��12(����� *!UserExtendedStatusForFullStatus+�� $��StackPanelBlocked��$��2��$��0,0,0,5q�$��Center=�$ 
Horizontal=�.+��� $��9��$��0,0,5,0q�$/��)/dodicall;component/Resources/blocked.png���� 
�������$��12(�  ViewContact_Blocked#���   �� Triggers �� $ false�� �� *IsAccessStatus+.a���  �� 
TargetName$ RowDefinitionStatus�� � ��$	 0 ��� $ TextBlockFullName�� � ��$	 0q��� $
 true�� �� *	Blocked+.a��� $ RowDefinitionStatus�� � ��$	 0 ��� $ TextBlockFullName�� � ��$	 0q��� $ StackPanelBlocked�� � y�$	 Visible=��� $ false�� �� *	Blocked+.a��� $ 
GridStatus�� � y�$	 Visible=��� $ StackPanelBlocked�� � y�$	 	Collapsed=��� $��3��
�������.+��� 
�������$��1��� -*   $��GridRequestInvite��.+��� -+   $��30��$��Stretch=�  ButtonStyleTransparent#��[ .���  �� $
��Auto ��� �� $
��Auto �.+��� $��0,0,10,0q�$��14��$��20��$3��-/dodicall;component/Resources/invitations.png���� $��1��$��Center=�
�������$��14(�  ViewContact_RequestInvite#���  ]� -,   $ ��DockPanelCountInvateUnread��$��2��.+�� .+��� --   $)��#RectangleDockPanelCountInvateUnread��
���  ��$��16��$��16��$ 8��$ 8���� $��0,0,0,1q�
�������$��Center=�$��Center=�$��10(����� *CountInvateUnreadString+� -.   $��GridFindNewContact��.+��� -/   $��30��$
��Left=�#��[ .���� $ 
Horizontal=�$��30��.+��� $��0,0,10,0q�$��14��$��20��$8��2/dodicall;component/Resources/find_new contact.png���� $��Center=�
�������$��14(�  ViewContact_FindNewContact#���  � -0   $��GridCall��$��2��y��� $
 HistoryList�   � -1   $��GridChat��$��2��y��� $
 ChatList�   � -2   $��GridDialpad��$��2��y��� $
 DialpadList�   