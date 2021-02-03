/* Напишите программу, принимающую от пользователя (через readLine) его имя, например Ivan,
   и выводящую на экран текст вежливого обращения к правителю созвездия Жука:
   Oh mighty ruler of Bug Kingdom, the earthling called Ivan seeks for your wisdom!
*/
fun main(args: Array<String>) {
    val Name: String? = readLine()
    println("Oh mighty ruler of Bug Kingdom, the earthling called ${Name} seeks for your wisdom!")
}
//Сокращение:
//
//fun main(args: Array<String>) {
//    println("Oh mighty ruler of Bug Kingdom, the earthling called ${readLine()} seeks for your wisdom!")
//}
//
//или
//
//fun main() = print("Oh mighty ruler of Bug Kingdom, the earthling called ${readLine()} seeks for your wisdom!")