   M S B A M L   `   `   `  ����D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  5      )    #dodicall.View.ViewUserAuthorization   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:dodicall.View     5      .��5	      � $��5q�6     � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   �� -   5        $��ComboBoxModelLanguage��5       $��0��6   $��Right=�6=   $	��130��6Y   ��6e   �� *ListModelLanguage+ �� SelectedItem 5      �� *$"CurrentModelLogin.ModelLanguageObj+��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��20��6    $��21��6,   ��67   �� *	Picture+�� 5      $��5,0,0,0q�6$   ��65   �� *Name+5      5      5      � 5   
   $��1��6   $	��440��6   $	��280��6)     5      �� 5      $	��150 �6    �� 5      $��60 �6    �� 5      $��60 �6    �� 5      $��30 �6    �� 5       $
��Auto �6    �� 5!      $��50 �6    �� 5"      $��30 �6    �� 5#      .+�5%      �� -   5        $��0��5%      $	��100��6!   $0��*/dodicall;component/Resources/big_logo.png��6.   $	��Top=�6b   6z   �� 5&      $��1��6   .+�5'      ��  #  ViewUserAuthorization_LoginText#���   6   �� 5(      -   5        $��TextBoxLogin��5(      ��6.   �� *CurrentModelLogin.Login+6o   5)      �� 5*      $��2��6   .+�5+      ��  & "ViewUserAuthorization_PasswordText#���  6   G� 5,      -   5        $��PasswordBoxUser��5,      65   5-      �� 5.      -   5        $��TextBlockFailLogin��5.   %   $��3��6   
���  ��$��Center=�6P    ' #ViewUserAuthorization_FailLoginText#���  6m   $y�Hidden=�6�   �� 5/      -   5        $��TextBlockSystemError��5/   %   $��3��6   
���  ��$��Center=�6R    ) %ViewUserAuthorization_SystemErrorText#���  6o   $y�Hidden=�6�   �� 50      -   5        $��TextBlockNoNetwork��50   %   $��3��6   
���  ��$��Center=�6P    # ViewUserAuthorization_NoNetwork#���  6m   $y�Hidden=�6�   �� 51      -   5        $��4��51      $
��Bold"�6"     ButtonStyleRed#��[ 64    % !ViewUserAuthorization_EnterButton#���  6\   6�   �� 52      $��5��6   $��Center=�6$   $
��Left=�6?   $��12(�6Z    + 'ViewUserAuthorization_AutostartCheckBox#���  6h    d� 	IsChecked 53      �� *CurrentModelLogin.Autostart+�� 54      $��6��6   $
��Left=�6$   $��12(�6?    + 'ViewUserAuthorization_AutoLoginCheckBox#���  6M    6�   �� *CurrentModelLogin.AutoLogin+�� 55      -	   5        $��ComboBoxServerArea��55   $   $��7��6    �� DisplayMemberPath$
 Name��6j   $y�Hidden=�56   U   ��55   >   �� *ListModelServerArea+ 56      �� *&$CurrentModelLogin.ModelServerAreaObj+6i   57      �� 58   
   $��2��6   $ 
Horizontal=�6"   .+�59      �� $��5q�6   
������� %	 !ViewUserAuthorization_VersionText#��� 	 67   �� 5:      -
   5        $��TextBlockVestion��5:   )   $��0,5,5,5q�6   
�������6A   5;      �� 5<   
   $��2��6   $��Right=�6"   $ 
Horizontal=�6>   .+�5=      �� $��30��6   $	��130��6!    
 ButtonStyleTransparent#��[
 6-   ��6]   �� *CommandForgotPassword+.��5>      �� $��12(�6   $��	Underline{�6*    . *ViewUserAuthorization_ForgotPasswordButton#���  6F   5?      �� 5@      $��30��6   $	��130��6!   #��[
 6-   ��6]   �� *CommandSignUp+.��5A      �� $��12(�6   $��	Underline{�6*    & "ViewUserAuthorization_SignUpButton#���  6F   5B      5C      5D      5E      