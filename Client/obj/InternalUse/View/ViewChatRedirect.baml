   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  7 clr-namespace:dodicall.Converterdodicall.Converter  $    dodicall.View.ViewChatRedirect   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     6	converter clr-namespace:dodicall.Converter     .���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� �� $
��Auto �.+�� $��46��$��0�� � ColumnDefinitions �� �� .+�� $��0��$��36��$��41��.+��� -   $��ImageContact��$��29��$��29������  �� 
TargetType" �� �� Triggers ��  �� Binding �� *UseContactList+ �� Value true.a���  �� Property � �� �� Value$7 1/dodicall;component/Resources/contacts_active.png����  �� *UseContactList+	 false.a���  � ��$9 3/dodicall;component/Resources/contacts_inactive.png�� J� InputBindings d� 	 d� Gesture$	 	LeftClicka�
 �� Command
 �� *CommandSelectContactList+�� -   $��RectangleContacts��$��0��$��Bottom=�$��2������ " �� ��  �� *UseContactList+ true.a���  � ��
 �  ����  �� *UseContactList+	 false.a���  � ��
 ���� � $��1��$��36��$��41��.+��� -   $��	ImageChat��$��29��$��29������ " �� ��  �� *UseChatList+ true.a���  � ��$3 -/dodicall;component/Resources/chat_active.png����  �� *UseChatList+	 false.a���  � ��$5 //dodicall;component/Resources/chat_inactive.png�� d� $	 	LeftClicka�
 �� *CommandSelectChatList+�� -   $��RectangleChat��$��1��$��Bottom=�$��2������ " �� ��  �� *UseChatList+ true.a���  � ��
 �  ����  �� *UseChatList+	 false.a���  � ��
 ���� � $��1�����#f2f2f2.+��� -   $��TextBoxSearch��$��8q�
������ ���� *SearchListFilter+ �� UpdateSourceTrigger$ PropertyChanged=��� -   $��TextBlockSearch��$��16,0,0,0q�$��Center=�$
��Left=�
�������$��14(�   ViewChatRedirect_Search#���   �� $��12��$��0,0,16,0q�$��Right=�$.��(/dodicall;component/Resources/search.png���� -    ��Name$ ListBoxContact��$��2��$��Visible=����� *CurrentListModelContact+���� .R�]� $��ItemContact��-   .+�� $
��Left=�$��50��$
��Left=� �� $��50 ��� .+��� $��2��� $��0��$��0,0,3,0q�$��36��$��36��.+��� ���� *Avatar+�� $��Right=�$��Bottom=�$��15��$��15��$6��0/dodicall;component/Resources/IconTray_small.png��� $��1��  �� ��  �Name$ RowDefinitionStatus���� $
��Auto �.+��� $��TextBlockFullName��$��0,8,0,0q�$
��Bold"�$��14(�$��CharacterEllipsis=�$��Center=����� *
FullName+� $��
GridStatus��$��1�� �� $
��Auto ��� .+���  �� Orientation$ 
Horizontal=�.+�A� $��EllipseStatus��$��3,7,3,5q�$��6��$��6������ *" ModelEnumUserBaseStatusObj.Color+�� $��5,5,0,5q�$��CharacterEllipsis=�$��12(����� *!ModelEnumUserBaseStatusObj.Name+�� $��1��$��0,5,0,5q�$��CharacterEllipsis=�$��12(����� *!UserExtendedStatusForFullStatus+�� $��StackPanelBlocked��$��2��$��0,0,0,5q�$��Center=�$ 
Horizontal=�.+��� $��9��$��0,0,5,0q�$/��)/dodicall;component/Resources/blocked.png���� 
�������$��12(�  ViewContact_Blocked#���   �� Triggers �� $ false�� �� *IsAccessStatus+.a���  �� 
TargetName$ RowDefinitionStatus�� � ��$ 0 ��� $ TextBlockFullName�� � ��$ 0q��� $
 true�� �� *	Blocked+.a��� $ RowDefinitionStatus�� � ��$ 0 ��� $ TextBlockFullName�� � ��$ 0q��� $ StackPanelBlocked�� � y�$ Visible=��� $ false�� �� *	Blocked+.a��� $ 
GridStatus�� � y�$ Visible=��� $ StackPanelBlocked�� � y�$ 	Collapsed=��� -	   $��ListBoxChat��$��2��$y�	Collapsed=����� *CurrentListModelChat+���� .R�� $��ItemChat��-
   $��50�� �� $��50 ��� $	��420��.+��� $��2��� $��0��$��0,0,10,0q�$��36��$��36��.+��� ���� *Avatar+�� $��Right=�$��Bottom=�$��15��$��15��$6��0/dodicall;component/Resources/IconTray_small.png��� $��1��  �� �� .+�� $��0�� �� $��* ��� $
��Auto �.+��� $��0��$��14(�$��CharacterEllipsis=�$��Center=�$
��Left=�$	��370������ *CheckedTitle+�� $��1��$��11(�
�������$��Center=����� *LastModifiedDateString+� $��1�� �� $��* ��� $
��Auto �.+��� $��TextBlockLastMessage��$��12(�$��CharacterEllipsis=�$	��400������ *LastMessage.StringContentLine+� $��1��y���  �� Path$ ExistNewMessages� �� 	Converter ;   5dodicall.Converter.ConverterBoolToVisibilityCollapsed  .+��� $$��RectangleGridCountInvateUnread��
���  ��$��16��$��16�� �� RadiusX$ 8�� �� RadiusY$ 8���� $��0,0,0,1q�
�������$��Center=�$��Center=�$��10(����� *CountMessagesUnreadString+ �� $
 true�� �� *LastMessage.Readed+.a��� $ TextBlockLastMessage�� � ��
 ������� $ false�� �� *LastMessage.Readed+.a��� $ TextBlockLastMessage�� � ��
 �   ��� $
 true�� �� *NewMessagesOver+.a��� $$ RectangleGridCountInvateUnread�� � ��$ 25���� $ false�� �� *NewMessagesOver+.a��� $$ RectangleGridCountInvateUnread�� � ��$ 16��� $��3��.+��� -   $��ButtonCancel��$��
0,20,10,20q�$��Right=�$��30��$	��130��  ViewChatRedirect_Cancel#���  