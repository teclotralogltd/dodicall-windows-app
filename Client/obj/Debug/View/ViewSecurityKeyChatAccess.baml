   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  5      -    'dodicall.View.ViewSecurityKeyChatAccess   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:dodicall.View     5      $��872.214��5      $��1077.5��60   .��5
      � $��	0,100,0,0q�6   .+�5   
   � -   5        $��GridMain��5      $	��516��6     � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      .+�5      �� $��0��6   $
��Wrap=�6%   $��35,20,40,30q�69   $��Center=�6N   ���#44a167$��18(�6~    �� TextAlignment$ Center=�6�    (  $ViewSecurityKeyChatAccess_ChatAccess#���   6�   �� 5      $��1��6   $
��Wrap=�6%   $��
20,0,20,40q�69   $��Center=�6M   
�������$��18(�6|   $ Center=�6�    1 -ViewSecurityKeyChatAccess_ImportEncryptionKey#���  6�   � 5      -   5        $��MenuButtons��5      $��2��6&   $��0,0,0,45q�63     5      �� 5      $
��Auto �6$    � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   .+�5      �� $��1��6   $��0��6&     ButtonStyleTransparent#��[ 66   $��Center=�6f   ��6�   �� *CommandCopyFromClipBoard+.��5       ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�5!      �� $,��&/dodicall;component/Resources/copy.png��6    �� 5"      $��14(�6$   $��Center=�62   $��25,0,0,0q�6M    / +ViewSecurityKeyChatAccess_CopyFromClipboard#���  6_   5#      5$      �� 5%      $��1��6   $��1��6&   #��[ 66   $��Center=�6f   ��6�   �� *CommandComingSoon+.��5&      �� $ 
Horizontal=�6!   .+�5'      �� $*��$/dodicall;component/Resources/qr.png��6    �� 5(      $��14(�6$   $��Center=�62   $��25,0,0,0q�6M    ( $ViewSecurityKeyChatAccess_ScanQRCode#���  6`   5)      5*      5+      �� 5,      $��3��6   $��18(�6"   $��60��60    & "ViewSecurityKeyChatAccess_Continue#���  6<   ��6{   �� *CommandContinue+��5-      �� 5.       �� 
TargetType" ��6   .�5/      ��  �� Property � �� �� Value �#44a16767   �� 50       � ��
 �����67   51      53      54      55      57      