#install.packages(c("xlsx", "dplyr", "moments", "psych", "ggplot2", "DescTools"))
#library(ggplot2)
library(DescTools)
#library(xlsx)
#library(dplyr)
#library(moments)
#library(psych)
Task <- function()
{
cat("\n- 1 -------------------------")
df <- read.csv("https://raw.githubusercontent.com/agconti/kaggle-titanic/master/data/train.csv")
print("Описательные статистики")
print(summary(df))
#всего на Титанике было 314 женщин и 577 мужчин
#возраст 177 пассажиров неизвестен  
#мин. возраст = 0,42, макс - 80.
#средний возраст = 29,70 
#медиана = 28
cat("\n-----------------------------")


cat("\n- 2 -------------------------")
hist(df$Age, col = 'blue', breaks = 40, main = 'Возраст пассажиров', ylab = "Плотность")
#больше людей с возрастом 23-24
#меньше людей с возрастом 78-80
cat("\n-----------------------------")

cat("\n- 3 -------------------------")
cat("\nВыбросов: ",length(boxplot(df$Age, ylab = "Возраст", main = "Диаграмма размаха (Age)")$out))
cat("\n-----------------------------")

cat("\n- 4 -------------------------")
BinomCI(sum(df$Sex == "female" & df$Survived == 1), sum(df$Survived == 1), conf.level = 0.95)
# С 95%-ной уверенностью, доля женщин среди выживших в интервале от 0.63 до 0.73
BinomCI(sum(df$Sex == "male" & df$Survived == 1), sum(df$Survived == 1), conf.level = 0.95)
# С 95%-ной уверенностью, доля мужчин среди выживших в интервале от 0.27 до 0.37
cat("\n-----------------------------")
}