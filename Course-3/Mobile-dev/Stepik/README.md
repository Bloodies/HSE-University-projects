# Задания из курса по дисциплине "Мобильная разработка"

### Курс выполнен студентом третьего курса НИУ "ВШЭ - Пермь" направления Программная инженерия.

#### Курс предоставлен сайтом [stepik.org](https://stepik.org/)

<details>
<summary>English version</summary>

# Tasks from the course "Mobile development"

### The course was conducted by a third-year Software engineering student from "HSE-Perm".

#### The course is provided by the website [stepik.org](https://stepik.org/)
</details>

_______________________

GROUP 2 (Знакомство с Kotlin)
- [X] 2.1
    - [X] 2.1.7	
	```kotlin
	fun main(args: Array<String>) { println("Hello brave citizen of glorious Bug Kingdom!") }
	```
    - [X] 2.1.11
	```kotlin
	fun main() = print("Oh mighty ruler of Bug Kingdom, the earthling called ${readLine()} seeks for your wisdom!")
	```
    - [X] 2.1.14
	```kotlin
	fun calcChairs(bugs: Int): Int  = (bugs * 1.5).toInt()
	```
    - [X] 2.1.15
	```kotlin
	fun calcBugMoneyValue(dayNumber: Int, bugRank: Int, cashAmount: Int): Int {
    	return ((dayNumber*bugRank)+42)*cashAmount
	}
	```
- [X] 2.2
    - [X] 2.2.3
	```kotlin
	fun getYearEra(year: Int) = when {
    	    year < 1970 -> "before"
    	    year == 1970 -> "equals"
    	    year < 2000 -> "after (XX century)"
    	    year < 2100 -> "after (XXI century)"
    	    else -> "distant future"
	}
	```
    - [X] 2.2.6
	```kotlin
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
	```
    - [X] 2.2.7
	```kotlin
	fun calculateBugMentions(input: List<String>): Int {
	    var sum: Int = 0
	    for (i in input) {
	        if (i == "BUG") {
	            sum++
	        }
	    }
	    return sum
	}
	```
- [X] 2.3
    - [X] 2.3.3
	```kotlin
	class SimpleClass() { }
	```
    - [X] 2.3.4
	```kotlin
	class NibirunianClass {
	    public var namePlate: String = ""

	    fun createNamePlate(name: String) {
	        namePlate = "Live long and prosper, $name"
	    }
	}
	```
    - [X] 2.3.8
	```kotlin
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
	```
    - [X] 2.3.13
	```kotlin
	open class Bug(val rank: Int, val name:String) {
	    open fun getSugarLimit(): Int{
	        return rank
	    }
	
	    fun getId(): String{
	        return "${rank}.${name}"
	    }
	}
	
	class BugCivilian(rank: Int, name: String): Bug(rank, name) {
 	    override fun getSugarLimit(): Int {
 	       return super.getSugarLimit() / 2
	   }
	}
	```
- [X] 2.4
    - [X] 2.4.2 (Отметьте верное высказывание)
	```
	Array имеет постоянный размер
	```
    - [X] 2.4.3
	```
	var a: List<Int> = emptyList()                  - Пустой неизменяемый список Int
	var a: Array<Int> = arrayOf(1,2)                - Массив [1,2]
	var a: Array<Int> = Array(2, {it})              - Массив [0,1]
	var a: MutableList<Int> = emptyMutableList()    - Пустой изменяемый список Int
	```
    - [X] 2.4.4
	```
	Array<Int>  - Массив целых чисел
	LinkedList  - Связанный список
	ArrayList   - Реализация *List интерфейсов с помощью массивов
	List        - Интерфейс для неизменяемых списков
	MutableList - Интерфейс для изменяемых списков
	```
    - [X] 2.4.5
	```kotlin
	fun getCubeList(n:Int): List<Int>{
	    var list = mutableListOf<Int>()
	    for(i in 0 until n) list.add(i*i*i)
	    return list
	}
	```
    - [X] 2.4.6
	```kotlin
	fun calculateWordStat(input:String): String{
	    return input.split(" ").groupBy({ it }).map({ it.value }).maxBy({ it.size }).orEmpty().first()
	}
	```
    - [X] 2.4.10
	```kotlin
	fun exceptionExample(){ throw Exception("Exception") }
	```
    - [X] 2.4.11
	```kotlin
	class DogException() : Exception()
	class CatException() : Exception()
	fun checkClient(client:String) {
	    if(client == "dog") throw DogException()
	    if(client == "cat") throw CatException()
	}
	```
    - [X] 2.4.12
	```kotlin
	class SphinxesException() : Exception() {}
	```
    - [X] 2.4.13
	```kotlin
	fun engineStatus(engineNumber:Int): String{
	    return try { rawEngineStatus(engineNumber) }
	    catch (e: EngineNotFoundException) { "engine $engineNumber not found" }
	    catch (e: SensorsMeltException) { "engine $engineNumber offline" }
	}
	```
