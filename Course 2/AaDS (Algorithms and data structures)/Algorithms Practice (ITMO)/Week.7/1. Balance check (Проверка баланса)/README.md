Задание взято с сайта с онлайн курсами ([openedu.ru](https://courses.openedu.ru))

# Задние 1 "Проверка сбалансированности" (7 неделя)
| Имя входного файла: | input.txt |
|:--------------------:|:----------:|
| Имя выходного файла: | output.txt |
| Ограничение по времени: | 2 секунды |
| Ограничение по памяти: | 256 мегабайт |

АВЛ-дерево является сбалансированным в следующем смысле: для любой вершины высота ее левого поддерева отличается от высоты ее правого поддерева не больше, чем на единицу.

Введем понятие баланса вершины: для вершины дерева ***V*** ее баланс ***B(C)*** равен разности высоты правого поддерева и высоты левого поддерева. Таким образом, свойство АВЛ-дерева, приведенное выше, можно сформулировать следующим образом: для любой ее вершины ***V*** выполняется следующее неравенство:

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w7_t1_1.png)

__Обратите внимание, что, по историческим причинам, определение баланса в этой и последующих задачах этой недели "зеркально отражено" по сравнению с определением баланса в лекциях!__ Надеемся, что этот факт не доставит Вам неудобств. В литературе по алгоритмам — как российской, так и мировой — ситуация, как правило, примерно та же.

Дано двоичное дерево поиска. Для каждой его вершины требуется определить ее баланс.
__________________
Формат входного файла  
Входной файл содержит описание двоичного дерева. В первой строке файла находится число ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w7_t1-t4_2.png) — число вершин в дереве. В последующих ***N*** строках файла находятся описания вершин дерева. В (***i***+1)-ой строке файла ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_2.png) находится описание ***i***-ой вершины, состоящее из трех чисел ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_3.png) разделенных пробелами — ключа в ***i***-ой вершине ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_4.png), номера левого ребенка ***i***-ой вершины ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_5.png), если левого ребенка нет) и номера правого ребенка ***i***-ой вершины ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_6.png), если правого ребенка нет).

Все ключи различны. Гарантируется, что данное дерево является деревом поиска.

Формат выходного файла  
Для ***i***-ой вершины в ***i***-ой строке выведите одно число — баланс данной вершины.

Пример

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/format_w7_t1.png)
__________________
Результат

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/result_w7_t1.png)