   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  7 clr-namespace:dodicall.Converterdodicall.Converter  $    dodicall.View.ViewCallRedirect   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     7
converters clr-namespace:dodicall.Converter     -   $��UserControlCallRedirect��.��� -   $��GridMain��  � RowDefinitions  �� $
��Auto ��� $
��Auto ��� .+�� $��46��$��0�� � ColumnDefinitions �� �� �� .+�� $��0��$��36��$��41��.+��� -   $��ImageContact��$��29��$��29������  �� 
TargetType" �� �� Triggers ��  �� Binding �� *ContactList+ �� Value true.a���  �� Property � �� �� Value$7 1/dodicall;component/Resources/contacts_active.png����  �� *ContactList+	 false.a���  � ��$9 3/dodicall;component/Resources/contacts_inactive.png�� J� InputBindings d� 	 d� Gesture$	 	LeftClicka�
 �� Command
 �� *CommandSelectContactList+�� -   $��RectangleContacts��$��0��$��Bottom=�$��2������ " �� ��  �� *ContactList+ true.a���  � ��
 �  ����  �� *ContactList+	 false.a���  � ��
 ���� � $��1��$��36��$��41��.+��� -   $��ImageHistory��$��29��$��29������ " �� ��  �� *HistoryList+ true.a���  � ��$6 0/dodicall;component/Resources/history_active.png����  �� *HistoryList+	 false.a���  � ��$8 2/dodicall;component/Resources/history_inactive.png�� d� $	 	LeftClicka�
 �� *CommandSelectHistoryList+�� -   $��RectangleHistory��$��1��$��Bottom=�$��2������ " �� ��  �� *HistoryList+ true.a���  � ��
 �  ����  �� *HistoryList+	 false.a���  � ��
 ���� � $��2��$��36��$��41��.+��� -   $��ImageDialpad��$��29��$��29������ " �� ��  �� *DialpadList+ true.a���  � ��$6 0/dodicall;component/Resources/dialpad_active.png����  �� *DialpadList+	 false.a���  � ��$8 2/dodicall;component/Resources/dialpad_inactive.png�� d� $	 	LeftClicka�
 �� *CommandSelectDialpadList+�� -   $��RectangleDialpad��$��2��$��Bottom=�$��2������ " �� ��  �� *DialpadList+ true.a���  � ��
 �  ����  �� *DialpadList+	 false.a���  � ��
 ���� �� $��1�����#f2f2f2.+�� .+��� -	   $��TextBoxSearch��$��8q�
������ ���� *ContactListFilter+ �� UpdateSourceTrigger$ PropertyChanged=��� -
   $��TextBlockSearch��$��16,0,0,0q�$��Center=�$
��Left=�
�������$��14(�   ViewCallRedirect_Search#���   �� $��12��$��0,0,16,0q�$��Right=�$.��(/dodicall;component/Resources/search.png��� $��2�����#f2f2f2  �� $
��Auto ��� .+��� -   $��
10,15,0,15q�$
��Left=�$��Center=� �� Orientation$ 
Horizontal=�.+��� 
�������$��14(�$
��Bold"����� *CurrentApplyFilterName+�� -   $��ImageFilterContact��$��5,5,0,5q�$��6��$/��)/dodicall;component/Resources/up_down.png��$��Bottom=� � ContextMenu �� -   $��ContextMenuFilterContact��.@�m� -     ViewCallRedirect_AllContact#���  m� -    $  ViewCallRedirect_DodicallContact#���  m� -    ! ViewCallRedirect_SavedContact#���  m� -    # ViewCallRedirect_BlockedContact#���  m� -    ! ViewCallRedirect_WhiteContact#���  � $��1��  �� �� $
��Auto �.+��� -   $��ListBoxContact��$��0��$��Visible=�����  �� ElementName$ UserControlCallRedirect�� �� Path$ CurrentListModelContact����� .R��   �� $
��Auto ��� $
��Auto �.+��� -   $��0��  ButtonStyleTransparent#��[ $��Stretch=�.��� $��Stretch=�.+�� $��50��$
��Left=� �� $��50 ��� .+��� $��2��� $��0��$��0,0,3,0q�$��36��$��36��.+��� ���� *ModelContactObj.Avatar+�� $��Right=�$��Bottom=�$��15��$��15��$6��0/dodicall;component/Resources/IconTray_small.png��y��� $  ModelContactObj.IsDodicall� �� 	Converter ;   5dodicall.Converter.ConverterBoolToVisibilityCollapsed  � $��1��  �� ��  �Name$ RowDefinitionStatus���� $
��Auto �.+��� $��TextBlockFullName��$��0,8,0,0q�$
��Bold"�$��14(�$��CharacterEllipsis=�$��Center=����� *ModelContactObj.FullName+� $��
GridStatus��$��1�� �� $
��Auto ��� .+��� $ 
Horizontal=�.+�A� $��EllipseStatus��$��3,7,3,5q�$��6��$��6������ *20ModelContactObj.ModelEnumUserBaseStatusObj.Color+�� $��5,5,0,5q�$��CharacterEllipsis=�$��12(����� *1/ModelContactObj.ModelEnumUserBaseStatusObj.Name+�� $��1��$��0,5,0,5q�$��CharacterEllipsis=�$��12(����� *1/ModelContactObj.UserExtendedStatusForFullStatus+�� $��StackPanelBlocked��$��2��$��0,0,0,5q�$��Center=�$ 
Horizontal=�.+��� $��9��$��0,0,5,0q�$/��)/dodicall;component/Resources/blocked.png���� 
�������$��12(�  ViewContact_Blocked#���  � $��Right=�.+��� $��	ImageDown��$,��&/dodicall;component/Resources/down.png������ " �� ��  �� *
IsExpand+	 false.a���  � y�$ Visible=���  �� *
IsExpand+ true.a���  � y�$ 	Collapsed=��� $��ImageUp��$*��$/dodicall;component/Resources/up.png������ " �� ��  �� *
IsExpand+ true.a���  � y�$ Visible=���  �� *
IsExpand+	 false.a���  � y�$ 	Collapsed=��� $��StackPanelContact��-   $��1��y��� $ ImageUp��$ 
Visibility� �� Triggers �� $ false�� �� * ModelContactObj.IsAccessStatus+.a���  �� 
TargetName$ RowDefinitionStatus�� � ��$ 0 ��� $ TextBlockFullName�� � ��$ 0q��� $
 true�� �� *ModelContactObj.Blocked+.a��� $ RowDefinitionStatus�� � ��$ 0 ��� $ TextBlockFullName�� � ��$ 0q��� $ StackPanelBlocked�� � y�$ Visible=��� $ false�� �� *ModelContactObj.Blocked+.a��� $ 
GridStatus�� � y�$ Visible=��� $ StackPanelBlocked�� � y�$ 	Collapsed=����� " ��  ListBoxItemStyle �� BasedOn# [ .���  � |� .  ��  � ��$ 0q��� $��1��.+��� -   $��
0,20,10,20q�$��Right=�$��30��$	��130�� 	 ViewCallRedirect_Cancel#��� 	 