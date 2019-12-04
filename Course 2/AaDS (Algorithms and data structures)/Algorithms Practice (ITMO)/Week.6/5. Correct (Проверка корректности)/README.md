Задание взято с сайта с онлайн курсами ([openedu.ru](https://courses.openedu.ru))

# Задние 5 "Проверка корректности" (6 неделя)
| Имя входного файла: | input.txt |
|:--------------------:|:----------:|
| Имя выходного файла: | output.txt |
| Ограничение по времени: | 2 секунды |
| Ограничение по памяти: | 256 мегабайт |

ВСвойство двоичного дерева поиска можно сформулировать следующим образом: для каждой вершины дерева ***V*** выполняется следующее условие:

* все ключи вершин из левого поддерева меньше ключа вершины ***V***;
* все ключи вершин из правого поддерева больше ключа вершины ***V***.

Дано двоичное дерево. Проверьте, выполняется ли для него свойство двоичного дерева поиска.
__________________
Формат входного файла  
Входной файл содержит описание двоичного дерева. В первой строке файла находится число ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_1.png)  — число вершин в дереве. В последующих ***N*** строках файла находятся описания вершин дерева. В (***i***+1)-ой строке файла ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_2.png) находится описание ***i***-ой вершины, состоящее из трех чисел ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_3.png), разделенных пробелами — ключа в ***i***-ой вершине ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_4.png), номера левого ребенка ***i***-ой вершины ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_5.png) если левого ребенка нет) и номера правого ребенка ***i***-ой вершины ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_6.png), если правого ребенка нет).

Формат выходного файла  
Выведите «YES», если данное во входном файле дерево является двоичным деревом поиска, и «NO», если не является.

Пример

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/format_w6_t5.png)
__________________
Результат

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/result_w6_t5.png)