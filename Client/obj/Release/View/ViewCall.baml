   M S B A M L   `   `   `  ���� D  @dodicall, Version=2.16.4.0, Culture=neutral, PublicKeyToken=null-clr-namespace:dodicall.Viewdodicall.View  7 clr-namespace:dodicall.Converterdodicall.Converter      dodicall.View.ViewCall   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:dodicall.View     6	converter clr-namespace:dodicall.Converter     -   $��ViewCallUserControl��.��� .+��� -   $��ListBoxCallHistory��$��0q����� *20CurrentModelCallHistory.ListModelCallHistoryPeer+  �� SelectedItem  �� *CurrentModelCallHistoryPeer+���� .R�� -   $��50��$
��Left=�����  �� ElementName$ ViewCallUserControl�� �� Path$ WidthChatItem� � ContextMenu �� .@�m� $��ContextMenuCall��-      ViewCall_ContextMenuCall#���   m� $��ContextMenuWriteMessage��-    $  ViewCall_ContextMenuWriteMessage#���  ����  �� 
TargetType" m� �� Triggers ^�  ^� 
Conditions ��  �� Binding �� *ModelPeerObj.IsDodicall+ �� Value true��  �� *'%ModelPeerObj.ModelContactObj.IsFriend+ true.0��� 	 �� Property	 � {�
 �� Value
 . ��  �� Binding �� *ModelPeerObj.IsDodicall+ �� Value	 false.a��� 	 � {�
 .  ��  �� *'%ModelPeerObj.ModelContactObj.IsFriend+	 false.a��� 	 � {�
 .  m� $��ContextMenuCreateContact��-    % !ViewCall_ContextMenuCreateContact#���  ���� " m� ^�  ��  �� *ModelPeerObj.IsDodicall+	 false��  �� *'%ModelPeerObj.ModelContactObj.IsFriend+	 false.0��� 	 � y�$
 Visible=���  �� * ModelPeerObj.ExistModelContact+	 false.a��� 	 � y�$
 Visible=���  �� * ModelPeerObj.ExistModelContact+ true.a��� 	 � y�$
 	Collapsed=���  �� *ModelPeerObj.IsDodicall+ true.a��� 	 � y�$
 	Collapsed=���  �� *'%ModelPeerObj.ModelContactObj.IsFriend+ true.a��� 	 � y�$
 	Collapsed=� � ColumnDefinitions �� $��50 ��� .+��� $��2��� $��0��$��0,0,10,0q�$��36��$��36��.+��� ���� *ModelPeerObj.Avatar+�� $��Right=�$��Bottom=�$��15��$��15��$6��0/dodicall;component/Resources/IconTray_small.png��y��� $ ModelPeerObj.IsDodicall� �� 	Converter ;   5dodicall.Converter.ConverterBoolToVisibilityCollapsed  � $��1�� � RowDefinitions �� �� .+�� $��0�� �� $��* ��� $
��Auto �.+�� $��0��$
��Left=� �� $��* ��� $
��Auto �.+��� $��TextBlockTitle��$��0��$��14(�$��CharacterEllipsis=�$��Center=����� *ModelPeerObj.Title+�� $��TextBlockMissed��$��1��$��14(�$��Center=�
���  ������ *20ModelCallStatisticsObj.NumberOfMissedCallsString+�� $��1��$��11(�
�������$��Center=����� *LastModifiedDateString+�� $��1�� �� Orientation$ 
Horizontal=�$
��Left=�.+��� $��ImageSrtpIncoming��$��3,0,0,0q�$��17��$��12��$y�	Collapsed=�$0��*/dodicall;component/Resources/srtpGray.png���� $��8,0,0,0q�$��11��$��11��$:��4/dodicall;component/Resources/IncomingSuccessful.png���� $��3,0,0,0q����#00b300$��14(�$��Center=����� *86ModelCallStatisticsObj.NumberOfIncomingSuccessfulCalls+�� $��8,0,0,0q�$��11��$��11��$4��./dodicall;component/Resources/IncomingFail.png���� $��3,0,5,0q����#ff0019$��14(�$��Center=����� *:8ModelCallStatisticsObj.NumberOfIncomingUnsuccessfulCalls+�� $��ImageSrtpOutgoing��$��8,0,0,0q�$��17��$��12��$y�	Collapsed=�$0��*/dodicall;component/Resources/srtpGray.png���� $��8,0,0,0q�$��11��$��11��$:��4/dodicall;component/Resources/OutgoingSuccessful.png���� $��3,0,0,0q����#00b300$��14(�$��Center=����� *86ModelCallStatisticsObj.NumberOfOutgoingSuccessfulCalls+�� $��8,0,0,0q�$��11��$��11��$4��./dodicall;component/Resources/OutgoingFail.png���� $��3,0,0,0q����#ff0019$��14(�$��Center=����� *:8ModelCallStatisticsObj.NumberOfOutgoingUnsuccessfulCalls+ �� Triggers �� $
 true�� �� *1/ModelCallStatisticsObj.HasIncomingEncryptedCall+.a���  �� 
TargetName$ ImageSrtpIncoming��	 � y�$
 Visible=��� $ false�� �� *1/ModelCallStatisticsObj.HasIncomingEncryptedCall+.a��� $ ImageSrtpIncoming��	 � y�$
 	Collapsed=��� $
 true�� �� *1/ModelCallStatisticsObj.HasOutgoingEncryptedCall+.a��� $ ImageSrtpOutgoing��	 � y�$
 Visible=��� $ false�� �� *1/ModelCallStatisticsObj.HasOutgoingEncryptedCall+.a��� $ ImageSrtpOutgoing��	 � y�$
 	Collapsed=��� $
 true�� �� *" ModelCallStatisticsObj.HasMissed+.a��� $ TextBlockMissed��	 � y�$
 Visible=��� $ TextBlockTitle��	 � ��

 �  ���� $ false�� �� *" ModelCallStatisticsObj.HasMissed+.a��� $ TextBlockMissed��	 � y�$
 	Collapsed=��� $ TextBlockTitle��	 � ��

 �   