- [X] 2.5
    - [X] 2.5.4 (В предыдущем видео length постоянно по ошибке называют методом. Чем он является на самом деле относительно класса String?)
	```
	Свойством
	```
    - [X] 2.5.5 (Отметьте типы, значения которых могут быть null)
	```
	Double?
	Int?
	String?
	```
    - [X] 2.5.6
	```kotlin
	fun getLength(str: String?):Int? {return str?.length}
	```
    - [X] 2.5.7
	```kotlin
	fun getLength(str: String?):Int {return str?.length?:0}
	```
    - [X] 2.5.8
	```kotlin
	fun getLength(str: String?) = str!!.length
	```
    - [X] 2.5.9 (Отметьте выражения, после  выполнения которых переменная b никогда не станет null, при условии что: var a: String? = null)
	```
	var b = if (a != null) a.length else 0
	var b = a?. length ?: 0
	var b = a!!.length
	var b = if (a?.length !=null) a!!.length else -1
	```
    - [X] 2.5.10 (Отметьте участки кода, где точно возникнет NullPointerException. var a: String? = null)
	```
	a!!.length
	throw NullPointerException("Hello, Mister Kotlin:)")
	```
    - [X] 2.5.11 (Отметьте участки кода, где может возникнуть NullPointerException. var a: String?)
	```
	var b = externalJavaCall()
	var b = a!!.length
	```
    - [X] 2.5.12
	```kotlin
	fun calcNullValues(input: Array<Int?>):Array<Int>{
	    return arrayOf(input.count { it == null }, input.filterNotNull().sumBy { it })
	}
	```

GROUP 3 (Архитектура Android)
- [X] 3.1
    - [X] 3.1.4 (Почему, с точки зрения пользователя, к мобильным приложениям предъявляются более строгие требования, чем к приложениям для настольных платформ?)
	```
	Мобильное устройство имеет ограниченное время автономной работы
	Мобильные приложения ближе к чувствительным данным пользователя
	```
    - [X] 3.1.5 (Каким образом смартфоны используются чаще всего?)
	```
	Смартфон удерживается и используется одной рукой
	```
    - [X] 3.1.6 (Выберите два приложения, которые не получится перенести на мобильные платформы без потери удобства использования.)
	```
	Профессиональный редактор графики (Adobe Photoshop, Gimp)
	Электронные таблицы (MS Excel, LibreOffice Calc)
	```
    - [X] 3.1.7 (Какой фактор повлияет сильнее всего на впечатление от приложения?)
	```
	размер экрана
	```
