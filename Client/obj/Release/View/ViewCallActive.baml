   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.16.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  7 clr-namespace:dodicall.Converterdodicall.Converter  "    dodicall.View.ViewCallActive   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     6	converter clr-namespace:dodicall.Converter     -   .��� -   $��GridMain������   �� Color$  #3d3334�� � RowDefinitions �� $
��Auto ��� �� $
��Auto �.+��� -   $��
ButtonChat��$��	10,3,3,10q�$��Right=�$��32��$��26��   ImageStyleButton#��[  $,��&/dodicall;component/Resources/chat.png�� J� InputBindings d�  d� Gesture$ 	LeftClicka� �� Command �� *CommandComingSoon+�� $��1��$��0,50,0,0q�$��Center=�.+��� -   $��TextBlockTitle��
�������$
��Bold"�$��14(� �� TextAlignment$ Center=�$��CharacterEllipsis=�$��Center=����� *CurrentModelCall.Title+�� 
�������$��14(�$��Center=����� *31CurrentModelCall.ModelEnumCallAddressTypeObj.Name+� $��Center=� �� �� �� .+�� $��0��$��25q�$	��124��$	��124��.+��� ���� *CurrentModelCall.Avatar+�� $��Right=�$��Bottom=�$��50��$��50��$6��0/dodicall;component/Resources/IconTray_small.png��y���  �� Path$! CurrentModelCall.IsDodicall� �� 	Converter 8   2dodicall.Converter.ConverterBoolToVisibilityHidden  �� -   $��	ImageSrtp��$��27��$��27��$��Right=�$	��Top=�$y�Hidden=�$,��&/dodicall;component/Resources/srtp.png��� $��1��$��Center=�.+��� -   $��StackPanelDirection�� �� Orientation$ 
Horizontal=�.+��� 
�������$��14(�$��Center=����� *1/CurrentModelCall.ModelEnumCallDirectionObj.Name+�� 
�������$��14(�$	��...���� -   $��TextBlockDuration��
�������$��14(�$y�Hidden=����� *
Duration+� $��2��$��Center=�	 � ColumnDefinitions	 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   $��StackPanelVideo��$��0��.+��� $��10q�$��57��$��57��#��[  $��Center=�$��Center=�$-��'/dodicall;component/Resources/video.png�� d� $ 	LeftClicka� �� *CommandComingSoon+�� 
�������$��Center=�  ViewCallActive_Video#���  �� -	   $��StackPanelMicrophone��$��1��$y�	Collapsed=�.+�� .+��� -
   $��ImageMicrophoneOn��$��10q�$��57��$��57��$��Center=�$��Center=�$+��%/dodicall;component/Resources/mic.png�� d� $ 	LeftClicka� �� *CommandMuteMicrophone+���� 
 �� 
TargetType"
 �� �� BasedOn# [   �� Triggers ��  �� Binding �� *Mute+ �� Value true.a���  �� Property � y� �� Value$ Hidden=���  �� *Mute+	 false.a���  � y�$ Visible=��� -   $��ImageMicrophoneOff��$��10q�$��57��$��57��$��Center=�$��Center=�$/��)/dodicall;component/Resources/mic_off.png�� d� $ 	LeftClicka� �� *CommandMuteMicrophone+���� "
 ��# [   ��  �� *Mute+ true.a���  � y�$ Visible=���  �� *Mute+	 false.a���  � y�$ Hidden=��� 
�������$��Center=�  ViewCallActive_Mute#���  �� -   $��StackPanelAddUser��$��2��$y�	Collapsed=�.+��� $��10q�$��57��$��57��#��[  $��Center=�$��Center=�$0��*/dodicall;component/Resources/add_user.png�� d� $ 	LeftClicka� �� *CommandComingSoon+�� 
�������$��Center=�  ViewCallActive_Add#���  �� $��3��.+��� $��10q�$��56��$��56��#��[  $��Center=�$��Center=�$0��*/dodicall;component/Resources/awaiting.png�� d� $ 	LeftClicka� �� *CommandComingSoon+�� 
�������$��Center=�  ViewCallActive_Pause#���  �� -   $��StackPanelCallTransfer��$��4��$y�	Collapsed=�.+��� $��10q�$��56��$��56��#��[  $��Center=�$��Center=�$0��*/dodicall;component/Resources/transfer.png�� d� $ 	LeftClicka� �� *CommandCallTransfer+�� $
��Wrap=�
�������$��Center=�  ViewCallActive_CallTransfer#���  � $��2��$��Center=�$��Center=�	 �� $
��Auto ��� .+��� -   $��ImageAccept��$��0��$��10q�$��56��$��55��#��[  $��Center=�$��Center=�$y�	Collapsed=�$.��(/dodicall;component/Resources/accept.png�� d� $ 	LeftClicka� �� *CommandAcceptCall+�� $��1��$��10q�$��56��$��55��#��[  $��Center=�$��Center=�$/��)/dodicall;component/Resources/decline.png�� d� $ 	LeftClicka� �� *CommandHangupCall+