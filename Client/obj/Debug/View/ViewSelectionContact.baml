   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  5      (    "dodicall.View.ViewSelectionContact   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:dodicall.View     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      �� 5      $��5 �6   �� 5      $��60 �6   �� 5      $��5 �6   �� 5      $��60 �6   .+�5   
   � $��0��6   
�������.+�5      �� -   5        $��TextBoxSearch��5      $��8q�6+   
������ ��66   �� *ContactListFilter+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5      -   5        $��TextBlockSearch��5      $��16,0,0,0q�6/   $��Center=�6A   $
��Left=�6\   
�������$��14(�6�      ViewSelectionContact_Search#���   6�   6�   �� 5      $��12��6   $��0,0,16,0q�6   $��Right=�61   $.��(/dodicall;component/Resources/search.png��6M   5      �� 5   
   $��1��6   $
��Auto=�6$   .��5      �� -   5        $��StackPanelInviteControl��5      5      �� 5   
   -   5        $��RectangleMessage1��5   !   $��2��6   $��1��6:   
�������$��	10,0,10,0q�6U   �� 5   
   -   5        $��ScrollChatUserList��5   $   $��3��6   $��Disabled=�6>   $
��Auto=�6g   .��5      �� -   5        $��ItemsControlChatUserList��5      $
��Left=�6;   ��6W   �� *SelectedModelContactList+��5      �� 5      .R�5      � $
��Left=�6$   5       5!      ��5#      �� 5$      .R�5%      � $��ChatMemberDetail��6<   $��20��6   $��2,2,2,2q�6+   .+�5&      �� 
������� �� RadiusX$ 10��6:    �� RadiusY$ 10��6G   �� 5'       �� Orientation$ 
Horizontal=�6)   .+�5(   "   A� $��EllipseStatus��6?   $��6��6*   $��6��65   $��6,6,0,6q�6T   $
��Left=�6e   ��6�   �� *" ModelEnumUserBaseStatusObj.Color+�� 5)   "   $��TextBlockFullName��6=   $��0,0,0,0q�6,   $
��Left=�6V   ��6q   �� *
FullName+�� 5*   "   -   5        $6��0/dodicall;component/Resources/revoke_contact.png��5*   (   $��15��6b   $��15��6m   $��Right=�6y   $��5,0,3,0q�6�   6�   5+      5,       �� Triggers 5-      �� 5.       �� Value$ false��6f    �� Binding 6*   �� *)'ModelContactPermissionObj.CanViewStatus+.a�5/   "   ��  �� 
TargetName$ EllipseStatus��6)   	 �� Property	 � y�
 �� Value$
 	Collapsed=�6Z   �� 50   "   $ TextBlockFullName��6)   	 � ��$
 6,0,6,0q�6Z   51      53      55      56      �� 59   
   -   5        $��RectangleMessage��59   !   $��4��6   $��1��69   
�������$��	10,0,10,0q�6T   � 5:   
   $��6��6   .+�5;      �� $��10q�6   $ 
Horizontal=�6%   $��Center=�6>   .+�5<      ��  ! ViewSelectionContact_Selected#���  6   �� 5=      -	   5        $��TextBlockCountSelected��5=      $��5,0,5,0q�6:   
���  ��6K   �� 5>      ��6   �� *CountSelectedModelContact+5?      �� 5@      -
   5        $��ButtonOk��5@      $��10q�6%   $��30��61   $	��130��6=   $��Right=�6I    # ViewSelectionContact_CreateChat#���  6e   6�   5A      5B      5C      