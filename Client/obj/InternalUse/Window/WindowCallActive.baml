   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null1clr-namespace:dodicall.Windowdodicall.Window  7 clr-namespace:dodicall.Converterdodicall.Converter  &     dodicall.Window.WindowCallActive   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:dodicall.Window     6	converter clr-namespace:dodicall.Converter     -      VS2012WindowCallStyle#��[    � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=�$	��135��$	��460�� � ShowInTaskbar .   � Topmost . .���  � ColumnDefinitions �� $
��Auto ��� �� $
��Auto ��� $
��Auto �.+�� $��4��
������� J� Opacity$	 0.7Q�� $��0��$��12q�$��56��$��56��.+��� ���� *CurrentModelCall.Avatar+�� $��Right=�$��Bottom=�$��25��$��25��$6��0/dodicall;component/Resources/IconTray_small.png��y���  �� Path$! CurrentModelCall.IsDodicall� �� 	Converter 8   2dodicall.Converter.ConverterBoolToVisibilityHidden  � $��1��$��
0,10,10,10q� � RowDefinitions �� �� .+��� $��0��$��0,8,0,0q�$��14(�$
��Bold"�
�������$��Center=�$��CharacterEllipsis=����� *CurrentModelCall.Title+�� $��1��$��3,0,0,8q�	 �� Orientation$	 
Horizontal=�.+��� -   $��	ImageSrtp��$��0,0,10,0q�$��17��$��13��$��Center=�$y�	Collapsed=�$1��+/dodicall;component/Resources/LockWhite.png���� -   $��TextBlockDirection��$��14(�
�������$��Center=����� *1/CurrentModelCall.ModelEnumCallDirectionObj.Name+�� -   $��TextBlockDuration��
�������$��14(�$��Center=�$y�Hidden=����� *
Duration+�� -   $��ImageMicrophoneOn��$��2��$��12,0,0,0q�$��57��$��57��  ImageStyleButton#��[ $y�	Collapsed=�$+��%/dodicall;component/Resources/mic.png��
 J� InputBindings
 d�  d� Gesture$ 	LeftClicka� �� Command �� *CommandMuteMicrophone+�� -   $��ImageMicrophoneOff��$��2��$��12,0,0,0q�$��57��$��57��#��[ $y�	Collapsed=�$/��)/dodicall;component/Resources/mic_off.png��
 d� $ 	LeftClicka� �� *CommandMuteMicrophone+�� -   $��ImageAccept��$��2��$��12,0,0,0q�$��57��$��57��#��[ $y�	Collapsed=�$.��(/dodicall;component/Resources/accept.png��
 d� $ 	LeftClicka� �� *CommandAcceptCall+�� $��3��$��12q�$��56��$��55��#��[ $/��)/dodicall;component/Resources/decline.png��
 d� $ 	LeftClicka� �� *CommandHangupCall+