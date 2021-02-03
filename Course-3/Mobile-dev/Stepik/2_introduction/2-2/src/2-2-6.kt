/* Реализуйте функцию calculateEvenDigits, которая находит сумму четных цифр в строке. */
fun calculateEvenDigits(input: String): Int {
    var sum: Int = 0
    for(c in input) {
        val digit = c - '0'
        if (digit % 2 == 0) {
            sum += digit
            println("c = $c, sum = $sum")
        }
    }
    return sum
}