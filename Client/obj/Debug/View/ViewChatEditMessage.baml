   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  5      '    !dodicall.View.ViewChatEditMessage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:dodicall.View     5      -   5        .��5	      �   � RowDefinitions  5
   
   �� 5      $��3* �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   � $��0��6     5      �� 5      �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� -   5        $��TextEditMessage��5   #   $��0��6   $��10q�6:   $��14(�6F    �� TextWrapping$
 Wrap=�6T   
�������$��0q�6{   6�   �� 5      -   5        $��RectangleQuotedMessage��5   %   $��1��6   $��1��6C   
�������$y�	Collapsed=�6Z   � 5      -   5        $!��GridVisibilityQuotedMessage��5       $��2��6   $��10q�6C   $y�	Collapsed=�6O    � ColumnDefinitions 5      �� 5      �� 5      $
��auto �6'   .+�5      �� $
��Auto=�6   $	��100��6C   .��5      � -   5        $��GridQuotedMessage��5      5      �� 5      -   5        $��ButtonRemoveQuotedMessage��5   )   $��1��6   $��Right=�6J   $��25��6g   $��25��6s   $��10,0,0,0q�6~      ButtonStyleTransparent#��[  6�   ��5       �� 5!      $��12��6    $��12��6,   $��Center=�67   $��Center=�6T   $-��'/dodicall;component/Resources/abort.png��6o   5#      5&      5)      �� 5*   
   $��1��6   � 5+   
   $��2��6     5,      �� 5-      $��* �6    �� 5.      $
��auto �6    �� 5/      $��* �6    .+�51      � $��1��6    52      �� 53      $	��20* �6'   �� 54      $��* �6'   �� 55      $��8* �6'   �� 56      $��2* �6'   �� 57      $��8* �6'   �� 58      $��* �6'   .+�5:      �� -   5        $��ButtonCancel��5:      $��4��6-     ViewChatEditMessage_Cancel#���  6=   6t   �� 5;      -   5        $��
ButtonSave��5;      $��2��6+     ViewChatEditMessage_Save#���  6;   6p   5=      5>      5?      5@      