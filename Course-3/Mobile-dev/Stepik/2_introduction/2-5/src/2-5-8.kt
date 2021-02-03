/* Создайте (крайне) null-опасную функцию getLength(str: String?):Int
   Функция должна:
    возвращать длину str, если str не равно null;
    генерировать NullPointerException, если str равно null.
*/
fun getLength(str: String?) = str!!.length