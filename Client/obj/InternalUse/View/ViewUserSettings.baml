   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.15.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  7 clr-namespace:dodicall.Converterdodicall.Converter  $    dodicall.View.ViewUserSettings   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     6	converter clr-namespace:dodicall.Converter     .���   � ColumnDefinitions  �� $��* �$	��200���� $��5 ��� $��3* �$	��450�� � RowDefinitions �� �� $
��Auto �.+��� $��0��$��2��$��0q� �� SelectedItem �� *#!CurrentModelEnumUserSettingsGroup+���� * ListModelEnumUserSettingsGroup+���� .R��� $��10q�$��12(����� *Name+�� $��1��$��0��$��2��$��Stretch=�$��Stretch=�$��5��
�������
������ �� $��2��$��0��.��� .+�� -   $��
GridCommon��$��10q�$y�	Collapsed=� �� $��30 ��� $
��Auto ��� $��50 ��� $��50 ��� $��30 ��� $
��Auto ��� $��50 ��� $��50 ��� $��30 ��� $
��Auto ��� $��50 ��� .+��� $��0��$��Center=�
���  ��$
��Bold"�   ViewUserSettings_Status#���   �� $��1��$��1��
��������� $��2��$��Center=�#���   �� $��2��$��Center=�$��Right=�$	��260������ *ListModelEnumUserBaseStatus+ �� *53CurrentModelUserSettings.ModelEnumUserBaseStatusObj+���� .R���  �� Orientation$ 
Horizontal=�.+�A� $��EllipseStatus��$��6��$��6������ *Color+�� $��5,0,0,0q����� *Name+�� $��3��$��Center=� # ViewUserSettings_ExtendedStatus#���  �� $��3��$��Center=�$��Right=�$	��260������ *-+CurrentModelUserSettings.UserExtendedStatus+�� $��4��$��Center=�
���  ��$
��Bold"�  ViewUserSettings_Enter#���  �� $��5��$��1��
��������� $��6��$��Center=�  ViewUserSettings_AutoLogin#���  �� $��6��$��Center=�$��Right=� d� 	IsChecked �� *$"CurrentModelUserSettings.AutoLogin+�� $��7��$��Center=�  ViewUserSettings_Autostart#���  �� $��7��$��Center=�$��Right=� �� *$"CurrentModelUserSettings.Autostart+�� $��8��$��Center=�
���  ��$
��Bold"�  ViewUserSettings_Mode#���  �� $��9��$��1��
��������� $��10��$��Center=�$ 
Horizontal=�.+���  % !ViewUserSettings_DoNotDesturbMode#���  �� $��5,0,0,0q����� *CountWhiteContactString+�� -   $��CheckBoxDoNotDesturb��$��10��$��Center=�$��Right=� �� *+)CurrentModelUserSettings.DoNotDesturbMode+� -   $��GridSecurity��$��10q�$y�	Collapsed=� �� $��30 ��� $
��Auto ��� $��50 ��� $��50 ��� $��30 ��� $
��Auto ��� $��50 ��� $��50 ��� $
��Auto ��� $
��Auto ��� .+��� $��0��$��Center=�
���  ��$
��Bold"� ( $ViewUserSettings_ExportEncryptionKey#���  �� $��1��$��1��
��������� $��2��$��Center=�{��� *ExportAviable+.+��� -     ButtonStyleTransparent#��[ $��Center=�$
��Left=�.���� $ 
Horizontal=�$��Center=�.+��� $,��&/dodicall;component/Resources/copy.png��$��20��$��20���� $��12(�$��Center=�$��15,0,0,0q� $	  ViewUserSettings_CopyToClipboard#��� 	 �� $��3��$��Center=�{��� *ExportAviable+.+��� -   #��[ $��Center=�$
��Left=�.���� $ 
Horizontal=�.+��� $.��(/dodicall;component/Resources/export.png��$��20��$��20���� $��12(�$��Center=�$��15,0,0,0q� 
 ViewUserSettings_SaveAs#��� 
 �� $��4��$��Center=�
���  ��$
��Bold"� ( $ViewUserSettings_ImportEncryptionKey#���  �� -   $��ImportedPanel��$��4��$ 
Horizontal=�$��Center=�$��Right=�$y�Hidden=�.+��� $ 
Horizontal=�.+��� $��Center=�$��15��$��15��$0��*/dodicall;component/Resources/imported.png���� $��14(�$��Center=�$��5,0,0,0q�
�������  ViewUserSettings_Imported#���  �� $��5��$��1��
��������� $��6��$��Center=�.+��� #��[ $��Center=�$
��Left=����� *CommandComingSoon+.���� $ 
Horizontal=�$��Center=�.+��� $*��$/dodicall;component/Resources/qr.png��$��20��$��20���� $��12(�$��Center=�$��15,0,0,0q�  ViewUserSettings_ScanQRCode#���  ]� $��7��$��Center=�.+��� $��Center=�.+��� #��[ $��Center=�$
��Left=����� *CommandPasteFromClipBoard+.���� $ 
Horizontal=�.+��� $-��'/dodicall;component/Resources/paste.png��$��20��$��20���� $��12(�$��Center=�$��15,0,0,0q� & "ViewUserSettings_CopyFromClipBoard#���  � -   $��ImportPanel��$��8������  �� 
TargetType" � �� Triggers ��  �� Binding �� *ShowImportPanel+ �� Value true.a��� 	 �� Property	 � y�
 �� Value$
 Visible=���  �� *ShowImportPanel+	 false.a��� 	 � y�$
 	Collapsed=� �� $	��120 ��� $
��Auto � � 	Resources   brushWatermarkBackground �� Color  brushWatermarkForeground   BooleanToVisibilityConverter  EntryFieldStyle��%�   &	       &	      &	 #     &	 (     �� $ White���� $ Silver���� �� " �.��� 	 � ��$
 Stretch=��� 	 � ��$
 Center=��� 	 � ��$

 20,0q�.+�� $��0��#��[ #��[ $	��120��$��10,0,0,0q�.+��� $��Segoe UI)�$��14(�$	��8,4q� ' #ViewUserSettings_PasteEncryptionKey#���  #��[ y���  �� ElementName$ TextBoxEncryption�� �� Path$ Text.IsEmpty� �� 	Converter# [ �� -   $��TextBoxEncryption�� �� AcceptsReturn . 
������  �� TextWrapping$
 Wrap=����� *CryptKeyForImport+ �� Mode$ TwoWay=�� $��1��$��	20,10,0,0q� �� $
��Auto �  �� $��* ��� $��* �.+��� $��0��$��1��$ 
Horizontal=�$��Right=�$��Center=�.+��� -	     ViewUserSettings_Import#���  � $��9�� �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��Center=�
���  ��$
��Bold"�  ViewUserSettings_Refresh#���  �� $��1��$��1��
��������� $��2��$��Center=�$ 
Horizontal=�.+��� -
   #��[ $��Center=�$
��Left=�.���� $ 
Horizontal=�.+��� $>��8/dodicall;component/Resources/refresh_encryption_key.png��$��20��$��20���� $��12(�$��Center=�$��15,0,0,0q� ! ViewUserSettings_CreateNewKey#���  �� 
���  ��$��Italic$� * &ViewUserSettings_LostOldCorrespondence#���  $��Center=�$��30,0,0,0q�� -   $��GridTelecommunication��$��10q�$y�	Collapsed=� �� $��30 ��� $
��Auto ��� $��50 ��� $��50 ��� $��30 ��� $
��Auto ��� $��50 ��� $��30 ��� $
��Auto ��� $��50 ��� $��50 ��� $��30 ��� $
��Auto ��� $��50 ��� .+��� $��0��$��Center=�
���  ��$
��Bold"�  ViewUserSettings_Account#���  �� $��1��$��1��
��������� $��2��$��Center=� # ViewUserSettings_DefaultAccount#���  �� $��2��$��Center=�$��Right=�$	��260�� �� DisplayMemberPath$ Account������ */-CurrentModelUserSettings.ListModelUserAccount+ �� *53CurrentModelUserSettings.DefaultModelUserAccountObj+�� $��3��$��Center=� ! ViewUserSettings_VoiceMessage#���  �� -   $��3��$��30��$	��100��$��Right=� & "ViewUserSettings_VoiceMessageStart#���  �� $��4��$��Center=�
���  ��$
��Bold"�  ViewUserSettings_Encryption#���  �� $��5��$��1��
��������� $��6��$��Center=� # ViewUserSettings_VoipEncryption#���  �� $��6��$��Center=�$��Right=�$	��260��$
 Name������ *ListModelEnumVoipEncryption+ �� *53CurrentModelUserSettings.ModelEnumVoipEncryptionObj+�� $��7��$��Center=�
���  ��$
��Bold"�  ViewUserSettings_Video#���  �� $��7��$��Center=�$��Right=� �� *'%CurrentModelUserSettings.VideoEnabled+.���� $��Center=�$��12(� ! ViewUserSettings_VideoEnabled#���  �� $��8��$��1��
��������� $��9��$��Center=� "  ViewUserSettings_VideoSizeWifi#���   �� $��9��$��Center=�$��Right=�$	��260��$
 Name������ *ListModelEnumVideoSizeWifi+ �� *42CurrentModelUserSettings.ModelEnumVideoSizeWifiObj+�� $��10��$��Center=� "! ViewUserSettings_VideoSizeCell#��� ! �� $��10��$��Center=�$��Right=�$	��260��$
 Name������ *ListModelEnumVideoSizeCell+ �� *42CurrentModelUserSettings.ModelEnumVideoSizeCellObj+�� $��11��$��Center=�
���  ��$
��Bold"� %" !ViewUserSettings_EchoCancellation#��� " �� $��12��$��1��
��������� $��13��$��Center=� )# %ViewUserSettings_EchoCancellationMode#��� # �� $��13��$��Center=�$��Right=�$	��260��$
 Name������ *#!ListModelEnumEchoCancellationMode+ �� *;9CurrentModelUserSettings.ModelEnumEchoCancellationModeObj+� -   $��GridChat��$��10q�$y�	Collapsed=� �� $��30 ��� $
��Auto ��� $��50 ��� $��50 ��� .+��� $��0��$��Center=�
���  ��$
��Bold"� $ ViewUserSettings_Font#��� $ �� $��1��$��1��
��������� $��2��$��Center=� % ViewUserSettings_FontSize#��� % �� $��2��$��Center=�$��Right=�$	��260�� �� Text �� *&$CurrentModelUserSettings.GuiFontSize+.@��� $��10���� $��12���� $��14���� $��16���� $��18���� $��20���� $��22���� $��24���� -   $��3��$��30��$	��130��$��Right=� & ViewUserSettings_ClearChat#��� & � -   $��GridGuiSettings��$��10q�$y�	Collapsed=� �� $��30 ��� $
��Auto ��� $��50 ��� $��50 ��� .+��� $��0��$��Center=�
���  ��$
��Bold"� ' ViewUserSettings_View#��� ' �� $��1��$��1��
��������� $��2��$��Center=� ( ViewUserSettings_Language#��� ( �� $��2��$��Center=�$��Right=�$	��260������ *ListModelLanguage+ �� *+)CurrentModelUserSettings.ModelLanguageObj+���� .R��� $ 
Horizontal=�.+��� $��20��$��21������ *	Picture+�� $��5,0,0,0q����� *Name+�� $��3��$��Center=� ) ViewUserSettings_Theme#��� ) �� $��3��$��Center=�$��Right=�$	��260��$
 Name������ *ListModelEnumTheme+ �� *,*CurrentModelUserSettings.ModelEnumThemeObj+� -   $��GridInformation��$��10q�$y�	Collapsed=� �� $��30 ��� $
��Auto ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� .+��� $��0��$��Center=�
���  ��$
��Bold"�  * ViewUserSettings_Information#��� * �� $��1��$��1��
��������� $��2��$��Center=�$
��Left=��� -    �Name$ HyperlinkAbout��
���   ����� *
AboutUri+.����  + ViewUserSettings_About#�� + .��� $��3��$��Center=�$
��Left=��� -   $ HyperlinkNews��
���   ����� *	NewsUri+.����  , ViewUserSettings_News#�� , .��� $��4��$��Center=�$
��Left=��� -   $ HyperlinkProblems��
���   ����� *ProblemsUri+.����  - ViewUserSettings_Problems#�� - .��� $��5��$��Center=�$
��Left=��� -   $ HyperlinkPrivacyPolicy��
���   ����� *PrivacyPolicyUri+.����  %. !ViewUserSettings_PrivacyPolicyUri#�� . .��� $��6��$��Center=�$
��Left=��� -   $ HyperlinkHelp��
���   ����� *	HelpUri+.����  / ViewUserSettings_Help#�� / .�� -   $��	GridTrace��$��10q�$y�	Collapsed=� �� $��30 ��� $
��Auto ��� $��50 ��� $��30 ��� $
��Auto ��� $
��Auto ��� $��30 ��� $
��Auto ��� $
��Auto ��� $��30 ��� $
��Auto ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��30 ��� $
��Auto ��� $��50 ��� .+��� $��0��$��Center=�
���  ��$
��Bold"� 0 ViewUserSettings_Trace#��� 0 �� $��1��$��1��
��������� $��2��$��Center=� 1 ViewUserSettings_TraceMode#��� 1 �� -   $��CheckBoxTrace��$��2��$��Center=�$��Right=��� $��3��$��Center=�
���  ��$
��Bold"� $2  ViewUserSettings_CodecsAudioWifi#��� 2 �� $��4��$��1��
��������� -   $&�� StackPanelCodecSettingsAudioWifi��$��5���� $��6��$��Center=�
���  ��$
��Bold"� $3  ViewUserSettings_CodecsAudioCell#��� 3 �� $��7��$��1��
��������� -   $&�� StackPanelCodecSettingsAudioCell��$��8���� $��9��$��Center=�
���  ��$
��Bold"�  4 ViewUserSettings_CodecsVideo#��� 4 �� $��10��$��1��
��������� -   $"��StackPanelCodecSettingsVideo��$��11���� $��12��$��Center=�
���  ��$
��Bold"� 5 ViewUserSettings_Logs#��� 5 �� $��13��$��1��
��������� $��14��$��Center=� 6 ViewUserSettings_LogVoip#��� 6 �� $��14��$��30��$	��100��$��Right=� 7 ViewUserSettings_LogOpen#��� 7 ���� *CommandOpenLogVoip+�� $��15��$��Center=� #8 ViewUserSettings_LogHistoryCall#��� 8 �� $��15��$��30��$	��100��$��Right=�#��� 7 ���� *CommandOpenLogCallHistory+�� $��16��$��Center=� #9 ViewUserSettings_LogQualityCall#��� 9 �� $��16��$��30��$	��100��$��Right=�#��� 7 ���� *CommandOpenLogCallQuality+�� $��17��$��Center=� : ViewUserSettings_LogChat#��� : �� $��17��$��30��$	��100��$��Right=�#��� 7 ���� *CommandOpenLogChat+�� $��18��$��Center=� !; ViewUserSettings_LogsDatabase#��� ; �� $��18��$��30��$	��100��$��Right=�#��� 7 ���� *CommandOpenLogDatabase+�� $��19��$��Center=� #< ViewUserSettings_LogApplication#��� < �� $��19��$��30��$	��100��$��Right=�#��� 7 ���� *CommandOpenLogGui+�� $��20��$��Center=� = ViewUserSettings_LogTrace#��� = �� -   $��20��$��30��$	��100��$��Right=�#��� 7 �� $��21��$��Center=� > ViewUserSettings_LogRequest#��� > �� $��21��$��30��$	��100��$��Right=�#��� 7 ���� *CommandOpenLogRequest+�� -   $��22��$��30��$	��120��$��Right=� ? ViewUserSettings_LogClear#��� ? ���� *CommandClearLogs+�� $��23��$��Center=�
���  ��$
��Bold"� @ ViewUserSettings_Error#��� @ �� $��24��$��1��
��������� $��25��$��Center=�  A ViewUserSettings_ErrorReport#��� A �� -   $��26��$��30��$	��120��$��Right=� B ViewUserSettings_ErrorSend#��� B �� $��2��$��1��$��5q�$��Right=�$ 
Horizontal=�.+��� $��30��$	��130�� C ViewUserSettings_Save#��� C ���� *CommandSave+�� -   $��30��$��5q�$	��130�� D ViewUserSettings_Cancel#��� D 