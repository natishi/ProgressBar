   M S B A M L   `   `   `  ���� J  FProgressBar, Version=12.11.20.18, Culture=neutral, PublicKeyToken=null-clr-namespace:SortingStatusSortingStatus      SortingStatus.UCPrgBar   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:SortingStatus     -     � Cursor$
  Hand��.���  � ColumnDefinitions �� $��* � � RowDefinitions �� $
��Auto ��� $��* �.+�� $��0��$��RightToLeft=� �� $
��Auto � �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� -    ��Name$ lblMinimize��$��Center=�$��Right=�$��-��$
��Bold"�$��0��$��0��$
  Hand��$��0,0,5,0q�$��14(� � ToolTip$ מזער���� -    ��Name$ 	cbTopMost��$��Center=�$��Right=�$��0��$��1��$
  Hand��$��0,5,5,0q�$ נעילת מסך��.��נעילת מסך�� -    ��Name$ imgWarningIcon��$��Center=�$��Right=�$��0��$��3��$ יחס אינו חוקי��$��1,0,0,0q�� $��1,5,1,5q�$��2��.+�� -   $��progressBar��$��0q� � Value$ 0Q����#bdbdbd � Minimum$ 0Q����� *	PbColor+	 �� Mode$	 TwoWay=��� $��Center=�$��Center=��� 
�������$��14(���� *WindowHeader+ �� $��14(�
���������� 
 �� ElementName$
 progressBar�� �� Path$ Value� �� StringFormat$ {0:0}%�� �� $	� - ��
������� �� $
��Bold"�
�������$��14(���� *
Progress+$	 TwoWay=