- [X] 3.2
    - [X] 3.2.2
	```
	Linux Kernel			- Управление памятью и энергопотреблением
	Hardware Abstraction Layer	- Реализация функциональных возможностей, не затрагивая систему более высокого уровня.
	Android Runtime Environment	- Среда выполнения для приложений Android
	Java API Framework		- Базовые библиотеки для создания приложений на Android
	System Apps			- Уже существующие приложения на устройстве
	```
    - [X] 3.2.4 (Используя ссылки из предыдущего шага, отметьте версии API, относящиеся к Jelly Bean или Marshmallow)
	```
	17
	18
	23
	```
    - [X] 3.2.9 (Какой параметр определяет версию SDK и компилятора, используемый Gradle для сборки приложения?)
	```
	compileSdkVersion
	```
    - [X] 3.2.11
	```
	<uses-sdk>		- Указание уровня API
	<intent-filter>		- Указание типов интентов, на которые может реагировать, сервис
	<uses-permission>	- Позволяет запрашивать у системы разрешения, которые нужны приложению для доступа к различным функциям
	<permission>		- Объявляет разрешение, которое может использоваться для ограничения доступа к определенным компонентам
	```
- [ ] 3.3
    - [ ] 3.3.8
    - [ ] 3.3.9
    - [ ] 3.3.10
    - [ ] 3.3.12
    - [ ] 3.3.13

GROUP 4 (Программирование пользовательских интерфейсов)
- [X] 4.1
    - [X] 4.1.4 (Для изображенного пользовательского интерфейса выберите самую неудобную кнопку для нажатия большим пальцем левой  руки.)
	```
	2
	```
    - [X] 4.1.10 (Отсортируйте сверху вниз способы взаимодействия с мобильным устройством по возрастанию сложности.)
	```
	Клик
	Долгое нажатие на экран
	Выбор элемента из выпадающего списка
	Ввод текста
	```
    - [X] 4.1.12
	```
	android:layout_height="40px"		- Высота объекта 40px
	android:layout_width="40px"		- Ширина объекта 40px
	android:layout_marginEnd="40px"		- Внешний отступ справа/слева (rtl) 40 px
	android:background="@color/colorAccent"	- Задание фона объекта
	android:id="@+id/button"		- Объект с идентификатором button
	android:id="@+id/text"			- Объект с идентификатором text
	android:visibility="invisible"		- Объект не виден пользователю
	android:visibility="gone"		- Объект удален с поля
	```
- [X] 4.2
    - [X] 4.2.3 (Чем отличаются UI и UX?)
	```
	UI - про дизайн и оформление, UX - про удобство использования
	```
    - [X] 4.2.5 (Какие действия пользователя учитываются в сценарии использования?)
	```
	Нажатие на кнопки
	Ожидание загрузки анимаций
	Свайпы и скроллы в приложении
	```
    - [X] 4.2.7 (Необходимо ли пользователю регистрироваться в Вашем сервисе?)
	```
	Можно оставить ограниченные возможности без регистрации, но она все-таки нужна.
	```
    - [X] 4.2.9 (Что можно разместить в качестве стартового экрана приложения?)
	```
	Ключевой функционал приложения
	Инструкцию по использованию приложения
	Соглашение о (не)распространении, если мы используем много приватных данных.
	```
    - [X] 4.2.11 (Сколько элементов ввода (полей, чекбоксов и т.д.) стоит располагать на экране?)
	```
	3-5 будет достаточно
	```
    - [X] 4.2.15 (Что из нижеперечисленного можно реализовать, чтобы не вызывать у пользователя подозрений?)
	```
	Предупредим о необходимости при запуске приложения и запросим только запись звука, остальное по запросу.
	Запросим разрешение, когда пользователь нажмет "Запись".
	```
- [ ] 4.3
    - [ ] 4.3.2
    - [ ] 4.3.6
    - [ ] 4.3.7
    - [ ] 4.3.8
    - [ ] 4.3.10
    - [ ] 4.3.11
    - [ ] 4.3.13
    - [ ] 4.3.14
    - [ ] 4.3.15
- [ ] 4.4
    - [ ] 4.4.5
    - [ ] 4.4.7
    - [ ] 4.4.8
    - [ ] 4.4.9
    - [ ] 4.4.10
    - [ ] 4.4.12
    - [ ] 4.4.13
    - [ ] 4.4.15
