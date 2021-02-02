validate <- function(method, format = "null") 
{
  repeat
    {     
    switch(format,
      "numeric" = {input = sub(",", ".", readline(prompt = "Введите дробное число: "))},
      "speed" = {input =  readline(prompt = "Введите скорость целым числом в км/ч: ")},
      "default" = {input = readline(prompt = "Введите строку: ")}
    )
    
    result = suppressWarnings(method(input))  
    if (!is.na(result))
    {           
      return(result)
    }
  }
}

task_1_1 <- function()
{
  name <- readline("Введите имя: ")
  
  cat(sprintf("Hello, %s!", name))
}

task_1_2 <- function()
{
  num1 <- suppressWarnings(validate(as.numeric, "numeric"))
  num2 <- suppressWarnings(validate(as.numeric, "numeric"))
  
  cat("Сумма чисел:", num1 + num2);
}

task_1_3 <- function()
{
  spd <- validate(as.integer, "speed")
  
  cat("Скорость в метрах в секунду: ", (spd*1000)/3600, "м/с")
}

task_2_1 <- function()
{
  vector <- c(1, 0, 2, 3, 6, 8, 12, 15, 0, NA, NA, 9, 4, 16, 2, 0)
  
  cat('Первый элемент:                ', vector[1], '\n')
  cat('Последний элемент:             ', vector[length(vector)], '\n')
  cat('Элементы с 3 по 5:             ', vector[3:5], '\n')
  cat('Элементы равные 2:             ', vector[vector == 2], '\n')
  cat('Элементы больше 4:             ', vector[vector > 4], '\n')
  cat('Элементы кратные 3:            ', vector[vector %% 2 == 0], '\n')
  cat('Элементы кратные 3 и больше 4: ', vector[vector > 4 & vector %% 2 == 0], '\n')
  cat('Элементы меньше 1 или больше 5:', vector[vector < 1 | vector > 5], '\n')
  cat('Индексы равных 0:              ', which(vector == 0), '\n')
  cat('Индексы не меньше 2 и больше 8:', which(vector >= 2 & vector < 8), '\n')
}

task_2_2 <- function()
{
  # Указываем произвольный вектор
  vector <- c(1, 200, 0, TRUE, 5, 6, 7, NA, 2, -1, 20.0, "TEST")
  # Заменяем элемент, индекс которого равен длине вектора
  vector[length(vector)] = NA
  
  cat("Вектор с замененным последним элементом на NA:", vector)
}

task_2_3 <- function()
{
  vector <- c(1, 200, NA, NA, TRUE, 5, NA, 7, NA, 2, -1, 20.0, "TEST")
  
  cat("Индексы пропущенных элементов:", which(is.na(vector)))
}

task_2_4 <- function()
{
  vector <- c(1, 200, NA, NA, TRUE, 5, NA, 7, NA, 2, -1, 20.0, "TEST")
  
  cat("Число пропущенных элементов:", length(vector[is.na(vector)]))
}

task_2_5 <- function(){
  amount = 100;
  vector <- c()
  for (c in 1:amount)
    vector[c] = c
  
  cat("Вектор со", amount, "уникальными значениями:", vector)
}

task_2_6 <- function()
{
  repeats = 5;        # Количество повторений
  yearFrom = 2000;    # Год начала отсчета
  # Генерируем страны
  countries = c()
  for (i in c("France", "Italy", "Spain"))
    countries <- c(countries, rep(i, repeats))
  # Генерируем года    
  years = c();
  for (i in 1:repeats)
    years[i] = yearFrom + i - 1
  
  # Выводим таблицу
  table <- data.frame("Country" = countries, "Year" = years)
  print(table)
}

task_2_7 <- function()
{
  income <- c(10000, 32000, 28000, 150000, 65000, 1573)
  average = sum(income) / length(income)
  
  # Производим замену
  income_class <- replace(temp <- replace(income, income < average, 0), temp >= average, 1) 
  
  cat("Вектор income_class:", income_class)
}

#startTask <- function(arg){
#  shell("cls");
#  switch(
#    arg, 
#    "1.1" = task_1_1(),
#    "1.2" = task_1_2(),
#    "1.3" = task_1_3(),
#    "2.1" = task_2_1(),
#    "2.2" = task_2_2(),
#    "2.3" = task_2_3(),
#    "2.4" = task_2_4(),
#    "2.5" = task_2_5(),
#    "2.6" = task_2_6(),
#    "2.7" = task_2_7(),
#  )
#  cat('\n');
#}