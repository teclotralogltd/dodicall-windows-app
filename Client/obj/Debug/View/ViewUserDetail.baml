   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  5      "    dodicall.View.ViewUserDetail   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:dodicall.View     5      .��5	      �� $��10q�6   .��5
   
   �   � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      .+�5      � $��10q�6   $	��124��6   $	��124��6,   .+�5      �� ��6   �� *CurrentModelUser.Avatar+�� 5      $��Right=�6   $��Bottom=�64   $��50��6O   $��50��6[   $6��0/dodicall;component/Resources/IconTray_small.png��6f   5      � 5      $��1��6     5      �� 5      $
��Auto �6$   �� 5      .+�5      �� $��8,8,8,0q�6   $
��Bold"�6-   $��14(�6?   ��6M   �� *CurrentModelUser.FullName+� 5      -   5        $��1��5       � ColumnDefinitions 5      �� 5      $
��Auto �6+   �� 5      -   5         �Name$ ColumnDefinitionStatus��5   +   .+�5      �� -   5         �� Orientation$ 
Horizontal=�5   !   .+�5       A� -   5        $��EllipseStatus��5    B   $��5q�6"   $��6��6-   $��6��68   ��6W   �� *31CurrentModelUser.ModelEnumUserBaseStatusObj.Color+6�   �� 5!      -   5        $��5,5,0,5q�5!   $   ��65   �� *20CurrentModelUser.ModelEnumUserBaseStatusObj.Name+6w   5"      �� 5#      -   5        $!��TextBlockUserExtendedStatus��5#   A   $��1��6    $��0,5,5,5q�60   $
��Left=�6d   $��CharacterEllipsis=�6   ��6�   �� *20CurrentModelUser.UserExtendedStatusForFullStatus+6�   5$      5%      � 5&      $��2��6     5'      �� 5(      $��30 �6$   �� 5)      $
��Auto �6$   �� 5*      $
��Auto �6$   �� 5+      $��30 �6$   �� 5,      $
��Auto �6$   �� 5-      $
��Auto �6$   .+�5/      �� $��0��6   $��Center=�6)   
���  ��$
��Bold"�6U      ViewUserDetail_MyContact#���   6g   �� 50      $��1��6   $��1��6)   
�������64   �� 51      -   5        $��StackPanelMyContact��51   *   $��2��6   6*   �� 52      -   5        $��TextBlockExtraContact��52   )   $��3��6   $��Center=�6F   
���  ��$
��Bold"�6r     ViewUserDetail_ExtraContact#���  6�   �� 53      -	   5        $��RectangleExtraContact��53   )   $��4��6   $��1��6F   
�������6Q   �� 54      -
   5        $��StackPanelExtraContact��54   *   $��5��6   6*   55      56      57      58      