- [X] 4.5
    - [X] 4.5.6
	```
	px	- Торжественно клянусь, что не буду использовать!
	in	- Указание реального размера в дюймах
	mm	- Самая маленькая ЕИ реального размера
	pt	- Указание реального размера (1=1/72 дюйма)
	dp	- Относительная ЕИ, не зависит от плотности экрана
	sp	- Использую эту штуку для шрифтов!
	```
- [X] 4.7
    - [X] 4.7.2 (Какой метод отвечает за вывод TimePickerDialog?)
	```
	show()
	```
    - [X] 4.7.4 (На сколько будет заполнен индикатор ProgressBar, объявленный следующим образом?)
	```
	7%	
	```
    - [X] 4.7.5 (Сопоставьте определениям в XML их внешнее представление.)
	```
	1 - Горизонтальный индикатор, заполненный на 25%
	2 - Неопределенный маленький круговой индикатор
	3 - Неопределенный горизонтальный индикатор
	4 - Неопределенный круговой индикатор
	```
    - [X] 4.7.8 (Выберите правильно составленные методы Espresso.)
	```
	onView(withId(R.id.myView)).perform(click())
	onView(allOf(instanceOf(android.widget.Button), withText("Test"))).perform(longPress()).check(matches(withText("Test2")))
	```
    - [X] 4.7.9
	```
	matches()	- Проверяет, совпадает ли точно свойство, которое передано аргументом ViewMatcher.
	perform()	- Выполняет какое-либо действие.
	onView()	- Возвращает элемент, который подходит переданному ViewMatcher и находится на экране.
	isAbove()	- Проверяет, находится ли элемент выше, чем аргумент ViewMatcher.
	longClick()	- Совершает длительное нажатие.
	check()		- Проверяет, выполняется ли какое-либо условие.
	```
    - [X] 4.7.11
	```
	event.getActionMasked()	- Получение события
	event.getActionIndex()	- Получение индекса касания
	event.getPointerCount()	- Получение количества касаний
	```

GROUP 5 (Задачи для закрепления)
- [ ] 5.1
    - [ ] 5.1.1
    - [ ] 5.1.2
    - [ ] 5.1.3
    - [ ] 5.1.4
    - [ ] 5.1.6
    - [ ] 5.1.7
    - [ ] 5.1.9

GROUP 6 (Пользовательские интерфейсы)
- [ ] 6.1
    - [ ] 6.1.9
    - [ ] 6.1.10
    - [ ] 6.1.11
    - [ ] 6.1.12
- [ ] 6.2
    - [ ] 6.2.4
    - [ ] 6.2.5
    - [ ] 6.2.6
    - [ ] 6.2.7
- [ ] 6.3
    - [ ] 6.3.5
    - [ ] 6.3.6
    - [ ] 6.3.7
    - [ ] 6.3.8

GROUP 7 (Android advanced)
- [ ] 7.1
    - [ ] 7.1.5
    - [ ] 7.1.6
    - [ ] 7.1.9
    - [ ] 7.1.1
- [ ] 7.2
    - [ ] 7.2.5
    - [ ] 7.2.6
    - [ ] 7.2.7
- [ ] 7.3
    - [ ] 7.3.4
    - [ ] 7.3.5
    - [ ] 7.3.7
    - [ ] 7.3.8
    - [ ] 7.3.10

GROUP 8 (Публикация Android-приложений)
- [X] 8.1
    - [X] 8.1.3 (Что подразумевает публикация приложения?)
	```
	Загрузка приложения и материалов в Google Play, Yandex app store
	```
    - [X] 8.1.6 (Какие этапы может включать в себя подготовка приложения к публикации?)
	```
	Сборка APK или AAB
	Генерация уникального имени пакета
	Создание цифровой подписи
	Создание release-версии приложения
	Включение компьютера
	```
    - [X] 8.1.8 (Что из нижеперечисленного может выступать корректным описанием приложения?)
	```
	Перечислить основные возможности приложения в коротком описании.
	```
    - [X] 8.1.10 (Что из нижеперечисленного стоит проделать в Google Play Console при публикации приложения?)
	```
	Учесть разницу в законодательной политике и скрыть страны, в которых может нарушаться закон.
	```
