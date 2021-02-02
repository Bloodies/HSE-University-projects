library(mice) 
library(VIM)


  cat("\n- 1 -------------------------")
  df <- read.csv("https://raw.githubusercontent.com/agconti/kaggle-titanic/master/data/train.csv")
  cat("\nЧисло наблюдений: ", nrow(df))
  cat("\nЧисло Переменных: ", ncol(df))
  cat("\nПеременые и их типы\n")
  str(df)
  cat("\n-----------------------------")
  
  cat("\n- 2 -------------------------")
  cat("\nВсего полностью заполненных строк: ", sum(complete.cases(df)))
  df_na <- df[!complete.cases(df),]
  cat("\n-----------------------------")

  cat("\n- 3 -------------------------")
  aggr(df)
  cat("\n-----------------------------")
  
  cat("\n- 4 -------------------------")
  md.pattern(df)
  cat("\n-----------------------------")

  cat("\n- 5 -------------------------")
  df <- na.omit(df)
  cat("\n-----------------------------")



  cat("\n- 1 -------------------------")
  df$female <- as.integer(ifelse(df$Sex == "female", 0, 1))
  cat("\n-----------------------------")

  cat("\n- 2 -------------------------")
  df2 <- subset(df, Age > 25 & Age < 45 & (Pclass == 2 | Pclass == 3))
  cat("\n-----------------------------")

  cat("\n- 3 -------------------------")
  cat("\nВсего пассажиров женского пола: ", nrow(subset(df, Sex == "female")))
  cat("\nВсего пассажиров мужского пола: ", nrow(subset(df, Sex == "male")))
  cat("\n-----------------------------")

  cat("\n- 4 -------------------------")
  survived_people <- subset(df, Survived == 1)
  cat("\nСамый молодой выживший: ", min(survived_people$Age, na.rm = TRUE))
  cat("\nСамый старый выживший: ", max(survived_people$Age, na.rm = TRUE))
  buf <- subset(survived_people, Pclass == 1)
  cat("\nСредний возраст пассажиров: ", sum(buf$Age)/nrow(buf))
  cat("\n-----------------------------")
