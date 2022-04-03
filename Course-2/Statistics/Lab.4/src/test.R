#install.packages(c("xlsx", "dplyr", "moments", "psych", "ggplot2", "DescTools"))
library(ggplot2)
library(DescTools)
library(xlsx)
library(dplyr)
library(moments)
library(psych)

  cat("\n- 1 -------------------------")
  df <- read.xlsx("AppleStore.xlsx", 1,encoding = "UTF-8")
  cat("\n-----------------------------")

  cat("\n- 2 -------------------------")
  df2 <- subset(df[which(colnames(df) != "id" & colnames(df) != "currency")])
  cat("\n-----------------------------")

  cat("\n- 3 -------------------------")
  cat("\nСтруктура данных df2:\n")
  print(str(df2))
  cat("\n-----------------------------")

  cat("\n- 4 -------------------------\n")
  cat("\nСуммарная статистика переменных:\n")
  print(summary(subset(df, select = c(price, user_rating, lang_num, size_bytes))))
  cat("\n-----------------------------")

  cat("\n- 5 -------------------------")
  cat("\nПриложение с максимальным числом языков: ")
  print(df$name[df$lang_num==max(df$lang_num)])
  cat("\n-----------------------------")

  cat("\n- 6 -------------------------")
  cat("\nКвантели цены: ", quantile(df$price))
  cat("\nКвантели рейтинга пользователей: ", quantile(df$user_rating))
  cat("\nКвантели количества языков: ", quantile(df$lang_num))
  cat("\n-----------------------------")

  cat("\n- 7 -------------------------")
  all_quantile = df2[, sapply(df2, is.numeric)]
  cat("\nКоэффициент эксцесса: \n")
  print(kurtosis(df2[, sapply(df2, is.numeric)], na.rm = TRUE))
  cat("\nКоэффициент ассиметрии: \n")
  print(skewness(df2[, sapply(df2, is.numeric)], na.rm = TRUE))
  cat("\nКоэффициент вариации: \n")
  
  cat("\nsize_bytes: ", sd(df2$size_bytes) / mean(df2$size_bytes) * 100)
  cat("\nprice: ", sd(df2$price) / mean(df2$price) * 100)
  cat("\nrating_count_tot: ", sd(df2$rating_count_tot) / mean(df2$rating_count_tot) * 100)
  cat("\nuser_rating: ", sd(df2$user_rating) / mean(df2$user_rating) * 100)
  cat("\nlang_num: ", sd(df2$lang_num) / mean(df2$lang_num) * 100)
  cat("\n-----------------------------")

  cat("\n- 8 -------------------------")
  boxplot((df2[, sapply(df2, is.numeric)])$size_bytes, xlab = "Количество байт", main = "Диаграмма размаха (size_bytes)", horizontal = TRUE)
  boxplot((df2[, sapply(df2, is.numeric)])$price, xlab = "Стоимость в долларах", main = "Диаграмма размаха (price)", horizontal = TRUE)
  boxplot((df2[, sapply(df2, is.numeric)])$rating_count_tot, xlab = "Число загрузок", main = "Диаграмма размаха (rating_count_tot)", horizontal = TRUE)
  boxplot((df2[, sapply(df2, is.numeric)])$user_rating, xlab = "Номер в рейтинге", main = "Диаграмма размаха (user_rating)", horizontal = TRUE)
  boxplot((df2[, sapply(df2, is.numeric)])$lang_num, xlab = "Количество языков", main = "Диаграмма размаха (lang_num)", horizontal = TRUE)
  cat("\n-----------------------------")

  cat("\n- 9 -------------------------")
  pie(table(df2$prime_genre), cex = 0.7, radius = 2, main = "Популярность жанров", col = c(3:16))
  cat("\n-----------------------------")

  cat("\n- 10 ------------------------")
  hist((df2[, sapply(df2, is.numeric)])$size_bytes, xlab = "Количество байт", ylab = "Плотность", main = "Гистограмма (size_bytes)", horizontal = TRUE)
  curve(dnorm(x, mean(df$size_bytes), sd = sd(df$size_bytes)), add = TRUE)
  hist((df2[, sapply(df2, is.numeric)])$price, xlab = "Стоимость в долларах", ylab = "Плотность", main = "Гистограмма (price)", horizontal = TRUE)
  curve(dnorm(x, mean(df$price), sd = sd(df$price)), add = TRUE)
  hist((df2[, sapply(df2, is.numeric)])$rating_count_tot, xlab = "Число загрузок", ylab = "Плотность", main = "Гистограмма (rating_count_tot)", horizontal = TRUE)
  curve(dnorm(x, mean(df$rating_count_tot), sd = sd(df$rating_count_tot)), add = TRUE)
  hist((df2[, sapply(df2, is.numeric)])$user_rating, xlab = "Номер в рейтинге", ylab = "Плотность", main = "Гистограмма (user_rating)", horizontal = TRUE)
  curve(dnorm(x, mean(df$user_rating), sd = sd(df$user_rating)), add = TRUE)
  hist((df2[, sapply(df2, is.numeric)])$lang_num, xlab = "Количество языков", ylab = "Плотность ", main = "Гистограмма (lang_num)", horizontal = TRUE)
  curve(dnorm(x, mean(df$lang_num), sd = sd(df$lang_num)), add = TRUE)
  cat("\n-----------------------------")

  cat("\n- 11 ------------------------")
  df %>% group_by(df$prime_genre) %>% summarise(count = n()) %>% arrange(count) %>% tail()
  cat("\nНаиболее популярный жанр: Games")
  cat("\n-----------------------------")

  cat("\n- 12 ------------------------")
  df3 = subset(df2, prime_genre == "Games")
  cat("\nСуммарная статистика переменных")
  print(summary(subset(df3, select = c(price, user_rating, lang_num))))
  cat("\n-----------------------------")
  
  cat("\n- 13 ------------------------")
  ks.test(df2$price, "pnorm", mean(df2$price), sd(df2$price))
  
  ks.test(df3$price, "pnorm", mean(df3$price), sd(df3$price))
  
  shapiro.test(df2$price)
  shapiro.test(df3$price)
  cat("\n-----------------------------")
  
  cat("\n- 1 -------------------------")
  df <- read.csv("https://raw.githubusercontent.com/allatambov/R-programming-3/master/seminars/sem8-09-02/demography.csv", encoding = "UTF-8")
  cat("\n-----------------------------")
  
  cat("\n- 2 -------------------------")
  df$young_share <- as.double(round(df$young_total/df$popul_total*100, 2))
  df$trud_share <- as.double(round(df$wa_total/df$popul_total*100, 2))
  df$old_share <- as.double(round(df$ret_total/df$popul_total*100, 2))
  cat("\n-----------------------------")
  
  cat("\n- 3 -------------------------")
  ggplot(data = df, aes(x = trud_share)) +
    geom_histogram(binwidth = 0.3, fill = "green", color = "black") +
    labs(x = "Процент", y = "Частота", title = "Трудоспособное население") +
    geom_vline(xintercept = median(df$trud_share),color = "red") +
    geom_rug()
  cat("\n-----------------------------")
  
  cat("\n- 4 -------------------------")
  ggplot(data = NULL, aes(x = trud_share, group = df$region, fill = df$region)) +
    geom_density(alpha = 0.7)
  
  #строим ящики, так как мало наблюдений
  ggplot(data = NULL, aes(x = "", y = trud_share, group = df$region, fill = df$region)) +
    geom_boxplot()
  cat("\n-----------------------------")
  
  cat("\n- 5 -------------------------")
  ggplot(data = NULL, aes(x = young_share, y = old_share)) +
    geom_point(color = "blue",pch = 11) + 
    labs(title = "Диаграмма рассеивания", x = "Процент населения моложе трудоспособного", y = "Процент населения старше трудоспособного") 
  #можно ли сказать, что чем больше процент молодого населения, тем меньше процент пожилых людей
  cat("\n-----------------------------")
  
  
  cat("\n- 6 -------------------------")
  male_share <- (df$wa_male + df$ret_male + df$young_male)/df$popul_total*100
  female_share <- (df$wa_female + df$ret_female + df$young_female)/df$popul_total*100
  male <- ifelse(male_share > female_share, 1, 0) 
  male <- as.factor(male)
  class(male)
  cat("\n-----------------------------")
  
  cat("\n- 7 -------------------------")
  ggplot(data = NULL, aes(x = young_share, y = old_share)) +
    geom_point(aes(size = male_share, color = male)) + 
    labs(title = "Диаграмма рассеивания", x = "Процент населения моложе трудоспособного", y = "Процент населения старше трудоспособного")
  cat("\n-----------------------------")
  
  cat("\n- 8 -------------------------")
  ggplot(df, aes(x = factor(region))) +
    geom_bar(stat = "count", width = 0.5, fill = "blue")
  cat("\n-----------------------------")
  
  cat("\n- 1 -------------------------")
  df <- read.csv("https://raw.githubusercontent.com/agconti/kaggle-titanic/master/data/train.csv")
  View(df)
  summary(df)
  #всего на Титанике было 314 женщин и 577 мужчин
  #возраст 177 пассажиров неизвестен  
  #мин. возраст = 0,42, а макс - 80.
  #средний возраст = 29,70, а медиана = 28
  cat("\n-----------------------------")
  
  
  cat("\n- 2 -------------------------")
  ggplot(data = df, aes(x = Age), na.rm = TRUE) +
    geom_histogram(binwidth = 0.3, fill = "green", color = "black") + 
    labs(title = "Гистограмма", x = "Возраст", y = "Количество")
  #больше всего людей с возрастом 23-24
  #меньше всего людей с возрастом 80
  cat("\n-----------------------------")
  
  cat("\n- 3 -------------------------")
  ggplot(data = df, aes(x = "", y = Age), na.rm = TRUE) +
    geom_boxplot()
  #7 выбросов
  cat("\n-----------------------------")
  
  cat("\n- 4 -------------------------")
  install.packages("DescTools")
  library(DescTools)
  BinomCI(sum(df$Sex == "female" & df$Survived == 1), sum(df$Survived == 1), conf.level = 0.95)
  # С 95%-ной уверенностью можно утверждать, что доля женщин среди выживших лежит в интервале от 0.63 до 0.73
  BinomCI(sum(df$Sex == "male" & df$Survived == 1), sum(df$Survived == 1), conf.level = 0.95)
  # С 95%-ной уверенностью можно утверждать, что доля мужчин среди выживших лежит в интервале от 0.27 до 0.37
  # Выжило больше женщин, чем мужчин
  cat("\n-----------------------------")
  
