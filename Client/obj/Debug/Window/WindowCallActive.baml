   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null1clr-namespace:dodicall.Windowdodicall.Window  5   	   7 clr-namespace:dodicall.Converterdodicall.Converter  5   	   &     dodicall.Window.WindowCallActive   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:dodicall.Window     5   	   6	converter clr-namespace:dodicall.Converter     5   	   -   5           VS2012WindowCallStyle#��[  5	   	     � 
ResizeMode$  NoResize=�68    � WindowStartupLocation$ CenterScreen=�5
   	   $	��135��6.   $	��460��6;    � ShowInTaskbar .   � Topmost . .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��4��6   
������� J� Opacity$	 0.7Q�65   � 5   
   $��0��6   $��12q�6   $��56��6+   $��56��67   .+�5      �� ��6   �� *CurrentModelCall.Avatar+�� 5      $��Right=�6   $��Bottom=�60   $��25��6K   $��25��6W   $6��0/dodicall;component/Resources/IconTray_small.png��6b   y�6�   ��  �� Path$! CurrentModelCall.IsDodicall� �� 	Converter 8   2dodicall.Converter.ConverterBoolToVisibilityHidden  5      � 5   
   $��1��6   $��
0,10,10,10q�6    � RowDefinitions 5      �� 5      �� 5      .+�5      �� $��0��6   $��0,8,0,0q�6%   $��14(�66   $
��Bold"�6D   
�������$��Center=�6i   $��CharacterEllipsis=�6�   ��6�   �� *CurrentModelCall.Title+�� 5       $��1��6   $��3,0,0,8q�6&   	 �� Orientation$	 
Horizontal=�67   .+�5!      �� -   5        $��	ImageSrtp��5!   *   $��0,0,10,0q�6   $��17��6;   $��13��6G   $��Center=�6R   $y�	Collapsed=�6m   $1��+/dodicall;component/Resources/LockWhite.png��6�   �� 5"      -   5        $��TextBlockDirection��5"      $��14(�66   
�������$��Center=�6W   ��6r   �� *1/CurrentModelCall.ModelEnumCallDirectionObj.Name+�� 5#      -   5        $��TextBlockDuration��5#      
�������$��14(�6H   $��Center=�6V   $y�Hidden=�6q   ��6�   �� *
Duration+5$      5%      �� 5'   
   -   5        $��ImageMicrophoneOn��5'   2   $��2��6   $��12,0,0,0q�6    $��57��6K   $��57��6V     ImageStyleButton#��[ 6b   $y�	Collapsed=�6�   $+��%/dodicall;component/Resources/mic.png��6�   
 J� InputBindings
 5(      d� 5)       d� Gesture$ 	LeftClicka�6    �� Command 63   �� *CommandMuteMicrophone+5+      �� 5-   
   -   5        $��ImageMicrophoneOff��5-   2   $��2��6   $��12,0,0,0q�6    $��57��6L   $��57��6W   #��[ 6c   $y�	Collapsed=�6�   $/��)/dodicall;component/Resources/mic_off.png��6�   
 5.      d� 5/      $ 	LeftClicka�6    63   �� *CommandMuteMicrophone+51      �� 53   
   -   5        $��ImageAccept��53   2   $��2��6   $��12,0,0,0q�6    $��57��6E   $��57��6P   #��[ 6\   $y�	Collapsed=�6�   $.��(/dodicall;component/Resources/accept.png��6�   
 54      d� 55      $ 	LeftClicka�6    63   �� *CommandAcceptCall+57      �� 59   
   $��3��6   $��12q�6    $��56��6,   $��55��67   #��[ 6C   $/��)/dodicall;component/Resources/decline.png��6m   
 5:      d� 5;      $ 	LeftClicka�6    63   �� *CommandHangupCall+5=      5>      5?      