/* Чтобы пилоты-испытатели не оказались слишком далеко от Земли в неисправном корабле,
   реализуйте функцию engineStatus(engineNumber:Int): String,
   которая делает безопасный (с точки зрения необработанных исключений)
   вызов функции rawEngineStatus(engineNumber:Int):String :
    если произошло исключение EngineNotFoundException, то вернуть строку "engine <number> not found";
    если произошло исключение SensorsMeltException, то вернуть строку "engine <number> offline";
    если не произошло исключение, то вернуть результат работы rawEngineStatus.
   где <number> - номер двигателя.
*/

//fun engineStatus(engineNumber:Int): String{
//    return try { rawEngineStatus(engineNumber) }
//    catch (e: EngineNotFoundException) { "engine $engineNumber not found" }
//    catch (e: SensorsMeltException) { "engine $engineNumber offline" }
//}