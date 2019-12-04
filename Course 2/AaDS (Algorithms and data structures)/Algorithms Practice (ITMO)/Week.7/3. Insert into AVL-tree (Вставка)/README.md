Задание взято с сайта с онлайн курсами ([openedu.ru](https://courses.openedu.ru))

# Задние 3 "Вставка в АВЛ-дерево" (7 неделя)
| Имя входного файла: | input.txt |
|:--------------------:|:----------:|
| Имя выходного файла: | output.txt |
| Ограничение по времени: | 2 секунды |
| Ограничение по памяти: | 256 мегабайт |

Вставка в АВЛ-дерево вершины ***V*** с ключом ***X*** при условии, что такой вершины в этом дереве нет, осуществляется следующим образом:

* находится вершина ***W***, ребенком которой должна стать вершина ***V***;
* вершина ***V*** делается ребенком вершины ***W***;
* производится подъем от вершины ***W*** к корню, при этом, если какая-то из вершин несбалансирована, производится, в зависимости от значения баланса, левый или правый поворот.

Первый этап нуждается в пояснении. Спуск до будущего родителя вершины ***V*** осуществляется, начиная от корня, следующим образом:

* Пусть ключ текущей вершины равен ***Y***.
* Если ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w7_t3_1.png) у текущей вершины есть левый ребенок, переходим к левому ребенку.
* Если ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w7_t3_1.png) у текущей вершины нет левого ребенка, то останавливаемся, текущая вершина будет родителем новой вершины.
* Если ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w7_t3_2.png) у текущей вершины есть правый ребенок, переходим к правому ребенку.
* Если ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w7_t3_2.png) у текущей вершины нет правого ребенка, то останавливаемся, текущая вершина будет родителем новой вершины.

Отдельно рассматривается следующий крайний случай — если до вставки дерево было пустым, то вставка новой вершины осуществляется проще: новая вершина становится корнем дерева.
__________________
Формат входного файла  
Входной файл содержит описание двоичного дерева, а также ключа вершины, которую требуется вставить в дерево.  
В первой строке файла находится число ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_1.png) — число вершин в дереве. В последующих ***N*** строках файла находятся описания вершин дерева. В (***i***+1)-ой строке файла ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_2.png) находится описание ***i***-ой вершины, состоящее из трех чисел ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_3.png) разделенных пробелами — ключа в ***i***-ой вершине ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_4.png), номера левого ребенка ***i***-ой вершины ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_5.png), если левого ребенка нет) и номера правого ребенка ***i***-ой вершины ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_6.png), если правого ребенка нет).

Все ключи различны. Гарантируется, что данное дерево является корректным АВЛ-деревом.

В последней строке содержится число ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w7_t3-t4_3.png) — ключ вершины, которую требуется вставить в дерево. Гарантируется, что такой вершины в дереве нет.

Формат выходного файла  
Выведите в том же формате дерево после осуществления операции вставки. Нумерация вершин может быть произвольной при условии соблюдения формата.

Пример

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/format_w7_t3.png)
__________________
Результат

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/result_w7_t3.png)