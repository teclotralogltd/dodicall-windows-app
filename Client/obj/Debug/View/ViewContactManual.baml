   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  5      %    dodicall.View.ViewContactManual   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:dodicall.View     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $
��Auto �6   �� 5       � RowDefinitions 5   
   �� 5      �� 5      $
��Auto �6   .+�5   
   �� $��30q�6   $	��100��6   $	��100��6)   $	��Top=�65   ��6M   �� *CurrentModelContact.Avatar+�� 5   
   $��1��6   $��0��6'   .��5      � $��10,10,30,10q�6    5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   .+�5      �� .+�5      �� $��0,10,0,0q�6       ViewManualContact_FirstName#���   62   �� 5      -   5        $��TextBoxFirstName��5      ��66   �� *CurrentModelContact.FirstName+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5      $��0,10,0,0q�6      ViewManualContact_LastName#���  62   �� 5      -   5        ��5      �� *CurrentModelContact.LastName+$ PropertyChanged=�6�   5      �� 5       -   5        $��StackPanelNumberPhone��5    =   $��1��6   $��	0,20,0,10q�6*   5)      �� 5*      $��2��6   $
��Left=�6&   $��90��6A     ButtonStyleTransparent#��[ 6L   ��6|   �� *CommandAddUserContact+��5+      �� 5,       �� Orientation$ 
Horizontal=�6%   .+�5-      �� $��10��6$   $��10��60   $+��%/dodicall;component/Resources/add.png��6;   �� 5.      $��5,0,0,0q�6(   $��12(�69     ViewManualContact_Add#���  6G   5/      51      52      53      �� 54   
   $��1��6   $��1��6%   $��5q�62   $��Right=�6=   $ 
Horizontal=�6Y   .+�55      �� -   5        $��ButtonOk��55      $��30��6%   $	��130��61   $��Ok��6=   {�.  ��6\   �� *CommandSave+56      57      58      