/* Создайте функцию calcNullValues(input: Array<Int?>):Array<Int> , возвращающую массив,
   первым элементом которого является количество null-значений в массиве input,
   а вторым - сумма всех не-null значений.
*/

fun calcNullValues(input: Array<Int?>):Array<Int>{
    return arrayOf(input.count { it == null }, input.filterNotNull().sumBy { it })
}