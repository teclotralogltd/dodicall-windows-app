   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  5      ,    &dodicall.View.ViewContactRequestInvite   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:dodicall.View     5      .��5	      � .+�5
   
   � -   5        $$��GridContactRequestInviteDetail��5
      $y�Hidden=�65     � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      .+�5      � -   5        $��GridButtonBack��5       $��0��6   $��32��66   
�������.+�5      �� -   5        $
��Left=�5      .��5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��9��6    $��0,0,5,0q�6+   $,��&/dodicall;component/Resources/back.png��6<   �� 5      
�������$��14(�68   $
��Bold"�6F      ViewContactDetail_Back#���   6X   5      5      5      5      � 5   
   -   5        $��GridContactRequestInvite��5        5      �� 5      $��46 �6    �� 5      .+�5      � 
�������.+�5      �� $��8q�6   
�������$��14(�6;   $
��Bold"�6I    * &ViewContactRequestInvite_RequestInvite#���  6[   $��Center=�6�   5      �� 5!      $��1��6   .��5"      �� .+�5#      �� -   5        $��StackPanelInvite��5#   !   .+�5$      �� $ 
Horizontal=�6%   .+�5%      �� $��8q�6(   
���  ��$��14(�6D   $
��Bold"�6R    # ViewContactRequestInvite_Invite#���  6d   � 5&      -   5        $��GridCountInvateUnread��5&   #   .+�5'   "   �� -   5        $$��RectangleGridCountInvateUnread��5'   ,   
���  ��$��16��6]   $��16��6i    �� RadiusX$ 8��6t    �� RadiusY$ 8��6�   �� 5(   "   $��0,0,0,1q�6(   
�������$��Center=�6L   $��Center=�6p   $��10(�6�   ��6�   �� *CountInvateUnreadString+5)      5*      �� 5+      $��8q�6$   $��1��6/   
�������6:   5,      �� 5.      -   5        $��StackPanelInviteControl��5.   !   �� 50      -	   5        $��StackPanelRequest��50   !   .+�51      �� $��8q�6$   
���  ��$��14(�6@   $
��Bold"�6N    $  ViewContactRequestInvite_Request#���  6`   �� 52      $��8q�6$   $��1��6/   
�������6:   53      �� 55      -
   5        $��StackPanelRequestControl��55   !   57      58      5:      5;      5<      