# Задания из курса по дисциплине "Мобильная разработка"

### Курс выполнен студентом третьего курса НИУ "ВШЭ - Пермь" направления Программная инженерия.

#### Курс предоставлен сайтом [stepik.org](https://stepik.org/)

https://stepik.org/course/4792

<details>
<summary>English version</summary>

# Tasks from the course "Mobile development"

### The course was conducted by a third-year Software engineering student from "HSE-Perm".

#### The course is provided by the website [stepik.org](https://stepik.org/)
</details>

_______________________

### GROUP 2 (Знакомство с Kotlin)
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
    		var map:MutableMap<String, Int> = HashMap()
    		var n:Int?=0
    		var max="str" to 0
    		for(str in input.split(' '))
    		{
        		n=map.get(str)
        		if(n==null){
            			map.put(str,1)
        		}
        		else{
            			map.remove(str)
            			map.put(str,n+1)
        		}
    		}

    		for(it in map){
        		if(it.component2()>max.component2())
            			max=it.toPair()
    		}
    		return max.component1()
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
-----------
### GROUP 3 (Архитектура Android)
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
	<intent-filter>	- Указание типов интентов, на которые может реагировать, сервис
	<uses-permission>	- Позволяет запрашивать у системы разрешения, которые нужны приложению для доступа к различным функциям
	<permission>		- Объявляет разрешение, которое может использоваться для ограничения доступа к определенным компонентам
	```
- [X] 3.3
    - [X] 3.3.8 (nit_1_check_ui)
	```
	JKFHWK
	```
    - [X] 3.3.9 (nit_logcat)
	```
	com.example.company.myapplication.MainActivity@3530717
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: com.example.company.myapplication.MainActivity@3530717
	```
    - [X] 3.3.10 (nit_2_logs)
	```
	159j0kjhk
	```
    - [X] 3.3.12 (Загрузите APK-файл и распакуйте на своем компьютере. Определите, чему равна третья строчка файла META-INF/MANIFEST.MF.)
	```
	Created-By: Android Gradle 3.0.1
	```
    - [X] 3.3.13 (nit_build_and_inspect)
	```
	5tOoOlQAhWeZ6llJZOtG9ZnIknufHaFElgFk1lP8WD8=
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: 5tOoOlQAhWeZ6llJZOtG9ZnIknufHaFElgFk1lP8WD8=
	```
-----------
### GROUP 4 (Программирование пользовательских интерфейсов)
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
- [X] 4.3
    - [X] 4.3.2 ()
	```
	Absolute Layout	- Позволяет позиционировать дочерние элементы с помощью точного местоположения (координаты x / y).
	Relative Layout	- Позволяет указать местоположение элементов управления друг относительно друга и относительно родительского элемента.
	Linear Layout	- Позволяет позиционировать дочерние элементы в виде вертикального или горизонтального списка. Создает полосу прокрутки, если длина окна превышает длину экрана.
	Grid Layout	- Макет, который помещает своих детей в прямоугольную сетку.
	```
    - [X] 4.3.6 (nit_ui_attr)
	```
	MWE4Mjc1ZmJhMWRmZGJlOWFmNWZkM2E4
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: MWE4Mjc1ZmJhMWRmZGJlOWFmNWZkM2E4
	```
    - [X] 4.3.7 (nit_ui_string)
	```
	MWI2NzQ1MjNlNTc2ZDhjZGVhYTI2Yzk1
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: MWI2NzQ1MjNlNTc2ZDhjZGVhYTI2Yzk1
	```
    - [X] 4.3.8 (Укажите свойства, которые определяют размер элемента в рамках родительского элемента.)
	```
	layout_height
	layout_width
	```
    - [X] 4.3.10 (nit_ui_button_hello)
	```
	N2U2MU5ZWEz
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: N2U2MU5ZWEz
	```
    - [X] 4.3.11 (nit_ui_less_simplest)
	```
	RlMjZDA3GVhZ
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: RlMjZDA3GVhZ
	```
    - [X] 4.3.13 (Выберите номера корректных XML:)
	```
	1
	3
	```
    - [X] 4.3.14 (Выберите номер ﻿XML, который описывает следующую структуру элементов:)
	```
	4
	```
    - [X] 4.3.15 (Выберите номер ﻿XML, который описывает следующее приложение:)
	```
	3
	```
- [X] 4.4
    - [X] 4.4.5 (nit_ui_stupid_button)
	```
	ZDQwzNTM
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: ZDQwzNTM
	```
    - [X] 4.4.7
	```
	элемент не виден				- android.support.test.espresso.PerformException: Error performing 'single click' on view 'Animations or transitions are enabled on the target device.
	элемент не найден по id				- android.support.test.espresso.NoMatchingViewException: No views in hierarchy found matching: with id: 0 (resource name not found)
	текст элемента не соответствует требуемому	- android.support.test.espresso.base.DefaultFailureHandler$ AssertionFailedWithCauseError: 'with text: is "button"' doesn't match the selected view.
	```
    - [X] 4.4.8 (nit_ui_count_click)
	```
	ZmVkZGY4ZWQQxY2Y1YmM3Y
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: ZmVkZGY4ZWQQxY2Y1YmM3Y
	```
    - [X] 4.4.9 (nit_ui_edittext_to_text_view)
	```
	MzNlYjJmMGMZhMDFjNGNkMcwNTM0MWRhZTxY2I3NTVkZG
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: MzNlYjJmMGMZhMDFjNGNkMcwNTM0MWRhZTxY2I3NTVkZG
	```
    - [X] 4.4.10 (nit_calculator)
	```
	MGFoPM7DlxAVqQV4TEbliCNHSnR9quNYOdNd7H8BBc3usCobx1GJqm68kbNDEpI1YbI5l203YkiIWxWo
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: MGFoPM7DlxAVqQV4TEbliCNHSnR9quNYOdNd7H8BBc3usCobx1GJqm68kbNDEpI1YbI5l203YkiIWxWo
	```
    - [X] 4.4.12 (nit_ui_on_change_count)
	```
	ZWE0YYjU4M
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: ZWE0YYjU4M
	```
    - [X] 4.4.13 (nit_ui_autochange)
	```
	ZDlmNODAwYN2M3M
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: ZDlmNODAwYN2M3M
	```
    - [X] 4.4.15 (Какой метод задает текст сообщения в диалоговом окне AlertDialog?)
	```
	setMessage
	```
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
-----------
### GROUP 5 (Задачи для закрепления)
- [X] 5.1
    - [X] 5.1.1 (nit_prime_checker)
	```
	ravsdfvsvrsdkjhsdskvvradfvdfradfvdfradfvdfradfvdfradfvdfradfvdfradfvdfradfvdfradfvdfradfvdf
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: ravsdfvsvrsdkjhsdskvvradfvdfradfvdfradfvdfradfvdfradfvdfradfvdfradfvdfradfvdfradfvdfradfvdf
	```
    - [X] 5.1.2 (nit_converter3)
	```
	oRT6oJM3bu8FjRTLXescimLHcg8SQuf21Zh7k86uoRT6oJM3bu
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: oRT6oJM3bu8FjRTLXescimLHcg8SQuf21Zh7k86uoRT6oJM3bu
	```
    - [X] 5.1.3 (nit_tcs)
	```
	ZTRkMGFhNTM5EwczZGZTZlJlYTNDZmJk
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: ZTRkMGFhNTM5EwczZGZTZlJlYTNDZmJk
	```
    - [X] 5.1.4 (nit_quadratic_equation)
	```
	Dt9pbBmxpKxu5WunqkhMcgHzY0nEgoiq5wf7Hk3eGAOGX3RrPrT3WQBVYW1pzjks6WdesqLSjOkCRQFfEGdnGvOaZGkg6kRcVHnho7Nn3nZ7b5SK95orPGRFds2Lw2s0Hw7CZh6tppWIXhlu3Akrn94ACPufzU8QHW2N37ahgU9o0NZJzVXSO6gdZERVBM
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: Dt9pbBmxpKxu5WunqkhMcgHzY0nEgoiq5wf7Hk3eGAOGX3RrPrT3WQBVYW1pzjks6WdesqLSjOkCRQFfEGdnGvOaZGkg6kRcVHnho7Nn3nZ7b5SK95orPGRFds2Lw2s0Hw7CZh6tppWIXhlu3Akrn94ACPufzU8QHW2N37ahgU9o0NZJzVXSO6gdZERVBM
	```
    - [X] 5.1.6 (nit_eath_shape)
	```
	r2wvwevwvwkepvkwrvddfvr2323r2323vsdvlav
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: r2wvwevwvwkepvkwrvddfvr2323r2323vsdvlav
	```
    - [X] 5.1.7 (nit_simple_text_editor)
	```
	451066361138540890019720703477353935997633274503818570764599817773670566788144519461017248
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: 451066361138540890019720703477353935997633274503818570764599817773670566788144519461017248
	```
    - [X] 5.1.9 (nit_tic_tac)
	```
	random_Stricsdcsdcngrandomvdfvdfringrandom_Stringvsdvsrandom_Strsvdssingrandom_Stringcsdvsvsv
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: random_Stricsdcsdcngrandomvdfvdfringrandom_Stringvsdvsrandom_Strsvdssingrandom_Stringcsdvsvsv
	```
-----------
GROUP 6 (Пользовательские интерфейсы)
- [X] 6.1
    - [X] 6.1.9 (nit_simplest_activity_transition)
	```
	uyiyudsfadfgdaafsaf
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: uyiyudsfadfgdaafsaf
	```
    - [X] 6.1.10 (nit_transition_between_activities)
	```
	oqwergrtuhghweqwehshshhjkhjkkkttyui
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: oqwergrtuhghweqwehshshhjkhjkkkttyui
	```
    - [X] 6.1.11 (nit_activity_transition_with_data)
	```
	uysagduqmgbpzgwspgfia
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: uysagduqmgbpzgwspgfia
	```
    - [X] 6.1.12 (nit_activity_lifecycle)
	```
	chodvgaadjfgiopdfg
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: chodvgaadjfgiopdfg
	```
- [X] 6.2
    - [X] 6.2.4 (nit_clicable_list_view2)
	```
	qlkggssdgkghwssssrqhef
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: qlkggssdgkghwssssrqhef
	```
    - [X] 6.2.5 (nit_add_elements_to_listView)
	```
	bvysgsgdddsopbhssqbcidgt
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: bvysgsgdddsopbhssqbcidgt
	```
    - [X] 6.2.6 (nit_rm_elements_from_listView)
	```
	sdfgiosgdjxhhodhhfdhiodhqqpvh
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: sdfgiosgdjxhhodhhfdhiodhqqpvh
	```
    - [X] 6.2.7 (nit_list_view_with_bar)
	```
	posyhsttyuifdghsdfhdghdhfhdssdgijsxxgyw
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: posyhsttyuifdghsdfhdghdhfhdssdgijsxxgyw
	```
- [X] 6.3
    - [X] 6.3.5 (nit_simple_gridview)
	```
	sipujghddigjhnadrh
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: sipujghddigjhnadrh
	```
    - [X] 6.3.6 (nit_clickable_gridView)
	```
	glehjhsawesashsdfdndciyggds
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: glehjhsawesashsdfdndciyggds
	```
    - [X] 6.3.7 (nit_rm_elements_from_gridView)
	```
	vsgsdgdqogsggnysysswhsnnsdasghishr
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: vsgsdgdqogsggnysysswhsnnsdasghishr
	```
    - [X] 6.3.8 (nit_spinner_dynamic_list)
	```
	qnihzdgajmiaasoidfjhsdgkosdsdhagsjsehnousdgs
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: qnihzdgajmiaasoidfjhsdgkosdsdhagsjsehnousdgs
	```
-----------
GROUP 7 (Android advanced)
- [X] 7.1
    - [X] 7.1.5 (nit_permission_normal_simple)
	```
	zY2JmYjI0ZDFjYmI2NGEyMzBjNDc4ODM
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: zY2JmYjI0ZDFjYmI2NGEyMzBjNDc4ODM
	```
    - [X] 7.1.6 (nit_permission_normal)
	```
	iNGU0NDcwTNmMEwMN2UzMmQyM2I3ZmY4
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: iNGU0NDcwTNmMEwMN2UzMmQyM2I3ZmY4
	```
    - [X] 7.1.9 (nit_permission_danger_simple)
	```
	jYWZhY2IwMzc3ZjZkZWQ0MmM3Y2M4ZjN
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: jYWZhY2IwMzc3ZjZkZWQ0MmM3Y2M4ZjN
	```
    - [X] 7.1.11 (nit_permission_danger)
	```
	jYWZhY2IwMzc3ZjZkZWQ0MmM3Y2M4ZjN
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: jYWZhY2IwMzc3ZjZkZWQ0MmM3Y2M4ZjN
	```
- [X] 7.2
    - [X] 7.2.5 (nit_file_read_new)
	```
	FfsfwsfwOooOlsL
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: FfsfwsfwOooOlsL
	```
    - [X] 7.2.6 (nit_file_exchange)
	```
	dhkssweghllYuidfsFNWWewewqqq
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: dhkssweghllYuidfsFNWWewewqqq
	```
    - [X] 7.2.7 (nit_file_counter)
	```
	zFjYmJhmMzlkMYjkzM2NmIyY2VRiZDQ4
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: zFjYmJhmMzlkMYjkzM2NmIyY2VRiZDQ4
	```
- [X] 7.3
    - [X] 7.3.4 (nit_notification)
	```
	NGU3NzgwYmU5MGVkNzUzMzM5NmRiN2Qz
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: NGU3NzgwYmU5MGVkNzUzMzM5NmRiN2Qz
	```
    - [X] 7.3.5 (nit_notification_data)
	```
	NWE4M2U5MmYxNDZlMDk1MWQxMTI2MWI3
	```
	```
	2000-01-01 00:00:00.000 9000-9999/? D/CHECKER: NWE4M2U5MmYxNDZlMDk1MWQxMTI2MWI3
	```
    - [X] 7.3.7 (nit_notification_click)
	```
	zhjYzE3MmVhZjQwMGFiNTM5Y2E3ZWYwM
	```
	```
	2000-01-01 16:03:15.413 9000-9999/? D/CHECKER: zhjYzE3MmVhZjQwMGFiNTM5Y2E3ZWYwM
	```
    - [X] 7.3.8
	```
	notificationManager.cancel(id)				- Удаление уведомления
	notificationManager.notify(id, notification)		- Отображение уведомления
	val notification : Notification = builder.build()	- Создание уведомления
	```
    - [X] 7.3.10
	```
	val alarmManager : AlarmManager = getSystemService(Context.ALARM_SERVICE)		- Создание AlarmManager
	PendingIntent.getBroadcast(this,1, intentAlarm, PendingIntent.FLAG_UPDATE_CURRENT))	- Создание операции интента
	val intentAlarm : Intent = Intent(this, AlarmNotification.class)			- Определение базового интента
	```
-----------
### GROUP 8 (Публикация Android-приложений)
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
