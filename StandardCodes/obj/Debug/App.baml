   M S B A M L   `   `   `  ����O  Kmscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel 5      \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35X@clr-namespace:System.Windows.Data;assembly=PresentationFrameworkSystem.Windows.Data 5      H DStandardCodes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:StandardCodesStandardCodes 5	         StandardCodes.App   5      W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35I EWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=null^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5      0sys&clr-namespace:System;assembly=mscorlib  5      <col2clr-namespace:System.Collections;assembly=mscorlib  5      Acm8clr-namespace:System.ComponentModel;assembly=WindowsBase  5      Kdata@clr-namespace:System.Windows.Data;assembly=PresentationFramework  5      'localclr-namespace:StandardCodes  5	        �� 	Resources  5         bool2VisibilityConverter'  !StandardCodes.BackgroundConverter  BackgroundConverter"  StandardCodes.ColorConverter  ColorConverter!  StandardCodes.ItemConverter  ItemConverter  StandardCodes.BaSource  BaSource  OrdersDataProvider S� 
ObjectType$  StandardCodes.OrdersDataSource S� 
MethodName # BasicRowValidationErrorTemplate  defaultRowStyle �� 
TargetType,  &Microsoft.Windows.Controls.DataGridRow �� Property �� Value �� Triggers P� Property P� Value	 �� RelativeSource
 �� Path  customerRowStyle �� BasedOn 	 NewRow_ControlTemplate �� 
TargetType ��Name J� SnapsToDevicePixelsB  <Microsoft.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions=  7Microsoft.Windows.Controls.Primitives.DataGridRowHeader$   SelectiveScrollingOrientation �� AncestorType)	  #Microsoft.Windows.Controls.DataGrid! 	  HeadersVisibilityConverter �� 	Converter:
  4Microsoft.Windows.Controls.DataGridHeadersVisibility
 
  Row �� ConverterParameter 
 newItemRowStyle  defaultCellStyle-  'Microsoft.Windows.Controls.DataGridCell(  "System.Windows.Controls.GroupStyle  
gs_Default   PanelA  ;Microsoft.Windows.Controls.Primitives.DataGridRowsPresenter   HeaderTemplate �� Padding  defaultColumnHeaderStyle@  :Microsoft.Windows.Controls.Primitives.DataGridColumnHeader  DefaultRowHeaderStyle  
PopupStyle  DefaultGridStyle  CrossError_ControlTemplate �� Stretch " TextBoxColumnStyleWorkingCross��%,  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 �     &	 {    7  &	 G    7  &		 �    &	
 �    7 	 &	 �    &	 �    &	 _    &	 �    &	 	    &	 e	    &	 �	    &	 6    7  �� 5   
   6'     5   
   6$     5   
   6     5   
   6     5   
   6   S� 5   
   "  68   $ 	GetOrders��6e   �� 5   
   .R�5      ]� ��. .+�5      �� $��Right=�6   
������$��14(�6K   $
��Bold"�6Y   *6n   �� 5      
������$��2q�62   ._�5      �� $��adornerPlaceholder��60   6L   5      5      5      �� 5    
   "  6(   .�5!      ��  � ��$	 150��6*   �� 5"       �@ Header 6'   �� *Id+�� 5$       �@=�ErrorTemplate8   69    5%      P� 5&       �@ 	IsEditing . . �5'      ��  � ��
 �  ��64   �� 5(       � ��$ 2q�68   5)      P� 5*       �@=�HasError . . �5+      ��  �@�ToolTip 60   �� 	 �� *Self+$)
 #(Validation.Errors)[0].ErrorContent�5,      5.      �� 51   
   "  6)   8   .�52      ��  �@ Header 6'   Z� �� 53       �@=�ErrorTemplate 6:   Z� 54      �� 57   
   "  69   .R�58      �� $ 
DGR_Border��6   #��z��6)   #��z��6S   #��z��6    . ._�59         5:      �� 5;      $
��Auto �6+   �� 5<      $��* �6+    5?      �� 5@      $��* �6(   �� 5A      $
��Auto �6(   .+�5D      �� $#��Click here to add a new item.��6    $��1��6E     5F      $ Vertical=�6+   $��2��5G   +   y�5H   +   �� 	 �� " 	 $
 HeadersVisibility�# Z # Z 5I      5J      5K      �� 5N   
   "  6(   .�5O      ��  � ��8   6)   5P      �� 5S   
   "  6)    5T      P� 5U       �@ 	IsEditing . . �5V      ��  � ��
 �  ��64   �� 5W       � ��$ 2q�68   5X      5Z        5^   
    5_      �� 5`      .R�5a        5b       5d      �� 5e      .R�5f      �� $
