   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null1clr-namespace:dodicall.Windowdodicall.Window  5   	   '    !dodicall.Window.WindowInformation   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:dodicall.Window     5   	      VS2012AboutStyle#��[  5   	      WindowInformation_AboutTitle  � Title#  [ 5	   	    � WindowStartupLocation$ CenterScreen=�6?   $	��480��6d   $	��650��6q    � 
ResizeMode$ NoResize=�6}   .��5
      �  � RowDefinitions 5   
   �� 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   (  "System.Windows.Controls.WebBrowser  -   5        $��WebBrowserMain��5   .   $��0��6   $��10q�6"   6.   �� 5   
   -   5        $��StackPanelVersion��5      $��1��6.   $��10,0,0,0q�6;    �� Orientation$ 
Horizontal=�6M   $��Bottom=�6f   $y�	Collapsed=�6�   .+�5      �� $��5q�6   
������� " WindowInformation_AboutVersion#��[ 67   �� 5      $��5q�6   
���������67   �� *	Version+5      �� 5   
   -   5        $��2��5      $��10q�6   $��Right=�6*   $	��100��6F   $��Ok��6R   6_   5      5      