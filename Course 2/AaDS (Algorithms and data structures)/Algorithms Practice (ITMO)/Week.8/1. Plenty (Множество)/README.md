Задание взято с сайта с онлайн курсами ([openedu.ru](https://courses.openedu.ru))

# Задние 1 "Множество" (8 неделя)
| Имя входного файла: | input.txt |
|:--------------------:|:----------:|
| Имя выходного файла: | output.txt |
| Ограничение по времени: | 2 секунды |
| Ограничение по памяти: | 256 мегабайт |

Реализуйте множество с операциями «добавление ключа», «удаление ключа», «проверка существования ключа».
__________________
Формат входного файла  
В первой строке входного файла находится строго положительное целое число операций ***N***, не превышающее ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w8_t1_1.png). В каждой из последующих ***N*** строк находится одна из следующих операций:

A ***x*** — добавить элемент  в множество. Если элемент уже есть в множестве, то ничего делать не надо.
D ***x*** — удалить элемент ***x***. Если элемента ***x*** нет, то ничего делать не надо.
? ***x*** — если ключ ***x*** есть в множестве, выведите Y, если нет, то выведите N.
Аргументы указанных выше операций — целые числа, не превышающие по модулю ![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w8_t1_2.png).

Формат выходного файла  
Выведите последовательно результат выполнения всех операций «?». Следуйте формату выходного файла из примера.

Пример

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/format_w8_t1.png)
__________________
Результат

![none](https://github.com/Bloodies/University.Projects/blob/master/Course%202/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/result_w8_t1.png)