   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.16.2.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  7 clr-namespace:dodicall.Converterdodicall.Converter  (    "dodicall.View.ViewContactDirectory   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     6	converter clr-namespace:dodicall.Converter     .��� .+�� -   $ ��GridContactDirectoryDitail��$y�Hidden=�  � RowDefinitions  �� $
��Auto ��� .+�� -   $"��GridButtonBackToSearchResult��$��0��$��32��
�������.+��� -   $
��Left=�.����  �� Orientation$ 
Horizontal=�.+��� $��9��$��0,0,5,0q�$,��&/dodicall;component/Resources/back.png���� 
�������$��14(�$
��Bold"� (  $ViewContactDetail_BackToSearchResult#���   � -   $��
GridSearch��  �� $
��Auto ��� .+�� 
�������.+��� -   $��TextBoxSearch��$��8q�
������ ���� *Filter+ �� UpdateSourceTrigger$ PropertyChanged=��� -   $��TextBlockSearch��$��16,0,0,0q�$��Center=�$
��Left=�
�������$��14(�  ViewContactDirectory_Search#���  �� $��12��$��0,0,16,0q�$��Right=�$.��(/dodicall;component/Resources/search.png���� -   $��ImageSearchInDodicall��$��1��$	��137��$:��4/dodicall;component/Resources/search_in_dodicall.png���� -   $��ListBoxContact��$��1��$��0q�$y�Hidden=����� *ListModelContact+���� .R�� $��50������  �� ElementName$ ViewContactDirectory�� �� Path$ WidthContactItem� � ColumnDefinitions �� $��50 ��� �� $��50 �.+�� $��0��$��0,0,10,0q�$��36��$��36��.+��� ���� *Avatar+�� $��Right=�$��Bottom=�$��15��$��15��$6��0/dodicall;component/Resources/IconTray_small.png���� $��1��$
��Bold"�$��14(�$��CharacterEllipsis=�$��Center=����� *
FullName+�� $��ButtonSendRequest��-	   $��2��$��5q�  ButtonStyleTransparent#��[ $y�	Collapsed=�.���� $��24��$4��./dodicall;component/Resources/send_request.png�� �� Triggers ��  �� Value$ 0�� �� Binding �� *Id+.a��� 	 �� 
TargetName$	 ButtonSendRequest��
 �� Property
 � y� �� Value$ Visible=�   dodicall.View.ViewProcess  -
     Name$ ViewProcessMain��$��1��$y�Hidden=