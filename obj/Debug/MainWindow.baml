   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   I EAudioExtractor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AudioExtractorAudioExtractor 5   	      AudioExtractor.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   .localclr-namespace:AudioExtractor     5   	     � Title$  
MainWindow��5	   	   $	��350��6   $	��525��6)    � 	Resources 5      ,  &AudioExtractor.InverseBooleanConverter   InverseBooleanConverter��%   &	          5   
   6(   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   �� -   5         ��Name$ UrlBox��5      $��0��5      $��Stretch=�5      $��20q�5       �� TextWrapping$
 Wrap=�5      $��Stretch=�5      " �� VerticalScrollBarVisibility$
 auto=�5       �� AcceptsReturn . 5      � 5   
   $��1��5      $��Center=�5        5!      �� 5"      $
��auto �6    �� 5#      $
��auto �6     � ColumnDefinitions 5%      �� 5&      $
��auto �6#   �� 5'      $
��auto �6#   .+�5)      �� -   5         ��Name$ ExtractButton��5*      $��0��5+      $��0��5,      $��Extract!��5-      $��35��5.      $	��162��5/      $��0,0,20,0q�50      $��Center=�51      $��Center=�52      {�53      �� *
InProgress+	 �� 	Converter#	 [  �� 54      $��0��55      $��1��56      .+�57      �� -   5        $ OutputPathButton��58      $��Output path...��59      $
��Left=�5:      $	��Top=�5;      $��90��5<      $��0,0,0,3q�5=      {�5>      �� *
InProgress+#	 [  �� 5?      -   5        $ OutputPathBox��5@      $
��Left=�5A      $��10��5B      $
 Wrap=�5C      $	��Top=�5E      $	��188��5F      ��5D      �� *
OutputPath+{�5G      �� *
InProgress+#	 [  5H      �� 5I      $��1��5J      $��0��5K      $��2��5L      
 �� Orientation$
 
Horizontal=�5M      $��Center=�5N      $��Center=�5O      $��0,20,0,0q�5P      .+�5Q      )   #MahApps.Metro.Controls.ProgressRing  $��0,0,20,0q�5S      $��30��5T         IsActive 5R      �� *
InProgress+5T      �� 5U      -   5         ��Name$ StatusMessages��5V      $��35��5X      $
��Wrap=�5Y      $��Center=�5Z      $��Center=�5[      ��5W      �� *Status+5[      5\      5]      5_      5`      