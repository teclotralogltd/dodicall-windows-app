   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  5      7 clr-namespace:dodicall.Converterdodicall.Converter  5      (    "dodicall.View.ViewContactDirectory   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:dodicall.View     5      6	converter clr-namespace:dodicall.Converter     5      .��5
      � .+�5   
   � -   5        $ ��GridContactDirectoryDitail��5      $y�Hidden=�61     � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      .+�5      � -   5        $"��GridButtonBackToSearchResult��5   ,   $��0��6   $��32��6    
�������.+�5      �� -   5        $
��Left=�5      .��5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��9��6    $��0,0,5,0q�6+   $,��&/dodicall;component/Resources/back.png��6<   �� 5      
�������$��14(�68   $
��Bold"�6F    (  $ViewContactDetail_BackToSearchResult#���   6X   5      5      5      5      � 5   
   -   5        $��
GridSearch��5        5      �� 5      $
��Auto �6    �� 5      .+�5      � 
�������.+�5       �� -   5        $��TextBoxSearch��5       $��8q�6/   
������ ��6:   �� *Filter+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5!      -   5        $��TextBlockSearch��5!      $��16,0,0,0q�63   $��Center=�6E   $
��Left=�6`   
�������$��14(�6�     ViewContactDirectory_Search#���  6�   6�   �� 5"      $��12��6   $��0,0,16,0q�6#   $��Right=�65   $.��(/dodicall;component/Resources/search.png��6Q   5#      �� 5$      -   5        $��ImageSearchInDodicall��5$   !   $��1��6   $	��137��6>   $:��4/dodicall;component/Resources/search_in_dodicall.png��6K   �� 5%      -   5        $��ListBoxContact��5%   #   $��1��6   $��0q�6b   $y�Hidden=�6�   ��69   �� *ListModelContact+��5'      �� 5(      .R�5)      � $��50��6   ��6+   ��  �� ElementName$ ViewContactDirectory�� �� Path$ WidthContactItem� � ColumnDefinitions 5*      �� 5+   "   $��50 �63   �� 5,   "   �� 5-   "   $��50 �63   .+�5/      � $��0��6#   $��0,0,10,0q�63   $��36��6E   $��36��6Q   .+�50   "   �� ��6(   �� *Avatar+�� 51   "   $��Right=�6(   $��Bottom=�6D   $��15��6_   $��15��6k   $6��0/dodicall;component/Resources/IconTray_small.png��6v   52      �� 53      $��1��6(   $
��Bold"�68   $��14(�6J   $��CharacterEllipsis=�6X   $��Center=�6�   ��6y   �� *
FullName+6�   �� 54      $��ButtonSendRequest��6%   -	   5        $��2��54   >   $��5q�6N     ButtonStyleTransparent#��[ 6Y   $y�	Collapsed=�6�   .��55   "   �� $��24��6(   $4��./dodicall;component/Resources/send_request.png��64   56      57       �� Triggers 58      �� 59       �� Value$ 0��6A    �� Binding 6*   �� *Id+.a�5:   "   �� 	 �� 
TargetName$	 ButtonSendRequest��6)   
 �� Property
 � y� �� Value$ Visible=�6^   5;      5=      5?         dodicall.View.ViewProcess  5@      -
   5          Name$ ViewProcessMain��5@   -   $��1��6    $y�Hidden=�6F   5A      5C      5D      