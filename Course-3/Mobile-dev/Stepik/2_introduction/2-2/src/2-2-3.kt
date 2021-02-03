/* Реализуйте функцию getYearEra, которая для введенного в качестве аргумента функции года возвращает
один из возможных вариантов его положения относительно даты начала Unix-эры (1970 год) в виде строки:
    before
    equals
    after (XX century)
    after (XXI century)
    distant future

Например:
    2712  -> "distant future"
    1971  -> "after (XX century)"
 */

fun getYearEra(year: Int) = when {
    year < 1970 -> "before"
    year == 1970 -> "equals"
    year < 2000 -> "after (XX century)"
    year < 2100 -> "after (XXI century)"
    else -> "distant future"
}