   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null1clr-namespace:dodicall.Windowdodicall.Window  5   	   %    dodicall.Window.WindowException   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:dodicall.Window     5   	      VS2012WindowStandarsStyle#��[  5   	   $	��600��5	   	   $	��700��6     � WindowStartupLocation$  CenterScreen=�6"   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   �� $��10q�6   $��CharacterEllipsis=�6    ��6A   �� *TargetSite.Name+ �� Mode$ OneWay=��� 5   
   $��1��6   $��10q�6!   $��CharacterEllipsis=�6-   ��6N   �� *Source+$ OneWay=��� 5   
   $��2��6   $��10q�6   $��12(�6+    �� MaxLines$ 6��69    �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�6X    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6�   ��6�   �� *	Message+$ OneWay=��� 5   
   $��3��6   $��10q�6   $��12(�6+    . $
 Wrap=�6K    . $ Visible=�6t   ��6�   �� *
StackTrace+$ OneWay=��� 5   
   -   5        $��4��5      $��10q�6   $	��160��6*   $��Ok��66   6C   5      5      