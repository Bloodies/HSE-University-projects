[:arrow_backward:Назад (Back)](https://github.com/Bloodies/University.Projects/tree/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures))
[:rewind:В корневую папку (In root folder)](https://github.com/Bloodies/University.Projects)

Задание взято с сайта с онлайн курсами ([openedu.ru](https://courses.openedu.ru))

# Задние 4 "Удаление из АВЛ-дерева" (7 неделя)
| Имя входного файла: | input.txt |
|:--------------------:|:----------:|
| Имя выходного файла: | output.txt |
| Ограничение по времени: | 2 секунды |
| Ограничение по памяти: | 256 мегабайт |

Удаление из АВЛ-дерева вершины с ключом ***X***, при условии ее наличия, осуществляется следующим образом:

- путем спуска от корня и проверки ключей находится ***V*** — удаляемая вершина;
- если вершина ***V*** — лист (то есть, у нее нет детей):
    - удаляем вершину;
    - поднимаемся к корню, начиная с бывшего родителя вершины ***V***, при этом если встречается несбалансированная вершина, то производим поворот.
- если у вершины ***V*** не существует левого ребенка:
    - следовательно, баланс вершины равен единице и ее правый ребенок — лист;
    - заменяем вершину ***V*** ее правым ребенком;
    - поднимаемся к корню, производя, где необходимо, балансировку.
- иначе:
    - находим ***R*** — самую правую вершину в левом поддереве;
    - переносим ключ вершины ***R*** в вершину ***V***;
    - удаляем вершину ***R*** (у нее нет правого ребенка, поэтому она либо лист, либо имеет левого ребенка, являющегося листом);
    - поднимаемся к корню, начиная с бывшего родителя вершины ***R***, производя балансировку.

Исключением является случай, когда производится удаление из дерева, состоящего из одной вершины — корня. Результатом удаления в этом случае будет пустое дерево.

Указанный алгоритм не является единственно возможным, но мы просим Вас реализовать именно его, так как тестирующая система проверяет точное равенство получающихся деревьев.
__________________
Формат входного файла  
Входной файл содержит описание двоичного дерева, а также ключа вершины, которую требуется удалить из дерева.

В первой строке файла находится число ***N*** ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w7_t1-t4_2.png) — число вершин в дереве. В последующих ***N*** строках файла находятся описания вершин дерева. В (***i***+1)-ой строке файла ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_2.png) находится описание ***i***-ой вершины, состоящее из трех чисел ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_3.png) разделенных пробелами — ключа в ***i***-ой вершине ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_4.png), номера левого ребенка ***i***-ой вершины ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_5.png), если левого ребенка нет) и номера правого ребенка ***i***-ой вершины ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w6-w7_6.png), если правого ребенка нет).

Все ключи различны. Гарантируется, что данное дерево является деревом поиска.

В последней строке содержится число ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w7_t3-t4_3.png) — ключ вершины, которую требуется удалить из дерева. Гарантируется, что такая вершина в дереве существует.

Формат выходного файла  
Выведите в том же формате дерево после осуществления операции удаления. Нумерация вершин может быть произвольной при условии соблюдения формата.

Пример

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/format_w7_t4.png)
__________________
Результат

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/result_w7_t4.png)