   M S B A M L   `   `   `  ���� K  GTwoZeroFourEight, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TwoZeroFourEightTwoZeroFourEight  &     TwoZeroFourEight.Win_GameSetting   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:TwoZeroFourEight       � WindowStartupLocation$  CenterScreen=� � Title$ 游戏选项�� � Icon$ Images\Brain.ico��$	��800��$	��500�� � SizeToContent$ Height=�.��� 
������� � RowDefinitions �� �� �� �� ��  � ColumnDefinitions �� .+��� $��0��$��0��$��2��$��Center=�$��Center=�$	��730��$��46��$��26(�
��� ���.��US三步完成你的个性化设置(格子内容个数要与格子颜色个数对应)�� $��1��$��0��$��10,0,0,0q� �� Orientation$ 
Horizontal=�$��40��.+��� $��5q�$��36��$��18(�
���� ��$��Center=�.��,*第一步：设置游戏板行数及列数�� $��行数��$��	15,11,0,0q�$��18(�$��36��$��Center=�
����  ��� -   $��txt_RowCount��$	��100��$��10,0,0,0q�$��18(�$��Center=��� $��列数��$��	15,11,0,0q�$��18(�$��36��$��Center=�
����  ��� -   $��txt_ColumnCount��$	��100��$��10,0,0,0q�$��18(�$��Center=��� $��2��$��0��$��10,0,0,0q�$ 
Horizontal=�$	��100��.+��� $��5q�$��36��$��18(�
���� ��$��Center=�.��)'第二步：设置每一级格子内容�� -   $��txt_GridContent��$��15,2,0,2q�$	��500��$	��100�� �� TextWrapping$
 Wrap=�$��13(��� $��3��$��0��$��10,0,0,0q�$ 
Horizontal=�$	��100��.+��� $��5q�$��36��$��18(�
���� ��$��Center=�.��)'第三步：设置每一级格子颜色�� -   $��txt_GridColor��$��15,2,0,2q�$	��500��$	��100��$
 Wrap=�$��13(��� $��4��$��0��$��10,0,0,0q�$ 
Horizontal=�$��Center=�.+��� -    ��Name$ btn_Confirm��$��确定��$	��100��$��40��$��	10,0,15,0q�$��18(�   btnStyle#��[  �� -   $ 
btn_Cancel��$��取消��$	��100��$��40��$��	10,0,15,0q�$��18(�#��[  