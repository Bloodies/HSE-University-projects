/* Реализуйте функцию calculateBugMentions, которая находит количество употреблений слова BUG в списке */
fun calculateBugMentions(input: List<String>): Int {
    var sum: Int = 0
    for (i in input) {
        if (i == "BUG") {
            sum++
        }
    }
    return sum
}