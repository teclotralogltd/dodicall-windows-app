   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.16.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View      dodicall.View.ViewUpdate   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     .���   � RowDefinitions  �� �� $
��Auto � � ColumnDefinitions �� $��80 ��� .+��� $��5,0,0,0q�$��33��$��33��$8��2/dodicall;component/Resources/icon_information.png���� -   $��StackPanelMessage��$��1��$
��Left=�$��Center=�.+��� $��14(�$
��Wrap=�   ViewUpdate_MessageUpdate1#���   �� $��14(�$
��Wrap=�  ViewUpdate_MessageUpdate2#���  �� -   $��StackPanelWarning��$��1��$
��Left=�$��Center=�$y�Hidden=�.+��� $
��Left=�$��Center=�$��14(�$
��Wrap=� # ViewUpdate_MessageUpdateWarning#���  �� $
��Left=�$��Center=�$��14(�
���  ��$
��Wrap=� $  ViewUpdate_MessageUpdateWarning2#���  �� -   $��TextBlockDownloading��$��1��$
��Left=�$��Center=�$��14(� ! ViewUpdate_MessageDownloading#���  $y�Hidden=��� -   $��TextBlockUpdateFail��$��1��$
��Left=�$��Center=�$��14(�  ViewUpdate_StartUpdateFail#���  $y�Hidden=��� -   $��ButtonOk��$��1��$��2��$��Ok��$��30��$	��100��$y�Hidden=��� -   $$��StackPanelButtonDownloanCancel��$��1��$��2�� �� Orientation$ 
Horizontal=�$��Right=�.+��� -     ViewUpdate_ButtonUpdate#���  $��30��$	��150��$��15q��� -      ViewUpdate_ButtonUpdateLater#���  $��30��$	��180��$��
0,15,15,15q