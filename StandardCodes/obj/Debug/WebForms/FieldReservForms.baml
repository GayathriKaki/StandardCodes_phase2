   M S B A M L   `   `   `  ����O  Kmscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel 5      H DStandardCodes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:StandardCodesStandardCodes 5      -   'StandardCodes.WebForms.FieldReservForms   5      W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35I EWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=null^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5      0sys&clr-namespace:System;assembly=mscorlib  5      <col2clr-namespace:System.Collections;assembly=mscorlib  5      Acm8clr-namespace:System.ComponentModel;assembly=WindowsBase  5      'localclr-namespace:StandardCodes  5        N� Title$  FieldReservForms��5	      $	��400��6    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5   
   �� 5      $ ../Themes/ScTheme.xaml?�6!   %    5      .��5      � -   5        $��myGrid��5      $��10q�6    5   
    S� IsAsynchronous   categoriesDataProvider S� 
ObjectType  StandardCodes.Categories S� 
MethodName  DistrictDataProvider"  StandardCodes.DebugConverter  DebugConverter��%�   &	        &	 8     &	 n     S� 5       . "  6V   $ GetCategories��6}   S� 5       . "  6T   $ GetDistrict��6{     5      6#   5       � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   $��0��6*   �� 5      $
��Auto �6   �� 5       $��* �6   �� 5!      $
��Auto �6   $��23��6*   �� 5"      $
��Auto �6   .+�5(   
   "  StandardCodes.CustomDataGrid  -   5        	  Name$	 DataGrid_Field��5(      "  StandardCodes.CustomDataGrid
   CellEditEnding$
 DataGrid_CellEditEnding��67      
MouseLeave$ DataGrid_Field_MouseLeave��6`      GotFocus$ DataGrid_Field_GotFocus��6�   -  'System.Windows.Input.KeyboardNavigation   AcceptsReturn . )  #Microsoft.Windows.Controls.DataGrid"   VerticalScrollBarVisibility$
 Auto=�5*      $   HorizontalScrollBarVisibility$
 Auto=�6>   $��2��6f      ClipboardCopyMode$ IncludeHeader=�6s      AutoGenerateColumns .  
������    RowHeaderWidth$ 10Q�5-         	RowHeight$ 40Q�5.         RowBackground
 �����   AlternatingRowBackground
 ����� �� AlternationCount$ 2��51         SelectionUnit$ CellOrRowHeader=�52        defaultRowStyle   RowStyle# [ 53        DefaultRowHeaderStyle   RowHeaderStyle# [ 54        defaultColumnHeaderStyle   ColumnHeaderStyle# [ 55        defaultCellStyle   	CellStyle# [ 56      $��	11,52,0,0q�6D      Columns 58      3  -Microsoft.Windows.Controls.DataGridTextColumn  5:      /  )Microsoft.Windows.Controls.DataGridColumn   Header$ 
