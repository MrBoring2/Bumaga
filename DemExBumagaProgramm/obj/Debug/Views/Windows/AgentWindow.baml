   M S B A M L   `   `   `  ����N  JDemExBumagaProgramm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:DemExBumagaProgramm.Views.Windows!DemExBumagaProgramm.Views.Windows  5   	   3    -DemExBumagaProgramm.Views.Windows.AgentWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:DemExBumagaProgramm.Views.Windows     5   	     � Title$#  Карточка агента��5   	   $	��670��6!   $	��915��6.    � 
ResizeMode$ NoResize=�6:    � Icon$W Q/DemExBumagaProgramm;component/Resources/Images/Приятный шелест.png��6P    �� BitmapScalingMode$ HighQuality=�6�   .��5	      �  � RowDefinitions 5
   
   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $#��Карточка агента��6   $��Center=�6+   $��2��6H   $��30(�6\   $	��Top=�6j   $	��5,0q�6�   $��34��6�   �� 5   
   $��0��6   $��1��6%   $��5q�62   $��2��6=   .+�5      �� $��Наименование��6   .��5      �� ��6   �� *	AgentName+5      �� 5      $��Тип агента��6   .��5      ��  �� DisplayMemberPath$
 Name��6a    �� SelectedValuePath$ Id��6�   ��6   �� *
AgentTypes+ �� SelectedItem 6>   �� *	AgentType+	 �� SelectedValue	 6z   �� *AgentType.Id+6�   5      �� 5      $��Приоритет��6   .��5      �� ��6   �� *
Priority+5      �� 5      $��
Адрес��6   .��5      �� ��6   �� *Adres+5       �� 5!      $��ИНН��6   .��5"      �� ��6   �� *INN+5#      �� 5$      $��КПП��6   .��5%      �� ��6   �� *KPP+5&      �� 5'      $��Имя директора��6   .��5(      �� ��6   �� *DirectorName+5)      �� 5*      $��Телефон��6   .��5+      �� ��6   �� *PhoneNumber+5,      �� 5-      $��Email��6   .��5.      �� ��6   �� *Email+5/      �� 50      $	��100��6   $	��100��6!   $��5q�6F   ��6-   �� *Image+6F   �� 51      -   5        
 ��Name$
 	LoadImage��51      $��Загрузить��6(   $	��150��6<   $��5,0,5,5q�6H   6Y   52      �� 54   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Center=�6K   $��2��6f   $��2��6s   .+�55      �� -   5        $

 Save��55      $��Сохранить��6#   $	��150��67   $��5q�6C   6N   �� 56      -   5        $
 Delete��56      $��Удалить��6%   $	��150��67   $��5q�6C   y�6N   �� *RemoveVisivility+6v   �� 57      -   5        $
 Cancel��57      $��Отмена��6%   $	��150��66   $��5q�6B   6M   58      �� 59   
   $��1��6   $��1��6    $��5q�60   $/��)Реализоация продукции��6;   .��5:      �� .+�5;      �� $��Добавить��6   .��5<      �� .+�5=      �� $	��2,2q�6"   ��6/   �� *Search+ �� UpdateSourceTrigger$ PropertyChanged=��� 5>      $��2,0,2,2q�6#   $
 Name��64   ��6M   �� *DisplayedProducts+ 6w   �� *SelectedProduct+5?      5@      &   System.Windows.Controls.DataGrid  5B         AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserResizeColumns .  $	��406��6:  $	��Top=�6G  
������ $	��0,5q�6x  $
��Auto=�6�  ��6�   �� *
AgentSales+ 6  �� *SelectedProductSale+   Columns 5C      4  .System.Windows.Controls.DataGridTemplateColumn  5D      ,  &System.Windows.Controls.DataGridColumn   Header$' !Название продукта��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6L      CellTemplate 5E      �� 5F   "    �� DataType*  $System.Windows.Controls.DataGridCell"  6/   .R�5G   &   �� $
��Wrap=�6N   ��60   �� *Product.Name+6N   5H   #   5J      0  *System.Windows.Controls.DataGridTextColumn  5K      $ Количество��6-   $ * 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *Count+6[     5L      $ Дата продажи��6-   $ * 6C    6M   �� *DatefRealization+ �� StringFormat$ {0:dd.MM.yyyy}��5N      �� 5O      -   5        $
 RemoveFromSales��5O      $&�� Удалить из списка��62   $��5q�6N   6Y   5P      5R      5S      5T      