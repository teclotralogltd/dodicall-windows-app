   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  )    #dodicall.View.ViewUserAuthorization   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     .��� $��5q�  � RowDefinitions  �� $
��Auto ��� �� $
��Auto �.+��� -   $��ComboBoxModelLanguage��$��0��$��Right=�$	��130������ *ListModelLanguage+ �� SelectedItem �� *$"CurrentModelLogin.ModelLanguageObj+���� .R���  �� Orientation$ 
Horizontal=�.+��� $��20��$��21������ *	Picture+�� $��5,0,0,0q����� *Name+� $��1��$	��440��$	��280��  �� $	��150 ��� $��60 ��� $��60 ��� $��30 ��� $
��Auto ��� $��50 ��� $��30 ��� .+��� -   $��0��$	��100��$0��*/dodicall;component/Resources/big_logo.png��$	��Top=��� $��1��.+���  #  ViewUserAuthorization_LoginText#���   �� -   $��TextBoxLogin������ *CurrentModelLogin.Login+�� $��2��.+���  & "ViewUserAuthorization_PasswordText#���  G� -   $��PasswordBoxUser���� -   $��TextBlockFailLogin��$��3��
���  ��$��Center=� ' #ViewUserAuthorization_FailLoginText#���  $y�Hidden=��� -   $��TextBlockSystemError��$��3��
���  ��$��Center=� ) %ViewUserAuthorization_SystemErrorText#���  $y�Hidden=��� -   $��TextBlockNoNetwork��$��3��
���  ��$��Center=� # ViewUserAuthorization_NoNetwork#���  $y�Hidden=��� -   $��4��$
��Bold"�  ButtonStyleRed#��[  % !ViewUserAuthorization_EnterButton#���  �� $��5��$��Center=�$
��Left=�$��12(� + 'ViewUserAuthorization_AutostartCheckBox#���   d� 	IsChecked �� *CurrentModelLogin.Autostart+�� $��6��$
��Left=�$��12(� + 'ViewUserAuthorization_AutoLoginCheckBox#���   �� *CurrentModelLogin.AutoLogin+�� -	   $��ComboBoxServerArea��$��7�� �� DisplayMemberPath$
 Name��$y�Hidden=����� *ListModelServerArea+ �� *&$CurrentModelLogin.ModelServerAreaObj+�� $��2��$ 
Horizontal=�.+��� $��5q�
������� %	 !ViewUserAuthorization_VersionText#��� 	 �� -
   $��TextBlockVestion��$��0,5,5,5q�
��������� $��2��$��Right=�$ 
Horizontal=�.+��� $��30��$	��130�� 
 ButtonStyleTransparent#��[
 ���� *CommandForgotPassword+.���� $��12(�$��	Underline{� . *ViewUserAuthorization_ForgotPasswordButton#���  �� $��30��$	��130��#��[
 ���� *CommandSignUp+.���� $��12(�$��	Underline{� & "ViewUserAuthorization_SignUpButton#���  