Field Name��5;   (      Width8	  2Microsoft.Windows.Controls.DataGridLengthConverter$ 90	 6<     tb_ElementStyle4
  .Microsoft.Windows.Controls.DataGridBoundColumn 
  ElementStyle# [ 5<   K     
  Binding  6(   �� ! �� Path$! 	FIELDNAME�" 
  EditingElementStyle" 5=      �� 5>      # �� 
TargetType"# ��6    .�5?      �� $ �� Property$ �@��	MaxLength% �� Value$% 60��6:   5@      5A      5C      7  1Microsoft.Windows.Controls.DataGridComboBoxColumn  5E      $ State��6,   $	 100	 6;   &   SelectedValuePath$& 
STATE_NAME��5G   ,   '   DisplayMemberPath$' 
STATE_NAME��5H   ,   (   SelectedValueBinding( 5F   ,   �� $! CurrentCategory�)   ItemsSource) 5I   ,   �� * �� Source#* [  5K        5M      $ County Name/Code��6,   $	 110	 6F   $& COUNTY_NAME��5O   ,   $' COUNTY_NAME��5P   ,   ( 5N   ,   �� $! CurrentProduct�+   ElementStyle+ 5Q      �� 5R      "# ��6    .�5S      �� $ � ��% 6<   �� $! ProductsInCategory�5T      ,   EditingElementStyle, 5V      �� 5W      "# ��6    .�5X      �� $ � ��% 6<   �� $! ProductsInCategory��� 5Y      $ �@��
IsEditable% . 6;   5Z      5\        5^      $	 100	 6-   $ District ( TX Only)��5_   (   -   5        cmbDistrict��$& DISTRICT_NAME��5c   ,   $' DISTRICT_NAME��6N   ( 5`   5   �� $! DISTRICT�) 5b   ,   �� #* [ + 5d      �� 5e      "# ��6    .�5f      �� %$ �@��IsSynchronizedWithCurrentItem$% FalseY�6N   �� 5g      $ � ��$
% left=�6E   6U   5h      5j        5l      $ MMS Field Name��5m   (   $	 100	 6A   # [ 5o   (     5n   (   �� $! MMSFieldName�" 5p      �� 5q      "# ��6    .�5r      �� $ �@��	MaxLength$% 12��6:   5s      5t      5v        5x      $ Source��6-   $ 70	 6=   -   SelectedItemBinding- 6I   �� $! 
MMS_SOURCE�) 5y      "   System.Collections.ArrayList  5z      ��5{      PI��6-   ��5|      MMS��6.   5}      + 5      �� 5�      "# ��6    .�5�      �� $ � ��$
% left=�6E   6U   5�      5�        5�      $ Temp Gradient��5�   (   $ 90	 6?   # [ 5�   (     5�   (   �� $! TempGradient�" 5�      �� 5�      "# ��6    .�5�      �� $ �@��	MaxLength$% 9��6:   5�      5�      5�        5�      $ Field State Code��5�   (   $	 100	 6C   # [ 5�   (     5�   (   �� $! FieldStateCode�" 5�      �� 5�      "# ��6    .�5�      �� $ �@��	MaxLength$% 12��6:   5�      5�      5�        5�      $' !Alt State (Federal Offshore Only)��5�   (   $	 180	 6S   # [ 5�   (     5�   (   �� $! AltState�" 5�      �� 5�      "# ��6    .�5�      �� $ �@��	MaxLength$% 12��6:   5�      5�      5�        5�      $ Standard Code��5�   (   $ 90	 6?   # [ 5�   (     5�   (   �� $! StandardCode�" 5�      �� 5�      "# ��6    .�5�      �� $ �@��	MaxLength$% 12��6:   5�      5�      5�        5�      $ Comments��5�   (   $ 90	 6;   # [ 5�   (     5�   (   �� $! 
Comments_F�" 5�      �� 5�      "# ��6    .�5�      �� $ �@��	MaxLength$	% 240��6:   5�      5�      5�        5�      $ Pool_ID��5�   (   $ 60	 6:   # [ 5�   (     5�   (   �� $! Pool_ID�" 5�      �� 5�      "# ��6    .�5�      �� $ �@��	MaxLength$% 12��6:   5�      5�      5�        5�      $ Field_ID��5�   (   $ 60	 6<   # [ 5�   (     5�   (   �� $! Field_ID�" 5�      �� 5�      "# ��6    .�5�      �� $ �@��	MaxLength$% 12��6:   5�      5�      5�        5�      $ State��5�   (   $	 100	 67   # [ 5�   (     5�   (   �� $! State_p�" 5�      �� 5�      "# ��6    .�5�      �� $ �@��	MaxLength$% 12��6:   5�      5�      5�        5�      $ Form_ID��5�   (   $ 60	 69   # [ 5�   (     5�   (   �� $! Form_ID�" 5�      �� 5�      "# ��6    .�5�      �� $ �@��	MaxLength$% 20��6:   5�      5�      5�        5�      $ Comments��5�   (   $ 80	 6:   # [ 5�   (     5�   (   �� $! 
Comments_P�" 5�      �� 5�      "# ��6    .�5�      �� $ �@��	MaxLength$	% 240��6:   5�      5�      5�        5�      $ Reservoir(Pool) Name��5   (   $	 130	 6F   # [ 5  (     5  (   �� $! ReservoirName�" 5     �� 5     "# ��6    .�5     �� $ �@��	MaxLength$% 60��6:   5     5     5	       5     $ State RSVR Code��5  (   $	 100	 6B   # [ 5  (     5  (   �� $! StateRSVRCode�" 5     �� 5     "# ��6    .�5     �� $ �@��	MaxLength$% 60��6:   5     5     5       5     $ STATE ��5  (   $	 100	 69   $& 
STATE_NAME��5  ,   $' 
STATE_NAME��6K   ( 5  (   �� $! 
STATE_NAME�- 5  .   �� $! 
STATE_NAME�.   TextBinding. 6_   �� *
STATE_NAME+) 5  ,   ��   StateCollection#* [ + 5     �� 5     "# ��6    .�5     �� %$ �@��IsSynchronizedWithCurrentItem$% FalseY�6N   �� 5      $ � ��% 6<   �� $! 
STATE_NAME��� 5!     $ � ��$
% left=�6E   6U   5"     , 5$     �� 5%     "# ��6    .�5&     �� $ � ��% 6<   �� $! 
STATE_NAME��� 5'     $ �@��IsDropDownOpen% . 6?   5(     5*       5,     $ Standard Code��5-  (   $ 90	 6?   # [ 5/  (     5.  (   �� $! StandardCode_R�" 50     �� 51     "# ��6    .�52     �� $ �@��	MaxLength$% 12��6:   53     54     56       58     $ Formation Name��59  (   $	 100	 6A   # [ 5;  (     5:  (   �� $! FormationName�" 5<     �� 5=     "# ��6    .�5>     �� $ �@��	MaxLength$% 60��6:   5?     5@     5B       5D     $	 API��5E  (   $	 150	 67   # [ 5G  (     5F  (   �� $	! API�" 5H     �� 5I     "# ��6    .�5J     �� $ �@��	MaxLength$% 50��6:   5K     5L     5N       5P     $ Region��5Q  (   $	 100	 69   $& REGION��5U  ,   $' REGION��6G   ( 5R  (   �� $! REGION�- 5S  .   �� $! REGION�. 6[   �� *REGION+) 5T  ,   ��  	 RegionCollection#* [	 + 5V     �� 5W     "# ��6    .�5X     �� %$ �@��IsSynchronizedWithCurrentItem$% FalseY�6N   �� 5Y     $ � ��% 6<   �� $! REGION��� 5Z     $ � ��$
% left=�6E   6U   5[     , 5]     �� 5^     "# ��6    .�5_     �� $ � ��% 6<   �� $! REGION��� 5`     $ �@��IsDropDownOpen% . 6?   5a     5c       5e     $ 70	 6*   $ Perf Top��5f  (   # [ 5h  (     5g  (   �� $	! Top�" 5i     �� 5j     "# ��6    .�5k     �� $ �@��	MaxLength$% 50��6:   5l     5m     5o       5q     $ Perf Bottom��5r  (   $ 70	 6>   # [ 5t  (     5s  (   �� $! Bottom�" 5u     �� 5v     "# ��6    .�5w     �� $ �@��	MaxLength$% 50��6:   5x     5y     5{       5}     $ TD��5~  (   $ 50	 65   # [ 5�  (     5  (   �� $! TD�" 5�     �� 5�     "# ��6    .�5�     �� $ �@��	MaxLength$% 11��6:   5�     5�     5�       5�     $ Geo Age��5�  (   $ 60	 69   # [ 5�  (     5�  (   �� $! GeoAge�" 5�     �� 5�     "# ��6    .�5�     �� $ �@��	MaxLength$% 3��6:   5�     5�     5�       5�     $ Basin��5�  (   $	 160	 68   $& GEOLOGIC_PROVINCE��5�  ,   $' 	LONG_NAME��6R   ( 5�  (   �� $! GEOLOGIC_PROVINCE�- 5�  .   �� $! GEOLOGIC_PROVINCE�. 6f   �� *GEOLOGIC_PROVINCE+) 5�  ,   ��  
 BasinCollection#* [
 5�       5�     $ Comments��5�  (   $	 100	 6;   # [ 5�  (     5�  (   �� $! Comments_FR�" 5�     �� 5�     "# ��6    .�5�     �� $ �@��	MaxLength$	% 240��6:   5�     5�     5�     7  1Microsoft.Windows.Controls.DataGridCheckBoxColumn  5�     $ 80	 6,   $ Active��67   /   
IsReadOnly/ . 0 �� 
IsSelected0 .   6G   �� $! 
ACTIVE_IND�6~   5�     �� 5�  
   -   5        $��btn_save��5�  X   $��1��6   $��3��6!   $��23��6.   $��Center=�6:   $	��Top=�6h   $��75��6�   .��6�   Send Request6�   �� 5�  
   -   5        $��	lblHeader��5�  E   $��2��6   $��28��6   $
��Left=�6)   $	��Top=�6V   $	��300��6n   $��12(�6z   $
��Bold"�6�   $��11,0,0,0q�6�   .��6�   #!Field Reservoir Formation - Field6�   	� 5�  
   -   5        $��RdField��5�  B   $��2��6   $��16��6#   $��	50,33,0,0q�6/   1 	� 	GroupName$	1 FRF��6Q   $	��Top=�6a   $
��Left=�6�   $��41��6�   .��6�   Field6�   	� 5�  
   -   5        $��RdPoolInField��5�  ^   $��2��6   $��16��6#   $
��Left=�6/   $��
102,33,0,0q�6J   $	1 FRF��6s   $	��Top=�6�   $��83��6�   .��6�   Pool In Field6�   	� 5�  
   -   5        $��RdReservoir��5�  ^   $��2��6   $��16��6#   $
��Left=�6/   $��
192,33,0,0q�6J   $	1 FRF��6q   $	��Top=�6�   $��63��6�   .��6�   	Reservoir6�   	� 5�  
   -	   5        $��RdFormation��5�  n   $��2��6   $	1 FRF��6#   $��16��63   $
��Left=�6?   $��
264,33,0,0q�6Z   $	��Top=�6�   $��65��6�   .��6�   	Formation6�   	� 5�  
   -
   5        $��RdAll��5�  n   $��2��6   $	1 FRF��6#   $��16��63   $
��Left=�6?   $��
339,33,0,0q�6Z   $	��Top=�6{   $��49��6�   .��6�   All6�   5�     5�     