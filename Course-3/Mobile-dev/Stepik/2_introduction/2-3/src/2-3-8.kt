/* Создайте класс для сахарного хранилища SugarStorage. Класс должен реализовывать следующие методы:
   первичный конструктор, инициализирующий начальный баланс хранилища.
   decreaseSugar(v:Int) - уменьшить баланс хранилища на v.
   increaseSugar(v:Int) - увеличить баланс хранилища на v.

   Также класс должен обладать публичным свойством volume:Int, задающим текущий баланс хранилища.
   Обратите внимание, что
   volume не может быть отрицательным.
   При попытке уменьшить баланс на величину, превышающую volume, значение volume должно становиться нулем.
   decreaseSugar и increaseSugar должны игнорировать отрицательные аргументы.
*/
class SugarStorage(public var volume: Int = 10){
    fun increaseSugar(v: Int) {
        if (v > 0) { volume += v }
    }
    fun decreaseSugar(v: Int) {
        if (v > 0) {
            if (volume - v <= 0) { volume = 0 }
            else { volume -= v }
        }
    }
}