   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  "    dodicall.View.ViewUserDetail   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     .���� $��10q�.���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� .+�� $��10q�$	��124��$	��124��.+��� ���� *CurrentModelUser.Avatar+�� $��Right=�$��Bottom=�$��50��$��50��$6��0/dodicall;component/Resources/IconTray_small.png��� $��1��  �� $
��Auto ��� .+��� $��8,8,8,0q�$
��Bold"�$��14(����� *CurrentModelUser.FullName+� -   $��1�� � ColumnDefinitions �� $
��Auto ��� -    �Name$ ColumnDefinitionStatus��.+��� -    �� Orientation$ 
Horizontal=�.+�A� -   $��EllipseStatus��$��5q�$��6��$��6������ *31CurrentModelUser.ModelEnumUserBaseStatusObj.Color+�� -   $��5,5,0,5q����� *20CurrentModelUser.ModelEnumUserBaseStatusObj.Name+�� -   $!��TextBlockUserExtendedStatus��$��1��$��0,5,5,5q�$
��Left=�$��CharacterEllipsis=����� *20CurrentModelUser.UserExtendedStatusForFullStatus+� $��2��  �� $��30 ��� $
��Auto ��� $
��Auto ��� $��30 ��� $
��Auto ��� $
��Auto �.+��� $��0��$��Center=�
���  ��$
��Bold"�   ViewUserDetail_MyContact#���   �� $��1��$��1��
��������� -   $��StackPanelMyContact��$��2���� -   $��TextBlockExtraContact��$��3��$��Center=�
���  ��$
��Bold"�  ViewUserDetail_ExtraContact#���  �� -	   $��RectangleExtraContact��$��4��$��1��
��������� -
   $��StackPanelExtraContact��$��5��