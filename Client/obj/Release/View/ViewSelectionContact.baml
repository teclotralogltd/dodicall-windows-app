   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.16.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  (    "dodicall.View.ViewSelectionContact   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     .���   � RowDefinitions  �� $
��Auto ��� �� $��5 ��� $��60 ��� $��5 ��� $��60 �.+�� $��0��
�������.+��� -   $��TextBoxSearch��$��8q�
������ ���� *ContactListFilter+ �� UpdateSourceTrigger$ PropertyChanged=��� -   $��TextBlockSearch��$��16,0,0,0q�$��Center=�$
��Left=�
�������$��14(�   ViewSelectionContact_Search#���   �� $��12��$��0,0,16,0q�$��Right=�$.��(/dodicall;component/Resources/search.png���� $��1��$
��Auto=�.���� -   $��StackPanelInviteControl���� -   $��RectangleMessage1��$��2��$��1��
�������$��	10,0,10,0q��� -   $��ScrollChatUserList��$��3��$��Disabled=�$
��Auto=�.���� -   $��ItemsControlChatUserList��$
��Left=����� *SelectedModelContactList+���� .R�� $
��Left=����� .R�� $��ChatMemberDetail��$��20��$��2,2,2,2q�.+��� 
������� �� RadiusX$ 10�� �� RadiusY$ 10����  �� Orientation$ 
Horizontal=�.+�A� $��EllipseStatus��$��6��$��6��$��6,6,0,6q�$
��Left=����� *" ModelEnumUserBaseStatusObj.Color+�� $��TextBlockFullName��$��0,0,0,0q�$
��Left=����� *
FullName+�� -   $6��0/dodicall;component/Resources/revoke_contact.png��$��15��$��15��$��Right=�$��5,0,3,0q� �� Triggers ��  �� Value$ false�� �� Binding �� *IsAccessStatus+.a���  �� 
TargetName$ EllipseStatus��	 �� Property	 � y�
 �� Value$
 	Collapsed=��� $ TextBlockFullName��	 � ��$
 6,0,6,0q��� -   $��RectangleMessage��$��4��$��1��
�������$��	10,0,10,0q�� $��6��.+��� $��10q�$ 
Horizontal=�$��Center=�.+���  ! ViewSelectionContact_Selected#���  �� -	   $��TextBlockCountSelected��$��5,0,5,0q�
���  ���� ���� *CountSelectedModelContact+�� -
   $��ButtonOk��$��10q�$��30��$	��130��$��Right=� # ViewSelectionContact_CreateChat#���  