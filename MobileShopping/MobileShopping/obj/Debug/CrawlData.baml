   M S B A M L   `   `   `  ����  MobileShopping     MobileShopping.CrawlWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
Crawl Data��5   	   $	��546��6   $��771.125��6)    � DataContext 5   	   ��  �� RelativeSource �� *Self+.��5      �  � ColumnDefinitions 5   
   �� 5      .+�5
   
   �� -   5        $��	lvProduct��5
   (   $��10,41,10,50q�6   ��69   �� *Data+ �� View 5      �� 5      .P�5      �� $��Sản phấm��6%    �� Width$	 350��67    �� DisplayMemberBinding 6C   �� *ProductName+�� 5      $
��Giá��6%   $	 100��62    6>   �� *Price+�� 5      $��Hình ảnh��6%   $	 300��67    �� CellTemplate 5      �� 5      .R�5   "   �� $��120px��6E   $
��70px��6S   ��6(   �� *	Thumbnail+6S   5      5      5      5      �� 5   
   -   5        $��btnLast��5   K   $��23��6   $
��Left=�6   $��	10,0,0,18q�68   $��Bottom=�5      $	��114��6,   .��6M   Tất cả sản phẩm6^   5      5      