��Bold"�6A   $ 3q�6S   ��6    �� $
 
First_Name�6S   5g      5i      �� 5l   
   "  61   .�5m      ��  � ��$ Tahoma)�6,   6>   �� 5n       � ��$ 10(�6*   68   �� 5o       � ��$
 Bold"�6,   6<   �� 5p       � ��$ 30��6(   66   �� 5q       � �� �#81A4D26+   �� 5r       � ��$ Center=�6;   �� 5t       � �� 5u      �� 5v      .R�5w      �� $
��Wrap=�6$   ��68   �� 6K   5x      5z      5|      �� 5}   
   "  6.   5�      �� 5�   
   " ��6#   .�5�      ��  � �� �#81A4D26+   5�      �� 5�   
   " 	 6)   .�5�      ��  � ��
 �����6+   5�      �� 5�   
   .R�5�      ]� ��. .+�5�      � $��Center=�6   $��1,0,0,0q�5�   (   $��Right=�5�   (   .+�5�      F� $
��Left=�6   $	��Top=�5�   +   $��16.575��5�   +   $��15.013��5�   +   ���	#FFFF0000$
 Fill=�5�   +   {��������@M� �����@M� ��Ȣ?"@��,@�� ����i@S\2� �i��@Q� `q�99�@Q� `q�99�@��4��$@ o� 	$��0,1.221,0,0q�5�   +   F� 5�      $
��Left=�6   $	��Top=�5�   +   $��12.077��5�   +   $��14.755��5�   +   ���	#FFFF0000$
 Fill=�5�   +   ����0�G0zX@5G�7�}�?p<DH��@T�Yt�@��� ᆼ *$� _�� S�k�{)<h^0@��� J�E��@��� J�E��@S�ӟ J�E��@�z��@�l*nq���G0zX@5G�7�}�?	$��2.832,1.183,0,0q�5�   +   F� 5�      $
��Left=�6   $��10.032,2.432,0,0q�5�   +   $	��Top=�5�   +   $
��6.52��5�   +   $��5.383��5�   +   ���	#FFAB0404$
 Fill=�5�   +   ����0O2��� @����{r@O2��� @����{r@ Kzs�"@yG|���@U�� ��E �@S8�� f5���@Q� Jb͒@Q� Jb͒@>4`5W!@5���� @	5�   +   F� 5�      $
��Left=�6   $��0.287,8.6,0,0q�5�   +   $	��Top=�5�   +   $
��8.35��5�   +   $��7.361��5�   +   ���	#FFAB0404$
 Fill=�5�   +   {����d�/���@�c� �d�/���@�c� Y�M �q� �^ �*�<$�"@SX�y _/��R@�RKά@If���!@�RKά@If���!@�ጫ���@��� 	5�   +   F� 5�      $
��Left=�6   $��2.815,1.686,0,0q�5�   +   $	��Top=�5�   +   $��4.308��5�   +   $��6.059��5�   +   ���	#FFAB0404$
 Fill=�5�   +   ����0��5��."@|H~�@��5��."@|H~�@����� @?;;t��@8�f�� @6g3=�@���/� @�Y!�S�
@�ȧ ���H��@�ȧ ���H��@Q�� ->�x�@	5�   +   F� 5�      $
��Left=�6   $��7.841,8.657,0,0q�5�   +   $	��Top=�5�   +   $��6.912��5�   +   $��7.304��5�   +   ���	#FFAB0404$
 Fill=�5�   +   e���p�� ����~]!@S�� ����~]!@��� �Ŧ�B�@K� �[��/@��� �v *U� u�� *U� u�� ��� Lj� 	5�   +   5�      5�      5�      �� 5�   
   " ��67    5�      P� 5�       �@=�HasError . . �5�      ��  � ��
 �z���6I   �� 5�       �@�ToolTip 60   �� 	 �� *Self+$)
 #(Validation.Errors)[0].ErrorContent�5�      5�      .�5�      ��  �@=�ErrorTemplate8   69   5�      5�      5�      