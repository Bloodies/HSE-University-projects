/* Реализуйте функцию getCubeList (n), возвращающую список целых чисел,
   состоящий из кубов порядковых номеров элементов от 0 до n-1 включительно. Нумерация начинается с 0.
 */
fun getCubeList(n:Int): List<Int>{
    var list = mutableListOf<Int>()
    for(i in 0 until n) list.add(i*i*i)
    return list
}