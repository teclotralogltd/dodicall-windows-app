   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  5          dodicall.View.ViewUpdate   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:dodicall.View     5      .��5	      �   � RowDefinitions  5
   
   �� 5      �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��80 �6   �� 5      .+�5   
   �� $��5,0,0,0q�6   $��33��6!   $��33��6-   $8��2/dodicall;component/Resources/icon_information.png��68   �� 5   
   -   5        $��StackPanelMessage��5   %   $��1��6   $
��Left=�6>   $��Center=�6Y   .+�5      �� $��14(�6   $
��Wrap=�6&      ViewUpdate_MessageUpdate1#���   6:   �� 5      $��14(�6   $
��Wrap=�6&     ViewUpdate_MessageUpdate2#���  6:   5      �� 5   
   -   5        $��StackPanelWarning��5   %   $��1��6   $
��Left=�6>   $��Center=�6Y   $y�Hidden=�6t   .+�5      �� $
��Left=�6   $��Center=�63   $��14(�6N   $
��Wrap=�6\    # ViewUpdate_MessageUpdateWarning#���  6p   �� 5      $
��Left=�6   $��Center=�63   $��14(�6N   
���  ��$
��Wrap=�6m    $  ViewUpdate_MessageUpdateWarning2#���  6�   5      �� 5   
   -   5        $��TextBlockDownloading��5   $   $��1��6   $
��Left=�6@   $��Center=�6[   $��14(�6v    ! ViewUpdate_MessageDownloading#���  6�   $y�Hidden=�6�   �� 5   
   -   5        $��TextBlockUpdateFail��5   $   $��1��6   $
��Left=�6?   $��Center=�6Z   $��14(�6u     ViewUpdate_StartUpdateFail#���  6�   $y�Hidden=�6�   �� 5   
   -   5        $��ButtonOk��5   2   $��1��6   $��2��6   $��Ok��6B   $��30��6O   $	��100��6[   $y�Hidden=�6g   6{   �� 5   
   -   5        $$��StackPanelButtonDownloanCancel��5   2   $��1��6   $��2��6"    �� Orientation$ 
Horizontal=�6X   $��Right=�6q   .+�5      �� -   5          ViewUpdate_ButtonUpdate#���  5      $��30��6I   $	��150��6U   $��15q�6a   6m   �� 5       -   5           ViewUpdate_ButtonUpdateLater#���  5       $��30��6N   $	��180��6Z   $��
0,15,15,15q�6f   6z   5!      5"      5#      