[:arrow_backward:Назад (Back)](https://github.com/Bloodies/HSE-University-projects/tree/master/Course-2/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO))  
[:rewind:В корневую папку (In root folder)](https://github.com/Bloodies/HSE-University-projects)  

Задание взято с сайта с онлайн курсами ([openedu.ru](https://courses.openedu.ru))

# Задние 2 "Очередь" (4 неделя)
| Имя входного файла: | input.txt |
|:--------------------:|:----------:|
| Имя выходного файла: | output.txt |
| Ограничение по времени: | 2 секунды |
| Ограничение по памяти: | 256 мегабайт |

Реализуйте работу стека. Для каждой операции изъятия элемента выведите ее результат.

На вход программе подаются строки, содержащие команды.  
Каждая строка содержит одну команду.  
Команда — это либо "+ N", либо "−". Команда "+ N" означает добавление в стек числа N, по модулю не превышающего 10^9.  
Команда "−" означает изъятие элемента из стека.  
Гарантируется, что не происходит извлечения из пустого стека.  
Гарантируется, что размер стека в процессе выполнения команд не превысит 10^6 элементов. 
__________________
Формат входного файла  
В первой строке входного файла содержится ![none](https://github.com/Bloodies/HSE-University-projects/blob/master/Course-2/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/txt_w4_t1-t2_1.png) — число команд. Каждая последующая строка исходного файла содержит ровно одну команду.

Формат выходного файла  
Выведите числа, которые удаляются из стека с помощью команды "−", по одному в каждой строке. Числа нужно выводить в том порядке, в котором они были извлечены из стека. Гарантируется, что изъятий из пустого стека не производится.

Пример

![none](https://github.com/Bloodies/HSE-University-projects/blob/master/Course-2/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/format_w4_t2.png)
__________________
Результат

![none](https://github.com/Bloodies/HSE-University-projects/blob/master/Course-2/AaDS%20(Algorithms%20and%20data%20structures)/Algorithms%20Practice%20(ITMO)/Resources/result_w4_t2.png)