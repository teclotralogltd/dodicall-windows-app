   M S B A M L   `   `   `  ����   dodicall8    2dodicall.Styles.CustomizedWindow.VS2012WindowStyleW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TitleBarBackgroundBrush  �� Color  VS2012WindowBorderBrush # VS2012WindowBorderBrushInactive   VS2012WindowStatusForeground ( $VS2012WindowStatusForegroundInactive  BitmapImage �� 	UriSource # VS2012WindowStyleTitleBarButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property	 P� Value
 �� 
TargetName  VS2012WindowStyle&   System.Windows.Media.TextOptions   TextRenderingMode   TextFormattingMode �� Padding J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   Opacity   
BlurRadius   ShadowDepth   	Direction � ColumnDefinitions ��Name ��Name J� Opacity �� Path �� Mode �� RelativeSource ��Name �� Stretch �� EdgeMode �� AncestorType F�Name  ��Name! � Cursor	" �� X1# � ActualWidth	$ �� X2	% �� Y1	& �� Y2' � ActualHeight( ��Name) �� 	Resources* �� Triggers+ �� Binding, �� Value  VS2012WindowStandarsStyle 	 VS2012MessageBoxStyle +
 'VS2012MessageBoxStyleWithoutCloseButton  VS2012AboutStyle  VS2012WindowCallStyle   F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     %�.  &	        &	      &	 !     &	 4     &	 G     &	 Z     &	 �     7  &	     7  7  7   7  7  7  7  7  7  7  7  &	 _    7  7  7   7  7  7  7  7  7  7  7  &		 �    7  7  7   7  7  7  7  7  7  &	
 E    7  7  7   7  7  7  7  7  &	 #    7  7  7   7  7  7  7  7  7  &	 p(    7  7  7  7  7  7  7  �� $  White���� $
  Gray���� $  #999999���� $  #FFFFFF���� $  #FFFFFF���� $5 //dodicall;component/Resources/ddcall_icon32.png?��� " ��.���  � |� .  ��  � �� �� " ��.R�� .+��� $ PART_border��
������ ��  P�  �@J�IsMouseOver	 . . ��� $
 PART_border�� � ��
 ������� $
 PART_border�� �@J�Opacity$	 0.7Q�P�  � ��	 . . ��� $
 PART_border��8    � ���� $
 PART_border�� �@J�Opacity$ 1Q���  � �� �#FFF�� " �.���  �@�Icon8   ��  �@�AllowsTransparency . ��  �@�WindowStyle$
 None=���  � ��
 ���� ��  �@�
ResizeMode$ CanResizeWithGrip=�-   ��  � �� �� " �.R�� $��7q�$ 	ClearType=�$ Display=�.+��� $ PART_Border��$
��Auto��$
��Auto��
������ 8�� $��1q�$ 0q�   $ black��$	 0.5Q�$ 7Q�$ 2Q�$	 315Q�._�]� $��Stretch=�
�������$��Stretch=�.+��� $ TitleBar��-   $	��Top=�8�� $��0q�._�� $��32�� �� $
��Auto ��� �� $��34 ��� $��34 ��� $��34 �.+��� $
 Icon��-   $��0��$2��,/dodicall;component/Resources/logo_small.png��$
��Left=�$��3,0,0,0q�$��16���� $ Caption��$��1��$
��Left=�$��Center=�$��5,0,0,0q�$��Segoe UI)�$��12(�$
 0.66Q����� $ Title�$ OneWay=� �� *TemplatedParent+�� $ 	MinButton��-   $��2��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $��1��$
 None=�$ Aliased=�=����     z � z  z �    ��j � z ��j �    ��[ � z ��[ 	����  �� $ ButtonL�$ 
Foreground��� $ 	MaxButton��-   $��3��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $ PART_MaxButton_Path��$��1��$
 None=�$ Aliased=�X����        � z     � z  z �     z �        �    @B � z @B �    �� � z �� 	����  �� $ ButtonL�$ 
Foreground��� $ CloseButton��-   $��4��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $	��1.5��$
 None=�+����        � z  z �     z � z     	����  �� $ ButtonL�$ 
Foreground��� �� $  lnSizeNorth��-   
������ $! SizeNS��$" 1��#$ z# $% 1��$& 1��$��3���� $  lnSizeSouth��-   
������ $��Bottom=�$! SizeNS��$" 1��#$ z# #% z' #& z' $��3���� $  
lnSizeWest��-	   
������ $! SizeWE��$" 1��$$ 1��$% 1��#& z' $��3���� $  
lnSizeEast��-
   
������ $��Right=�$! SizeWE��#" z# #$ z# $% 1��#& z' $��3���� $( rectSizeNorthWest��-   $! SizeNWSE��
������ $��5��$��5��$	��Top=�$
��Left=��� $( rectSizeNorthEast��-   $! SizeNESW��
������ $��5��$��5��$	��Top=�$��Right=��� $( rectSizeSouthWest��-   $! SizeNESW��
������ $��5��$��5��$��Bottom=�$
��Left=��� $( rectSizeSouthEast��-   $! SizeNWSE��
������ $��5��$��5��$��Bottom=�$��Right=� P�  �@�WindowState$	 	Maximized=�. ��� $
 PART_MaxButton_Path�� � ��| ��    ��- �@KL ��- �@KL  z �     z �    ��- �     	= �@KL  	= ���-     � z     � z @KL ���j @KL ���- @B � z @B 	P�  �@�IsActive	 .  . ��� $
 PART_Border��8   � ��) ���� '��       " ��* �� + �� *
IsActive+ �� $ WindowL�, True.a���  � ��8  ��  � ��8  �� + �� *
IsActive+ �� $ WindowL�	, False.a���  � ��8 	 ��  � ��8 
 �� " �.���  �@�Icon8   ��  �@�AllowsTransparency . ��  �@�WindowStyle$
 None=���  � ��
 ���� ��  �@�
ResizeMode$ CanResizeWithGrip=���  � �� �� " �.R�� $��7q�$ 	ClearType=�$ Display=�.+��� $ PART_Border��$
��Auto��$
��Auto��
�������8�� $��1q�$ 0q�   $ black��$	 0.5Q�$ 7Q�$ 2Q�$	 315Q�._�]� $��Stretch=�
������ $��Stretch=�.+��� $ TitleBar��-   $	��Top=�8�� $��0q�._�� $��32�� �� $
��Auto ��� �� $��34 ��� $��34 ��� $��34 �.+��� $
 Icon��-   $��0��$2��,/dodicall;component/Resources/logo_small.png��$
��Left=�$��3,0,0,0q�$��16���� $ Caption��$��1��$
��Left=�$��Center=�$��5,0,0,0q�$��Segoe UI)�$��12(�$
 0.66Q����� $ Title�$ OneWay=� �� *TemplatedParent+�� $ 	MinButton��-   $��2��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $��1��$
 None=�$ Aliased=�=����     z � z  z �    ��j � z ��j �    ��[ � z ��[ 	����  �� $ ButtonL�$ 
Foreground��� $ 	MaxButton��-   $��3��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $ PART_MaxButton_Path��$��1��$
 None=�$ Aliased=�X����        � z     � z  z �     z �        �    @B � z @B �    �� � z �� 	����  �� $ ButtonL�$ 
Foreground��� $ CloseButton��-   $��4��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $	��1.5��$
 None=�+����        � z  z �     z � z     	����  �� $ ButtonL�$ 
Foreground��� �� $  lnSizeNorth��-   
������ $! SizeNS��$" 1��#$ z# $% 1��$& 1��$��3���� $  lnSizeSouth��-   
������ $��Bottom=�$! SizeNS��$" 1��#$ z# #% z' #& z' $��3���� $  
lnSizeWest��-   
������ $! SizeWE��$" 1��$$ 1��$% 1��#& z' $��3���� $  
lnSizeEast��-   
������ $��Right=�$! SizeWE��#" z# #$ z# $% 1��#& z' $��3���� $( rectSizeNorthWest��-   $! SizeNWSE��
������ $��5��$��5��$	��Top=�$
��Left=��� $( rectSizeNorthEast��-   $! SizeNESW��
������ $��5��$��5��$	��Top=�$��Right=��� $( rectSizeSouthWest��-   $! SizeNESW��
������ $��5��$��5��$��Bottom=�$
��Left=��� $( rectSizeSouthEast��-   $! SizeNWSE��
������ $��5��$��5��$��Bottom=�$��Right=� P�  �@�WindowState$	 	Maximized=�. ��� $
 PART_MaxButton_Path�� � ��| ��    ��- �@KL ��- �@KL  z �     z �    ��- �     	= �@KL  	= ���-     � z     � z @KL ���j @KL ���- @B � z @B 	P�  �@�IsActive	 .  . ��� $
 PART_Border��8   � ��) ���� '��       " ��* �� + �� *
IsActive+ �� $ WindowL�, True.a���  � ��8  ��  � ��8  �� + �� *
IsActive+ �� $ WindowL�	, False.a���  � ��8 	 ��  � ��8 
 �� " �.���  �@�Icon8   ��  �@�AllowsTransparency . ��  �@�WindowStyle$
 None=���  � ��
 ���� ��  �@�
ResizeMode$ CanResizeWithGrip=���  � �� �� " �.R�� $��7q�$ 	ClearType=�$ Display=�.+��� $ PART_Border��$
��Auto��$
��Auto��
�������8�� $��1q�$ 0q�   $ black��$	 0.5Q�$ 7Q�$ 2Q�$	 315Q�._�]� $��Stretch=�
������ $��Stretch=�.+��� $ TitleBar��-   $	��Top=�8�� $��0q�._�� $��32�� �� $
��Auto ��� $
��Auto ��� �� $��34 ��� $��34 ��� $��34 �.+��� $
 Icon��-   $��0��$2��,/dodicall;component/Resources/logo_small.png��$
��Left=�$��3,0,0,0q�$��16���� $��1��$��Center=�$��   - ���� $ Caption��$��2��$
��Left=�$��Center=�$��4,0,0,0q�$��Segoe UI)�$��12(�$
 0.66Q����� $ Title�$ OneWay=� �� *TemplatedParent+�� $ CloseButton��-   $��5��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $	��1.5��$
 None=�+����        � z  z �     z � z     	����  �� $ ButtonL�$ 
Foreground���  P�  �@�IsActive	 .  . ��� $
 PART_Border��8   � ��) ���� '��       " ��* �� + �� *
IsActive+ �� $ WindowL�, True.a���  � ��8  ��  � ��8  �� + �� *
IsActive+ �� $ WindowL�	, False.a���  � ��8  ��  � ��8  �� " �.���  �@�Icon8   ��  �@�AllowsTransparency . ��  �@�WindowStyle$
 None=���  � ��
 ���� ��  �@�
ResizeMode$ CanResizeWithGrip=���  � �� �� " �.R�� $��7q�$ 	ClearType=�$ Display=�.+��� $ PART_Border��$
��Auto��$
��Auto��
�������8�� $��1q�$ 0q�   $ black��$	 0.5Q�$ 7Q�$ 2Q�$	 315Q�._�]� $��Stretch=�
������ $��Stretch=�.+��� $ TitleBar��-   $	��Top=�8�� $��0q�._�� $��32�� �� $
��Auto ��� $
��Auto ��� �� $��34 ��� $��34 ��� $��34 �.+��� $
 Icon��-    $��0��$2��,/dodicall;component/Resources/logo_small.png��$
��Left=�$��3,0,0,0q�$��16���� $��1��$��Center=�$��   - ���� $ Caption��$��2��$
��Left=�$��Center=�$��4,0,0,0q�$��Segoe UI)�$��12(�$
 0.66Q����� $ Title�$ OneWay=� �� *TemplatedParent+��  P�  �@�IsActive	 .  . ��� $
 PART_Border��8   � ��) ���� '��       " ��* �� + �� *
IsActive+ �� $ WindowL�, True.a���  � ��8  ��  � ��8  �� + �� *
IsActive+ �� $ WindowL�	, False.a���  � ��8  ��  � ��8  �� " �.���  �@�Icon8   ��  �@�WindowStyle$
 None=���  � ��
 ���� ��  �@�
ResizeMode$ CanResizeWithGrip=���  � �� �� " �.R�� $��0q�$ 	ClearType=�$ Display=�.+��� $ PART_Border��$
��Auto��$
��Auto��
�������8�� $��1q�$ 0q�   $ black��$	 0.5Q�$ 7Q�$ 2Q�$	 315Q�._�]� $��Stretch=�
������ $��Stretch=�.+��� $ TitleBar��-!   $	��Top=�8�� $��0q�._�� $��32�� �� $
��Auto ��� $
��Auto ��� �� $��34 ��� $��34 ��� $��34 �.+��� $
 Icon��-"   $��0��$2��,/dodicall;component/Resources/logo_small.png��$
��Left=�$��3,0,0,0q�$��16���� $��1��$��Center=�$��   - ���� $ Caption��$��2��$
��Left=�$��Center=�$��4,0,0,0q�$��Segoe UI)�$��12(�$
 0.66Q����� $ Title�$ OneWay=� �� *TemplatedParent+�� $ CloseButton��-#   $��5��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $	��1.5��$
 None=�+����        � z  z �     z � z     	����  �� $ ButtonL�$ 
Foreground���  P�  �@�IsActive	 .  . ��� $
 PART_Border��8   � ��) ���� '��       " ��* �� + �� *
IsActive+ �� $ WindowL�, True.a���  � ��8  ��  � ��8  �� + �� *
IsActive+ �� $ WindowL�	, False.a���  � ��8  ��  � ��8  �� " �.���  �@�Icon8   ��  �@�AllowsTransparency . ��  �@�WindowStyle$
 None=���  � ��
 ���� ��  �@�
ResizeMode$ CanResizeWithGrip=���  � �� �� " �.R�� $��7q�$ 	ClearType=�$ Display=�.+��� $ PART_Border��$
��Auto��$
��Auto��
������ 8�� $��1q�$ 0q�   $ black��$	 0.5Q�$ 7Q�$ 2Q�$	 315Q�._�]� $��Stretch=�
������ $��Stretch=�.+��� $ TitleBar��-$   $	��Top=�
������ $��0q�._�� $��30�� �� $
��Auto ��� $
��Auto ��� �� $��34 ��� $��34 ��� $��34 �.+�� $��6��
���   �$	 0.6Q��� $
 Icon��-%   $��0��$1��+/dodicall;component/Resources/LogoWhite.png��$
��Left=�$��15,0,0,0q�$��11���� $ Caption��$��2��$
��Left=�$��Center=�$��4,0,0,0q�$��Segoe UI)�$��12(�$
 0.66Q����� $ Title�$ OneWay=� �� *TemplatedParent+��  P�  �@�IsActive	 .  . ��� $
 PART_Border��8   � ��) ���� '��       " ��* �� + �� *
IsActive+ �� $ WindowL�, True.a���  � ��8  ��  � ��8  �� + �� *
IsActive+ �� $ WindowL�	, False.a���  � ��8  ��  � ��8  