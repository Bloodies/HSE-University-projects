/* Реализуйте функцию calculateWordStat, которая находит самое часто употребляемое слово в строке. */
fun calculateWordStat(input:String): String{
    return input.split(" ").groupBy({ it }).map({ it.value }).maxBy({ it.size }).orEmpty().first()
}