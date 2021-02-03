/* Необходимо реализовать функцию checkClient(client : String), которая:
генерирует DogException в случае, если аргумент client содержит слово "dog"
генерирует CatException в случае, если аргумент client содержит слово "cat"
*/
class DogException() : Exception()
class CatException() : Exception()
fun checkClient(client:String) {
    if(client == "dog") throw DogException()
    if(client == "cat") throw CatException()
}